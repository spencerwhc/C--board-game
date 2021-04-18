using System;
using System.Collections;
using System.IO;

namespace vesrion2
{
    public class BoardGame
    {
        public  Player player1;
        public Player player2;

        const string FILENAME = "GameHistory.txt";


        
        public void selectPlayer()
        {

            Console.WriteLine("Welcome to this game, please choose the game mode");
            Console.WriteLine("1: Human vs Human");
            Console.WriteLine("2: Human vs AI");
            Console.Write("Select Players: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();


            switch (choice)
            {
                case 1:

                    player1 = new HumanPlayer(1);

                    player2 = new HumanPlayer(2);

                    GameData.Instance.mode = 1;
                    GameData.Instance.difficulty = 0;
                    GameData.Instance.currentPlayer = 1;
                 

                    break;

                case 2:

                    

                    player1 = new HumanPlayer(1);
                    GameData.Instance.mode = 2;
                    GameData.Instance.currentPlayer = 1;
                    selectDifficulty();

                    break;

                case 3:
                    break;

            }



        }
        public void selectDifficulty()
        {
            Console.Clear();
            Console.WriteLine("Select AI difficulty ");
            Console.WriteLine("1: Easy mode ");
            Console.WriteLine("2: Hard mode ");
            int choice = Convert.ToInt32(Console.ReadLine());
            if (choice == 1)
            {
                player2 = new EasyAIPlayer();
                GameData.Instance.difficulty = 1;
      
                
            }
            else
            {
                player2 = new HardAIPlayer();
                GameData.Instance.difficulty = 2;
            

            }

        }

        public bool DropPiece(int[] move, string Colour)
        {


            Board.Instance.cell[move[0], move[1]] = Colour;
            Board.Instance.pieceCount++;
            Piece.Instance.PiecePosition.Add(move);
            GameData.Instance.RedoList.Add(move);
            
          

            return true;
        }

        public void getMove()
        {
           
            bool gameLoop = true;
            bool inputLoop;
            Player currentPlayer;
            
            

            int[] move = new int[2];
            while (gameLoop)
            {

                
                if (GameData.Instance.currentPlayer == 1)
                {
                    currentPlayer = player1;
                }
                else
                {
                    currentPlayer = player2;
                }
                Console.Clear();
                Board.Instance.DisplayPlayer();
                Board.Instance.DisplayBoard();


                do
                {
                    inputLoop = true;
                    move = currentPlayer.makeMove(currentPlayer);
                    if (move != null)
                    {
                        DropPiece(move, currentPlayer.Colour);
                        saveHistory(GameData.Instance.inputNumber, move,currentPlayer.ID, currentPlayer.Name,currentPlayer.Colour);
                        inputLoop = false;
                        GameData.Instance.inputNumber++;
                    }


                } while (inputLoop);

                IRule rule = new Connect4Rule();
                RuleCommand gameOn = new ConcreteRuleCommand(rule);
                CommandInvoker ruleC = new CommandInvoker(gameOn);

                if (ruleC.hasWinner(currentPlayer))
                {
                    Console.Clear();
                    Board.Instance.DisplayBoard();
                    Console.Write("Player {0} has won!!", currentPlayer.Name);
                    Console.WriteLine("\nPress enter to quit.");
                    GameData.Instance.winner = currentPlayer.ID;
                    gameLoop = false;
                }
                if (Board.Instance.BoardIsFull())
                {
                    System.Console.Clear();
                    Board.Instance.DisplayBoard();
                    Console.WriteLine("\nIt is a draw.");
                    Console.WriteLine("\nPress enter to quit.");
                    gameLoop = false;
                }
                else
                {
                    if(GameData.Instance.currentPlayer == 1)
                    {
                        GameData.Instance.currentPlayer = 2;
                    }
                    else
                    {
                        GameData.Instance.currentPlayer = 1;
                    }



                }
            }

            Console.ReadKey();


        }

        public void saveHistory(int inputNum, int[] move, int playerID, string playerName, string playerColour)
        {
            
            

            //Create new game histroy
        
            StreamWriter writer = File.AppendText(FILENAME);


            //save data info into the file
            if (inputNum == 0)
            {
                writer.WriteLine("new");
            }

            writer.WriteLine($"{inputNum},{Piece.Instance.PiecePosition.Count},{playerID},{playerName},{playerColour},{move[0]},{move[1]},{GameData.Instance.mode},{GameData.Instance.difficulty}");
            writer.Close();
        }

        public void redoMove()
        {
            //get the Piece pistition arrayList length
            int count = GameData.Instance.RedoList.Count;
            if (count == 0)
            {
                Console.WriteLine("No move to be redo");
                Menu.Instance.DisplayMenu();
            }
            // get the last player move 

            int[] opponentMove = (int[])GameData.Instance.RedoList[count - 1];

            ////get the current player last move 
            int[] currentPlayerLastMove = (int[])GameData.Instance.RedoList[count - 2];

            //Redo the last player move and current player last move
            Board.Instance.cell[opponentMove[0], opponentMove[1]] = GameData.Instance.OpponentColour;
            Board.Instance.cell[currentPlayerLastMove[0], currentPlayerLastMove[1]] = GameData.Instance.playerColour;
            Board.Instance.DisplayBoard();

            //Add the undoMove from Piece pistition arrayList and gameHistroy
            Piece.Instance.PiecePosition.Add(currentPlayerLastMove);
            saveHistory(Piece.Instance.PiecePosition.Count -1,currentPlayerLastMove,GameData.Instance.playerID, GameData.Instance.playerName, GameData.Instance.playerColour);
            Piece.Instance.PiecePosition.Add(opponentMove);
            saveHistory(Piece.Instance.PiecePosition.Count - 1, opponentMove, GameData.Instance.OpponentID, GameData.Instance.OpponentName, GameData.Instance.OpponentColour);


            getMove();

        }

        public void deletLine(int lineToDelet,string path)
        {
            //Delet move from the game histroy


            //Read the whole file into memory
            
                string[] readText = File.ReadAllLines(path);
                ArrayList gameHistroy = new ArrayList(readText);

                //Delet the line in the file
                gameHistroy.RemoveRange(gameHistroy.Count - lineToDelet, lineToDelet);
                GameData.Instance.inputNumber = GameData.Instance.inputNumber - lineToDelet;


                //replace the old file with new line
                readText = (string[])gameHistroy.ToArray(typeof(string));
            File.AppendAllLines(FILENAME, readText);





        }

        public void undoMove()
        {
            //get the Piece pistition arrayList length
            int count = GameData.Instance.RedoList.Count;

            if(count == 0)
            {
                Console.WriteLine("No move to be undo");
                Menu.Instance.DisplayMenu();
            }
            else
            {
                // get the last player move 

                int[] opponentMove = (int[])GameData.Instance.RedoList[count - 1];

                ////get the current player last move 
                int[] currentPlayerLastMove = (int[])GameData.Instance.RedoList[count - 2];

                //Undo the last player move and current player last move
                Board.Instance.cell[opponentMove[0], opponentMove[1]] = " ";
                Board.Instance.cell[currentPlayerLastMove[0], currentPlayerLastMove[1]] = " ";


                //Delete the undoMove from Piece pistition arrayList
                Piece.Instance.PiecePosition.RemoveAt(count -1);
                Piece.Instance.PiecePosition.RemoveAt(count -2);
            }

            
            deletLine(2, FILENAME); 
            getMove();



        }
    }

}
