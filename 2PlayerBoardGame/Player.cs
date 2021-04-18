using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace PlayerBoardGame
{
    public abstract class Player
    {

        public string Name { get; set; }
        public int ID { get; set; }
        public string Colour { get; set; }

        public Player() { }
        public void SetPlayerName(int playerNum)
        {
            Console.Clear();
            Console.WriteLine("What is player {0} Name", playerNum);
            Name = Console.ReadLine();
            ID = playerNum;

        }


        internal void setAsRed()
        {
            Colour = "X";


        }

        internal void setAsYellow()
        {
            Colour = "O";
        }

        public abstract bool makeMove(Board b, IRule r, Player player);

    }

    public class EasyAIPlayer : Player
    {

        public EasyAIPlayer()
        {
            Name = "Easy AI";
            setAsYellow();
        }

        public override bool makeMove(Board b, IRule r, Player player)
        {
            Boolean moveMade = false;
            Random random = new Random();
            Moves move = null;
            while (!moveMade)
            {
                int AI_Move = random.Next(7);
                move = r.isValidComputerEasyMove(b, AI_Move, player);
                    if (move != null)
                    {
                        b.DropPiece(move, player);
                        moveMade = true;
                    }
                    else
                    {
                        continue;
                    }
                
                }

            return moveMade;
        }



    }

    public class HardAIPlayer : Player
    {

        public HardAIPlayer()
        {
            Name = "Hard AI";
            setAsYellow();
        }

        public override bool makeMove(Board b, IRule r, Player player)
        {
            Boolean moveMade = false;

            var random = new Random();
            var moves = new List<Tuple<int, int>>();
            for (int i = 0; i < b.Columnscount; i++)
            {
                Moves move = r.isValidComputerEasyMove(b, i, player);
                b.DropPiece(move, player);
                if (move == null)
                    continue;
                moves.Add(Tuple.Create(i, r.isValidComputerHardMove(6, b, false, r, player)));
                b.UndoPiece(move, player);
            }
            int maxMoveScore = moves.Max(t => t.Item2); 
            var bestMoves = moves.Where(t => t.Item2 == maxMoveScore).ToList();
            
            Moves bestmove = r.isValidComputerEasyMove(b,bestMoves[random.Next(0, bestMoves.Count)].Item1, player);
            if (b.DropPiece(bestmove, player))
                moveMade = true;

            return moveMade;


        }
    }

    public class HumanPlayer : Player
        {

            public HumanPlayer(int playerNum)
            {
                if (playerNum == 1)
                {
                    SetPlayerName(playerNum);
                    setAsRed();
                }

                if (playerNum == 2)
                {
                    SetPlayerName(playerNum);
                    setAsYellow();
                }

            }

            public override bool makeMove(Board b, IRule r, Player player)
            {


                Boolean moveMade = false;
                int col_move;

                Console.Write("player {0} turn's ", Name);
                while (!moveMade)
                {
                    Console.Write("Make a Move from 1 to {0}:  ", b.Columnscount);
                    if (Int32.TryParse(Console.ReadLine(), out col_move))
                    {
                        if (1 <= col_move && col_move <= b.Columnscount)
                        {

                            if (r.isValidMovementRule(b, col_move, player))
                            {
                                Console.Clear();
                                b.DisplayBoard();
                                moveMade = true;
                            }
                            else
                            {
                                Console.WriteLine("\nThat column is full.");
                                continue;
                            }

                        }


                    }
                    else
                    {
                        Console.Clear();
                        b.DisplayBoard();
                        Console.WriteLine("\nPlease enter an valid integer.");
                        continue;
                    }
                }

                return moveMade;
            }

        }
    
}


    
