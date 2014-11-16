using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Field
    {
        public enum BackgroundColorEnum
        {
            BLACK,
            WHITE
        }

        public BackgroundColorEnum Background { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public ChessPiece Piece { get; set; }

        public bool isOccupied(ChessPiece.PieceType? type = null, Chess.Game.Player? player = null)
        {
            return Piece != null && (type == null || Piece.GetType() == type) && (player == null || Piece.GetPlayer() == player);
        }

    }
}
