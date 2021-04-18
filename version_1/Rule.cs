using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace PlayerBoardGame
{

    public interface IRule
    {
        bool isValidMovementRule(Board board, int colIndex, Player player);
        bool iswinningCriteria(Board b, Player player);
        int setMaxPieceNum(int rowsNum, int colsNum);
        Moves isValidComputerEasyMove(Board board, int colIndex, Player player);
        int isValidComputerHardMove(int depth, Board board, bool maximizingPlayer, IRule rule, Player player);
    }
    
    public class Connect4Rule : IRule
    {
        public int setMaxPieceNum(int rowsNum, int colsNum)
        {
            return rowsNum * colsNum;
        }

        public Moves isValidComputerEasyMove(Board board, int colIndex, Player player)
        {
        
            const string EMPTY = " ";
            Moves validMove = null;
            if (board.grid[0, colIndex] != EMPTY)
                return null;

            for (int y = 0; y < board.RowsCount; y++)
            {
                if ((y == board.RowsCount - 1) || (board.grid[y + 1, colIndex] != EMPTY))
                {
                    Moves move= new Moves(y, colIndex);

                    validMove = move;
                    break;

                }
            }
            return validMove;
        }

        public int isValidComputerHardMove(int depth, Board board, bool maximizingPlayer, IRule rule, Player player)
        {
            if (depth <= 0)
                return 0;
            var winner = board.hasWinner(rule,board,player);
            if (winner == 2)
                return depth;
            if (winner == 1)
                return -depth;
            if (board.BoardIsFull(rule))
                return 0;
            int bestValue = maximizingPlayer ? -1 : 1;

            for (int i = 0; i < board.Columnscount; i++)
            {
                Moves move = rule.isValidComputerEasyMove(board, i, player);
                if (move == null)
                    continue;
                board.DropPiece(move, player);
                int v = isValidComputerHardMove(depth - 1, board, !maximizingPlayer,rule,player);
                bestValue = maximizingPlayer ? Math.Max(bestValue, v) : Math.Min(bestValue, v);
                board.UndoPiece(move, player);
            }

            return bestValue;
        }

        public bool isValidMovementRule(Board board,int colIndex, Player player)
        {
            colIndex--;


            const string EMPTY = " ";
            if (board.grid[0, colIndex] != EMPTY)
                return false;

            for (int y = 0; y < board.RowsCount; y++)
            {
                if ((y == board.RowsCount - 1) || (board.grid[y + 1, colIndex] != EMPTY))
                {
                    Moves validMove = new Moves(y, colIndex);
                    board.DropPiece(validMove, player);
                    
                    break;
                    
                }
            }
            return true;


        }

        public bool iswinningCriteria (Board b, Player player)
        {
            string piece = player.Colour;
            
            // Horizontal check:

            for (int y = 0; y < b.RowsCount; y++)
                for (int x = 0; x < 4; x++)
                    if (b.grid[y, x] == piece && b.grid[y, x + 1] == piece)
                        if (b.grid[y, x + 2] == piece && b.grid[y, x + 3] == piece)
                            return true;

            // Vertical check:

            for (int y = 0; y < 3; y++)
                for (int x = 0; x < b.Columnscount; x++)
                    if (b.grid[y, x] == piece && b.grid[y + 1, x] == piece)
                        if (b.grid[y + 2, x] == piece && b.grid[y + 3, x] == piece)
                            return true;

            // Diagonal check:

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < b.Columnscount; x++)
                {

                    if (b.grid[y, x] == piece)
                    {

                        // Diagonally left:
                        try
                        {
                            if (b.grid[y + 1, x - 1] == piece)
                            {
                                if (b.grid[y + 2, x - 2] == piece)
                                    if (b.grid[y + 3, x - 3] == piece)
                                        return true;
                            }
                        }
                        catch (IndexOutOfRangeException) { }

                        // Diagonally right:
                        try
                        {
                            if (b.grid[y + 1, x + 1] == piece)
                            {
                                if (b.grid[y + 2, x + 2] == piece)
                                    if (b.grid[y + 3, x + 3] == piece)
                                        return true;
                            }
                        }
                        catch (IndexOutOfRangeException) { }
                    }
                }
            }

            return false;
        }
    }

}

