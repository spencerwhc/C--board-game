using System;
using System.Linq;

namespace PlayerBoardGame
{
    public class Game
    {
        
        public int RowsCount { get; set; }
        public int ColumnsCount { get; set; }
        public Player player1 { get; set; }

        public Player player2 { get; set; }

        const int RED = 1, YELLOW = 2;
        public Game(int rows, int col)
        {
            RowsCount = rows;
            ColumnsCount = col;
            

        }

        public void selectPlayer()
        {

            Console.WriteLine("Welcome to this game, please choose the game mode");
            Console.WriteLine("1: Human vs Human");
            Console.WriteLine("2: Human vs AI");
            Console.WriteLine("3: AI vs AI");
            Console.Write("Select Players: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();


            switch (choice)
            {
                case 1:
                    player1 = new HumanPlayer(1);
                    player2 = new HumanPlayer(2);
         
                    break;

                case 2:

                    selectDifficulty();
                    player1 = new HumanPlayer(1);
                    break;

                case 3:
                    player1 = new EasyAIPlayer();
                    player2 = new HardAIPlayer();
                    break;


                case 4:
                    break;

            }



        }

        public void selectDifficulty() {
            Console.Clear();
            Console.WriteLine("Select AI difficulty ");
            Console.WriteLine("1: Easy mode ");
            Console.WriteLine("2: Hard mode ");
            int choice = Convert.ToInt32(Console.ReadLine());
            if (choice == 1)
            {
                player2 = new EasyAIPlayer();
            }
            else
            {
                player2 = new HardAIPlayer();

            }

        }

        public void getMove(Board b, IRule r)
        {
            
            bool gameLoop = true;
            bool inputLoop;
            Player currentPlayer = player1;
            while (gameLoop)
            {
               
                Console.Clear();

                b.DisplayPlayer(player1,player2);
                b.DisplayBoard();

                do
                {
                    inputLoop = true;
                    if (currentPlayer.makeMove(b, r, currentPlayer))
                         inputLoop = false; 

                } while (inputLoop);

                if (b.hasWinner(r, b, currentPlayer) == 1 || b.hasWinner(r, b, currentPlayer) == 2)
                {
                    Console.Clear();
                    b.DisplayBoard();
                    Console.Write("Player {0} has won!!", currentPlayer.Name);
                    Console.WriteLine("\nPress enter to quit.");
                    gameLoop = false;
                }
                else if (b.BoardIsFull(r))
                {
                    System.Console.Clear();
                    b.DisplayBoard();
                    Console.WriteLine("\nIt is a draw.");
                    Console.WriteLine("\nPress enter to quit.");
                    gameLoop = false;
                }
                else
                {
                    currentPlayer = currentPlayer == player1 ? player2 : player1;
                }
            }

            Console.ReadKey();


        }

    }

}

//int turn = RED;
//while (inputLoop)
//{

//    switch (turn)
//    {
//        case RED:
//            currentPlayer = player1;
//            player1.makeMove(b, r,player1);
//            turn = YELLOW;
//            break;
//        case YELLOW:
//            currentPlayer = player2;
//            player2.makeMove(b, r, player2);
//            turn = RED;
//            break;
//    }


//}