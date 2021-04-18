using System;

namespace vesrion2
{
    //Template method to create base class for player
    public abstract class Player
    {

        public string Name { get; set; }
        public int ID { get; set; }
        public string Colour { get; set; }

        public Player() { }

        public void SetPlayerName(int playerNum)
        {
           
            Console.Clear();
            Console.WriteLine("What is player {0} Name", playerNum);
            Name = Console.ReadLine();
            
            

        }

        internal void setAsRed()
        {
            Colour = "X";


        }

        internal void setAsYellow()
        {
            Colour = "O";
        }

        public abstract int[] makeMove( Player player);

    }
    public class EasyAIPlayer : Player
    {

        public EasyAIPlayer()
        {
            Name = "Easy AI";
            setAsYellow();
            ID = 2;
            GameData.Instance.OpponentName = Name;
            GameData.Instance.OpponentID = ID;
            GameData.Instance.OpponentColour = Colour;
        }

        //Command Pattern: Recevier
        public override int[] makeMove(Player player)
        {
            IRule rule = new Connect4Rule();
            RuleCommand gameOn = new ConcreteRuleCommand(rule);
            CommandInvoker ruleC = new CommandInvoker(gameOn);

            int[] move = new int[2];
            Random random = new Random();
            bool moveMade = false;
            while (!moveMade)
            {
                int AI_Move = random.Next(1,7);
                
                if (ruleC.getValidMove(AI_Move) != null)
                {
                    move = ruleC.getValidMove(AI_Move);
                    moveMade = true;
                }
                else
                {
                    continue;
                }

            }

            return move;
        }



    }

    public class HardAIPlayer : Player
    {

        public HardAIPlayer()
        {
            Name = "Hard AI";
            ID = 2;
            setAsYellow();
            GameData.Instance.OpponentName = Name;
            GameData.Instance.OpponentID = ID;
            GameData.Instance.OpponentColour = Colour;
        }

        //Command Pattern: Recevier
        public override int[] makeMove(Player player)
        {
            //easy rule;
            IRule rule = new Connect4Rule();
            RuleCommand gameOn = new ConcreteRuleCommand(rule);
            CommandInvoker ruleC = new CommandInvoker(gameOn);

            int[] move = new int[2];
            Random random = new Random();
            bool moveMade = false;
            while (!moveMade)
            {
                int AI_Move = random.Next(1, 7);

                if (ruleC.getValidMove(AI_Move) != null)
                {
                    move = ruleC.getValidMove(AI_Move);
                    moveMade = true;
                }
                else
                {
                    continue;
                }

            }

            return move;

            //Hard Rule haven't been implement yet
            //IRule rule = new Connect4Rule();
            //RuleCommand gameOn = new ConcreteRuleCommand(rule);
            //CommandInvoker ruleC = new CommandInvoker(gameOn);

            //int[] move = new int[2];

            //bool moveMade = false;
            //while (!moveMade)
            //{
            //    for(int i = 0; i<Board.Instance.ColumnsCount; i++)
            //    {
            //        if (ruleC.getValidMove(i) != null)
            //        {
            //            move = ruleC.getValidMove(i);


            //            if (ruleC.getComputerHardMove(i,player) != null)
            //            {
            //                move = ruleC.getComputerHardMove(i, player);
            //                moveMade = true;
            //            }
            //            else
            //            {
            //                return move;
            //            }

            //        }
            //        else
            //        {
            //            continue;
            //        }
            //    }



            //}

            //return move;


        }
    }

    public class HumanPlayer : Player
    {

        public HumanPlayer(int playerNum)
        {
            if (Piece.Instance.PiecePosition.Count == 0)
            {
                if (playerNum == 1)
                {
                    SetPlayerName(playerNum);
                    ID = playerNum;
                    setAsRed();
                    GameData.Instance.playerName = Name;
                    GameData.Instance.playerID = ID;
                    GameData.Instance.playerColour = Colour;
                }
                else
                {
                    SetPlayerName(playerNum);
                    ID = playerNum;
                    setAsYellow();
                    GameData.Instance.OpponentName = Name;
                    GameData.Instance.OpponentID = ID;
                    GameData.Instance.OpponentColour = Colour;
                }
                    
            }
            else 
            {
                if (playerNum == 1)
                {
                    Name = GameData.Instance.playerName;
                    ID = GameData.Instance.playerID;
                    Colour = GameData.Instance.playerColour;
                }
                else
                {
                    Name = GameData.Instance.OpponentName;
                    ID = GameData.Instance.OpponentID;
                    Colour = GameData.Instance.OpponentColour;
                }
            }
            
      
           

        }

        //Command Pattern: Recevier
        public override int[] makeMove(Player player)
        {
            IRule rule = new Connect4Rule();
            RuleCommand gameOn = new ConcreteRuleCommand(rule);
            CommandInvoker ruleC = new CommandInvoker(gameOn);

            Boolean moveMade = false;
            int col_move;
            int[] move = new int[2];

            Console.Write("player {0} turn's ", Name);
            while (!moveMade)
            {
                Console.Write("Make a Move from 1 to {0} or read menu by  999:  ", Board.Instance.ColumnsCount);
                if (Int32.TryParse(Console.ReadLine(), out col_move))
                {
                    if (1 <= col_move && col_move <= Board.Instance.ColumnsCount)
                    {
                        move = ruleC.getValidMove(col_move);

                        if (move != null)
                        {
                            Console.Clear();
                            Board.Instance.DisplayBoard();
                            moveMade = true;
                        }
                        else
                        {
                            Console.WriteLine("\nThat column is full.");
                            continue;
                        }

                    }
                    else if(col_move == 999)
                    {
                        Menu.Instance.DisplayMenu();

                    }
                }
                else
                {
                    Console.Clear();
                    Board.Instance.DisplayBoard();
                    Console.WriteLine("\nPlease enter an valid integer.");
                    continue;
                }
            }

            return move;
        }

    }

}