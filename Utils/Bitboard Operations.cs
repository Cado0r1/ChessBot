using static ChessBot.Logic.ChessGame;
using static ChessBot.Utils.PieceSquareTables;
using System.Numerics;

namespace ChessBot.Utils
{
    public static class BitboardOperations
    {
        public static void PrintBitBoard(ulong board, string bitboardName)
        {
            Console.WriteLine($"{bitboardName}: ");
            Console.WriteLine("");
            for (int rank = 7; rank >= 0; rank--)
            {
                for (int file = 0; file < 8; file++)
                {
                    int square = rank * 8 + file;
                    ulong mask = (ulong)1 << square;
                    Console.Write((board & mask) != 0 ? "1 " : "0 ");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }

        public static ulong Rank8 = 0xFF00000000000000;
        public static ulong Rank7 = 0x00FF000000000000;
        public static ulong Rank6 = 0x0000FF0000000000;
        public static ulong Rank5 = 0x000000FF00000000;
        public static ulong Rank4 = 0x00000000FF000000;
        public static ulong Rank3 = 0x0000000000FF0000;
        public static ulong Rank2 = 0x000000000000FF00;
        public static ulong Rank1 = 0x00000000000000FF;
        public static ulong FileA = 0x0101010101010101;
        public static ulong FileB = 0x0202020202020202;
        public static ulong FileC = 0x0404040404040404;
        public static ulong FileD = 0x0808080808080808;
        public static ulong FileE = 0x1010101010101010;
        public static ulong FileF = 0x2020202020202020;
        public static ulong FileG = 0x4040404040404040;
        public static ulong FileH = 0x8080808080808080;

        public static ulong IntToBitboard(int index)
        {
            return (ulong)1 << index;
        }

        public static ulong GetLSB(ulong Pieces)
        {
            ulong LSB = Pieces & (~Pieces + 1);
            return LSB;
        }

        public static int FlipIndex(int index)
        {
            return index + (7 - 2 * (index % 8));
        }

        public static ulong GetAllWhitePieces()
        {
            return WhitePawns | WhiteRooks | WhiteKnights | WhiteBishops | WhiteQueens | WhiteKing;
        }

        public static ulong GetAllBlackPieces()
        {
            return BlackPawns | BlackRooks | BlackKnights | BlackBishops | BlackQueens | BlackKing;
        }

        public static ulong GetAllPieces()
        {
            return GetAllWhitePieces() | GetAllBlackPieces();
        }

        public static Char GetPieceAt(ulong square)
        {
            if ((square & WhitePawns) != 0) return 'P';
            if ((square & WhiteBishops) != 0) return 'B';
            if ((square & WhiteKnights) != 0) return 'N';
            if ((square & WhiteRooks) != 0) return 'R';
            if ((square & WhiteQueens) != 0) return 'Q';
            if ((square & WhiteKing) != 0) return 'K';
            if ((square & BlackPawns) != 0) return 'p';
            if ((square & BlackBishops) != 0) return 'b';
            if ((square & BlackKnights) != 0) return 'n';
            if ((square & BlackRooks) != 0) return 'r';
            if ((square & BlackQueens) != 0) return 'q';
            if ((square & BlackKing) != 0) return 'k';
            return ' ';
        }

        public static void UpdateBitBoard(ulong fromSquare, ulong toSquare, string Piece, bool isWhite)
        {
            char Colour = isWhite ? 'W' : 'B';
            switch (Piece)
            {
                case "Pawn":
                    {
                        if (Colour == 'W')
                        { WhitePawns = (WhitePawns & ~fromSquare) | toSquare; }
                        else { BlackPawns = (BlackPawns & ~fromSquare) | toSquare; }
                        CheckRooks();
                        break;
                    }
                case "Rook":
                    {
                        if (Colour == 'W')
                        { WhiteRooks = (WhiteRooks & ~fromSquare) | toSquare; }
                        else { BlackRooks = (BlackRooks & ~fromSquare) | toSquare; }
                        CheckRooks();
                        break;
                    }
                case "Bishop":
                    {
                        if (Colour == 'W')
                        { WhiteBishops = (WhiteBishops & ~fromSquare) | toSquare; }
                        else { BlackBishops = (BlackBishops & ~fromSquare) | toSquare; }
                        CheckRooks();
                        break;
                    }
                case "Knight":
                    {
                        if (Colour == 'W')
                        { WhiteKnights = (WhiteKnights & ~fromSquare) | toSquare; }
                        else { BlackKnights = (BlackKnights & ~fromSquare) | toSquare; }
                        CheckRooks();
                        break;
                    }
                case "Queen":
                    {
                        if (Colour == 'W')
                        { WhiteQueens = (WhiteQueens & ~fromSquare) | toSquare; }
                        else { BlackQueens = (BlackQueens & ~fromSquare) | toSquare; }
                        CheckRooks();
                        break;
                    }
                case "King":
                    {
                        if (Colour == 'W')
                        { WhiteKing = (WhiteKing & ~fromSquare) | toSquare; }
                        else { BlackKing = (BlackKing & ~fromSquare) | toSquare; }
                        CheckRooks();
                        break;
                    }
                default: Console.WriteLine("Invalid Piece: " + Piece); break;
            }
        }

        public static void CheckRooks()
        {
            if (BitOperations.PopCount(WhiteRooks) > 2 || BitOperations.PopCount(BlackRooks) > 2)
            {
                Console.WriteLine("STOP");
                PrintBitBoard(WhiteRooks, "WR");
                PrintBitBoard(BlackRooks, "BR");
                
            }
        }

        public static void DestroyPiece(ulong Square, char Piece)
        {
            CheckRooks();
            switch (Piece)
            {
                case 'r': BlackRooks &= ~Square; break;
                case 'n': BlackKnights &= ~Square; break;
                case 'b': BlackBishops &= ~Square; break;
                case 'q': BlackQueens &= ~Square; break;
                case 'k': BlackKing &= ~Square; break;
                case 'p': BlackPawns &= ~Square; break;
                case 'R': WhiteRooks &= ~Square; break;
                case 'N': WhiteKnights &= ~Square; break;
                case 'B': WhiteBishops &= ~Square; break;
                case 'Q': WhiteQueens &= ~Square; break;
                case 'K': WhiteKing &= ~Square; break;
                case 'P': WhitePawns &= ~Square; break;
                default: Console.WriteLine("Invalid Piece to Destroy"); break;
            }
            CheckRooks();
        }

        public static void AddPiece(ulong Square, char Piece)
        {
            CheckRooks();
            switch (Piece)
            {
                case 'r': BlackRooks |= Square; break;
                case 'n': BlackKnights |= Square; break;
                case 'b': BlackBishops |= Square; break;
                case 'q': BlackQueens |= Square; break;
                case 'k': BlackKing |= Square; break;
                case 'p': BlackPawns |= Square; break;
                case 'R': WhiteRooks |= Square; break;
                case 'N': WhiteKnights |= Square; break;
                case 'B': WhiteBishops |= Square; break;
                case 'Q': WhiteQueens |= Square; break;
                case 'K': WhiteKing |= Square; break;
                case 'P': WhitePawns |= Square; break;
                default: Console.WriteLine("Invalid Piece to Add"); break;
            }
            CheckRooks();
        }

        public static void UpdateCastle(string name)
        {
            switch (name)
            {
                case "WRSC":
                    {
                        WhiteRooks = (WhiteRooks & ~0x0000000000000080UL) | 0x0000000000000020UL;
                        WhiteKing = (WhiteKing & ~0x0000000000000010UL) | 0x0000000000000040UL;
                        CheckRooks();
                        return;
                    }
                case "WLSC":
                    {
                        WhiteRooks = (WhiteRooks & ~0x0000000000000001UL) | 0x0000000000000008UL;
                        WhiteKing = (WhiteKing & ~0x0000000000000010UL) | 0x0000000000000004UL;
                        CheckRooks();
                        return;
                    }
                case "BLSC":
                    {
                        BlackRooks = (BlackRooks & ~0x0100000000000000UL) | 0x0800000000000000UL;
                        BlackKing = (BlackKing & ~0x1000000000000000UL) | 0x0400000000000000UL;
                        CheckRooks();
                        return;
                    }
                case "BRSC":
                    {
                        BlackRooks = (BlackRooks & ~0x8000000000000000UL) | 0x2000000000000000UL;
                        BlackKing = (BlackKing & ~0x1000000000000000UL) | 0x4000000000000000UL;
                        CheckRooks();
                        return;
                    }
                default:
                    return;
            }
        }

        public static void UNDOCastle(string name)
        {
            switch (name)
            {
                case "WRSC":
                    {
                        WhiteRooks = (WhiteRooks & ~0x0000000000000020UL) | 0x0000000000000080UL;
                        WhiteKing = (WhiteKing & ~0x0000000000000040UL) | 0x0000000000000010UL;
                        CheckRooks();
                        return;
                    }
                case "WLSC":
                    {
                        WhiteRooks = (WhiteRooks & ~0x0000000000000008UL) | 0x0000000000000001UL;
                        WhiteKing = (WhiteKing & ~0x0000000000000004UL) | 0x0000000000000010UL;
                        CheckRooks();
                        return;
                    }
                case "BLSC":
                    {
                        BlackRooks = (BlackRooks & ~0x0800000000000000UL) | 0x0100000000000000UL;
                        BlackKing = (BlackKing & ~0x0400000000000000UL) | 0x1000000000000000UL;
                        CheckRooks();
                        return;
                    }
                case "BRSC":
                    {
                        BlackRooks = (BlackRooks & ~0x2000000000000000UL) | 0x8000000000000000UL;
                        BlackKing = (BlackKing & ~0x4000000000000000UL) | 0x1000000000000000UL;
                        CheckRooks();
                        return;
                    }
                default:
                    return;
            }
        }

        public static float EvaluateMove(char Piece, ulong Postion, MoveFlags moveFlags, char CapturedPiece)
        {
            float Value = 0.0f;
            bool MidGame = false;
            if (Turn > 24) MidGame = true;
            switch (Piece)
            {
                case 'r': Value += MidGame ? BlackRooksMidGame[BitboardToNumber[Postion]] : BlackRooksStartGame[BitboardToNumber[Postion]]; break;
                case 'n': Value += MidGame ? BlackKnightsMidGame[BitboardToNumber[Postion]] : BlackKnightsStartGame[BitboardToNumber[Postion]]; break;
                case 'b': Value += MidGame ? BlackBishopsMidGame[BitboardToNumber[Postion]] : BlackBishopsStartGame[BitboardToNumber[Postion]]; break;
                case 'q': Value += MidGame ? BlackQueenMidGame[BitboardToNumber[Postion]] : BlackQueenStartGame[BitboardToNumber[Postion]]; break;
                case 'k': Value += MidGame ? BlackKingMidGame[BitboardToNumber[Postion]] : BlackKingStartGame[BitboardToNumber[Postion]]; break;
                case 'p': Value += MidGame ? BlackPawnsMidGame[BitboardToNumber[Postion]] : BlackPawnsStartGame[BitboardToNumber[Postion]]; break;
                case 'R': Value += MidGame ? WhiteRooksMidGame[BitboardToNumber[Postion]] : WhiteRooksStartGame[BitboardToNumber[Postion]]; break;
                case 'N': Value += MidGame ? WhiteKnightsMidGame[BitboardToNumber[Postion]] : WhiteKnightsStartGame[BitboardToNumber[Postion]]; break;
                case 'B': Value += MidGame ? WhiteBishopsMidGame[BitboardToNumber[Postion]] : WhiteBishopsStartGame[BitboardToNumber[Postion]]; break;
                case 'Q': Value += MidGame ? WhiteQueenMidGame[BitboardToNumber[Postion]] : WhiteQueenStartGame[BitboardToNumber[Postion]]; break;
                case 'K': Value += MidGame ? WhiteKingMidGame[BitboardToNumber[Postion]] : WhiteKingStartGame[BitboardToNumber[Postion]]; break;
                case 'P': Value += MidGame ? WhitePawnsMidGame[BitboardToNumber[Postion]] : WhitePawnsStartGame[BitboardToNumber[Postion]]; break;
                default: Console.WriteLine("Invalid Piece"); break;
            }
            switch (moveFlags)
            {
                case MoveFlags.Capture: Value += GetValue(CapturedPiece); break;
                case MoveFlags.Promotion: Value += 9; break;
                case MoveFlags.Castle: Value += 2; break;
                case MoveFlags.EnPassantCapture: Value += 10; break;
                default: break;
            }
            return Value;
        }

        public static int GetValue(char Piece)
        {
            Piece = char.ToLower(Piece);
            switch (Piece)
            {
                case 'r': return 5;
                case 'n': return 3;
                case 'b': return 3;
                case 'q': return 9;
                case 'k': return 999;
                case 'p': return 1;
                default: return 0;
            }
        }

        public static void PrintAll()
        {
            PrintBitBoard(WhitePawns, "White Pawns");
            PrintBitBoard(WhiteKnights, "White Knights");
            PrintBitBoard(WhiteBishops, "White Bishops");
            PrintBitBoard(WhiteRooks, "White Rooks");
            PrintBitBoard(WhiteQueens, "White Queen");
            PrintBitBoard(WhiteKing, "White King");

            PrintBitBoard(BlackPawns, "Black Pawns");
            PrintBitBoard(BlackKnights, "Black Knights");
            PrintBitBoard(BlackBishops, "Black Bishops");
            PrintBitBoard(BlackRooks, "Black Rooks");
            PrintBitBoard(BlackQueens, "Black Queen");
            PrintBitBoard(BlackKing, "Black King");
        }

        public static ulong GetPieces(char Piece)
        {
            switch (Piece)
            {
                case 'r': return BlackRooks;
                case 'n': return BlackKnights;
                case 'b': return BlackBishops;
                case 'q': return BlackQueens;
                case 'k': return BlackKing;
                case 'p': return BlackPawns;
                case 'R': return WhiteRooks;
                case 'N': return WhiteKnights;
                case 'B': return WhiteBishops;
                case 'Q': return WhiteQueens;
                case 'K': return WhiteKing;
                case 'P': return WhitePawns;
                default: return 0UL;
            }
        }

    }
}
