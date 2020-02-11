using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using Chess.Configuration;

namespace Chess.Model
{
    public class Game
    {
        public Board Board { get; private set; }
        public Position Selection { get; private set; }
        public Player Turn { get; private set; }
        public TimeSpan WhiteTime = new TimeSpan(0);
        public TimeSpan BlackTime = new TimeSpan(0);
        public List<MoveHistory> Moves { get; set; }


        private readonly IBoardView _ui;
        private readonly int _numberOfPlayers;                      //0=> AI vs AI | 1=> AI vs Human | 2=> Human vs Human

        public Game(IBoardView ui, int numberOfPlayers = 1, bool setupBoard = true)
        {
            // callback setup
            Moves = new List<MoveHistory>();

            _ui = ui;
            _ui.SetStatus(true, "Generating...");


            // number of players = {0, 1, 2}
            _numberOfPlayers = numberOfPlayers;
            // white always starts
            Turn = Player.White;

            // create a new blank board unless setup is true
            Board = new Board();
            if (setupBoard) Board.SetInitialPlacement();

            // Refresh ui
            _ui.SetBoard(Board);
            _ui.SetStatus(false, "White's turn.");
        }

        /// <summary>
        /// checks if a position is valid
        /// </summary>
        /// <param name="pos">position to be tested</param>
        /// <returns>true if position is valid</returns>
        private bool IsValidPosition(Position pos)
        {
            return pos.Letter >= 0 && pos.Number >= 0;
        }

        /// <summary>
        /// finds the best move from the artificial intelligence class and makes it
        /// </summary>
        public void ArtificialIntelligenceMove()
        {
            while (ArtificialIntelligence.Running) Thread.Sleep(100);                           // If AI already running wait

            _ui.SetStatus(true, "Thinking...");                                                 // Refresh UI

            var move = ArtificialIntelligence.CalculateBestMove(Board, Turn);       // Calculate best moves based on selected algorithm
            if (!IsValidPosition(move.FromPosition)) return;
            var piece = Board.Grid[move.FromPosition.Number][move.FromPosition.Letter];

            if (IsValidPosition(move.ToPosition))
            {
                // Valid "To" position
                MakeMove(move);
            }
            else 
            {
                // This should never happen
                if (!ArtificialIntelligence.Stop) _ui.LogMove("Null Move\n", Turn, piece);      // Refresh UI
            }

            var checkmate = false;
            if (!ArtificialIntelligence.Stop)
            {
                SwitchPlayer();
                checkmate = DetectCheckmate();
            }

            ArtificialIntelligence.Running = false;

            if (!ArtificialIntelligence.Stop && _numberOfPlayers == 0 && !checkmate) new Thread(ArtificialIntelligenceMove).Start();
        }

        /// <summary>
        /// method to check if castling move
        /// </summary>
        /// <param name="pos"></param>
        /// <returns>true if castling move</returns>
        private bool IsCastling(Position pos)
        {
            /* Definition
             * Castling is a move in the game of chess involving a player's king 
             * and either of the player's original rooks. It is the only move in 
             * chess in which a player moves two pieces in the same move, and it 
             * is the only move aside from the knight's move where a piece can be 
             * said to "jump over" another ----- Wikipedia 
             * https://www.google.com/url?sa=t&rct=j&q=&esrc=s&source=web&cd=1&cad=rja&uact=8&ved=2ahUKEwiRo9qood_fAhXk5YMKHW-wArEQFjAAegQIBxAB&url=https%3A%2F%2Fen.wikipedia.org%2Fwiki%2FCastling&usg=AOvVaw2UWFdcMP4dAUHNAxXbPUgZ
             * */
            return Board.Grid[pos.Number][pos.Letter].PieceType == PieceType.King && Math.Abs(pos.Letter - Selection.Letter) == 2;
        }

