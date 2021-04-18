using System;
namespace vesrion2
{
    //Command Pattern: Command
    public interface GameCommand
    {
         void excute();
         void newGame();
         void loadGame();
         void exitGame();
        
    }
}
