using static ChessBot.Utils.BitboardOperations;
using static ChessBot.Logic.MoveGenerator;
using static ChessBot.Utils.FenService;
using static ChessBot.Utils.PieceSquareTables;
using System.Threading;
using System.Linq;
using System.Windows;
using System.Numerics;

namespace ChessBot.Logic
{
    public static class ChessGame
    {

        const string FEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
        public static ulong PinMask;
        public static bool Check = false;
        public static ulong WhiteLSC = 0x0000000000000001;
        public static ulong WhiteRSC = 0x0000000000000080;
        public static ulong BlackLSC = 0x8000000000000000;
        public static ulong BlackRSC = 0x0100000000000000;
        public static bool WhiteRightSideCastle = true;
        public static bool WhiteLeftSideCastle = true;
        public static bool BlackRightSideCastle = true;
        public static bool BlackLeftSideCastle = true;

        public static int Nodes = 0;

        public static int Turn = 0;
        public static ulong TempEnPassant = 0UL;

        public static bool MoveRandomness = true;
        public static bool BotStart = false;
        public static bool HumanisWhite = true;
        public static bool HumanColour = HumanisWhite;
        public static bool BotColour = !HumanColour;

        public static ulong WhitePawns, BlackPawns, WhiteBishops, BlackBishops, WhiteKnights, BlackKnights, WhiteRooks, BlackRooks, WhiteKing, BlackKing, WhiteQueens, BlackQueens;

        public struct Move(ulong from, ulong to, string piece, MoveFlags flags, char captured = ' ')
        {
            public ulong fromSquare = from;
            public ulong toSquare = to;
            public string Piece = piece;
            public MoveFlags moveFlags = flags;
            public char capturedPiece = captured;
            //public float moveEval = EvaluateMove(GetPieceAt(from), to, flags, captured);
        }

        [Flags]
        public enum MoveFlags
        {
            None = 0,
            Normal = 1 << 0,
            Capture = 1 << 1,
            EnPassant = 1 << 2,
            Promotion = 1 << 3,
            Castle = 1 << 4,
            EnPassantCapture = 1 << 5,
            CapturePromotion = 1 << 6
        }

        public static byte GetCastlingRights()
        {
            byte rights = 0;
            if (WhiteRightSideCastle) rights |= 1 << 0;
            if (WhiteLeftSideCastle)  rights |= 1 << 1;
            if (BlackRightSideCastle) rights |= 1 << 2;
            if (BlackLeftSideCastle)  rights |= 1 << 3;
            return rights;
        }

        public static void SetCastlingRights(byte rights)
        {
            WhiteRightSideCastle = (rights & (1 << 0)) != 0;
            WhiteLeftSideCastle  = (rights & (1 << 1)) != 0;
            CheckRooks();
            BlackRightSideCastle = (rights & (1 << 2)) != 0;
            BlackLeftSideCastle  = (rights & (1 << 3)) != 0;
        }

        public static void StartGame()
        {
            SetBitboards();
            if(BotStart) BotMove(BotColour);
        }

        public static async void ValidateHumanMove(ulong fromTile, ulong toTile)
        {
            List<Move> Movelist = GenerateValidMoves(Turn%2==0);
            if (Movelist.Count == 0) Console.WriteLine("Checkmate!!!");
            Move ActiveMove = Movelist.FirstOrDefault(m => m.fromSquare == fromTile && m.toSquare == toTile);
            if (!ActiveMove.Equals(default(Move)))
            {
                TempEnPassant = 0UL;
                //if (ActiveMove.Piece == "King" || ActiveMove.Piece == "Rook") CheckCastleAvailability();
                HandleMove(ActiveMove, Turn%2==0, true);
                Turn++;
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.ReDrawBoard();
                await Task.Delay(50);
                await Task.Run(() => BotMove(BotColour));
            }
        }

