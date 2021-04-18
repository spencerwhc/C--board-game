using System;

namespace PlayerBoardGame
{
    class MainClass
    {
        public static void Main(string[] args)
        {

            BoardGameFactory gameFactory = new BoardGameFactory();

            BoardGame game = null;

            while(game == null) {
                Console.WriteLine("What type of game you like to play? (Connect4 / Checker) ");
                string theGame = Console.ReadLine();
            
                if (theGame == "Connect4" ||  theGame =="Checker")
                {
                    game = gameFactory.createBoardGame(theGame);

                    if(game != null)
                    {
                        startPlay(game);
                    }

            }
            else
            {
                Console.WriteLine("Please type correct game name");
                    continue;
            }

            }


             void startPlay(BoardGame newgame)
            {
                newgame.startPlay();
            }

        }
    }
}
