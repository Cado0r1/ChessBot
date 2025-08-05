using static ChessBot.Logic.ChessGame;
using static ChessBot.Utils.BitboardOperations;
using static ChessBot.Logic.Fills;
using System.Linq;

namespace ChessBot.Logic
{
    public static class MoveGenerator
    {
        public static List<Move> GenerateValidMoves(bool isWhite)
        {
            ulong Pieces = isWhite ? GetAllWhitePieces() : GetAllBlackPieces();
            char Colour = isWhite ? 'W' : 'B';
            List<Move> Movelist = new List<Move>();
            while (Pieces != 0)
            {
                ulong Square = GetLSB(Pieces);
                char Piece = char.ToLower(GetPieceAt(Square));
                switch (Piece)
                {
                    case 'p': Movelist.AddRange(CalculatePawnMoves(Colour, Square)); break;
                    case 'n': Movelist.AddRange(CalculateKnightMoves(Colour, Square)); break;
                    case 'b': Movelist.AddRange(CalculateBishopMoves(Colour, Square)); break;
                    case 'r': Movelist.AddRange(CalculateRookMoves(Colour, Square)); break;
                    case 'q': Movelist.AddRange(CalculateQueenMoves(Colour, Square)); break;
                    case 'k': Movelist.AddRange(CalculateKingMoves(Colour, Square)); break;
                    default: Console.WriteLine("invalid Piece"); break;
                }
                Pieces &= ~Square;
            }
            Movelist = ValidateMoves(Movelist, Colour, Colour == 'W'? WhiteKing : BlackKing);
            return Movelist;
        }

        public static List<Move> CalculatePawnMoves(char Colour, ulong StartingSquare, bool GetAttacksOnly = false)
        {
            ulong enemyPieces = Colour == 'W' ? GetAllBlackPieces() : GetAllWhitePieces();
            ulong singlePush = Colour == 'W' ? (StartingSquare << 8) : (StartingSquare >> 8);
            ulong doublePush = Colour == 'W' ? (StartingSquare << 16) : (StartingSquare >> 16);
            ulong captureLeft = Colour == 'W' ? (StartingSquare << 7) : (StartingSquare >> 9);
            ulong captureRight = Colour == 'W' ? (StartingSquare << 9) : (StartingSquare >> 7);
            ulong pushLimit = Colour == 'W' ? Rank8 : Rank1;
            ulong doublePushCondition = Colour == 'W' ? Rank2 : Rank7;
            ulong promotionRank = Colour == 'W' ? Rank8 : Rank1;
            List<Move> Movelist = new List<Move>();
            if ((StartingSquare & FileA) == 0 && (captureLeft & enemyPieces) != 0)
            {
                if ((captureLeft & promotionRank) != 0)
                {
                    Movelist.Add(new Move(StartingSquare, captureLeft, "Pawn", MoveFlags.CapturePromotion, GetPieceAt(captureLeft)));
                }
                else Movelist.Add(new Move(StartingSquare, captureLeft, "Pawn", MoveFlags.Capture, GetPieceAt(captureLeft)));
            }
            if ((StartingSquare & FileA) == 0 && (captureLeft & TempEnPassant) != 0)
            {
                Movelist.Add(new Move(StartingSquare, captureLeft, "Pawn", MoveFlags.EnPassantCapture, Colour == 'W' ? 'p' : 'P'));
            }
            if ((StartingSquare & FileH) == 0 && (captureRight & enemyPieces) != 0)
            {
                if ((captureRight & promotionRank) != 0)
                {
                    Movelist.Add(new Move(StartingSquare, captureRight, "Pawn", MoveFlags.CapturePromotion, GetPieceAt(captureRight)));
                }
                else Movelist.Add(new Move(StartingSquare, captureRight, "Pawn", MoveFlags.Capture, GetPieceAt(captureRight)));
            }
            if ((StartingSquare & FileH) == 0 && (captureRight & TempEnPassant) != 0)
            {
                Movelist.Add(new Move(StartingSquare, captureRight, "Pawn", MoveFlags.EnPassantCapture, Colour == 'W' ? 'p' : 'P'));
            }
            if (GetAttacksOnly) return Movelist;
            if ((StartingSquare & pushLimit) == 0 && (singlePush & GetAllPieces()) == 0)
            {
                if ((singlePush & promotionRank) != 0)
                {
                    Movelist.Add(new Move(StartingSquare, singlePush, "Pawn", MoveFlags.Promotion));
                }
                else
                {
                    Movelist.Add(new Move(StartingSquare, singlePush, "Pawn", MoveFlags.Normal));
                }
            }
            if (((StartingSquare & doublePushCondition) != 0) && ((singlePush | doublePush) & GetAllPieces()) == 0)
            {
                Movelist.Add(new Move(StartingSquare, doublePush, "Pawn", MoveFlags.EnPassant));
            }
            return Movelist;
        }