        /// <summary>
        /// highlights possible moves, this only applies to the white player when the white AI is not playing
        /// </summary>
        /// <param name="pos">the starting position</param>
        /// <returns>list of positions to be highlighted gray</returns>
        public List<Position> Highlight(Position pos)
        {
            if (
                Board.Grid[Selection.Number][Selection.Letter].PieceType != PieceType.None      // Something was selected
                && Turn == Board.Grid[Selection.Number][Selection.Letter].Player                // The selected piece belongs to the current player
                && (_numberOfPlayers == 2 || Turn == Player.White)                                       // AI vs AI or it the white turn
                ) 
            {
                var moves = AllowedMovement.GetAllowedMove(Board, Selection);                        // Get all the legal moves
                foreach (var move in moves)
                {
                    if (!move.Equals(pos)) continue;                                            // Ignore empty moves

                    MakeMove(new Move(Selection, pos));                                         // Make the move and refhresh board

                    // If piece that was just moved is a king and it moved anything other than 1 square, it was 
                    // a castling move, so we need to move the rook
                    if (IsCastling(pos))
                    {
                        var row = (Turn == Player.White) ? 0 : 7;

                        AllowedMovement.Move(Board,
                            pos.Letter < 4
                                ? new Move(new Position(0, row), new Position(3, row))          // Swap with Black Rook
                                : new Move(new Position(7, row), new Position(5, row)));        // Swap with White Rook
                    }
                                
                    
                    SwitchPlayer();                                                             // Swith Player
                    if (DetectCheckmate()) return new List<Position>();

                    if (_numberOfPlayers == 1)
                    {
                        // AI vs Player only
                        new Thread(ArtificialIntelligenceMove).Start();                       // Start the AI thread
                    }
                    return new List<Position>();
                }
            }

            if (Board.Grid[pos.Number][pos.Letter].Player == Turn && (_numberOfPlayers == 2 || Turn == Player.White))
            {
                // Player vs Player Only
                var moves = AllowedMovement.GetAllowedMove(Board, pos);
                Selection = pos;
                return moves;
            }

            Selection = new Position();
            return new List<Position>();
        }


        /// <summary>
        /// makes a move and updates the pieces new coordinates
        /// </summary>
        /// <param name="m">the move that is made</param>
        private void MakeMove(Move m)
        {
            // start move log output
            //string move = (Turn == Player.White) ? "W" : "B";

            var history = new MoveHistory
            {
                Move = m,
                Piece = Board.Grid[m.FromPosition.Number][m.FromPosition.Letter]
            };

            string move = string.Empty;

            var piece = Board.Grid[m.FromPosition.Number][m.FromPosition.Letter];

            // from Position
            switch (m.FromPosition.Letter)
            {
                case 0: move += "A"; break;
                case 1: move += "B"; break;
                case 2: move += "C"; break;
                case 3: move += "D"; break;
                case 4: move += "E"; break;
                case 5: move += "F"; break;
                case 6: move += "G"; break;
                case 7: move += "H"; break;
            }

            // number
            move += (m.FromPosition.Number + 1).ToString();
            move += "-";

            // letter
            switch (m.ToPosition.Letter)
            {
                case 0: move += "A"; break;
                case 1: move += "B"; break;
                case 2: move += "C"; break;
                case 3: move += "D"; break;
                case 4: move += "E"; break;
                case 5: move += "F"; break;
                case 6: move += "G"; break;
                case 7: move += "H"; break;
            }

            // number
            move += (m.ToPosition.Number + 1).ToString();

            // To Position
            if (Board.Grid[m.ToPosition.Number][m.ToPosition.Letter].PieceType != PieceType.None || AllowedMovement.IsEnPassant(Board, m))
            {
                move += " + ";  // Is a Kill
                _ui.LogKill(Turn, Board.Grid[m.ToPosition.Number][m.ToPosition.Letter]);
            }

            // if that move put someone in check
            if (AllowedMovement.IsCheck(Board, (Turn == Player.White) ? Player.Black : Player.White))
            {
                move += " # ";  // Check
            }

            // update board / make actual move
            Board = AllowedMovement.Move(Board, m);

            // show log
            _ui.LogMove(move + "\n", Turn, piece);

            Moves.Add(history);
        }

