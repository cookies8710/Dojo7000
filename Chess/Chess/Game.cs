using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Game
    {
        public enum Player
        {
            BLACK, 
            WHITE
        }

        public Player PlayerOnTurn { get; private set; }

        public Chessboard Chessboard { get; private set; }

        public Game()
        {
            PlayerOnTurn = Player.WHITE;
        }



    }
}
