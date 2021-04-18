using System;
namespace vesrion2
{
    //Command Pattern: Invoker
    class CommandInvoker
    {
        GameCommand gameCommand;
        MoveCommand moveCommand;
        RuleCommand ruleCommand;

        public CommandInvoker(GameCommand newGameCommand)
        {
            gameCommand = newGameCommand;

        }
        public CommandInvoker(MoveCommand newMoveCommand)
        {

            moveCommand = newMoveCommand;
        }

        public CommandInvoker(RuleCommand newRuleCommand)
        {

            ruleCommand = newRuleCommand;
        }


        public void chooseNewGame()
        {
            gameCommand.newGame();
        }

        public void chooseLoadGame()
        {
            gameCommand.loadGame();
        }

        public void chooseExitGame()
        {
            gameCommand.exitGame();
        }

        public void chooseUndoMove()
        {
            moveCommand.Undo();
        }

        public void chooseRedoMove()
        {
            moveCommand.Redo();
        }

        public int[] getComputerHardMove(int colIndex, Player player)
        {
            return ruleCommand.getComputerHardMove(colIndex,player);
        }

        public int getMaxPiece(int rowsNum, int colsNum)
        {
            return ruleCommand.getMaxPiece(rowsNum, colsNum);
        }

        public int[] getValidMove(int colIndex)
        {
            return ruleCommand.getValidMove(colIndex);

        }

        public bool hasWinner(Player player)
        {
            return ruleCommand.hasWinner(player);
        }
    }
}