        /// <summary>
        /// toggle who's turn it is
        /// </summary>
        private void SwitchPlayer()
        {
            Turn = (Turn == Player.White) ? Player.Black : Player.White;
            _ui.SetTurn(Turn);
            _ui.SetStatus(false, ((Turn == Player.White) ? "White" : "Black") + "'s Turn.");
            _ui.SetBoard(Board);
        }

        /// <summary>
        /// checks for checkmate, stalemate, draw
        /// </summary>
        /// <returns>true if checkmate, stalemate, draw</returns>
        public bool DetectCheckmate()
        {

            // In Here I can keep track of Draw, slatemate or checkmate
            // checks if there is only a white king or only a black king, essentially checks for win/loss/draw
            bool wkingonly = Board.Pieces[Player.White].Count == 1 && Board.Grid[Board.Pieces[Player.White][0].Number][Board.Pieces[Player.White][0].Letter].PieceType == PieceType.King;
            bool bkingonly = Board.Pieces[Player.Black].Count == 1 && Board.Grid[Board.Pieces[Player.Black][0].Number][Board.Pieces[Player.Black][0].Letter].PieceType == PieceType.King;

            //checks if the current player does not have legal moves
            if (!AllowedMovement.HasMoves(Board, Turn))
            {
                //if the player has no more moves, and is currently in check, that means the player is in checkmate and has lost
                if (AllowedMovement.IsCheck(Board, Turn))
                {
                    _ui.LogMove("Checkmate!\n", Turn, new Piece {PieceType = PieceType.None});
                    _ui.SetStatus(false, ((Turn == Player.White) ? "Black" : "White") + " wins!");
                }
                //otherwise, if the player has no more moves and is currently not in check, that means a stalemate has occurred
                else
                {
                    _ui.LogMove("Stalemate!\n", Turn, new Piece {PieceType = PieceType.None});
                }
                return true;
            }

            //if only one white king and only one black king, this means that a draw has occurred, this checks for that draw
            if (!wkingonly || !bkingonly) return false;


            _ui.LogMove("Draw.\n", Turn, new Piece { PieceType = PieceType.None });
            return true;
        }

        /// <summary>
        /// to XML file
        /// </summary>
        /// <returns>XML</returns>
        public string ToXml()
        {
            var configuration = new Configuration.Game
            {
                NumberOfPlayers = _numberOfPlayers,
                Turn = Turn.ToString(),
                WhiteTime = WhiteTime.Ticks,
                BlackTime = BlackTime.Ticks,
                Selection = new Selection
                {
                    Column = Selection.Letter,
                    Row = Selection.Number
                },
                Pieces = new Pieces(),
                Moves = new Moves(),
                LastMoves = new LastMoves()
            };


            configuration.Pieces.List = new List<Configuration.Piece>();
            configuration.Moves.List = new List<Configuration.Move>();
            configuration.LastMoves.List = new List<LastMove>();


            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    var boardPiece = Board.Grid[i][j];
                    if (boardPiece.PieceType == PieceType.None) continue;

                    var piece = new Configuration.Piece
                    {
                        Type = boardPiece.PieceType.ToString(),
                        LastPosition = new Selection
                        {
                            Column = boardPiece.LastPosition.Letter,
                            Row = boardPiece.LastPosition.Number
                        }
                    };
                    piece.Type = boardPiece.PieceType.ToString();

                    piece.Position = new Selection
                    {
                        Row = i,
                        Column = j
                    };

                    piece.Player = boardPiece.Player.ToString();

                    configuration.Pieces.List.Add(piece);
                }
            }

