namespace ChessBot.Utils
{
    public static class PieceSquareTables
    {
        public static Dictionary<ulong, int> BitboardToNumber = new Dictionary<ulong, int>
        {
            {(ulong)1 ,56},{(ulong)1 << 1 ,57},{(ulong)1 << 2 ,58},{(ulong)1 << 3 ,59},{(ulong)1 << 4 ,60},{(ulong)1 << 5 ,61},{(ulong)1 << 6 ,62},{(ulong)1 << 7 ,63},
            {(ulong)1 << 8 ,48},{(ulong)1 << 9 ,49},{(ulong)1 << 10 ,50},{(ulong)1 << 11 ,51},{(ulong)1 << 12 ,52},{(ulong)1 << 13 ,53},{(ulong)1 << 14 ,54},{(ulong)1 << 15 ,55},
            {(ulong)1 << 16 ,40},{(ulong)1 << 17 ,41},{(ulong)1 << 18 ,42},{(ulong)1 << 19 ,43},{(ulong)1 << 20 ,44},{(ulong)1 << 21 ,45},{(ulong)1 << 22 ,46},{(ulong)1 << 23 ,47},
            {(ulong)1 << 24 ,32},{(ulong)1 << 25 ,33},{(ulong)1 << 26 ,34},{(ulong)1 << 27 ,35},{(ulong)1 << 28 ,36},{(ulong)1 << 29 ,37},{(ulong)1 << 30 ,38},{(ulong)1 << 31 ,39},
            {(ulong)1 << 32 ,24},{(ulong)1 << 33 ,25},{(ulong)1 << 34 ,26},{(ulong)1 << 35 ,27},{(ulong)1 << 36 ,28},{(ulong)1 << 37 ,29},{(ulong)1 << 38 ,30},{(ulong)1 << 39 ,31},
            {(ulong)1 << 40 ,16},{(ulong)1 << 41 ,17},{(ulong)1 << 42 ,18},{(ulong)1 << 43 ,19},{(ulong)1 << 44 ,20},{(ulong)1 << 45 ,21},{(ulong)1 << 46 ,22},{(ulong)1 << 47 ,23},
            {(ulong)1 << 48 ,8},{(ulong)1 << 49 ,9},{(ulong)1 << 50 ,10},{(ulong)1 << 51 ,11},{(ulong)1 << 52 ,12},{(ulong)1 << 53 ,13},{(ulong)1 << 54 ,14},{(ulong)1 << 55 ,15},
            {(ulong)1 << 56 ,0},{(ulong)1 << 57 ,1},{(ulong)1 << 58 ,2},{(ulong)1 << 59 ,3},{(ulong)1 << 60 ,4},{(ulong)1 << 61 ,5},{(ulong)1 << 62 ,6},{(ulong)1 << 63 ,7}
        };

        public static List<float> WhitePawnsStartGame = new List<float>
        {
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.1f, 0.0f, 0.0f,
            0.1f, 0.1f, 0.2f, 0.7f, 0.5f, 0.2f, 0.1f, 0.1f,
            0.3f, 0.3f, 0.9f, 1.5f, 1.1f, 0.4f, 0.3f, 0.2f,
            0.3f, 0.3f, 0.4f, 0.1f, 0.3f, 0.3f, 0.5f, 0.3f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f
        };

        public static List<float> WhiteKnightsStartGame = new List<float>
        {
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.1f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.1f, 0.1f, 0.2f, 0.2f, 0.1f, 0.1f, 0.0f,
            0.1f, 0.0f, 0.1f, 0.4f, 0.2f, 0.1f, 0.0f, 0.0f,
            0.0f, 0.1f, 1.2f, 0.1f, 0.1f, 1.5f, 0.1f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.4f, 0.2f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f
        };

        public static List<float> WhiteRooksStartGame = new List<float>
        {
            0.0f, 0.0f, 0.1f, 0.1f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.1f, 0.1f, 0.1f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.1f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.1f, 0.1f, 0.1f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.1f, 0.1f, 0.1f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.1f, 0.1f, 0.1f, 0.1f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.1f, 0.2f, 0.1f, 0.1f, 0.0f, 0.0f,
            0.1f, 0.4f, 1.0f, 1.5f, 1.3f, 0.2f, 0.1f, 0.0f
        };

