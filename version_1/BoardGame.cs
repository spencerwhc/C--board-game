using System;

namespace PlayerBoardGame
{
    public abstract class BoardGame 
    {
        
       const int RED = 1, YELLOW = 2;

        private string Name;
        private int ColumnCounts;
        private int RowCounts;

       public string getName() { return Name; }

       public void setName(string newName) { Name = newName; }

       public int getColumnCounts() { return ColumnCounts; }

       public void setColumnCounts(int newCols) { ColumnCounts = newCols; }

       public int getRowCounts() { return RowCounts; }

       public void setRowCounts(int newRows) { RowCounts = newRows; }

    

        public void startPlay()
        {
            string name = getName();
            int ColumnCounts = getColumnCounts();
            int RowCounts = getRowCounts();
            Board board = CreateBoard(ColumnCounts, RowCounts);
            Game newgame = initialiseGame(ColumnCounts, RowCounts);

            IRule rule = new Connect4Rule();
            

            newgame.selectPlayer();
            board.CreateBoard();
            newgame.getMove(board, rule);
            board.DisplayBoard();
        }

        public virtual Board CreateBoard(int colsNum, int rowNum)
        {
           return new Board(rowNum, colsNum);
           
          
        }

        public virtual Game initialiseGame(int colsNum, int rowNum) {
            return new Game(rowNum, colsNum);
            

        }


        //public virtual void makePlay(Game game, Board board,Piece piece, Moves move) {


        //}

        public abstract bool endPlay();

        public abstract bool printWinner();


    }
}