using System;
namespace vesrion2
{
     class OnlineHelpSystem
    {
        //singletone pattern
        private static OnlineHelpSystem instance = null;
        public static OnlineHelpSystem Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OnlineHelpSystem();
                }

                return instance;
            }
        }

        private OnlineHelpSystem() { }

        public void DisplayGameRule()
        {
            
           Console.WriteLine("Connect Four aka Four in a Row. Two players take turns dropping pieces on a 7x6 board. The player forms an unbroken chain of four pieces horizontally, vertically, or diagonally, wins the game");
            Console.WriteLine("In the menu interface: User can choose what game they want to play by enter an option");
            Console.WriteLine("1) Start new Game ");
            Console.WriteLine("2) Load old Game");
            Console.WriteLine("3) Exit game");
            Console.WriteLine("4) Undo move");
            Console.WriteLine("5) Redo move");
            Console.WriteLine("6) Read Game Rule");
            Console.WriteLine("In order to make move, simply type in the rol number ");
        }
    }
}