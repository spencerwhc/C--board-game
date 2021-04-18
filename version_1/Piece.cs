namespace PlayerBoardGame
{
    class Piece
    {
        private static Piece instance;

        private Piece() { }

        public static Piece Instance()
        {
            if (instance == null)
                instance = new Piece();
            return instance;
        }

        public int Row_Postition;
        public int Col_Position;
        public string Colour;


        public Piece(int row, int col, string colour)
        {
            Row_Postition = row;
            Col_Position = col;
            Colour = colour;

        }


    }
}