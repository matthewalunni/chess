using System;
using System.Collections.Generic;
using Chess.Model;

namespace Chess.Model
{
    public class AllowedMovement
    {        
        /// <summary>
        /// updates the game state and moves the pieces
        /// </summary>
        /// <param name="b">The state of the game.</param>
        /// <param name="m">The desired move.</param>
        /// <returns>The new state of the game.</returns>
        public static Board Move(Board b, Move m)
        {
            // create a copy of the board
            Board newBoard = new Board(b); 

            // determine if move is enpassant or castling
            bool enpassant = (newBoard.Grid[m.FromPosition.Number][m.FromPosition.Letter].PieceType == PieceType.Pawn && IsEnPassant(newBoard, m));
            bool castle = (newBoard.Grid[m.FromPosition.Number][m.FromPosition.Letter].PieceType == PieceType.King && Math.Abs(m.ToPosition.Letter - m.FromPosition.Letter) == 2);

            // update piece list, remove old position from piece list for moving player
            newBoard.Pieces[newBoard.Grid[m.FromPosition.Number][m.FromPosition.Letter].Player].Remove(m.FromPosition);

            // if move kills a piece directly, remove killed piece from killed player piece list
            if (newBoard.Grid[m.ToPosition.Number][m.ToPosition.Letter].PieceType != PieceType.None && newBoard.Grid[m.FromPosition.Number][m.FromPosition.Letter].Player != newBoard.Grid[m.ToPosition.Number][m.ToPosition.Letter].Player)
                newBoard.Pieces[newBoard.Grid[m.ToPosition.Number][m.ToPosition.Letter].Player].Remove(m.ToPosition);
            else if(enpassant) 
            {
                // if kill was through enpassant determine which direction and remove the killed pawn
                int step = (b.Grid[m.FromPosition.Number][m.FromPosition.Letter].Player == Player.White) ? -1 : 1;
                newBoard.Pieces[newBoard.Grid[m.ToPosition.Number + step][m.ToPosition.Letter].Player].Remove(new Position(m.ToPosition.Letter, m.ToPosition.Number + step));
            }
            else if (castle)
            {
                // if no kill but enpassant, update the rook position
                if (m.ToPosition.Letter == 6)
                {
                    newBoard.Pieces[newBoard.Grid[m.ToPosition.Number][m.ToPosition.Letter].Player].Remove(new Position(7, m.ToPosition.Number));
                    newBoard.Pieces[newBoard.Grid[m.ToPosition.Number][m.ToPosition.Letter].Player].Add(new Position(5, m.ToPosition.Number));
                }
                else
                {
                    newBoard.Pieces[newBoard.Grid[m.ToPosition.Number][m.ToPosition.Letter].Player].Remove(new Position(0, m.ToPosition.Number));
                    newBoard.Pieces[newBoard.Grid[m.ToPosition.Number][m.ToPosition.Letter].Player].Remove(new Position(3, m.ToPosition.Number));
                }
            }

            // add the new piece location to piece list
            newBoard.Pieces[newBoard.Grid[m.FromPosition.Number][m.FromPosition.Letter].Player].Add(m.ToPosition);

            // update board grid
            newBoard.Grid[m.ToPosition.Number][m.ToPosition.Letter] = new Piece(newBoard.Grid[m.FromPosition.Number][m.FromPosition.Letter]);
            newBoard.Grid[m.ToPosition.Number][m.ToPosition.Letter].LastPosition = m.FromPosition;
            newBoard.Grid[m.FromPosition.Number][m.FromPosition.Letter].PieceType = PieceType.None;
            if (enpassant)
            {
                // if kill was through enpassant determine which direction and remove the killed pawn
                int step = (b.Grid[m.FromPosition.Number][m.FromPosition.Letter].Player == Player.White) ? -1 : 1;
                newBoard.Grid[m.ToPosition.Number + step][m.ToPosition.Letter].PieceType = PieceType.None;
            }
            else if (castle)
            {
                // if no kill but enpassant, update the rook position
                if (m.ToPosition.Letter == 6)
                {
                    newBoard.Grid[m.ToPosition.Number][5] = new Piece(newBoard.Grid[m.ToPosition.Number][7]);
                    newBoard.Grid[m.ToPosition.Number][7].PieceType = PieceType.None;
                }
                else
                {
                    newBoard.Grid[m.ToPosition.Number][3] = new Piece(newBoard.Grid[m.ToPosition.Number][0]);
                    newBoard.Grid[m.ToPosition.Number][0].PieceType = PieceType.None;
                }
            }


            //promotion
            if (newBoard.Grid[m.ToPosition.Number][m.ToPosition.Letter].PieceType == PieceType.Pawn)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (newBoard.Grid[0][i].PieceType == PieceType.Pawn)
                        newBoard.Grid[0][i].PieceType = PieceType.Queen;
                    if (newBoard.Grid[7][i].PieceType == PieceType.Pawn)
                        newBoard.Grid[7][i].PieceType = PieceType.Queen;
                }
            }

