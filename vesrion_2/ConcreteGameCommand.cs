using System;
namespace vesrion2
{
    //Command Pattern: Concrete Command
    class ConcreteGameCommand :GameCommand
    {
        private Game thegame;
        public ConcreteGameCommand(Game game)
        {
            thegame = game;
        }

        public void excute()
        {
            thegame.startGame();
        }

        public void exitGame()
        {
            thegame.exitGame();
        }

        public void loadGame()
        {
            thegame.loadGame();
        }

        public void newGame()
        {
            thegame.newGame();
        }
    }
}
