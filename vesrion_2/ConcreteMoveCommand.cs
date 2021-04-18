using System;
namespace vesrion2
{
    //Command Pattern: Concrete Command
    class ConcreteMoveCommand : MoveCommand
    {
        private BoardGame thegame;
        public ConcreteMoveCommand(BoardGame game)
        {
            thegame = game;
        }

        public void Redo()
        {
            thegame.redoMove();
        }

        public void Undo()
        {
            thegame.undoMove();
        }
    }
}