        public static List<float> WhiteBishopsStartGame = new List<float>
        {
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.1f, 0.1f, 0.0f, 0.1f, 0.0f,
            0.1f, 0.1f, 0.2f, 0.1f, 0.1f, 0.3f, 0.0f, 0.1f,
            0.0f, 0.5f, 0.1f, 0.2f, 0.2f, 0.1f, 1.0f, 0.0f,
            0.1f, 0.1f, 0.8f, 0.3f, 0.3f, 0.8f, 0.1f, 0.1f,
            0.1f, 0.3f, 0.2f, 1.3f, 1.5f, 0.4f, 0.2f, 0.1f,
            0.0f, 0.4f, 0.2f, 0.7f, 1.0f, 0.1f, 1.0f, 0.0f,
            0.0f, 0.0f, 0.1f, 0.1f, 0.1f, 0.2f, 0.0f, 0.0f
        };

        public static List<float> WhiteQueenStartGame = new List<float>
        {
            0.0f, 0.0f, 0.0f, 0.1f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.0f, 0.0f, 0.0f,
            0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f,
            0.1f, 0.1f, 0.1f, 0.2f, 0.1f, 0.1f, 0.1f, 0.2f,
            0.3f, 0.1f, 0.3f, 0.5f, 0.3f, 0.2f, 0.3f, 0.1f,
            0.1f, 0.7f, 0.4f, 0.8f, 0.6f, 0.7f, 0.2f, 0.1f,
            0.0f, 0.1f, 1.3f, 1.5f, 1.3f, 0.2f, 0.1f, 0.0f,
            0.0f, 0.1f, 0.2f, 0.2f, 0.2f, 0.0f, 0.0f, 0.0f
        };

        public static List<float> WhiteKingStartGame = new List<float>
        {
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.1f, 0.1f, 0.2f, 0.1f,
            0.0f, 0.1f, 0.1f, 0.0f, 0.0f, 0.1f, 1.5f, 0.1f
        };

        public static List<float> BlackPawnsStartGame = new List<float>
        {
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.7f, 0.4f, 0.6f, 0.7f, 1.1f, 0.3f, 0.8f, 0.5f,
            0.4f, 0.7f, 1.3f, 1.5f, 1.0f, 0.5f, 0.3f, 0.3f,
            0.1f, 0.2f, 0.3f, 0.8f, 0.3f, 0.1f, 0.1f, 0.1f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f
        };

        public static List<float> BlackKnightsStartGame = new List<float>
        {
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.1f, 0.7f, 0.2f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.1f, 0.8f, 0.1f, 0.1f, 1.5f, 0.1f, 0.0f,
            0.1f, 0.0f, 0.2f, 0.3f, 0.2f, 0.1f, 0.0f, 0.1f,
            0.0f, 0.1f, 0.1f, 0.1f, 0.2f, 0.0f, 0.1f, 0.0f,
            0.0f, 0.0f, 0.1f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f
        };

        public static List<float> BlackRooksStartGame = new List<float>
        {
            0.2f, 0.7f, 1.5f, 1.5f, 1.5f, 0.3f, 0.2f, 0.1f,
            0.1f, 0.1f, 0.2f, 0.3f, 0.2f, 0.1f, 0.0f, 0.0f,
            0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.1f, 0.1f, 0.1f, 0.1f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.1f, 0.1f, 0.1f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.1f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.1f, 0.1f, 0.1f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.1f, 0.1f, 0.1f, 0.0f, 0.0f, 0.0f
        };

        public static List<float> BlackBishopsStartGame = new List<float>
        {
            0.0f, 0.0f, 0.1f, 0.1f, 0.1f, 0.2f, 0.0f, 0.0f,
            0.0f, 0.8f, 0.1f, 0.9f, 1.5f, 0.1f, 1.1f, 0.0f,
            0.2f, 0.1f, 0.3f, 0.5f, 0.7f, 0.4f, 0.1f, 0.1f,
            0.0f, 0.1f, 0.4f, 0.3f, 0.2f, 0.5f, 0.1f, 0.1f,
            0.0f, 0.5f, 0.1f, 0.2f, 0.2f, 0.1f, 0.4f, 0.0f,
            0.0f, 0.0f, 0.2f, 0.1f, 0.1f, 0.2f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.1f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f
        };

