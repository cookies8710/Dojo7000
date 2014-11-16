using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Move
    {
        public Tuple<int, int> Position { get; private set; }

        public MoveType Type { get; private set; }

        public enum MoveType 
        { 
            ATTACK, MOVE, EN_PASSANT, CASTLING
        }

        public Move(int x, int y, MoveType type)
        {
            Position = new Tuple<int, int>(x, y);
            Type = type;
        }
    }
}
