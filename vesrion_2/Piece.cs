using System;
using System.Collections;

namespace vesrion2
{
    //Singleton pattern
    public class Piece
    {
        private static Piece instance = null;

        private Piece() { }
        public static Piece Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Piece();
                }

                return instance;
            }
        }


        public ArrayList PiecePosition = new ArrayList();
        public string Colour;
        public int currentPlayer;
       
        




    }
}