        public static List<float> BlackQueenStartGame = new List<float>
        {
            0.1f, 0.1f, 0.2f, 0.4f, 0.2f, 0.1f, 0.0f, 0.0f,
            0.1f, 0.2f, 1.5f, 0.8f, 1.1f, 0.2f, 0.1f, 0.0f,
            0.1f, 0.8f, 0.3f, 0.5f, 0.3f, 0.5f, 0.1f, 0.0f,
            0.6f, 0.1f, 0.3f, 0.4f, 0.3f, 0.2f, 0.2f, 0.1f,
            0.1f, 0.1f, 0.1f, 0.2f, 0.1f, 0.1f, 0.1f, 0.1f,
            0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.0f, 0.0f,
            0.0f, 0.1f, 0.1f, 0.1f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.1f, 0.0f, 0.0f, 0.0f, 0.0f
        };

        public static List<float> BlackKingStartGame = new List<float>
        {
            0.0f, 0.0f, 0.1f, 0.0f, 0.0f, 0.1f, 1.5f, 0.1f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.1f, 0.1f, 0.2f, 0.1f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f
        };

        public static List<float> WhitePawnsMidGame = new List<float>
        {
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f,
            0.3f, 0.3f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f,
            0.6f, 0.6f, 0.3f, 0.2f, 0.3f, 0.6f, 0.8f, 0.7f,
            0.9f, 0.8f, 0.4f, 0.1f, 0.2f, 1.1f, 1.5f, 1.5f,
            0.2f, 0.3f, 0.1f, 0.0f, 0.1f, 0.5f, 0.6f, 0.4f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f
        };

        public static List<float> WhiteKnightsMidGame = new List<float>
        {
            0.0f, 0.0f, 0.1f, 0.1f, 0.1f, 0.1f, 0.0f, 0.0f,
            0.1f, 0.2f, 0.3f, 0.3f, 0.3f, 0.3f, 0.1f, 0.1f,
            0.1f, 0.3f, 0.8f, 1.0f, 0.8f, 0.7f, 0.2f, 0.1f,
            0.2f, 0.5f, 1.1f, 1.2f, 1.3f, 1.0f, 0.6f, 0.2f,
            0.1f, 0.4f, 1.1f, 1.4f, 1.5f, 1.0f, 0.4f, 0.2f,
            0.1f, 0.3f, 0.8f, 1.1f, 1.2f, 1.0f, 0.4f, 0.1f,
            0.0f, 0.1f, 0.3f, 0.7f, 0.6f, 0.3f, 0.1f, 0.0f,
            0.0f, 0.0f, 0.1f, 0.1f, 0.2f, 0.2f, 0.1f, 0.0f
        };

        public static List<float> WhiteRooksMidGame = new List<float>
        {
            0.8f, 0.6f, 0.6f, 0.7f, 0.5f, 0.4f, 0.3f, 0.3f,
            1.5f, 1.3f, 1.2f, 1.1f, 0.8f, 0.7f, 0.6f, 0.6f,
            1.1f, 0.9f, 0.8f, 0.8f, 0.6f, 0.4f, 0.4f, 0.4f,
            0.8f, 0.7f, 0.7f, 0.7f, 0.6f, 0.4f, 0.3f, 0.3f,
            0.5f, 0.5f, 0.5f, 0.6f, 0.5f, 0.4f, 0.2f, 0.2f,
            0.3f, 0.3f, 0.4f, 0.6f, 0.5f, 0.4f, 0.1f, 0.1f,
            0.2f, 0.2f, 0.5f, 0.6f, 0.6f, 0.3f, 0.1f, 0.0f,
            0.4f, 0.5f, 0.7f, 0.9f, 0.7f, 0.5f, 0.2f, 0.1f
        };