            // update king position
            if (newBoard.Grid[m.ToPosition.Number][m.ToPosition.Letter].PieceType == PieceType.King)
            {
                newBoard.Kings[newBoard.Grid[m.ToPosition.Number][m.ToPosition.Letter].Player] = m.ToPosition;
            }

            // update last move 
            newBoard.LastMove[newBoard.Grid[m.ToPosition.Number][m.ToPosition.Letter].Player] = m.ToPosition;

            return newBoard;
        }

        /// <summary>
        /// tests if the player has moves
        /// </summary>
        /// <param name="b">the current board</param>
        /// <param name="player">current player</param>
        /// <returns>true if the player has moves</returns>
        public static bool HasMoves(Board b, Player player)
        {
            foreach(Position pos in b.Pieces[player])
                if (b.Grid[pos.Number][pos.Letter].PieceType != PieceType.None && 
                    b.Grid[pos.Number][pos.Letter].Player == player && 
                    GetAllowedMove(b, pos).Count > 0) return true;
            return false;
        }

        /// <summary>
        /// gets all player moves
        /// </summary>
        /// <param name="b">the board</param>
        /// <param name="player">current player</param>
        /// <returns>moves from one position to multiple</returns>
        public static Dictionary<Position, List<Position>> GetPlayerMoves(Board b, Player player)
        {
            var moves = new Dictionary<Position, List<Position>>();
            foreach (Position pos in b.Pieces[player])
            {
                if (b.Grid[pos.Number][pos.Letter].PieceType != PieceType.None)
                {
                    if (!moves.ContainsKey(pos)) moves[pos] = new List<Position>();
                    moves[pos].AddRange(GetAllowedMove(b, pos));
                }
            }
            return moves;
        }

        /// <summary>
        /// gets allowed moves from a specific position
        /// </summary>
        /// <param name="board">the board</param>
        /// <param name="pos">the position</param>
        /// <param name="verify_check">true</param>
        /// <returns>list of new positions the current piece can move to</returns>
        public static List<Position> GetAllowedMove(Board board, Position pos, bool verify_check = true)
        {
            Piece p = board.Grid[pos.Number][pos.Letter];
            if (p.PieceType == PieceType.None) return new List<Position>();

            switch (p.PieceType)
            {
                case PieceType.Pawn:
                    return Pawn(board, pos, verify_check);
                case PieceType.Rook:
                    return Rook(board, pos, verify_check);
                case PieceType.Knight:
                    return Knight(board, pos, verify_check);
                case PieceType.Bishop:
                    return Bishop(board, pos, verify_check);
                case PieceType.Queen:
                    return Queen(board, pos, verify_check);
                case PieceType.King:
                    return King(board, pos, verify_check);
                default:
                    return new List<Position>();
            }
        }

        /// <summary>
        /// slide along the path until you hit something
        /// </summary>
        private static List<Position> Slide(Board board, Player p, Position pos, Position step)
        {
            List<Position> moves = new List<Position>();

            //moves potentially 8 positions
            for (int i = 1; i < 8; i++)
            {
                Position moved = new Position(pos.Letter + i * step.Letter, pos.Number + i * step.Number);

                //if moved off grid
                if (moved.Letter < 0 || moved.Letter > 7 || moved.Number < 0 || moved.Number > 7)
                    break;

                //if there is a piece 
                if (board.Grid[moved.Number][moved.Letter].PieceType != PieceType.None)
                {
                    //if of the other team
                    if (board.Grid[moved.Number][moved.Letter].Player != p)
                        moves.Add(moved);
                    break;
                }
                moves.Add(moved);
            }
            return moves;
        }

        /// <summary>
        /// tells you whether or not the king is in check
        /// </summary>
        /// <param name="b">Board state</param>
        /// <param name="king">the currnet player</param>
        /// <returns>Is in check</returns>
        public static bool IsCheck(Board b, Player king)
        {
            if (b.Kings.Count == 0) return true;

            Position king_pos = b.Kings[king];
            if (king_pos.Number < 0 || king_pos.Letter < 0) return true;

            PieceType[] pieces = { PieceType.Pawn, PieceType.Rook, PieceType.Knight, PieceType.Bishop, PieceType.Queen, PieceType.King };

            Board tempBoard = new Board(b);

            for (int i = 0; i < 6; i++)
            {
                tempBoard.Grid[king_pos.Number][king_pos.Letter] = new Piece(pieces[i], king);
                List<Position> moves = GetAllowedMove(tempBoard, king_pos, false);
                foreach (var move in moves)
                {
                    if (b.Grid[move.Number][move.Letter].PieceType == pieces[i] &&
                        b.Grid[move.Number][move.Letter].Player != king)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// a list of all of the kings moves
        /// </summary>
        /// <param name="board">the board</param>
        /// <param name="pos">the current position</param>
        /// <param name="verify_check">true</param>
        /// <returns></returns>
        private static List<Position> King(Board board, Position pos, bool verify_check = true)
        {
            List<Position> moves = new List<Position>();

            Piece p = board.Grid[pos.Number][pos.Letter];
            if (p.PieceType == PieceType.None) return moves;

            // collect all relative moves possible
            List<Position> relative = new List<Position>();

            relative.Add(new Position(-1, 1));
            relative.Add(new Position(0, 1));
            relative.Add(new Position(1, 1));

            relative.Add(new Position(-1, 0));
            relative.Add(new Position(1, 0));

            relative.Add(new Position(-1, -1));
            relative.Add(new Position(0, -1));
            relative.Add(new Position(1, -1));

            // Iterate moves
            foreach (Position move in relative)
            {
                Position moved = new Position(move.Letter + pos.Letter, move.Number + pos.Number);

                // bound check
                if (moved.Letter < 0 || moved.Letter > 7 || moved.Number < 0 || moved.Number > 7)
                    continue;

                // if it's not blocked we can move
                if (board.Grid[moved.Number][moved.Letter].PieceType == PieceType.None || board.Grid[moved.Number][moved.Letter].Player != p.Player)
                {
                    if (verify_check) // make sure we don't put ourselves in check
                    {
                        Board newBoard = AllowedMovement.Move(board, new Move(pos, moved));
                        if(!IsCheck(newBoard, p.Player))
                        {
                            moves.Add(moved);
                        }
                    }
                    else
                    {
                        moves.Add(moved);
                    }
                }
            }
			
			// Castling
            /* A king can only castle if:
             * king has not moved
             * rook has not moved
             * king is not in check
             * king does not end up in check
             * king does not pass through any other peieces
             * king does not pass through any squares under attack
             * king knows secret handshake
             */
            if (verify_check)
            {
                if (!IsCheck(board, p.Player)
                    && p.LastPosition.Equals(new Position(-1,-1)))
                {
                    bool castleRight = allowCastle(board, p.Player, pos, true);
                    bool castleLeft = allowCastle(board, p.Player, pos, false);

                    if (castleRight)
                    {
                        moves.Add(new Position(6, pos.Number));
                    }
                    if (castleLeft)
                    {
                        moves.Add(new Position(2, pos.Number));
                    }
                }
            }

            return moves;
        }

        /// <summary>
        /// tests if we are allowed to castle or not
        /// </summary>
        /// <param name="board">the board</param>
        /// <param name="player">current player</param>
        /// <param name="pos">current position</param>
        /// <param name="isRight">if castling right or left</param>
        /// <returns>true if we are allowed to castle</returns>
        private static bool allowCastle(Board board, Player player, Position pos, bool isRight)
        {
            bool isValid = true;
            int rookPos;
            int kingDirection;
            if (isRight)
            {
                rookPos = 7;
                kingDirection = 1;
            }
            else
            {
                rookPos = 0;
                kingDirection = -1;
            }

            //Check for valid right castling
            // Is the peice at H,7 a rook owned by the player and has it moved
            if (board.Grid[pos.Number][rookPos].PieceType == PieceType.Rook &&
                board.Grid[pos.Number][rookPos].Player == player && board.Grid[pos.Number][rookPos].LastPosition.Equals(new Position(-1,-1)))
            {
                // Check that the adjacent two squares are empty
                for (int i = 0; i < 2; i++)
                {
                    if (board.Grid[pos.Number][pos.Letter + (i + 1) * kingDirection].PieceType != PieceType.None)
                    {
                        isValid = false;
                        break;
                    }
                }

                // Don't bother running secondary checks if the way isn't even clear
                if (isValid)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        // Move kings postion over i squares to check if king is passing over an attackable
                        // square
                        Board newBoard = AllowedMovement.Move(board, new Move(pos, new Position(pos.Letter + (i + 1) * kingDirection, pos.Number)));

                        // Attackable square is in between king and rook so
                        // its not possible to castle to the right rook
                        if (IsCheck(newBoard, player))
                        {
                            isValid = false;
                            break;
                        }
                    }
                }
            }
            else
            {
                isValid = false;
            }
            return isValid;
        }

        private static List<Position> Queen(Board board, Position pos, bool verify_check = true)
        {
            List<Position> moves = new List<Position>();

            Piece p = board.Grid[pos.Number][pos.Letter];
            if (p.PieceType == PieceType.None) return moves;

            // horizontal/vertical
            moves.AddRange(Slide(board, p.Player, pos, new Position(1, 0)));
            moves.AddRange(Slide(board, p.Player, pos, new Position(-1, 0)));
            moves.AddRange(Slide(board, p.Player, pos, new Position(0, 1)));
            moves.AddRange(Slide(board, p.Player, pos, new Position(0, -1)));

            // diagonals
            moves.AddRange(Slide(board, p.Player, pos, new Position(1, 1)));
            moves.AddRange(Slide(board, p.Player, pos, new Position(-1, -1)));
            moves.AddRange(Slide(board, p.Player, pos, new Position(-1, 1)));
            moves.AddRange(Slide(board, p.Player, pos, new Position(1, -1)));

            if (verify_check) // make sure each move doesn't put us in check
            {
                for (int i = moves.Count - 1; i >= 0; i--)
                {
                    Board newBoard = AllowedMovement.Move(board, new Move(pos, moves[i]));
                    if (IsCheck(newBoard, p.Player))
                    {
                        moves.RemoveAt(i);
                    }
                }
            }
            return moves;
        }

        private static List<Position> Bishop(Board board, Position pos, bool verify_check = true)
        {
            List<Position> moves = new List<Position>();

            Piece p = board.Grid[pos.Number][pos.Letter];
            if (p.PieceType == PieceType.None) return moves;

            // slide along diagonals to find available moves
            moves.AddRange(Slide(board, p.Player, pos, new Position(1, 1)));
            moves.AddRange(Slide(board, p.Player, pos, new Position(-1, -1)));
            moves.AddRange(Slide(board, p.Player, pos, new Position(-1, 1)));
            moves.AddRange(Slide(board, p.Player, pos, new Position(1, -1)));

            if (verify_check) // make sure each move doesn't put us in check
            {
                for (int i = moves.Count - 1; i >= 0; i--)
                {
                    Board newBoard = AllowedMovement.Move(board, new Move(pos, moves[i]));
                    if (IsCheck(newBoard, p.Player))
                    {
                        moves.RemoveAt(i);
                    }
                }
            }
            return moves;
        }

        private static List<Position> Knight(Board board, Position pos, bool verify_check = true)
        {
            List<Position> moves = new List<Position>();

            Piece p = board.Grid[pos.Number][pos.Letter];
            if (p.PieceType == PieceType.None) return moves;

            // collect all relative moves possible
            List<Position> relative = new List<Position>();

            relative.Add(new Position(2, 1));
            relative.Add(new Position(2, -1));

            relative.Add(new Position(-2, 1));
            relative.Add(new Position(-2, -1));

            relative.Add(new Position(1, 2));
            relative.Add(new Position(-1, 2));

            relative.Add(new Position(1, -2));
            relative.Add(new Position(-1, -2));

            // iterate moves
            foreach (Position move in relative)
            {
                Position moved = new Position(move.Letter + pos.Letter, move.Number + pos.Number);

                // bounds check
                if (moved.Letter < 0 || moved.Letter > 7 || moved.Number < 0 || moved.Number > 7)
                    continue;

                // if empty space or attacking
                if (board.Grid[moved.Number][moved.Letter].PieceType == PieceType.None ||
                    board.Grid[moved.Number][moved.Letter].Player != p.Player) 
                    moves.Add(moved);
            }

            if (verify_check)// make sure each move doesn't put us in check
            {
                for (int i = moves.Count - 1; i >= 0; i--)
                {
                    Board newBoard = AllowedMovement.Move(board, new Move(pos, moves[i]));
                    if (IsCheck(newBoard, p.Player))
                    {
                        moves.RemoveAt(i);
                    }
                }
            }
            return moves;
        }

        private static List<Position> Rook(Board board, Position pos, bool verify_check = true)
        {
            List<Position> moves = new List<Position>();

            Piece p = board.Grid[pos.Number][pos.Letter];
            if (p.PieceType == PieceType.None) return moves;

            // slide along vert/hor for possible moves
            moves.AddRange(Slide(board, p.Player, pos, new Position(1, 0)));
            moves.AddRange(Slide(board, p.Player, pos, new Position(-1, 0)));
            moves.AddRange(Slide(board, p.Player, pos, new Position(0, 1)));
            moves.AddRange(Slide(board, p.Player, pos, new Position(0, -1)));

            if (verify_check)// make sure each move doesn't put us in check
            {
                for (int i = moves.Count - 1; i >= 0; i--)
                {
                    Board newBoard = AllowedMovement.Move(board, new Move(pos, moves[i]));
                    if (IsCheck(newBoard, p.Player))
                    {
                        moves.RemoveAt(i);
                    }
                }
            }
            return moves;
        }

        private static List<Position> Pawn(Board board, Position pos, bool verify_check = true)
        {
            List<Position> moves = new List<Position>();

            Piece p = board.Grid[pos.Number][pos.Letter];
            if (p.PieceType == PieceType.None) return moves;

            // gather relative moves
            List<Position> relative = new List<Position>();
            relative.Add(new Position(-1, 1 * ((p.Player == Player.Black) ? -1 : 1)));
            relative.Add(new Position(0, 1 * ((p.Player == Player.Black) ? -1 : 1)));
            relative.Add(new Position(0, 2 * ((p.Player == Player.Black) ? -1 : 1)));
            relative.Add(new Position(1, 1 * ((p.Player == Player.Black) ? -1 : 1)));

            // iterate moves
            foreach (Position move in relative)
            {
                Position moved = new Position(move.Letter + pos.Letter, move.Number + pos.Number);

                // bounds check
                if (moved.Letter < 0 || moved.Letter > 7 || moved.Number < 0 || moved.Number > 7)
                    continue;

                // double forward move
                if (moved.Letter == pos.Letter && board.Grid[moved.Number][moved.Letter].PieceType == PieceType.None && Math.Abs(moved.Number - pos.Number) == 2)
                {
                    // check the first step
                    int step = -((moved.Number - pos.Number) / (Math.Abs(moved.Number - pos.Number)));
                    bool hasnt_moved = pos.Number == ((p.Player == Player.Black) ? 6 : 1);
                    if (board.Grid[moved.Number + step][moved.Letter].PieceType == PieceType.None && hasnt_moved)
                    {
                        moves.Add(moved);
                    }
                }
                // if it's not blocked we can move forward
                else if (moved.Letter == pos.Letter && board.Grid[moved.Number][moved.Letter].PieceType == PieceType.None)
                {
                    moves.Add(moved);
                }
                // angled attack
                else if (moved.Letter != pos.Letter && board.Grid[moved.Number][moved.Letter].PieceType != PieceType.None && board.Grid[moved.Number][moved.Letter].Player != p.Player)
                {
                    moves.Add(moved);
                }
                // en passant
                else if(IsEnPassant(board, new Move(pos,moved)))
                {
                    moves.Add(moved);
                }
            }

            if (verify_check)// make sure each move doesn't put us in check
            {
                for (int i = moves.Count - 1; i >= 0; i--)
                {
                    Board newBoard = AllowedMovement.Move(board, new Move(pos, moves[i]));
                    if (IsCheck(newBoard, p.Player))
                    {
                        moves.RemoveAt(i);
                    }
                }
            }
            return moves;
        }

        /// <summary>
        /// checks if the pawn can move enpassant  
        /// </summary>
        /// <param name="b">the board</param>
        /// <param name="m">the move that is wanted</param>
        /// <returns>true if en passant move is allowed</returns>
        public static bool IsEnPassant(Board b, Move m)
        {
            // step = where opposite pawn is
            int step = ((b.Grid[m.FromPosition.Number][m.FromPosition.Letter].Player == Player.White) ? -1 : 1);

            // true if
            // move is pawn
            // space is blank
            // move is diagonal
            // opposite pawn exists at step
            // the last move for opposite player was the pawn
            // the last move for opposite pawn was the double jump
            return (
                b.Grid[m.FromPosition.Number][m.FromPosition.Letter].PieceType == PieceType.Pawn &&
                b.Grid[m.ToPosition.Number][m.ToPosition.Letter].PieceType == PieceType.None &&
                m.ToPosition.Letter != m.FromPosition.Letter &&
                b.Grid[m.ToPosition.Number + step][m.ToPosition.Letter].PieceType == PieceType.Pawn &&
                b.LastMove[(b.Grid[m.FromPosition.Number][m.FromPosition.Letter].Player == Player.White) ? Player.Black : Player.White].Equals(new Position(m.ToPosition.Letter, m.ToPosition.Number + step)) &&
                Math.Abs(b.Grid[m.ToPosition.Number + step][m.ToPosition.Letter].LastPosition.Number - (m.ToPosition.Number + step)) == 2 //jumped from last position
                );
        }
    }
}
