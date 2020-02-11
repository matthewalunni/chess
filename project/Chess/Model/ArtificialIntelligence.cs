using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chess.Model
{
    public class ArtificialIntelligence
    {
        public static int Depth = 4;
        public static bool Running;
        public static bool Stop;
        private static Player maximizer = Player.Black;

        /// <summary>
        /// this method calculates the best move available 
        /// </summary>
        /// <param name="board"> the current board</param>
        /// <param name="turn">the player whose turn it is</param>
        /// <returns>the best move</returns>
        public static Move CalculateBestMove(Board board, Player turn)
        {
            Running = true;     // we've started running
            Stop = false;       // no interrupt command sent
            maximizer = turn;   // who is maximizing

            // gather all possible moves from current player
            Dictionary<Position, List<Position>> moves = AllowedMovement.GetPlayerMoves(board, turn);

            // because we're threading safely store best result from each thread
            int[] bestResults = new int[moves.Count];
            Move[] bestMoves = new Move[moves.Count];


            //for each that occurs in all of the threads at the same time
            //for all of the current players moves, it puts the elements of the list "moves", into the element
            // movelist, which is a list of positions

            Parallel.ForEach(moves, (movelist,state,index) =>
            {
                //if someone chooses to stop
                if (Stop) // interupt
                {
                    state.Stop();
                    return;
                }
                
                // initialize thread best
                bestResults[index] = int.MinValue;
                bestMoves[index] = new Move(new Position(-1, -1), new Position(-1, -1));

                // for each move for the current piece(thread)
                foreach (Position move in movelist.Value)
                {
                    if (Stop) // interrupt
                    {
                        state.Stop();
                        return;
                    }

                    // begin
                    Board newBoard = AllowedMovement.Move(board, new Move(movelist.Key, move));
                    int result = 0;

                    //for each move for the current player, it calculates the moves fitness and returns the best possible outcome for the 
                    //current player, if it is better than the current best outcome, then it will replace it
                    result = Minimax(newBoard, (turn == Player.White) ? Player.Black : Player.White, 1, Int32.MinValue, Int32.MaxValue);

                    // if result is better or best hasn't been set yet
                    //the replacement
                    if (bestResults[index] < result || (bestMoves[index].ToPosition.Equals(new Position(-1, -1)) && bestResults[index] == int.MinValue))
                    {
                        bestResults[index] = result;
                        bestMoves[index].FromPosition = movelist.Key;
                        bestMoves[index].ToPosition = move;
                    }
                }
            });

            // interupted
            if (Stop)
                return new Move(new Position(-1, -1), new Position(-1, -1)); 

            // find the best of the thread results
            int best = int.MinValue;
            Move aMove = new Move(new Position(-1, -1), new Position(-1, -1));
            for(int i = 0; i < bestMoves.Length; i++)
            {
                if (best < bestResults[i] || (aMove.ToPosition.Equals(new Position(-1,-1)) && !bestMoves[i].ToPosition.Equals(new Position(-1,-1))))
                {
                    best = bestResults[i];
                    aMove = bestMoves[i];
                }
            }

            //returns the best move from all of the threads
            return aMove;
        }

       /// <summary>
       /// minimax algorithm, explained in writeup
       /// </summary>
       /// <param name="board">the board</param>
       /// <param name="turn">the current player</param>
       /// <param name="depth">the depth</param>
       /// <param name="alpha">the alpha value</param>
       /// <param name="beta">the beta value</param>
       /// <returns></returns>
        private static int Minimax(Board board, Player turn, int depth, int alpha, int beta)
        {
            // base case, at maximum depth return board fitness
            if (depth >= Depth) return board.Fitness(maximizer);

            List<Board> boards = new List<Board>();

            // get available moves / board states from moves for the current player
            foreach (Position position in board.Pieces[turn])
            {
                if (Stop) return -1; // interupts
                //for each board piece, create a list of all of the allowed moves
                List<Position> moves = AllowedMovement.GetAllowedMove(board, position);

                //for each move, create a new board and add it to a list of new boards
                foreach (Position move in moves)
                {
                    if (Stop) return -1; // interupts
                    Board newBoard = AllowedMovement.Move(board, new Move(position, move));
                    boards.Add(newBoard);
                }
            }

            //the alpha beta pruning part of the algorithm
            int num = alpha, best = beta;
            if (turn != maximizer) // minimize
            {
                foreach (Board currentBoard in boards)
                {
                    if (Stop) return -1; // interupt
                    best = Math.Min(best, Minimax(currentBoard, (turn == Player.White) ? Player.Black : Player.White, depth + 1, num, best));
                    //above increases the depth each recursion

                    if (num >= best)
                        return num;
                }
                return best;
            }
            else // maximize
            {
                foreach (Board currentBoard in boards)
                {
                    if (Stop) return -1; // interupt
                    num = Math.Max(num, Minimax(currentBoard, (turn == Player.White) ? Player.Black : Player.White, depth + 1, num, best));
                    //above increases the depth each recursion

                    if (num >= best)
                        return best;
                }
                return num;
            }
        }
    }
}
