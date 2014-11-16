using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class ChessPiece
    {
        public IList<Move> moveHistory { get; private set; }

        protected Chessboard Chessboard { get; set; }

        protected Field Field { get; set; }

        private PieceType type;

        protected Chess.Game.Player player;

        public enum PieceType
        {
            PAWN, KNIGHT, BISHOP, ROOK, QUEEN, KING
        }

        public PieceType GetType()
        {
            return type;
        }

        public Chess.Game.Player GetPlayer()
        {
            return player;
        }

        public override string ToString()
        {
            return type.ToString().Substring(0, 2);
        }

        public abstract IEnumerable<Move> GetMoves();

       /* protected bool IsValidMove(Move move)
        { 
            if(move.Position.Item1 <= 0 || move.Position.Item1 >= 8 ||
               move.Position.Item1 <= 0 || move.Position.Item1 >= 8)
        }*/
    }
}
