//using System;
//using System.IO;

//namespace vesrion2
//{

//    class Connect4 : BoardGame
//    {

//        private Player player1;
//        private Player player2;


//        public new void selectPlayer()
//        {

//            Console.WriteLine("Welcome to this game, please choose the game mode");
//            Console.WriteLine("1: Human vs Human");
//            Console.WriteLine("2: Human vs AI");
//            Console.WriteLine("3: AI vs AI");
//            Console.Write("Select Players: ");
//            int choice = Convert.ToInt32(Console.ReadLine());
//            Console.WriteLine();


//            switch (choice)
//            {
//                case 1:
//                    player1 = new HumanPlayer(1);
//                    player2 = new HumanPlayer(2);
//                    GameData.Instance.gameMode = 1;
//                    break;

//                case 2:

//                    selectDifficulty();
//                    player1 = new HumanPlayer(1);
//                    GameData.Instance.gameMode = 2;
//                    break;

//                case 3:
//                    break;

//            }



//        }
//        public new void selectDifficulty()
//        {
//            Console.Clear();
//            Console.WriteLine("Select AI difficulty ");
//            Console.WriteLine("1: Easy mode ");
//            Console.WriteLine("2: Hard mode ");
//            int choice = Convert.ToInt32(Console.ReadLine());
//            if (choice == 1)
//            {
//                player2 = new EasyAIPlayer();
//                GameData.Instance.difficulty = 1;
//            }
//            else
//            {
//                player2 = new HardAIPlayer();
//                GameData.Instance.difficulty = 2;

//            }

//        }

//        public new bool DropPiece(int[] move, Player player)
//        {


//            Board.Instance.cell[move[0], move[1]] = player.Colour;
//            Board.Instance.pieceCount++;
//            Piece.Instance.PiecePosition.Add(move);


//            return true;
//        }

//        public  void getMove(IRule r)
//        {

//            bool gameLoop = true;
//            bool inputLoop;
//            Player currentPlayer = player1;
//            int[] move = new int[2];
//            while (gameLoop)
//            {

//                //Console.Clear();

//                Board.Instance.DisplayPlayer(player1, player2);
//                Board.Instance.DisplayBoard();



//                do
//                {
//                    inputLoop = true;
//                    GameData.Instance.playerName = currentPlayer.Name;
//                    GameData.Instance.playerID = currentPlayer.ID;
//                    GameData.Instance.playerColour = currentPlayer.Colour;
//                    move = currentPlayer.makeMove(currentPlayer);
//                    if (move != null)
//                    {
//                        DropPiece(move, currentPlayer);

//                        //save the move for redo or undo later
//                        //GameData.Instance.redo[0] = currentPlayer.ID;
//                        //GameData.Instance.redo[1] = move[0];
//                        //GameData.Instance.redo[2] = move[1];
//                        saveHistory(GameData.Instance.inputNumber,move);
//                        inputLoop = false;
//                        GameData.Instance.inputNumber++;
//                    }


//                } while (inputLoop);

//                if (isGameFinish(r, currentPlayer))
//                {
//                    Console.Clear();
//                    Board.Instance.DisplayBoard();
//                    Console.Write("Player {0} has won!!", currentPlayer.Name);
//                    Console.WriteLine("\nPress enter to quit.");
//                    GameData.Instance.winner = currentPlayer.ID;
//                    gameLoop = false;
//                }
//                if (Board.Instance.BoardIsFull(r))
//                {
//                    System.Console.Clear();
//                    Board.Instance.DisplayBoard();
//                    Console.WriteLine("\nIt is a draw.");
//                    Console.WriteLine("\nPress enter to quit.");
//                    gameLoop = false;
//                }
//                else
//                {
//                    currentPlayer = currentPlayer == player1 ? player2 : player1;
//                }
//            }

//            Console.ReadKey();


//        }




//        public  void saveHistory(int inputNum,int[] move)
//        {
//            //Create stream writer
//            const string FILENAME = "GameHistory.txt";
//            StreamWriter writer = File.AppendText(FILENAME);

//            //save data info into the file
//            if (inputNum == 0)
//            {
//                writer.WriteLine("new");
//            }

//            writer.WriteLine($"{inputNum} , {Board.Instance.pieceCount}, {GameData.Instance.playerID}, {GameData.Instance.playerName}, {GameData.Instance.playerColour}, {move[0]}, {move[1]}");
//            writer.Close();
//        }

//        public new  void redoMove()
//        {
//            Console.WriteLine("Move been redo");
//        }

//        public new void undoMove()
//        {
//            //get the Piece pistition arrayList length
//            int count = Piece.Instance.PiecePosition.Count;

//            // get the last player move 
       
//            int[] lastPlayerMove = (int[])Piece.Instance.PiecePosition[count - 1];

//            ////get the current player last move 
//            int[] currentPlayerLastMove = (int[])Piece.Instance.PiecePosition[count-2];

//            //Undo the last player move and current player last move
//            Board.Instance.cell[lastPlayerMove[0], lastPlayerMove[1]] = " ";
//            Board.Instance.cell[currentPlayerLastMove[0], currentPlayerLastMove[1]] = " ";
//            Board.Instance.DisplayBoard();

//            //Delete the undoMove from Piece pistition arrayList
//            Piece.Instance.PiecePosition.RemoveRange(count-3, 2);

//            //int row = GameData.Instance.redo[1];
//            //int col = GameData.Instance.redo[2];
//            ////set position
//            //Board.Instance.cell[row, col] = " ";
//            ////mark position


//        }

//    }



//}