        public static List<Move> CalculateKnightMoves(char Colour, ulong StartingSquare)
        {
            List<Move> Movelist = new List<Move>();
            ulong friendlyPieces = Colour == 'W' ? GetAllWhitePieces() : GetAllBlackPieces();
            ulong enemyPieces = Colour == 'W' ? GetAllBlackPieces() : GetAllWhitePieces();
            ulong[] Moves = {
                ((StartingSquare & (Rank7|Rank8|FileH)) == 0) ? (StartingSquare << 17) : 0UL,
                ((StartingSquare & (Rank7|Rank8|FileA)) == 0) ? (StartingSquare << 15) : 0UL,
                ((StartingSquare & (Rank1|Rank2|FileH)) == 0) ? (StartingSquare >> 15) : 0UL,
                ((StartingSquare & (Rank1|Rank2|FileA)) == 0) ? (StartingSquare >> 17) : 0UL,
                ((StartingSquare & (Rank8|FileG|FileH)) == 0) ? (StartingSquare << 10) : 0UL,
                ((StartingSquare & (Rank1|FileG|FileH)) == 0) ? (StartingSquare >> 6) : 0UL,
                ((StartingSquare & (Rank8|FileA|FileB)) == 0) ? (StartingSquare << 6) : 0UL,
                ((StartingSquare & (Rank1|FileA|FileB)) == 0) ? (StartingSquare >> 10) : 0UL
            };
            foreach (ulong Move in Moves)
            {
                if (Move != 0 && ((Move & friendlyPieces) == 0))
                {
                    if ((Move & enemyPieces) != 0)
                    {
                        Movelist.Add(new Move(StartingSquare, Move, "Knight", MoveFlags.Capture, GetPieceAt(Move)));
                    }
                    else
                    {
                        Movelist.Add(new Move(StartingSquare, Move, "Knight", MoveFlags.Normal));
                    }
                }
            }
            return Movelist;
        }

        public static List<Move> CalculateBishopMoves(char Colour, ulong StartingSquare)
        {
            List<Move> Movelist = new List<Move>();
            Movelist.AddRange(NorthEastFill(Colour, StartingSquare, "Bishop"));
            Movelist.AddRange(NorthWestFill(Colour, StartingSquare, "Bishop"));
            Movelist.AddRange(SouthEastFill(Colour, StartingSquare, "Bishop"));
            Movelist.AddRange(SouthWestFill(Colour, StartingSquare, "Bishop"));
            return Movelist;
        }

        public static List<Move> CalculateRookMoves(char Colour, ulong StartingSquare)
        {
            List<Move> Movelist = new List<Move>();
            Movelist.AddRange(NorthFill(Colour, StartingSquare, "Rook"));
            Movelist.AddRange(WestFill(Colour, StartingSquare, "Rook"));
            Movelist.AddRange(EastFill(Colour, StartingSquare, "Rook"));
            Movelist.AddRange(SouthFill(Colour, StartingSquare, "Rook"));
            return Movelist;
        }


        public static List<Move> CalculateQueenMoves(char Colour, ulong StartingSquare)
        {
            List<Move> Movelist = new List<Move>();
            Movelist.AddRange(NorthFill(Colour, StartingSquare, "Queen"));
            Movelist.AddRange(WestFill(Colour, StartingSquare, "Queen"));
            Movelist.AddRange(EastFill(Colour, StartingSquare, "Queen"));
            Movelist.AddRange(SouthFill(Colour, StartingSquare, "Queen"));
            Movelist.AddRange(NorthEastFill(Colour, StartingSquare, "Queen"));
            Movelist.AddRange(NorthWestFill(Colour, StartingSquare, "Queen"));
            Movelist.AddRange(SouthEastFill(Colour, StartingSquare, "Queen"));
            Movelist.AddRange(SouthWestFill(Colour, StartingSquare, "Queen"));
            return Movelist;
        }

