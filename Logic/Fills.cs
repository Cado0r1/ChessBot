using static ChessBot.Logic.ChessGame;
using static ChessBot.Utils.BitboardOperations;

namespace ChessBot.Logic
{
    public static class Fills
    {
        public static List<Move> NorthFill(Char Colour, ulong StartingSquare, string Piece)
        {
            List<Move> Movelist = new List<Move>();
            (ulong enemyPieces, ulong friendlyPieces) = Colour == 'W' ? (GetAllBlackPieces(), GetAllWhitePieces()) : (GetAllWhitePieces(), GetAllBlackPieces());
            if ((StartingSquare & Rank8) != 0) return Movelist;
            ulong North = StartingSquare << 8;
            while (true)
            {
                if ((North & friendlyPieces) == 0)
                {
                    if ((North & enemyPieces) != 0)
                    {
                        Movelist.Add(new Move(StartingSquare, North, Piece, MoveFlags.Capture, GetPieceAt(North)));
                        return Movelist;
                    }
                    else if ((North & Rank8) != 0)
                    {
                        Movelist.Add(new Move(StartingSquare, North, Piece, MoveFlags.Normal));
                        return Movelist;
                    }
                    else
                    {
                        Movelist.Add(new Move(StartingSquare, North, Piece, MoveFlags.Normal));
                        North <<= 8;
                    }
                }
                else
                {
                    return Movelist;
                }
            }
        }

        public static List<Move> EastFill(Char Colour, ulong StartingSquare, string Piece)
        {
            List<Move> Movelist = new List<Move>();
            (ulong enemyPieces, ulong friendlyPieces) = Colour == 'W' ? (GetAllBlackPieces(), GetAllWhitePieces()) : (GetAllWhitePieces(), GetAllBlackPieces());
            if ((StartingSquare & FileH) != 0) return Movelist;
            ulong East = StartingSquare << 1;
            while (true)
            {
                if ((East & friendlyPieces) == 0)
                {
                    if ((East & enemyPieces) != 0)
                    {
                        Movelist.Add(new Move(StartingSquare, East, Piece, MoveFlags.Capture, GetPieceAt(East)));
                        return Movelist;
                    }
                    else if ((East & FileH) != 0)
                    {
                        Movelist.Add(new Move(StartingSquare, East, Piece, MoveFlags.Normal));
                        return Movelist;
                    }
                    else
                    {
                        Movelist.Add(new Move(StartingSquare, East, Piece, MoveFlags.Normal));
                        East <<= 1;
                    }
                }
                else
                {
                    return Movelist;
                }
            }
        }

        public static List<Move> WestFill(Char Colour, ulong StartingSquare, string Piece)
        {
            List<Move> Movelist = new List<Move>();
            (ulong enemyPieces, ulong friendlyPieces) = Colour == 'W' ? (GetAllBlackPieces(), GetAllWhitePieces()) : (GetAllWhitePieces(), GetAllBlackPieces());
            if ((StartingSquare & FileA) != 0) return Movelist;
            ulong West = StartingSquare >> 1;
            while (true)
            {
                if ((West & friendlyPieces) == 0)
                {
                    if ((West & enemyPieces) != 0)
                    {
                        Movelist.Add(new Move(StartingSquare, West, Piece, MoveFlags.Capture, GetPieceAt(West)));
                        return Movelist;
                    }
                    else if ((West & FileA) != 0)
                    {
                        Movelist.Add(new Move(StartingSquare, West, Piece, MoveFlags.Normal));
                        return Movelist;
                    }
                    else
                    {
                        Movelist.Add(new Move(StartingSquare, West, Piece, MoveFlags.Normal));
                        West >>= 1;
                    }
                }
                else
                {
                    return Movelist;
                }
            }
        }

        public static List<Move> SouthFill(Char Colour, ulong StartingSquare, string Piece)
        {
            List<Move> Movelist = new List<Move>();
            (ulong enemyPieces, ulong friendlyPieces) = Colour == 'W' ? (GetAllBlackPieces(), GetAllWhitePieces()) : (GetAllWhitePieces(), GetAllBlackPieces());
            if ((StartingSquare & Rank1) != 0) return Movelist;
            ulong South = StartingSquare >> 8;
            while (true)
            {
                if ((South & friendlyPieces) == 0)
                {
                    if ((South & enemyPieces) != 0)
                    {
                        Movelist.Add(new Move(StartingSquare, South, Piece, MoveFlags.Capture, GetPieceAt(South)));
                        return Movelist;
                    }
                    else if ((South & Rank1) != 0)
                    {
                        Movelist.Add(new Move(StartingSquare, South, Piece, MoveFlags.Normal));
                        return Movelist;
                    }
                    else
                    {
                        Movelist.Add(new Move(StartingSquare, South, Piece, MoveFlags.Normal));
                        South >>= 8;
                    }
                }
                else
                {
                    return Movelist;
                }
            }
        }

