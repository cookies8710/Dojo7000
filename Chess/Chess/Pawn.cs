using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{

    public class Pawn : ChessPiece
    {
        public Pawn(Chess.Game.Player color)
        {
            base.player = color;
        }

        private bool enPassantRightLost;
        private bool enPassantLeftLost;

        public IEnumerable<Move> GetMoves()
        { 
            List<Move> moves = new List<Move>();
            Chess.Game.Player otherPlayer = (GetPlayer() == Chess.Game.Player.BLACK ? Chess.Game.Player.WHITE : Chess.Game.Player.BLACK);
            int enPassantYPos = (GetPlayer() == Chess.Game.Player.BLACK ? 4 : 5);

            // Moves on chessboard relative to player.
            int oneStep   = Field.Y + (GetPlayer() == Chess.Game.Player.BLACK ? -1 : +1);
            int twoSteps  = Field.Y + (GetPlayer() == Chess.Game.Player.BLACK ? -2 : +2);
            int leftStep  = Field.X + (GetPlayer() == Chess.Game.Player.BLACK ? +1 : -1);
            int rightStep = Field.X + (GetPlayer() == Chess.Game.Player.BLACK ? -1 : +1);
            
            // Initial move
            if(!moveHistory.Any() 
                && !Chessboard.Fields[Field.X][oneStep].isOccupied() 
                && !Chessboard.Fields[Field.X][twoSteps].isOccupied()) 
                    moves.Add(new Move(Field.X, twoSteps, Move.MoveType.MOVE));

            // 1 turn forward
            if(isInChessboard(oneStep))
            {                              
                // Move forward
                if(!Chessboard.Fields[Field.X][oneStep].isOccupied())
                    moves.Add(new Move(Field.X, oneStep, Move.MoveType.MOVE));
     
                // Attack right-forward
                if (isInChessboard(rightStep) 
                    && Chessboard.Fields[rightStep][oneStep].isOccupied(player: otherPlayer))
                        moves.Add(new Move(rightStep, oneStep, Move.MoveType.ATTACK));

                // Attack left-forward
                if (isInChessboard(leftStep) 
                    && Chessboard.Fields[leftStep][oneStep].isOccupied(player: otherPlayer))
                        moves.Add(new Move(leftStep, oneStep, Move.MoveType.ATTACK));
            }            

            // en passant
            if (Field.Y == enPassantYPos)
            {
                if (isInChessboard(rightStep)
                    && Chessboard.Fields[rightStep][Field.Y].isOccupied(ChessPiece.PieceType.PAWN, otherPlayer)
                    && Chessboard.Fields[rightStep][Field.Y].Piece.moveHistory.Count == 1
                    && !enPassantRightLost)                
                {
                    enPassantRightLost = true;
                    moves.Add(new Move(rightStep, Field.Y, Move.MoveType.EN_PASSANT));
                }

                if (isInChessboard(leftStep)
                    && Chessboard.Fields[leftStep][Field.Y].isOccupied(ChessPiece.PieceType.PAWN, otherPlayer)
                    && Chessboard.Fields[leftStep][Field.Y].Piece.moveHistory.Count == 1
                    && !enPassantLeftLost)
                {
                    enPassantLeftLost = true;
                    moves.Add(new Move(leftStep, Field.Y, Move.MoveType.EN_PASSANT));
                }

            }

            return moves;
        }
        private bool isInChessboard(int position)
        {
            return position >= 1 && position <= 8;
        }
    }
}
