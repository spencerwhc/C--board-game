using System;
namespace PlayerBoardGame
{
    public class Checker : BoardGame
    {
        public Checker()
        {
            setName("Checker");
            setRowCounts(8);
            setColumnCounts(8);
        }

        public override bool endPlay()
        {
            throw new NotImplementedException();
        }

        public override bool printWinner()
        {
            throw new NotImplementedException();
        }
    }
}
