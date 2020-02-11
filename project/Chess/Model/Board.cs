using System.Collections.Generic;


namespace Chess.Model
{
    public class Board
    {
        private static readonly int[] PieceWeights = { 1, 3, 4, 5, 7, 20 };

        public Piece[][] Grid { get; private set; }
        public Dictionary<Player, Position> Kings { get; private set; }
        public Dictionary<Player, List<Position>> Pieces { get; private set; }
        public Dictionary<Player, Position> LastMove { get; set; }
        public Dictionary<Player, List<Piece>> Kills { get; set; }

        public Board()
        {
            // init blank board grid
            Initalize();

            // Init last moves
            LastMove = new Dictionary<Player, Position>();
            LastMove[Player.Black] = new Position();
            LastMove[Player.White] = new Position();

            // init king positions
            Kings = new Dictionary<Player, Position>();

            // init piece position lists
            Pieces = new Dictionary<Player, List<Position>>
            {
                {Player.Black, new List<Position>()},
                {Player.White, new List<Position>()}
            };

            // init piece position lists
            Kills = new Dictionary<Player, List<Piece>>
            {
                {Player.Black, new List<Piece>()},
                {Player.White, new List<Piece>()}
            };
        }

        public Board(Board copy)
        {
            // init piece position lists
            Pieces = new Dictionary<Player, List<Position>>
            {
                {Player.Black, new List<Position>()},
                {Player.White, new List<Position>()}
            };

            // init board grid to copy locations
            Grid = new Piece[8][];
            for (int i = 0; i < 8; i++)
            {
                Grid[i] = new Piece[8];
                for (int j = 0; j < 8; j++)
                {
                    Grid[i][j] = new Piece(copy.Grid[i][j]);

                    // add piece location to list
                    if (Grid[i][j].PieceType != PieceType.None)
                        Pieces[Grid[i][j].Player].Add(new Position(j, i));
                }
            }

            // copy last known move
            LastMove = new Dictionary<Player, Position>();
            LastMove[Player.Black] = new Position(copy.LastMove[Player.Black]);
            LastMove[Player.White] = new Position(copy.LastMove[Player.White]);

            // copy king locations
            Kings = new Dictionary<Player, Position>();
            Kings[Player.Black] = new Position(copy.Kings[Player.Black]);
            Kings[Player.White] = new Position(copy.Kings[Player.White]);

            // Copy kills

            // Copy kills
            Kills = new Dictionary<Player, List<Piece>>();
            Kills[Player.Black] = new List<Piece>(copy.Kills[Player.Black]);
            Kills[Player.White] = new List<Piece>(copy.Kills[Player.White]);
            // Kills = new Dictionary<Player, List<Piece>>
            //{
            //    {Player.Black, new List<Piece>()},
            //    {Player.White, new List<Piece>()}
            //};
        }

        /// <summary>
        /// calculates the fitness for a board
        /// </summary>
        /// <param name="currentPlayer">the current player</param>
        /// <returns>the boards fitness</returns>
        public int Fitness(Player currentPlayer)
        {
            int fitness = 0;
            int[] blackPiecesCount = { 0, 0, 0, 0, 0, 0 };
            int[] whitePiecesCount = { 0, 0, 0, 0, 0, 0 };
            int blackMoves = 0; //number of moves available
            int whiteMoves = 0;

            // sum up the number of moves and pieces
            //for each of the black players pieces, and calculates the number of moves available
            //for the black player
            foreach (Position pos in Pieces[Player.Black])
            {
                blackMoves += AllowedMovement.GetAllowedMove(this, pos).Count;
                blackPiecesCount[(int)Grid[pos.Number][pos.Letter].PieceType]++;
            }

            // sum up the number of moves and pieces
            //for each of the black players pieces, and calculates the number of moves available
            //for the black player
            foreach (Position pos in Pieces[Player.White])
            {
                whiteMoves += AllowedMovement.GetAllowedMove(this, pos).Count;
                whitePiecesCount[(int)Grid[pos.Number][pos.Letter].PieceType]++;
            }

            // if viewing from black side
            if (currentPlayer == Player.Black)
            {
                // apply weighting to piece counts
                for (int i = 0; i < 6; i++)
                {
                    //the weight of the piece at index i times the difference between how many
                    //pieces of that type that you and your opponent have
                    //if white has more than black, fitness for black player will be negative 
                    fitness += PieceWeights[i] * (blackPiecesCount[i] - whitePiecesCount[i]);
                }

                // apply move value
                // this part accounts for not only the value of the pieces on the board, but also
                // accounts for how many moves that each player has available
                fitness += (int)(0.5 * (blackMoves - whiteMoves));
            }
            else
            {
                // apply weighting to piece counts
                for (int i = 0; i < 6; i++)
                {
                    fitness += PieceWeights[i] * (whitePiecesCount[i] - blackPiecesCount[i]);
                }

                // apply move value
                fitness += (int)(0.5 * (whiteMoves - blackMoves));
            }

            return fitness;
        }

        /// <summary>
        /// sets up the board for a new game
        /// </summary>
        public void SetInitialPlacement()
        {
            for (int i = 0; i < 8; i++)
            {
                PlacePiece(PieceType.Pawn, Player.White, i, 1);
                PlacePiece(PieceType.Pawn, Player.Black, i, 6);
            }

            PlacePiece(PieceType.Rook, Player.White, 0, 0);
            PlacePiece(PieceType.Rook, Player.White, 7, 0);
            PlacePiece(PieceType.Rook, Player.Black, 0, 7);
            PlacePiece(PieceType.Rook, Player.Black, 7, 7);

            PlacePiece(PieceType.Knight, Player.White, 1, 0);
            PlacePiece(PieceType.Knight, Player.White, 6, 0);
            PlacePiece(PieceType.Knight, Player.Black, 1, 7);
            PlacePiece(PieceType.Knight, Player.Black, 6, 7);

            PlacePiece(PieceType.Bishop, Player.White, 2, 0);
            PlacePiece(PieceType.Bishop, Player.White, 5, 0);
            PlacePiece(PieceType.Bishop, Player.Black, 2, 7);
            PlacePiece(PieceType.Bishop, Player.Black, 5, 7);

            PlacePiece(PieceType.King, Player.White, 4, 0);
            PlacePiece(PieceType.King, Player.Black, 4, 7);
            Kings[Player.White] = new Position(4, 0);
            Kings[Player.Black] = new Position(4, 7);
            PlacePiece(PieceType.Queen, Player.White, 3, 0);
            PlacePiece(PieceType.Queen, Player.Black, 3, 7);
        }

        /// <summary>
        /// makes a blank board and fills it with empty spaces
        /// </summary>
        public void Initalize()
        {
            Grid = new Piece[8][];
            for (int i = 0; i < 8; i++)
            {
                Grid[i] = new Piece[8];
                for (int j = 0; j < 8; j++)
                {
                    Grid[i][j] = new Piece(PieceType.None, Player.White);
                }
            }
        }

        /// <summary>
        /// places a piece onto the board 
        /// </summary>
        /// <param name="piece">piece type</param>
        /// <param name="player">the player</param>
        /// <param name="letter">the letter</param>
        /// <param name="number">the number</param>
        public void PlacePiece(PieceType piece, Player player, int letter, int number)
        {
            // set grid values
            Grid[number][letter].PieceType = piece;
            Grid[number][letter].Player = player;

            // add piece to list
            Pieces[player].Add(new Position(letter, number));

            // update king position
            if (piece == PieceType.King)
            {
                Kings[player] = new Position(letter, number);
            }
        }



    }
}