        public static List<Move> CalculateKingMoves(char Colour, ulong StartingSquare)
        {
            List<Move> Movelist = new List<Move>();
            ulong friendlyPieces = Colour == 'W' ? GetAllWhitePieces() : GetAllBlackPieces();
            ulong enemyPieces = Colour == 'W' ? GetAllBlackPieces() : GetAllWhitePieces();
            ulong[] offsets = {
                ((StartingSquare & Rank8) == 0) ? (StartingSquare << 8) : 0UL,
                ((StartingSquare & Rank1) == 0) ? (StartingSquare >> 8) : 0UL,
                ((StartingSquare & FileH) == 0) ? (StartingSquare << 1) : 0UL,
                ((StartingSquare & FileA) == 0) ? (StartingSquare >> 1) : 0UL,
                ((StartingSquare & (Rank8 | FileH)) == 0) ? (StartingSquare << 9) : 0UL,
                ((StartingSquare & (Rank8 | FileA)) == 0) ? (StartingSquare << 7) : 0UL,
                ((StartingSquare & (Rank1 | FileH)) == 0) ? (StartingSquare >> 7) : 0UL,
                ((StartingSquare & (Rank1 | FileA)) == 0) ? (StartingSquare >> 9) : 0UL
            };

            foreach (ulong offset in offsets)
            {
                if (offset != 0 && (offset & friendlyPieces) == 0)
                {
                    if ((offset & enemyPieces) != 0)
                    {
                        Movelist.Add(new Move(StartingSquare, offset, "King", MoveFlags.Capture, GetPieceAt(offset)));
                    }
                    else Movelist.Add(new Move(StartingSquare, offset, "King", MoveFlags.Normal));
                }
            }
            if (Colour == 'W')
            {
                if (WhiteRightSideCastle && (((StartingSquare >> 1) | (StartingSquare >> 2) | (StartingSquare >> 3)) & GetAllPieces()) == 0)
                {
                    Movelist.Add(new Move(StartingSquare, StartingSquare >> 2, "King", MoveFlags.Castle));
                }
                if (WhiteLeftSideCastle && (((StartingSquare << 1) | (StartingSquare << 2)) & GetAllPieces()) == 0)
                {
                    Movelist.Add(new Move(StartingSquare, StartingSquare << 2, "King", MoveFlags.Castle));
                }
            }
            else
            {
                if (BlackRightSideCastle && (((StartingSquare >> 1) | (StartingSquare >> 2) | (StartingSquare >> 3)) & GetAllPieces()) == 0)
                {
                    Movelist.Add(new Move(StartingSquare, StartingSquare >> 2, "King", MoveFlags.Castle));
                }
                if (BlackLeftSideCastle && (((StartingSquare << 1) | (StartingSquare << 2)) & GetAllPieces()) == 0)
                {
                    Movelist.Add(new Move(StartingSquare, StartingSquare << 2, "King", MoveFlags.Castle));
                }
            }
                    return Movelist;
        }

        public static bool IsInCheck(char Colour, ulong friendlyKing)
        {
            // Generate piece Move from King position then check if relating opposing pieces are present in the move
            List<Move> HorizontalMoves = CalculateRookMoves(Colour, friendlyKing);
            List<Move> DiagonalMoves = CalculateBishopMoves(Colour, friendlyKing);
            List<Move> KnightMoves = CalculateKnightMoves(Colour, friendlyKing);
            List<Move> PawnMoves = CalculatePawnMoves(Colour, friendlyKing, true);
            List<Move> KingMoves = CalculateKingMoves(Colour, friendlyKing);
            ulong Queens = Colour == 'W' ? BlackQueens : WhiteQueens;
            ulong Rooks = Colour == 'W' ? BlackRooks : WhiteRooks;
            ulong Bishops = Colour == 'W' ? BlackBishops : WhiteBishops;
            ulong Knights = Colour == 'W' ? BlackKnights : WhiteKnights;
            ulong Pawns = Colour == 'W' ? BlackPawns : WhitePawns;
            ulong King = Colour == 'W' ? BlackKing : WhiteKing;
            List<ulong> PieceChecks = new List<ulong> { Queens | Rooks, Queens | Bishops, Knights, Pawns, King };
            List<List<Move>> MoveChecks = new List<List<Move>> { HorizontalMoves, DiagonalMoves, KnightMoves, PawnMoves, KingMoves };
            for (int i = 0; i < 4; i++)
            {
                if (MoveChecks[i].Any(m => (m.toSquare & PieceChecks[i]) != 0))
                {
                    return true;
                }
            }
            return false;
        }

        public static List<Move> ValidateMoves(List<Move> Movelist, char Colour, ulong King)
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            int dex = Movelist.Count-1;
            bool isWhite = Colour == 'W' ? true : false; 
            while (true)
            {
                Move ActiveMove = Movelist[dex];
                HandleMove(ActiveMove, isWhite);
                if (IsInCheck(Colour, Colour == 'W' ? WhiteKing : BlackKing))
                {
                    Movelist.Remove(Movelist[dex]);
                }
                RevertMove(ActiveMove, isWhite);
                dex--;
                if (dex < 0)
                {
                    break;
                }
            }
            //watch.Stop();
            //var elapsedT = watch.ElapsedTicks;
            //Console.WriteLine(elapsedT+"Ticks");
            return Movelist;  
        }
    } 
}