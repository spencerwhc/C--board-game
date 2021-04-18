using System;
using System.Linq;
using System.Collections;
namespace vesrion2
{
    //Singleton pattern
    public class Board
    {
        //Board Data Field
        public int RowsCount;
        public int ColumnsCount;
        public string[,] cell;
        public int pieceCount;
        public const string EMPTY = " ";

        private static Board instance = null;
        private Board() { }
        public static Board Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Board();
                }

                return instance;
            }
        }

        public void CreateBoard()
        {

            cell = new string[RowsCount, ColumnsCount];

            for (int y = 0; y < RowsCount; y++)
                for (int x = 0; x < ColumnsCount; x++)
                    cell[y, x] = EMPTY;

        }

        public void DisplayBoard()
        {

            var top =    $"┏{string.Join("", Enumerable.Repeat("────┬", ColumnsCount - 1))}────┐";
            var middle = $"├{string.Join("", Enumerable.Repeat("────┼", ColumnsCount - 1))}────┤";
            var bottom = $"└{string.Join("", Enumerable.Repeat("────┴", ColumnsCount - 1))}────┘";

            Console.WriteLine(top);
            for (int y = 0; y < RowsCount; y++)
            {
                for (int x = 0; x < ColumnsCount; x++)
                {

                    Console.Write("│  {0} ", cell[y, x]);

                }

                Console.WriteLine("│");
                if (y < RowsCount - 1)
                    Console.WriteLine(middle);
            }
            Console.WriteLine(bottom);
        }

        public bool BoardIsFull()
        {
            IRule rule = new Connect4Rule();
            RuleCommand gameOn = new ConcreteRuleCommand(rule);
            CommandInvoker ruleC = new CommandInvoker(gameOn);
            return pieceCount >= ruleC.getMaxPiece(RowsCount, ColumnsCount);

        }

        public void DisplayPlayer()
        {
            Console.Clear();
            Console.WriteLine("Player1:{0}   Player2:{1}", GameData.Instance.playerName, GameData.Instance.OpponentName
                );
        }


    }
}