            foreach (var moveHistory in Moves)
            {
                if (moveHistory.Piece.PieceType == PieceType.None) continue;

                var move = new Configuration.Move
                {
                    FromPosition = new Selection
                    {
                        Column = moveHistory.Move.FromPosition.Letter,
                        Row = moveHistory.Move.FromPosition.Number
                    },
                    ToPosition = new Selection
                    {
                        Column = moveHistory.Move.ToPosition.Letter,
                        Row = moveHistory.Move.ToPosition.Number
                    },
                    Piece = new Configuration.Piece
                    {
                        Type = moveHistory.Piece.PieceType.ToString(),
                        LastPosition = new Selection
                        {
                            Column = moveHistory.Piece.LastPosition.Letter,
                            Row = moveHistory.Piece.LastPosition.Number
                        },
                        Player = moveHistory.Piece.Player.ToString()
                    }
                    
                };

                configuration.Moves.List.Add(move);
            }


            foreach (var player in Board.LastMove.Keys)
            {
                var lastMove = Board.LastMove[player];

                var move = new LastMove
                {
                    Player = player.ToString(),
                    Position = new Selection
                    {
                        Column = lastMove.Letter,
                        Row = lastMove.Number
                    }
                };

                configuration.LastMoves.List.Add(move);
            }


            return configuration.ToString();
        }

        /// <summary>
        /// takes in an xml file to a chess game
        /// </summary>
        /// <param name="ui">user interface</param>
        /// <param name="xml">xml to input</param>
        /// <returns>a game</returns>
        public static Game FromXml(IBoardView ui, string xml)
        {
            Configuration.Game configuration;

            var serializer = new XmlSerializer(typeof(Configuration.Game));

            using (TextReader reader = new StringReader(xml))
            {
                configuration = (Configuration.Game)serializer.Deserialize(reader);
            }

            Player turn;
            Enum.TryParse(configuration.Turn, out turn);

            var returnGame = new Game(ui, configuration.NumberOfPlayers, false)
            {
                BlackTime = TimeSpan.FromTicks(configuration.BlackTime),
                WhiteTime = TimeSpan.FromTicks(configuration.WhiteTime),
                Turn = turn
            };

            foreach (var configurationMove in configuration.Moves.List)
            {
                PieceType type;
                Enum.TryParse(configurationMove.Piece.Type, out type);

                Player player;
                Enum.TryParse(configurationMove.Piece.Player, out player);
                var piece = new Piece(type, player)
                {
                    LastPosition =
                        new Position(configurationMove.Piece.LastPosition.Column,
                            configurationMove.Piece.LastPosition.Row)
                };


                var history = new MoveHistory
                {
                    Move = new Move
                    {
                        FromPosition = new Position(configurationMove.FromPosition.Column, configurationMove.FromPosition.Row),
                        ToPosition = new Position(configurationMove.ToPosition.Column, configurationMove.ToPosition.Row)
                    },
                    Piece = piece
                };


                returnGame.Moves.Add(history);
            }

            returnGame.Board = new Board();
            returnGame.Board.Initalize();

            foreach (var configurationPiece in configuration.Pieces.List)
            {
                PieceType type;
                Enum.TryParse(configurationPiece.Type, out type);

                Player player;
                Enum.TryParse(configurationPiece.Player, out player);
                var piece = new Piece(type, player)
                {
                    LastPosition = new Position(configurationPiece.LastPosition.Column,
                            configurationPiece.LastPosition.Row)
                };

                returnGame.Board.PlacePiece(piece.PieceType, player, configurationPiece.Position.Column, configurationPiece.Position.Row);
            }

            returnGame.Board.LastMove = new Dictionary<Player, Position>();
            
            foreach (var configurationLastMove in configuration.LastMoves.List)
            {
                Player player;
                Enum.TryParse(configurationLastMove.Player, out player);

                if (returnGame.Board.LastMove.ContainsKey(player)) continue;

                var position = new Position(configurationLastMove.Position.Column,
                    configurationLastMove.Position.Row);
                returnGame.Board.LastMove.Add(player, position);
            }

            return returnGame;
        }

    }
}
