using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace PlayerBoardGame
{
    public class ConnectFour : BoardGame
    {
        const int RED = 1, YELLOW = 2;
        public ConnectFour()
        {
            setName("Connect Four");
            setRowCounts(6);
            setColumnCounts(7);

        }

        new public void startPlay()
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

        }

        public override bool endPlay()
        {
            throw new NotImplementedException();
        }

 
        //public override void makePlay(Game game, Board board, Piece piece, Moves move)
        //{

        //    throw new NotImplementedException();
        //}

        public override bool printWinner()
        {
            throw new NotImplementedException();
        }

       

       

    }
}