        public static async void BotMove(bool BotColour)
        {
            //if (Turn < 20) HandleMove(GetBestMove(2), BotColour);
            //else if(Turn > 20 && Turn < 50)HandleMove(GetBestMove(3), BotColour);
            Move move = GetBestMove(3, BotColour);
            if (move.moveFlags != MoveFlags.None)
            {
                HandleMove(move, BotColour, true);
                Console.WriteLine(move.moveFlags);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                    mainWindow.ReDrawBoard();
                });
                Console.WriteLine("Nodes: " + Nodes);
                Nodes = 0;
                Turn++;
                await Task.Delay(50);
                await Task.Run(() => BotMove(!BotColour));
            }
        }

        public static Move GetBestMove(int depth, bool Colour)
        {
            float bestValue = float.NegativeInfinity;
            Move bestMove = default;
            Move bestMove2 = default;
            Move bestMove3 = default;

            List<Move> Movelist = GenerateValidMoves(Colour);
            Stack<byte> castlingRightsStack = new();
            ulong SavedTempEnPassant = TempEnPassant;
            if (Movelist.Count == 0) Console.WriteLine("Checkmate!!!");

            foreach (Move ActiveMove in Movelist)
            {
                castlingRightsStack.Push(GetCastlingRights());
                HandleMove(ActiveMove, Colour, true);
                Turn++;

                float value = MiniMax(depth - 1, false, float.NegativeInfinity, float.PositiveInfinity, !Colour, castlingRightsStack);

                RevertMove(ActiveMove, Colour);
                TempEnPassant = SavedTempEnPassant;
                SetCastlingRights(castlingRightsStack.Pop());
                Turn--;

                if (value > bestValue)
                {
                    bestValue = value;
                    bestMove3 = bestMove2;
                    bestMove2 = bestMove;
                    bestMove = ActiveMove;
                }
            }
            if (MoveRandomness && bestMove.fromSquare != 0 && bestMove2.fromSquare != 0 && bestMove3.fromSquare != 0)
            {
                Random rnd = new();
                List<Move> RandomMoveList = new List<Move> { bestMove, bestMove2, bestMove3 };
                bestMove = RandomMoveList[rnd.Next(RandomMoveList.Count)];
            }
            return bestMove;
        }

        public static float MiniMax(int depth, bool isMax, float Alpha, float Beta, bool Colour, Stack<byte> castlingRightsStack)
        {
            Nodes++;
            float value = isMax ? float.NegativeInfinity : float.PositiveInfinity;

            if (depth == 0)
            {
                if (isMax)
                {
                    return evaluate(Colour);
                }
                return evaluate(!Colour);
            }

            List<Move> Movelist = GenerateValidMoves(Colour);
            if (Movelist.Count == 0)
            {
                if (IsInCheck(Colour ? 'W' : 'B', Colour ? WhiteKing : BlackKing))
                    return isMax ? -1000.00f : 1000.00f;
                return 0;
            }
            foreach (Move ActiveMove in Movelist)
            {
                CheckRooks();
                castlingRightsStack.Push(GetCastlingRights());
                HandleMove(ActiveMove, Colour, true);
                Turn++;

                float eval = MiniMax(depth - 1, !isMax, Alpha, Beta, !Colour, castlingRightsStack);

                RevertMove(ActiveMove, Colour);
                SetCastlingRights(castlingRightsStack.Pop());
                Turn--;

                if (isMax)
                {
                    value = Math.Max(value, eval);
                    Alpha = Math.Max(Alpha, value);
                    if (Alpha >= Beta)
                        break;
                }
                else
                {
                    value = Math.Min(value, eval);
                    Beta = Math.Min(Beta, value);
                    if (Beta <= Alpha)
                        break;
                }
            }
            return value;
        }

        //public static Move FindBestMove(int depth)
        //{
        //    Move bestMove = default;
        //    float bestScore = -9999f;

        //    List<Move> Movelist = GenerateValidMoves(BotColour);
        //    foreach (Move ActiveMove in Movelist)
        //    {
        //        HandleMove(ActiveMove, BotColour);
        //        Turn++;
        //        float score = -negaMax(depth - 1, ActiveMove.moveEval, !BotColour);
        //        RevertMove(ActiveMove, BotColour);
        //        Turn--;
        //        if (score > bestScore)
        //        {
        //            bestScore = score;
        //            bestMove = ActiveMove;
        //        }
        //    }
        //    return bestMove;
        //}

        //public static float negaMax(int depth, float currentscore, bool isWhite)
        //{
        //    if (depth == 0) return currentscore;
        //    float maxScore = -9999f;

        //    List<Move> Movelist = GenerateValidMoves(isWhite);
        //    foreach (Move ActiveMove in Movelist)
        //    {
        //        HandleMove(ActiveMove, isWhite);
        //        Turn++;
        //        float score = -negaMax(depth - 1, currentscore + ActiveMove.moveEval, !isWhite);
        //        RevertMove(ActiveMove, isWhite);
        //        Turn--;
        //        if (score > maxScore)
        //        {
        //            maxScore = score;
        //        }
        //    }
        //    return maxScore;
        //}

        public static void HandleMove(Move ActiveMove, bool isWhite, bool ChangeTempEnPassant = false, bool changeCastleRights = true)
        {
            CheckRooks();
            switch (ActiveMove.moveFlags)
            {
                case MoveFlags.Normal:
                    {
                        UpdateBitBoard(ActiveMove.fromSquare, ActiveMove.toSquare, ActiveMove.Piece, isWhite);
                        if (ChangeTempEnPassant) TempEnPassant = 0UL;
                        CheckRooks();
                        break;
                    }
                case MoveFlags.Capture:
                    {
                        DestroyPiece(ActiveMove.toSquare, ActiveMove.capturedPiece);
                        UpdateBitBoard(ActiveMove.fromSquare, ActiveMove.toSquare, ActiveMove.Piece, isWhite);
                        if (ChangeTempEnPassant) TempEnPassant = 0UL;
                        CheckRooks();
                        break;
                    }
                case MoveFlags.EnPassant:
                    {
                        UpdateBitBoard(ActiveMove.fromSquare, ActiveMove.toSquare, ActiveMove.Piece, isWhite);
                        if (ChangeTempEnPassant) TempEnPassant = isWhite ? ActiveMove.toSquare >> 8 : ActiveMove.toSquare << 8;
                        CheckRooks();
                        break;
                    }
                case MoveFlags.Promotion:
                    {
                        //UpdateBitBoard(ActiveMove.fromSquare, ActiveMove.toSquare, ActiveMove.Piece, isWhite);
                        DestroyPiece(ActiveMove.fromSquare, GetPieceAt(ActiveMove.fromSquare));
                        AddPiece(ActiveMove.toSquare, isWhite ? 'Q' : 'q');
                        if (ChangeTempEnPassant) TempEnPassant = 0UL;
                        CheckRooks();
                        break;
                    }
                case MoveFlags.Castle:
                    {
                        if ((ActiveMove.toSquare & 0x0000000000000040UL) != 0) UpdateCastle("WRSC");
                        if ((ActiveMove.toSquare & 0x0000000000000004UL) != 0) UpdateCastle("WLSC");
                        if ((ActiveMove.toSquare & 0x0400000000000000UL) != 0) UpdateCastle("BLSC");
                        if ((ActiveMove.toSquare & 0x4000000000000000UL) != 0) UpdateCastle("BRSC");
                        if (ChangeTempEnPassant) TempEnPassant = 0UL;
                        CheckRooks();
                        break;
                    }
                case MoveFlags.EnPassantCapture:
                    {
                        UpdateBitBoard(ActiveMove.fromSquare, ActiveMove.toSquare, ActiveMove.Piece, isWhite);
                        DestroyPiece(isWhite ? ActiveMove.toSquare >> 8 : ActiveMove.toSquare << 8, ActiveMove.capturedPiece);
                        if (ChangeTempEnPassant) TempEnPassant = 0UL;
                        CheckRooks();
                        break;
                    }
                case MoveFlags.CapturePromotion:
                    {
                        //UpdateBitBoard(ActiveMove.fromSquare, ActiveMove.toSquare, ActiveMove.Piece, isWhite);
                        DestroyPiece(ActiveMove.toSquare, ActiveMove.capturedPiece);
                        if (ChangeTempEnPassant) TempEnPassant = 0UL;
                        DestroyPiece(ActiveMove.fromSquare, GetPieceAt(ActiveMove.fromSquare));
                        AddPiece(ActiveMove.toSquare, isWhite ? 'Q' : 'q');
                        CheckRooks();
                        break;
                    }
                default: Console.WriteLine("No MoveFlags attached"); break;
            }
            if (changeCastleRights)
            {
                if (ActiveMove.Piece == "King")
                {
                    if (isWhite)
                    {
                        WhiteRightSideCastle = false;
                        WhiteLeftSideCastle = false;
                        CheckRooks();
                    }
                    else
                    {
                        BlackRightSideCastle = false;
                        BlackLeftSideCastle = false;
                    }
                }
                else if (ActiveMove.Piece == "Rook")
                {
                    if (isWhite)
                    {
                        if ((ActiveMove.fromSquare & WhiteRSC) != 0) WhiteRightSideCastle = false;
                        if ((ActiveMove.fromSquare & WhiteLSC) != 0) WhiteLeftSideCastle = false;
                        CheckRooks();
                    }
                    else
                    {
                        if ((ActiveMove.fromSquare & BlackRSC) != 0) BlackRightSideCastle = false;
                        if ((ActiveMove.fromSquare & BlackLSC) != 0) BlackLeftSideCastle = false;
                    }
                }
            }
            CheckRooks();
        }

        public static void RevertMove(Move RevertedMove, bool isWhite)
        {
            CheckRooks();
            switch (RevertedMove.moveFlags)
            {
                case MoveFlags.Normal:
                    {
                        UpdateBitBoard(RevertedMove.toSquare, RevertedMove.fromSquare, RevertedMove.Piece, isWhite);
                        CheckRooks();
                        break;
                    }
                case MoveFlags.Capture:
                    {
                        UpdateBitBoard(RevertedMove.toSquare, RevertedMove.fromSquare, RevertedMove.Piece, isWhite);
                        AddPiece(RevertedMove.toSquare, RevertedMove.capturedPiece);
                        CheckRooks();
                        break;
                    }
                case MoveFlags.EnPassant:
                    {
                        UpdateBitBoard(RevertedMove.toSquare, RevertedMove.fromSquare, RevertedMove.Piece, isWhite);
                        CheckRooks();
                        break;
                    }
                case MoveFlags.Promotion:
                    {
                        //UpdateBitBoard(RevertedMove.toSquare, RevertedMove.fromSquare, RevertedMove.Piece, isWhite);
                        AddPiece(RevertedMove.fromSquare, GetPieceAt(RevertedMove.fromSquare));
                        DestroyPiece(RevertedMove.toSquare, isWhite ? 'Q' : 'q');
                        CheckRooks();
                        break;
                    }
                case MoveFlags.Castle:
                    {
                        if ((RevertedMove.toSquare & 0x0000000000000040UL) != 0) UNDOCastle("WRSC");
                        if ((RevertedMove.toSquare & 0x0000000000000004UL) != 0) UNDOCastle("WLSC");
                        if ((RevertedMove.toSquare & 0x0400000000000000UL) != 0) UNDOCastle("BLSC");
                        if ((RevertedMove.toSquare & 0x4000000000000000UL) != 0) UNDOCastle("BRSC");
                        CheckRooks();
                        break;
                    }
                case MoveFlags.EnPassantCapture:
                    {
                        UpdateBitBoard(RevertedMove.toSquare, RevertedMove.fromSquare, RevertedMove.Piece, isWhite);
                        AddPiece(isWhite ? RevertedMove.toSquare >> 8 : RevertedMove.toSquare << 8, RevertedMove.capturedPiece);
                        CheckRooks();
                        break;
                    }
                case MoveFlags.CapturePromotion:
                    {
                        //UpdateBitBoard(RevertedMove.toSquare, RevertedMove.fromSquare, RevertedMove.Piece, isWhite);
                        AddPiece(RevertedMove.toSquare, RevertedMove.capturedPiece);
                        AddPiece(RevertedMove.fromSquare, GetPieceAt(RevertedMove.fromSquare));
                        DestroyPiece(RevertedMove.toSquare, isWhite ? 'Q' : 'q');
                        CheckRooks();
                        break;
                    }
                default: Console.WriteLine("No MoveFlags attached"); break;
            }
            CheckRooks();
        }

        //public static void CheckCastleAvailability()
        //{
        //    if ((WhiteRooks & WhiteRSC) == 0) WhiteRightSideCastle = false;
        //    if ((WhiteRooks & WhiteLSC) == 0) WhiteLeftSideCastle = false;
        //    if ((BlackRooks & BlackRSC) == 0) BlackRightSideCastle = false;
        //    if ((BlackRooks & BlackLSC) == 0) BlackLeftSideCastle = false;
        //    if ((WhiteKing & 0x0000000000000010UL) == 0) WhiteRightSideCastle = WhiteLeftSideCastle = false;
        //    if ((BlackKing & 0x1000000000000000UL) == 0) BlackRightSideCastle = BlackLeftSideCastle = false;
        //    CheckRooks();
        //}

        public static void SetBitboards()
        {
            string fen = ConvertToFen64(FEN);
            if (fen.Contains(' '))
            {
                fen = fen.Substring(0, fen.IndexOf(' '));
            }
            Console.WriteLine(fen);
            for (int i = 0; i < fen.Length; i++)
            {
                char piece;
                if (fen[i] != '.')
                {
                    piece = fen[i];
                    int position = i - 63;
                    if (position < 0) position = -position;
                    position = FlipIndex(position);
                    switch (piece)
                    {
                        case 'r': BlackRooks |= (ulong)1 << position; break;
                        case 'n': BlackKnights |= (ulong)1 << position; break;
                        case 'b': BlackBishops |= (ulong)1 << position; break;
                        case 'q': BlackQueens |= (ulong)1 << position; break;
                        case 'k': BlackKing |= (ulong)1 << position; break;
                        case 'p': BlackPawns |= (ulong)1 << position; break;
                        case 'R': WhiteRooks |= (ulong)1 << position; break;
                        case 'N': WhiteKnights |= (ulong)1 << position; break;
                        case 'B': WhiteBishops |= (ulong)1 << position; break;
                        case 'Q': WhiteQueens |= (ulong)1 << position; break;
                        case 'K': WhiteKing |= (ulong)1 << position; break;
                        case 'P': WhitePawns |= (ulong)1 << position; break;
                        default: Console.WriteLine("Invalid piece type"); break;
                    }
                }
            }
        }

        public static float evaluate(bool isWhite)
        {
            bool MidGame = (Turn > 24) ? true : false;
            ulong WhitePieces = GetAllWhitePieces();
            ulong BlackPieces = GetAllBlackPieces();
            float WhiteValue = 0f;
            float BlackValue = 0f;
            while (WhitePieces != 0)
            {
                ulong Position = GetLSB(WhitePieces);
                char Piece = GetPieceAt(Position);
                switch (Piece)
                {
                    case 'P': WhiteValue += 1 + (MidGame ? WhitePawnsMidGame[BitboardToNumber[Position]] : WhitePawnsStartGame[BitboardToNumber[Position]]); break;
                    case 'N': WhiteValue += 3 + (MidGame ? WhiteKnightsMidGame[BitboardToNumber[Position]] : WhiteKnightsStartGame[BitboardToNumber[Position]]); break;
                    case 'B': WhiteValue += 3 + (MidGame ? WhiteBishopsMidGame[BitboardToNumber[Position]] : WhiteBishopsStartGame[BitboardToNumber[Position]]); break;
                    case 'R': WhiteValue += 5 + (MidGame ? WhiteRooksMidGame[BitboardToNumber[Position]] : WhiteRooksStartGame[BitboardToNumber[Position]]); break;
                    case 'Q': WhiteValue += 9 + (MidGame ? WhiteQueenMidGame[BitboardToNumber[Position]] : WhiteQueenStartGame[BitboardToNumber[Position]]); break;
                    case 'K': WhiteValue += MidGame ? WhiteKingMidGame[BitboardToNumber[Position]] : WhiteKingStartGame[BitboardToNumber[Position]]; break;
                    default: break;
                }
                WhitePieces &= ~Position;
            }
            while (BlackPieces != 0)
            {
                ulong Position = GetLSB(BlackPieces);
                char Piece = GetPieceAt(Position);
                switch (Piece)
                {
                    case 'p': BlackValue += 1 + (MidGame ? BlackPawnsMidGame[BitboardToNumber[Position]] : BlackPawnsStartGame[BitboardToNumber[Position]]); break;
                    case 'n': BlackValue += 3 + (MidGame ? BlackKnightsMidGame[BitboardToNumber[Position]] : BlackKnightsStartGame[BitboardToNumber[Position]]); break;
                    case 'b': BlackValue += 3 + (MidGame ? BlackBishopsMidGame[BitboardToNumber[Position]] : BlackBishopsStartGame[BitboardToNumber[Position]]); break;
                    case 'r': BlackValue += 5 + (MidGame ? BlackRooksMidGame[BitboardToNumber[Position]] : BlackRooksStartGame[BitboardToNumber[Position]]); break;
                    case 'q': BlackValue += 9 + (MidGame ? BlackQueenMidGame[BitboardToNumber[Position]] : BlackQueenStartGame[BitboardToNumber[Position]]); break;
                    case 'k': BlackValue += MidGame ? BlackKingMidGame[BitboardToNumber[Position]] : BlackKingStartGame[BitboardToNumber[Position]]; break;
                    default: break;
                }
                BlackPieces &= ~Position;
            }
            if (isWhite) return WhiteValue - BlackValue;
            else return BlackValue - WhiteValue;
        }

        public static void PrintTheThing()
        {
            int A = 0;
            int B = 63;
            for (int i = 63; i >= 0; i--)
            {
                if (i == 63) Console.Write("{(ulong)1 ," + FlipIndex(B) + "},");
                else Console.Write("{(ulong)1 << " + A + " ," + FlipIndex(B) + "},");
                if (i % 8 == 0) Console.WriteLine();
                A++;
                B--;
            }
        }
    }
}