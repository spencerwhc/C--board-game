using System;
using System.Linq;
using System.Collections;


namespace PlayerBoardGame
{
    public class Board
    {
        public int RowsCount { get; set; }
        public int Columnscount { get; set; }
        int pieceCount;
        public string[,] grid;
        const string EMPTY = " ";


        public Board(int rows, int cols)
        {
            RowsCount = rows;
            Columnscount = cols;


        }

        public void CreateBoard()
        {

            grid = new string[RowsCount, Columnscount];

            for (int y = 0; y < RowsCount; y++)
                for (int x = 0; x < Columnscount; x++)
                    grid[y, x] = EMPTY;

        }

        public void DisplayBoard()
        {
  
            var top = $"┏{string.Join("", Enumerable.Repeat("────┬", Columnscount - 1))}────┐";
            var middle = $"├{string.Join("", Enumerable.Repeat("────┼", Columnscount - 1))}────┤";
            var bottom = $"└{string.Join("", Enumerable.Repeat("────┴", Columnscount - 1))}────┘";

            Console.WriteLine(top);
            for (int y = 0; y < RowsCount; y++)
            {
                for (int x = 0; x < Columnscount; x++)
                {

                    Console.Write("│  {0} ", grid[y, x]);

                }

                Console.WriteLine("│");
                if (y < RowsCount - 1)
                    Console.WriteLine(middle);
            }
            Console.WriteLine(bottom);
        }

        public bool DropPiece(Moves move, Player player)
        {

      
            grid[move.Row_position,move.Col_postition] = player.Colour;



            Piece drop_Piece = new Piece(move.Row_position, move.Col_postition, player.Colour);
            pieceCount++;


            return true;
        }


        public bool BoardIsFull( IRule r)
        {
            return pieceCount >= r.setMaxPieceNum(RowsCount, Columnscount);

        }

        public void DisplayPlayer(Player player1, Player player2)
        {
            Console.Clear();
            Console.WriteLine("Player1:{0}   Player2:{1}", player1.Name,player2.Name
                );
        }

        public void UndoPiece(Moves move, Player player)
        {
            grid[move.Row_position, move.Col_postition] = " ";
            pieceCount--;
        }

        public ArrayList CopyBoard()
        {
            throw new NotImplementedException();
        }

        public int hasWinner(IRule r,Board b, Player p)
        {
 
            int winner;
            if(r.iswinningCriteria(b, p))
            {
                winner = p.ID;
                return winner;
            }
            else
            {
                winner = 0;
                return winner;
            }
        }

    }
}
