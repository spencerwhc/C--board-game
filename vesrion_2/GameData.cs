using System;
using System.Collections;

namespace vesrion2
{
    public class GameData
    {
        //Singleton pattern
        private static GameData instance = null;
        public static GameData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameData();
                }

                return instance;
            }
        }

        private GameData() { }


        //Player Data field
        public int playerID;
        public string playerColour;
        public string playerName;
        public int OpponentID;
        public string OpponentColour;
        public string OpponentName;

        
        //Game Data field
        public int difficulty;
        public int gameStatus;
        public int inputNumber = 0;
        public int currentPlayer;
        public int winner;
        public int mode;

        //Move Data field
        public ArrayList RedoList = new ArrayList();

       

    }
}