        public static List<Move> NorthEastFill(Char Colour, ulong StartingSquare, string Piece)
        {
            List<Move> Movelist = new List<Move>();
            (ulong enemyPieces, ulong friendlyPieces) = Colour == 'W' ? (GetAllBlackPieces(), GetAllWhitePieces()) : (GetAllWhitePieces(), GetAllBlackPieces());
            if ((StartingSquare & (Rank8 | FileH)) != 0) return Movelist;
            ulong NorthEast = StartingSquare << 9;
            while (true)
            {
                if ((NorthEast & friendlyPieces) == 0)
                {
                    if ((NorthEast & enemyPieces) != 0)
                    {
                        Movelist.Add(new Move(StartingSquare, NorthEast, Piece, MoveFlags.Capture, GetPieceAt(NorthEast)));
                        return Movelist;
                    }
                    else if ((NorthEast & (Rank8 | FileH)) != 0)
                    {
                        Movelist.Add(new Move(StartingSquare, NorthEast, Piece, MoveFlags.Normal));
                        return Movelist;
                    }
                    else
                    {
                        Movelist.Add(new Move(StartingSquare, NorthEast, Piece, MoveFlags.Normal));
                        NorthEast <<= 9;
                    }
                }
                else
                {
                    return Movelist;
                }
            }
        }

        public static List<Move> NorthWestFill(Char Colour, ulong StartingSquare, string Piece)
        {
            List<Move> Movelist = new List<Move>();
            (ulong enemyPieces, ulong friendlyPieces) = Colour == 'W' ? (GetAllBlackPieces(), GetAllWhitePieces()) : (GetAllWhitePieces(), GetAllBlackPieces());
            if ((StartingSquare & (Rank8 | FileA)) != 0) return Movelist;
            ulong NorthWest = StartingSquare << 7;
            while (true)
            {
                if ((NorthWest & friendlyPieces) == 0)
                {
                    if ((NorthWest & enemyPieces) != 0)
                    {
                        Movelist.Add(new Move(StartingSquare, NorthWest, Piece, MoveFlags.Capture, GetPieceAt(NorthWest)));
                        return Movelist;
                    }
                    else if ((NorthWest & (Rank8 | FileA)) != 0)
                    {
                        Movelist.Add(new Move(StartingSquare, NorthWest, Piece, MoveFlags.Normal));
                        return Movelist;
                    }
                    else
                    {
                        Movelist.Add(new Move(StartingSquare, NorthWest, Piece, MoveFlags.Normal));
                        NorthWest <<= 7;
                    }
                }
                else
                {
                    return Movelist;
                }
            }
        }

        public static List<Move> SouthWestFill(Char Colour, ulong StartingSquare, string Piece)
        {
            List<Move> Movelist = new List<Move>();
            (ulong enemyPieces, ulong friendlyPieces) = Colour == 'W' ? (GetAllBlackPieces(), GetAllWhitePieces()) : (GetAllWhitePieces(), GetAllBlackPieces());
            if ((StartingSquare & (Rank1 | FileA)) != 0) return Movelist;
            ulong SouthWest = StartingSquare >> 9;
            while (true)
            {
                if ((SouthWest & friendlyPieces) == 0)
                {
                    if ((SouthWest & enemyPieces) != 0)
                    {
                        Movelist.Add(new Move(StartingSquare, SouthWest, Piece, MoveFlags.Capture, GetPieceAt(SouthWest)));
                        return Movelist;
                    }
                    else if ((SouthWest & (Rank1 | FileA)) != 0)
                    {
                        Movelist.Add(new Move(StartingSquare, SouthWest, Piece, MoveFlags.Normal));
                        return Movelist;
                    }
                    else
                    {
                        Movelist.Add(new Move(StartingSquare, SouthWest, Piece, MoveFlags.Normal));
                        SouthWest >>= 9;
                    }
                }
                else
                {
                    return Movelist;
                }
            }
        }
        
        public static List<Move> SouthEastFill(Char Colour, ulong StartingSquare, string Piece)
        {
            List<Move> Movelist = new List<Move>();
            (ulong enemyPieces, ulong friendlyPieces) = Colour == 'W' ? (GetAllBlackPieces(), GetAllWhitePieces()) : (GetAllWhitePieces(), GetAllBlackPieces());
            if ((StartingSquare & (Rank1|FileH)) != 0) return Movelist;
            ulong SouthEast = StartingSquare >> 7;
            while (true)
            {
                if ((SouthEast & friendlyPieces) == 0)
                {
                    if ((SouthEast & enemyPieces) != 0)
                    {
                        Movelist.Add(new Move(StartingSquare, SouthEast, Piece, MoveFlags.Capture, GetPieceAt(SouthEast)));
                        return Movelist;
                    }
                    else if ((SouthEast & (Rank1|FileH)) != 0)
                    {
                        Movelist.Add(new Move(StartingSquare, SouthEast, Piece, MoveFlags.Normal));
                        return Movelist;
                    }
                    else
                    {
                        Movelist.Add(new Move(StartingSquare, SouthEast, Piece, MoveFlags.Normal));
                        SouthEast >>= 7;
                    }
                }
                else
                {
                    return Movelist;
                }
            }
        }
    }
}