        public static List<float> WhiteBishopsMidGame = new List<float>
        {
            0.0f, 0.1f, 0.2f, 0.2f, 0.3f, 0.2f, 0.1f, 0.0f,
            0.1f, 0.4f, 0.3f, 0.4f, 0.3f, 0.4f, 0.2f, 0.1f,
            0.3f, 0.4f, 0.8f, 0.6f, 0.7f, 0.5f, 0.4f, 0.2f,
            0.2f, 0.8f, 0.9f, 1.5f, 0.9f, 0.7f, 0.4f, 0.2f,
            0.2f, 0.5f, 1.3f, 1.2f, 1.4f, 0.7f, 0.4f, 0.1f,
            0.1f, 0.6f, 0.8f, 1.4f, 1.2f, 1.1f, 0.3f, 0.2f,
            0.1f, 0.2f, 0.6f, 0.8f, 1.1f, 0.4f, 0.3f, 0.0f,
            0.0f, 0.1f, 0.3f, 0.5f, 0.4f, 0.5f, 0.1f, 0.0f
        };

        public static List<float> WhiteQueenMidGame = new List<float>
        {
            0.4f, 0.6f, 0.8f, 1.0f, 0.8f, 0.5f, 0.3f, 0.3f,
            0.5f, 0.8f, 0.9f, 0.9f, 0.9f, 0.7f, 0.3f, 0.3f,
            0.3f, 0.5f, 0.7f, 0.9f, 1.0f, 0.9f, 0.5f, 0.3f,
            0.2f, 0.4f, 0.8f, 1.3f, 1.5f, 0.9f, 0.7f, 0.4f,
            0.2f, 0.4f, 0.8f, 1.4f, 1.4f, 1.0f, 0.6f, 0.4f,
            0.1f, 0.4f, 0.8f, 1.0f, 1.1f, 1.0f, 0.5f, 0.1f,
            0.1f, 0.2f, 0.5f, 0.7f, 0.7f, 0.5f, 0.2f, 0.0f,
            0.1f, 0.1f, 0.2f, 0.2f, 0.2f, 0.1f, 0.0f, 0.0f
        };


        public static List<float> WhiteKingMidGame = new List<float>
        {
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.0f,
            0.0f, 0.1f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.1f,
            0.0f, 0.1f, 0.3f, 0.5f, 0.7f, 0.6f, 0.4f, 0.1f,
            0.0f, 0.2f, 0.4f, 0.9f, 1.3f, 1.4f, 1.0f, 0.4f,
            0.1f, 0.2f, 0.3f, 0.6f, 1.1f, 1.4f, 1.5f, 0.7f,
            0.0f, 0.1f, 0.1f, 0.1f, 0.3f, 0.7f, 0.6f, 0.2f
        };

        public static List<float> BlackPawnsMidGame = new List<float>
        {
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.1f, 0.2f, 0.1f, 0.0f, 0.1f, 0.5f, 0.5f, 0.4f,
            0.8f, 0.6f, 0.3f, 0.2f, 0.5f, 1.1f, 1.5f, 1.5f,
            0.6f, 0.5f, 0.3f, 0.3f, 0.5f, 0.6f, 0.8f, 0.7f,
            0.3f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f,
            0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.0f, 0.1f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f
        };


        public static List<float> BlackKnightsMidGame = new List<float>
        {
            0.0f, 0.1f, 0.1f, 0.2f, 0.2f, 0.2f, 0.1f, 0.0f,
            0.0f, 0.1f, 0.4f, 0.9f, 0.6f, 0.3f, 0.1f, 0.1f,
            0.1f, 0.4f, 0.9f, 1.1f, 1.2f, 1.1f, 0.4f, 0.1f,
            0.1f, 0.4f, 1.1f, 1.5f, 1.5f, 1.0f, 0.4f, 0.2f,
            0.2f, 0.5f, 1.0f, 1.2f, 1.3f, 1.0f, 0.5f, 0.2f,
            0.1f, 0.3f, 0.7f, 0.9f, 0.8f, 0.6f, 0.2f, 0.1f,
            0.1f, 0.1f, 0.2f, 0.3f, 0.3f, 0.2f, 0.1f, 0.1f,
            0.0f, 0.0f, 0.1f, 0.1f, 0.1f, 0.1f, 0.0f, 0.0f
        };


