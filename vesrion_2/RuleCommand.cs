using System;
namespace vesrion2
{
    //Command Pattern: Command
    interface RuleCommand
    {
        int[] getValidMove(int colIndex);
        bool hasWinner(Player player);
        int getMaxPiece(int rowsNum, int colsNum);
        int[] getComputerHardMove(int colIndex, Player player);
    }

    //Command Pattern: Concrete Command
    class ConcreteRuleCommand : RuleCommand
    {
        IRule therule;
        public ConcreteRuleCommand(IRule rule)
        {
            therule = rule;
        }

        public int[] getComputerHardMove(int colIndex, Player player)
        {
            return therule.isValidComputerHardMove(colIndex,player);
        }

        public int getMaxPiece(int rowsNum, int colsNum)
        {
            return therule.setMaxPieceNum(rowsNum,colsNum);
        }

        public int[] getValidMove(int colIndex)
        {
            return therule.isValidMove(colIndex);

        }

        public bool hasWinner(Player player)
        {
            return therule.Iswinning(player);
        }

        
    }

}
