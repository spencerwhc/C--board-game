using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace PlayerBoardGame
{
    public class Moves
    {

        public int Row_position { get; set; }
        public int Col_postition { get; set; }
           
        public Moves(int rows, int cols) {
            Row_position = rows;
            Col_postition = cols;
        }
        
  
    }
}