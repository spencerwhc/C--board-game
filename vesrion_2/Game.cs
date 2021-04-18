using System;
using System.Collections;
using System.IO;

namespace vesrion2
{
    //Command Pattern: Recevier
    class Game
    {
        const string FILENAME = "GameHistory.txt";
        BoardGame thegame;

        public Game(BoardGame game)
        {
            thegame = game;
        }

        public void startGame()
        {
            Board.Instance.RowsCount = 6;
            Board.Instance.ColumnsCount = 7;
            Board.Instance.CreateBoard();
            Console.Clear();
            thegame.selectPlayer();
            thegame.getMove();

        }

        public void exitGame()
        {
            Console.WriteLine("Game end");
        }

        public void getGameState()
        {
            int GameState;
            bool choiceMade = false;

            
          

            using (var sr = new StreamReader(FILENAME))
            {

                //Get the GameState from user
                string[] readText = File.ReadAllLines(FILENAME);
                    if(readText.Length == 0)
                    {
                        Console.WriteLine("No saved game, please start a new game");
                        startGame();
                    }
                ArrayList gameHistroy = new ArrayList(readText);
                //Display the game histroy
                for (int i = 1; i < gameHistroy.Count; i++)
                {

                    string state = (string)gameHistroy[i];
                    string[] stateInfo = state.Split(',');
                    Console.WriteLine($"Gamestate: {stateInfo[0]}, player Name: {stateInfo[3]}, row index: {stateInfo[5]}, column index: {stateInfo[6]}, diffuclty: {stateInfo[8]}");

                    //Store the data

                    GameHistroyData.Instance.oldData[i - 1, 0] = stateInfo[0]; //Status number
                    GameHistroyData.Instance.oldData[i - 1, 1] = stateInfo[1]; //Piece count
                    GameHistroyData.Instance.oldData[i - 1, 2] = stateInfo[2]; //Player ID
                    GameHistroyData.Instance.oldData[i - 1, 3] = stateInfo[3]; //Player Name
                    GameHistroyData.Instance.oldData[i - 1, 4] = stateInfo[4]; //Player Colour
                    GameHistroyData.Instance.oldData[i - 1, 5] = stateInfo[5]; //Row index
                    GameHistroyData.Instance.oldData[i - 1, 6] = stateInfo[6]; //Column index
                    GameHistroyData.Instance.oldData[i - 1, 7] = stateInfo[7]; //game mode
                    GameHistroyData.Instance.oldData[i - 1, 8] = stateInfo[8]; //Ai difficulty




                }



                //Promt user to selet a valid game state
                do
                {
                    Console.Write("Please choose the game state number to start the game:");
                    int choice;
                    if (Int32.TryParse(Console.ReadLine(), out choice))
                    {
                        if (choice < gameHistroy.Count - 1)
                        {
                            GameState = choice;
                            GameData.Instance.gameStatus = GameState;
                            choiceMade = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Input not valid");
                        continue;
                    }
                } while (!choiceMade);


                //Replace the game histroy file with new line
                int lineToDelete = gameHistroy.Count - 1 - GameData.Instance.gameStatus;
                
   



            }
        }

        public void loadGame()
        {
            getGameState();
            int num = GameData.Instance.gameStatus;

           



            Board.Instance.RowsCount = 6;
            Board.Instance.ColumnsCount = 7;
            Board.Instance.CreateBoard();

            ////Get piece position and Drop the piece in to the board
            for (int i = 0; i < num + 1; i++)
            {
                int[] move = new int[2];
                move[0] = Convert.ToInt32(GameHistroyData.Instance.oldData[i, 5]);
                move[1] = Convert.ToInt32(GameHistroyData.Instance.oldData[i, 6]);
                thegame.DropPiece(move, GameHistroyData.Instance.oldData[i, 4]);

            }

            //Initialise the game



            ////initialise the player

            if (num == 0)
            {

                GameData.Instance.mode = Int32.Parse(GameHistroyData.Instance.oldData[num, 7]);
                GameData.Instance.difficulty = Int32.Parse(GameHistroyData.Instance.oldData[num, 8]);
                GameData.Instance.playerID = Int32.Parse(GameHistroyData.Instance.oldData[num, 2]);
                GameData.Instance.playerName = GameHistroyData.Instance.oldData[num, 3];
                GameData.Instance.playerColour = GameHistroyData.Instance.oldData[num, 4];

                thegame.player1 = new HumanPlayer(1);

                thegame.player2 = new HumanPlayer(2);

            }
            else if (num > 0)
            {
                //initialise the player
                GameData.Instance.difficulty = Int32.Parse(GameHistroyData.Instance.oldData[num, 8]);

                Console.WriteLine(GameHistroyData.Instance.oldData[num, 8]);


                if (GameHistroyData.Instance.oldData[num, 8] == "0") // game mode is Player vs player
                {
                  
                    GameData.Instance.mode = Int32.Parse(GameHistroyData.Instance.oldData[num, 7]);
                    GameData.Instance.difficulty = Int32.Parse(GameHistroyData.Instance.oldData[num, 8]);
                    GameData.Instance.playerID = Int32.Parse(GameHistroyData.Instance.oldData[num, 2]);
                    GameData.Instance.playerName = GameHistroyData.Instance.oldData[num, 3];
                    GameData.Instance.playerColour = GameHistroyData.Instance.oldData[num, 4];
                    GameData.Instance.OpponentID = Int32.Parse(GameHistroyData.Instance.oldData[num - 1, 2]);
                    GameData.Instance.OpponentName = GameHistroyData.Instance.oldData[num - 1, 3];
                    GameData.Instance.OpponentColour = GameHistroyData.Instance.oldData[num - 1, 4];
                    GameData.Instance.currentPlayer = GameData.Instance.OpponentID;
                    thegame.player1 = new HumanPlayer(1);
                    thegame.player2 = new HumanPlayer(2);
                }
                else if (GameHistroyData.Instance.oldData[num, 8] == "1")// gme mode is player vs Easy AI
                {
                  
                    GameData.Instance.mode = Int32.Parse(GameHistroyData.Instance.oldData[num, 7]);
                    GameData.Instance.difficulty = Int32.Parse(GameHistroyData.Instance.oldData[num, 8]);
                    GameData.Instance.playerID = Int32.Parse(GameHistroyData.Instance.oldData[num - 1, 2]);
                    GameData.Instance.playerName = GameHistroyData.Instance.oldData[num - 1, 3];
                    GameData.Instance.playerColour = GameHistroyData.Instance.oldData[num - 1, 4];
                    GameData.Instance.OpponentID = Int32.Parse(GameHistroyData.Instance.oldData[num, 2]);
                    GameData.Instance.OpponentName = GameHistroyData.Instance.oldData[num, 3];
                    GameData.Instance.OpponentColour = GameHistroyData.Instance.oldData[num, 4];
                    GameData.Instance.currentPlayer = GameData.Instance.OpponentID;
                    thegame.player1 = new HumanPlayer(1);
                    thegame.player2 = new EasyAIPlayer();
                }
                else if (GameHistroyData.Instance.oldData[num, 8] == "2")// gme mode is player vs Hard AI
                {
                   
                    GameData.Instance.mode = Int32.Parse(GameHistroyData.Instance.oldData[num, 7]);
                    GameData.Instance.difficulty = Int32.Parse(GameHistroyData.Instance.oldData[num, 8]);
                    GameData.Instance.playerID = Int32.Parse(GameHistroyData.Instance.oldData[num - 1, 2]);
                    GameData.Instance.playerName = GameHistroyData.Instance.oldData[num - 1, 3];
                    GameData.Instance.playerColour = GameHistroyData.Instance.oldData[num - 1, 4];
                    GameData.Instance.OpponentID = Int32.Parse(GameHistroyData.Instance.oldData[num, 2]);
                    GameData.Instance.OpponentName = GameHistroyData.Instance.oldData[num, 3];
                    GameData.Instance.OpponentColour = GameHistroyData.Instance.oldData[num, 4];
                    GameData.Instance.currentPlayer = GameData.Instance.OpponentID;
                    thegame.player1 = new HumanPlayer(1);
                    thegame.player2 = new HardAIPlayer();
                }



            }


           


            //Continue the game
           
         
            thegame.getMove();

        }

        public void newGame()
        {
            const string FILENAME = "GameHistory.txt";
            if (File.Exists(FILENAME))
                File.Delete(FILENAME);
            startGame();
        }

    }
}