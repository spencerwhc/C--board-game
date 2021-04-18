using System;

namespace PlayerBoardGame
{
    public class BoardGameFactory
    {

		public BoardGame createBoardGame(String newBoardGame)
		{


			if (newBoardGame == "Connect4")
			{

				return new ConnectFour();

			}
			else

			if (newBoardGame == "Checker")
			{

				return new Checker();

			}

			else return null;

		}


		

	}
}