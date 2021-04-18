using System;

namespace vesrion2
{ 
    class Menu
    {
        //singletone pattern
        private static Menu instance = null;

        private Menu() { }
        public static Menu Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Menu();
                }

                return instance;
            }
        }

        public BoardGame thegame = new BoardGame();
        

      
        public void DisplayMenu()
        {
            bool choiceMade = false;
            while(!choiceMade)
            {
                Console.Clear();
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1) Start new Game ");
                Console.WriteLine("2) Load old Game");
                Console.WriteLine("3) Exit game");
                Console.WriteLine("4) Undo move");
                Console.WriteLine("5) Redo move");
                Console.WriteLine("6) Read Game Rule");
                Console.Write("\r\nSelect an option: ");
                int choice;
                if (Int32.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            
                            NewGame();
                            choiceMade = true;
                            break;
                        case 2:
                            LoadGame();
                            choiceMade = true;
                            break;
                        case 3:
                            ExitGame();
                            choiceMade = true;
                            break;
                        case 4:
                            
                            undoMove();
                            choiceMade = true;
                            break;
                        case 5:
                            RedoMove();
                            choiceMade = true;
                            break;
                        case 6:
                            getHelp();
                            choiceMade = true;
                            break;
                        case 7:
                            break;

                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid number");
                    continue;
                }
                
            } 
        }


        //Command Pattern: Client
        public void NewGame()
        {
            Game game = new Game(thegame);
            GameCommand gameOn = new ConcreteGameCommand(game);
            CommandInvoker onChose = new CommandInvoker(gameOn);
            onChose.chooseNewGame();
        }

        public void LoadGame()
        {
            Game game = new Game(thegame);
            GameCommand gameOn = new ConcreteGameCommand(game);
            CommandInvoker onChose = new CommandInvoker(gameOn);
            onChose.chooseLoadGame();
        }

        public void ExitGame()
        {
            Game game = new Game(thegame);
            GameCommand gameOn = new ConcreteGameCommand(game);
            CommandInvoker onChose = new CommandInvoker(gameOn);
            onChose.chooseExitGame();
        }

        public void undoMove()
        {

            MoveCommand gameOn = new ConcreteMoveCommand(thegame);
            CommandInvoker onChose = new CommandInvoker(gameOn);
            onChose.chooseUndoMove();
        }

        public void RedoMove()
        {
            MoveCommand gameOn = new ConcreteMoveCommand(thegame);
            CommandInvoker onChose = new CommandInvoker(gameOn);
            onChose.chooseRedoMove();
        }

        public void getHelp()
        {

            OnlineHelpSystem.Instance.DisplayGameRule();

        }

    }
}
