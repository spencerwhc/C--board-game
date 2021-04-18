using System;
namespace vesrion2
{
    interface IRule
    {

        int[] isValidMove(int colIndex);
        bool Iswinning(Player player);
        int setMaxPieceNum(int rowsNum, int colsNum);
        
        int[] isValidComputerHardMove(int colIndex, Player player);
    }

    public class Connect4Rule : IRule
    {
        public  int[] isValidComputerHardMove(int colIndex, Player player)
        {
            int[] validMove = new int[2];

            colIndex--;
     

            const string EMPTY = " ";

            for (int y = 0; y < Board.Instance.RowsCount; y++)
            {
                if ((y == Board.Instance.RowsCount - 1) || (Board.Instance.cell[y + 1, colIndex] != EMPTY))
                {

                    Board.Instance.cell[y, colIndex] = player.Colour;
                    if (Iswinning(player))
                    {
                        validMove[0] = y;
                        validMove[1] = colIndex;
                        Board.Instance.cell[y, colIndex] = EMPTY;
                        break;
                    }
                    
                   

                }
            }

            return validMove;
            
        }

        public  int[] isValidMove(int colIndex)
        {
            colIndex--;
            int[] validMove = new int[2];

            const string EMPTY = " ";
            if (Board.Instance.cell[0, colIndex] != EMPTY)
                return null;

            for (int y = 0; y < Board.Instance.RowsCount; y++)
            {
                if ((y == Board.Instance.RowsCount - 1) || (Board.Instance.cell[y + 1, colIndex] != EMPTY))
                {

                   
                    validMove[0] = y;
                    validMove[1] = colIndex;
                    break;

                }
            }
            return validMove;
        }

        public  bool Iswinning(Player player)
        {
       
                string piece = player.Colour;

                // Horizontal check:

                for (int y = 0; y <Board.Instance.RowsCount; y++)
                    for (int x = 0; x < 4; x++)
                        if (Board.Instance.cell[y, x] == piece && Board.Instance.cell[y, x + 1] == piece)
                            if (Board.Instance.cell[y, x + 2] == piece && Board.Instance.cell[y, x + 3] == piece)
                                return true;

                // Vertical check:

                for (int y = 0; y < 3; y++)
                    for (int x = 0; x < Board.Instance.ColumnsCount; x++)
                        if (Board.Instance.cell[y, x] == piece && Board.Instance.cell[y + 1, x] == piece)
                            if (Board.Instance.cell[y + 2, x] == piece && Board.Instance.cell[y + 3, x] == piece)
                                return true;

                // Diagonal check:

                for (int y = 0; y < 3; y++)
                {
                    for (int x = 0; x < Board.Instance.ColumnsCount; x++)
                    {

                        if (Board.Instance.cell[y, x] == piece)
                        {

                            // Diagonally left:
                            try
                            {
                                if (Board.Instance.cell[y + 1, x - 1] == piece)
                                {
                                    if (Board.Instance.cell[y + 2, x - 2] == piece)
                                        if (Board.Instance.cell[y + 3, x - 3] == piece)
                                            return true;
                                }
                            }
                            catch (IndexOutOfRangeException) { }

                            // Diagonally right:
                            try
                            {
                                if (Board.Instance.cell[y + 1, x + 1] == piece)
                                {
                                    if (Board.Instance.cell[y + 2, x + 2] == piece)
                                        if (Board.Instance.cell[y + 3, x + 3] == piece)
                                            return true;
                                }
                            }
                            catch (IndexOutOfRangeException) { }
                        }
                    }
                }

                return false;
            

        }

        public int setMaxPieceNum(int rowsNum, int colsNum)
        {
            return rowsNum * colsNum;
        }








    }
    

}