        public static List<float> BlackRooksMidGame = new List<float>
        {
            0.5f, 0.7f, 0.9f, 1.0f, 0.8f, 0.5f, 0.2f, 0.1f,
            0.3f, 0.3f, 0.5f, 0.6f, 0.6f, 0.4f, 0.1f, 0.0f,
            0.3f, 0.3f, 0.4f, 0.5f, 0.5f, 0.4f, 0.1f, 0.0f,
            0.5f, 0.4f, 0.6f, 0.6f, 0.5f, 0.4f, 0.2f, 0.1f,
            0.8f, 0.7f, 0.7f, 0.7f, 0.6f, 0.5f, 0.3f, 0.3f,
            1.1f, 0.9f, 0.8f, 0.6f, 0.4f, 0.4f, 0.4f, 0.4f,
            1.5f, 1.4f, 1.1f, 1.0f, 0.7f, 0.7f, 0.6f, 0.6f,
            0.8f, 0.6f, 0.6f, 0.6f, 0.5f, 0.4f, 0.3f, 0.3f
        };


        public static List<float> BlackBishopsMidGame = new List<float>
        {
            0.0f, 0.1f, 0.3f, 0.5f, 0.4f, 0.5f, 0.0f, 0.0f,
            0.1f, 0.3f, 0.5f, 0.9f, 0.9f, 0.4f, 0.4f, 0.0f,
            0.2f, 0.5f, 0.9f, 1.1f, 1.1f, 1.1f, 0.3f, 0.2f,
            0.2f, 0.5f, 1.2f, 1.3f, 1.3f, 0.8f, 0.4f, 0.1f,
            0.2f, 0.6f, 0.8f, 1.5f, 1.0f, 0.6f, 0.4f, 0.2f,
            0.2f, 0.4f, 0.8f, 0.6f, 0.6f, 0.5f, 0.3f, 0.1f,
            0.1f, 0.4f, 0.4f, 0.4f, 0.3f, 0.4f, 0.2f, 0.1f,
            0.0f, 0.1f, 0.2f, 0.2f, 0.2f, 0.2f, 0.1f, 0.0f
        };


        public static List<float> BlackQueenMidGame = new List<float>
        {
            0.1f, 0.1f, 0.2f, 0.3f, 0.2f, 0.2f, 0.0f, 0.0f,
            0.1f, 0.3f, 0.5f, 0.6f, 0.7f, 0.5f, 0.2f, 0.0f,
            0.1f, 0.4f, 0.7f, 0.9f, 1.0f, 1.0f, 0.4f, 0.1f,
            0.2f, 0.4f, 0.8f, 1.3f, 1.5f, 1.0f, 0.6f, 0.3f,
            0.3f, 0.5f, 0.8f, 1.4f, 1.5f, 1.1f, 0.6f, 0.4f,
            0.2f, 0.4f, 0.7f, 0.8f, 0.9f, 0.8f, 0.4f, 0.3f,
            0.4f, 0.8f, 0.9f, 0.9f, 0.8f, 0.7f, 0.3f, 0.2f,
            0.5f, 0.6f, 0.8f, 0.9f, 0.9f, 0.5f, 0.3f, 0.3f
        };
        
        public static List<float> BlackKingMidGame = new List<float>
        {
            0.0f, 0.0f, 0.1f, 0.1f, 0.3f, 0.7f, 0.6f, 0.2f,
            0.0f, 0.1f, 0.3f, 0.6f, 1.1f, 1.3f, 1.5f, 0.6f,
            0.0f, 0.1f, 0.3f, 0.7f, 1.0f, 1.3f, 0.9f, 0.3f,
            0.0f, 0.1f, 0.2f, 0.4f, 0.6f, 0.5f, 0.4f, 0.1f,
            0.0f, 0.1f, 0.1f, 0.2f, 0.2f, 0.2f, 0.2f, 0.1f,
            0.0f, 0.0f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f
        };
    }
}