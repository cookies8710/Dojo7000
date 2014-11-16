using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Chessboard
    {
        public Dictionary<int, Dictionary<int, Field>> Fields { get; private set; }

        public Chessboard()
        {            
            Fields = new Dictionary<int,Dictionary<int,Field>>();
            for (int col = 1; col <= 8; col++)
            {
                Fields.Add(col, new Dictionary<int, Field>());
                for (int row = 1; row <= 8; row++)
                {
                    Fields[col].Add(row, new Field());
                    Fields[col][row].X = col;
                    Fields[col][row].Y = row;
                }
            }

            for (var i = 1; i <= 8; i++)
            {
                Fields[i][2].Piece = new Pawn(Chess.Game.Player.WHITE);
                Fields[i][7].Piece = new Pawn(Chess.Game.Player.BLACK);
            }
        }

        public class Move
        {
            
        }
    }
}
