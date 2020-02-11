using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using Chess.Model;

namespace Chess
{
    public partial class MainForm : Form, IBoardView
    {
        readonly Color _black = Color.FromArgb(139, 69, 19);
        readonly Color _white = Color.FromArgb(255, 228, 181);
        readonly Color _blackSelected = Color.Gray;
        readonly Color _whiteSelected = Color.LightGray;
        readonly ImageProvider _graphics = new ImageProvider();

        bool _aigame;
        bool _checkmate;
        PictureBox[][] _board;
        Label[] _rowHeaders;
        Label[] _colHeaders;
        ToolStripMenuItem _selectedDifficulty; // selected difficulty
        Game _game;

        public MainForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Stop all current activity / games and reset everything.
        /// </summary>
        private void Stop()
        {
            SetStatus(false, "Choose New Game or Manual Board.");

            // stop the ai and reset chess
            ArtificialIntelligence.Stop = true;

            if (_game != null)
            {
                _game.WhiteTime = new TimeSpan(0);
                _game.BlackTime = new TimeSpan(0);
                lblWhiteTime.Text = _game.WhiteTime.ToString("c");
                lblBlackTime.Text = _game.BlackTime.ToString("c");
            }

            _game = null;

            // reset turn indicator
            SetTurn(Player.White);

            // reset timers
            timerWhitePlayer.Stop();
            timerBlackPlayer.Stop();

            // clear the board ui and log
            SetBoard(new Board());
            listViewLogWhite.Items.Clear();
            listViewLogBlack.Items.Clear();

            // reset board status vars
            _checkmate = false;
            _aigame = false;

            // reset the menu
            endCurrentGameToolStripMenuItem.Enabled = false;
            endCurrentGameToolStripMenuItem.Enabled = false;
        }

        /// <summary>
        /// Set up a new game for the specified number of players.
        /// </summary>
        private void NewGame(int nPlayers)
        {
            // clean up all of the things first
            Stop();

            // create new game for number of players
            _aigame = (nPlayers == 0);
            _game = new Game(this, nPlayers, true);
            
            // show turn status
            SetTurn(Player.White);
            SetStatus(false, "White's turn");

            // reset timers
            _game.WhiteTime = new TimeSpan(0);
            _game.BlackTime = new TimeSpan(0);
            lblWhiteTime.Text = _game.WhiteTime.ToString("c");
            lblBlackTime.Text = _game.BlackTime.ToString("c");


            // start the game
            SetStatus(false, "White's Turn");
            if (_aigame)
            {
                new Thread(_game.ArtificialIntelligenceMove).Start();
            }
            timerWhitePlayer.Start();

            // allow stopping the game
            endCurrentGameToolStripMenuItem.Enabled = true;
        }


        /// <summary>
        /// begins a turn, resets timers, etc
        /// </summary>
        /// <param name="p">the player whose turn it is</param>
        public void SetTurn(Player p)
        {
            // if a thread called this, invoke recursion
            if (InvokeRequired)
            {
                Invoke(new Action(() => SetTurn(p)));
                return;
            }

            // update the turn indicator
            var currentTurn= _game != null ? _game.Turn : Player.White;

            panelWhiteTurn.Visible = (currentTurn != Player.Black);
            panelBlackTurn.Visible = (currentTurn == Player.Black);

            // toggle whos timer is running
            if (p == Player.White)
            {
                timerBlackPlayer.Stop();
                timerWhitePlayer.Start();
            }
            else
            {
                timerWhitePlayer.Stop();
                timerBlackPlayer.Start();
            }

            // if game over just stop timers
            if (_game != null && (_checkmate || _game.DetectCheckmate()))
            {
                timerWhitePlayer.Stop();
                timerBlackPlayer.Stop();
            }
        }

        /// <summary>
        /// Records a kill to the kill log
        /// </summary>
        /// <param name="turn"></param>
        /// <param name="piece"></param>
        public void LogKill(Player turn, Piece piece)
        {
            // if a thread called this, invoke recursion
            if (InvokeRequired)
            {
                Invoke(new Action(() => LogKill(turn, piece)));
                return;
            }

            _game.Board.Kills[turn].Add(piece);

            if (turn == Player.Black)
            {
                listViewBlackKills.Items.Add(new ListViewItem
                {
                    ImageKey = piece.PieceType.ToString(),
                    Text = piece.PieceType.ToString()
                });
            }
            else
            {
                listViewWhiteKills.Items.Add(new ListViewItem
                {
                    ImageKey = piece.PieceType.ToString(),
                    Text = piece.PieceType.ToString()
                });
            }

        }

        /// <summary>
        /// renders the board on the ui
        /// </summary>
        /// <param name="board">the board to be displayed</param>
        public void SetBoard(Board board)
        {
            // if a thread called this, invoke recursion
            if (InvokeRequired)
            {
                Invoke(new Action(() => SetBoard(board)));
                return;
            }

            // update all tiles on board
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    PlacePiece(board.Grid[i][j].PieceType, board.Grid[i][j].Player, j, i);
                }
            }
        }
        /// <summary>
        /// resizes the board
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResizeBoard(object sender, EventArgs e)
        {
            // make sure board is initialized
            if (_board == null || _board[0] == null || _board[0][0] == null) return;

            int headerSize = 20;
            int availableWidth = (splitView.Panel1.Width - headerSize);
            int availableHeight = (splitView.Panel1.Height - headerSize);

            // smallest of height or width
            int cellSize = Math.Min(availableWidth, availableHeight);

            // 8 x 8 grid
            int width = cellSize / 8;   
            int height = cellSize / 8;



            // center padding/spacing like { margin: auto auto; }
            int left = (availableWidth - cellSize) / 2;
            int top = ((availableHeight - cellSize) / 2) + headerSize - 5;

            // position the row headers
            for (int i = 0; i < 8; i++)
            {
                _rowHeaders[i].Left = left;
                _rowHeaders[i].Top = cellSize - (i + 1) * height + top;
                _rowHeaders[i].Width = headerSize;
                _rowHeaders[i].Height = height;
            }


            left = ((availableWidth - cellSize) / 2) + headerSize;
            top = (availableHeight - cellSize) / 2;
            // position the col headers
            for (int i = 0; i < 8; i++)
            {
                _colHeaders[i].Left = i * width + left;
                _colHeaders[i].Top = top;
                _colHeaders[i].Width = width;
                _colHeaders[i].Height = headerSize;
            }


            left = (availableWidth - cellSize) / 2 + headerSize;
            top = ((availableHeight - cellSize) / 2) + headerSize - 5;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    _board[i][j].Left = j * width + left;
                    _board[i][j].Top = cellSize - (i + 1) * height + top;
                    _board[i][j].Width = width;
                    _board[i][j].Height = height;
                }
            }


        }

        /// <summary>
        /// sets a piece onto the board 
        /// </summary>
        /// <param name="piece">the type of piece to be set</param>
        /// <param name="player">the player for whom the piece is set </param>
        /// <param name="letter">the letter location for where the piece is to be set</param>
        /// <param name="number">the number for where the piece is to be set </param>
        private void PlacePiece(PieceType piece, Player player, int letter, int number)
        {
            // if a thread called this, invoke recursion
            if (InvokeRequired)
            {
                Invoke(new Action(() => PlacePiece(piece, player, letter, number)));
                return;
            }

            // out of bounds
            if (letter < 0 || letter > 7 || number < 0 || number > 7) return; 

            // clear tile
            if (piece == PieceType.None)
            {
                _board[number][letter].Image = null;
                _board[number][letter].Invalidate();
                return;
            }

            // update our render
            _board[number][letter].Image = _graphics.Pieces[player][piece];
            _board[number][letter].Invalidate();
        }


        /// <summary>
        /// logs a move into the display
        /// </summary>
        /// <param name="move">the move to be shown</param>
        /// <param name="turn">the player whose turn it is</param>
        /// <param name="piece">the piece to be shown</param>
        public void LogMove(string move, Player turn, Piece piece)
        {
            // if a thread called this, invoke recursion
            if (InvokeRequired)
            {
                Invoke(new Action(() => LogMove(move, turn, piece)));
                return;
            }

            // reset check indicators
            lblWhiteCheck.Visible = false;
            lblBlackCheck.Visible = false;

            // show check indicator
            if (move.Contains("+"))
            {
                lblWhiteCheck.Visible = _game.Turn == Player.Black;
                lblBlackCheck.Visible = _game.Turn == Player.White;
            }

            if (turn == Player.Black)
            {
                listViewLogBlack.Items.Add(new ListViewItem
                {
                    ImageKey = piece.PieceType.ToString(),
                    Text = move
                });
            }
            else if (turn == Player.White)
            {
                listViewLogWhite.Items.Add(new ListViewItem
                {
                    ImageKey = piece.PieceType.ToString(),
                    Text = move
                });
            }
        }

        /// <summary>
        /// updates the status text and the progress bar
        /// </summary>
        /// <param name="thinking"></param>
        /// <param name="message"></param>
        public void SetStatus(bool thinking, string message)
        {
            // if a thread called this, invoke recursion
            if (InvokeRequired)
            {
                Invoke(new Action(() => SetStatus(thinking, message)));
                return;
            }

            // update status text and progress bar
            lblStatus.Text = message;
            if (thinking)
            {
                prgThinking.MarqueeAnimationSpeed = 30;
                prgThinking.Style = ProgressBarStyle.Marquee;
            }
            else
            {
                prgThinking.MarqueeAnimationSpeed = 0;
                prgThinking.Value = 0;
                prgThinking.Style = ProgressBarStyle.Continuous;
            }
        }

        /// <summary>
        /// creates a new game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewGame(object sender, EventArgs e)
        {
            ToolStripMenuItem button = (ToolStripMenuItem)sender;
            if (button.Text.StartsWith("New AI vs AI"))
            {
                NewGame(0);
            }
            else if (button.Text.StartsWith("New AI vs Player"))
            {
                NewGame(1);
            }
            else if (button.Text.StartsWith("New Player"))
            {
                NewGame(2);
            }
        }
        /// <summary>
        /// ends the current game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EndGame(object sender, EventArgs e)
        {
            Stop();
        }

        /// <summary>
        /// closes the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shutdown(object sender, EventArgs e)
        {
            Stop();
            Close();
        }

        /// <summary>
        /// sets the difficulty of the game of chess
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Difficulty(object sender, EventArgs e)
        {
            // uncheck previously checked
            if (_selectedDifficulty != null)
            {
                _selectedDifficulty.CheckState = CheckState.Unchecked;
            }

            // if the ai was currently thinking, stop it
            bool was = ArtificialIntelligence.Running;
            ArtificialIntelligence.Stop = true;

            // check the desired difficulty
            _selectedDifficulty = (ToolStripMenuItem)sender;
            _selectedDifficulty.CheckState = CheckState.Checked;

            // update the ai difficulty
            ArtificialIntelligence.Depth = int.Parse((string)_selectedDifficulty.Tag);

            // if the ai was running when changed, restart their turn
            if (was) new Thread(_game.ArtificialIntelligenceMove).Start();
        }

        /// <summary>
        /// player turn timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrWhite_Tick(object sender, EventArgs e)
        {
            _game.WhiteTime = _game.WhiteTime.Add(new TimeSpan(0, 0, 0, 0, timerWhitePlayer.Interval));
            lblWhiteTime.Text = string.Format("{0:d2}:{1:d2}:{2:d2}.{3:d1}", _game.WhiteTime.Hours, _game.WhiteTime.Minutes, _game.WhiteTime.Seconds, _game.WhiteTime.Milliseconds / 100);
        }

        /// <summary>
        /// player turn timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrBlack_Tick(object sender, EventArgs e)
        {
            _game.BlackTime = _game.BlackTime.Add(new TimeSpan(0, 0, 0, 0, timerBlackPlayer.Interval));
            lblBlackTime.Text = string.Format("{0:d2}:{1:d2}:{2:d2}.{3:d1}", _game.BlackTime.Hours, _game.BlackTime.Minutes, _game.BlackTime.Seconds, _game.BlackTime.Milliseconds / 100);
        }


        /// <summary>
        /// sets up the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // Load Image list for black player
            imageListBlack.Images.Add(PieceType.Pawn.ToString(), _graphics.Pieces[Player.Black][PieceType.Pawn]);
            imageListBlack.Images.Add(PieceType.Rook.ToString(), _graphics.Pieces[Player.Black][PieceType.Rook]);
            imageListBlack.Images.Add(PieceType.Knight.ToString(), _graphics.Pieces[Player.Black][PieceType.Knight]);
            imageListBlack.Images.Add(PieceType.Bishop.ToString(), _graphics.Pieces[Player.Black][PieceType.Bishop]);
            imageListBlack.Images.Add(PieceType.Queen.ToString(), _graphics.Pieces[Player.Black][PieceType.Queen]);
            imageListBlack.Images.Add(PieceType.King.ToString(), _graphics.Pieces[Player.Black][PieceType.King]);

            // Load Image list for white player
            imageListWhite.Images.Add(PieceType.Pawn.ToString(), _graphics.Pieces[Player.White][PieceType.Pawn]);
            imageListWhite.Images.Add(PieceType.Rook.ToString(), _graphics.Pieces[Player.White][PieceType.Rook]);
            imageListWhite.Images.Add(PieceType.Knight.ToString(), _graphics.Pieces[Player.White][PieceType.Knight]);
            imageListWhite.Images.Add(PieceType.Bishop.ToString(), _graphics.Pieces[Player.White][PieceType.Bishop]);
            imageListWhite.Images.Add(PieceType.Queen.ToString(), _graphics.Pieces[Player.White][PieceType.Queen]);
            imageListWhite.Images.Add(PieceType.King.ToString(), _graphics.Pieces[Player.White][PieceType.King]);
            
            CreateBoard();

            // setup turn indicator
            panelBlackTurn.Visible = false;
            panelWhiteTurn.Visible = false;

            // setup initial ai depth
            _selectedDifficulty = mnuDif3;
            ArtificialIntelligence.Depth = 3;

            SetStatus(false, "Choose New Game or Manual Board.");

            // setup menu
            endCurrentGameToolStripMenuItem.Enabled = false;
        }

        /// <summary>
        /// stop all current activity / games and reset everything
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
        }

        /// <summary>
        /// sets the label for the row header 
        /// </summary>
        /// <param name="row">the row number</param>
        /// <returns>a new label</returns>
        private Label RowHeader(int row)
        {
            return new Label
            {
                Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0),
                Name = "labelRow" + row,
                Text = (row +1).ToString(),
                TextAlign = ContentAlignment.MiddleCenter,
                Parent = splitView.Panel1
            };
        }

        /// <summary>
        /// generates the incrementing letters
        /// </summary>
        /// <param name="row">the row number</param>
        /// <returns>the appropriate label</returns>
        private Label CowHeader(int row)
        {
            var letter = "A";

            switch (row)
            {
                case 1:
                    letter = "B";
                    break;
                case 2:
                    letter = "C";
                    break;
                case 3:
                    letter = "D";
                    break;
                case 4:
                    letter = "E";
                    break;
                case 5:
                    letter = "F";
                    break;
                case 6:
                    letter = "G";
                    break;
                case 7:
                    letter = "H";
                    break;
            }


            return new Label
            {
                Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0),
                Name = "labelRow" + row,
                Text = letter,
                TextAlign = ContentAlignment.TopCenter,
                Parent = splitView.Panel1
            };
        }

        /// <summary>
        /// generates the actual board
        /// </summary>
        private void CreateBoard()
        {

            // Reload RowHeader
            _rowHeaders = new Label[8];
            for (int i = 0; i < 8; i++)
            {
                _rowHeaders[i] = RowHeader(i);
            }

            // Reload CowHeader
            _colHeaders = new Label[8];
            for (int i = 0; i < 8; i++)
            {
                _colHeaders[i] = CowHeader(i);
            }


            // create the board pieces
            _board = new PictureBox[8][];
            for (int i = 0; i < 8; i++)
            {
                _board[i] = new PictureBox[8];
                for (int j = 0; j < 8; j++)
                {
                    _board[i][j] = new PictureBox
                    {
                        Parent = splitView.Panel1
                    };
                    _board[i][j].Click += BoardClick;
                    _board[i][j].BackColor = ((j + i) % 2 == 0) ? _black : _white;
                    _board[i][j].SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }

            // align the board
            ResizeBoard(null, null);
        }

        /// <summary>
        /// clears the board
        /// </summary>
        private void ClearBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    _board[i][j].BackColor = ((i + j) % 2 == 0) ? _black : _white;
                }
            }
        }

        /// <summary>
        /// deals with what happens when the board is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoardClick(object sender, EventArgs e)
        {
            try
            {
                splitView.Panel1.Enabled = false;

                if (_game != null && !_checkmate)
                {
                    ClearBoard();

                    for (int i = 0; i < 8; i++)
                    {
                        if (_board != null)
                        {
                            int k = Array.IndexOf(_board[i], sender);
                            if (k > -1)
                            {
                                // draw highlighting
                                List<Position> moves = _game.Highlight(new Position(k, i));
                                foreach (Position move in moves)
                                {
                                    if ((_game.Board.Grid[move.Number][move.Letter].Player != _game.Turn
                                         && _game.Board.Grid[move.Number][move.Letter].PieceType != PieceType.None)
                                        || AllowedMovement.IsEnPassant(_game.Board, new Move(_game.Selection, move)))
                                    {
                                        // attack
                                        _board[move.Number][move.Letter].BackColor = Color.Red;
                                    }
                                    else
                                    {
                                        // move
                                        if (_board[move.Number][move.Letter].BackColor == _black) _board[move.Number][move.Letter].BackColor = _blackSelected;
                                        if (_board[move.Number][move.Letter].BackColor == _white) _board[move.Number][move.Letter].BackColor = _whiteSelected;
                                    }
                                }

                            }
                        }
                    }
                }
            }
            finally
            {

                splitView.Panel1.Enabled = true;
            }



        }


        /// <summary>
        /// saves the current board to a chess file
        /// </summary>
        public void SaveToFile()
        {
            timerWhitePlayer.Stop();
            timerBlackPlayer.Stop();

            using(var saveDlg = new FolderBrowserDialog())
            {
                var result = saveDlg.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(saveDlg.SelectedPath))
                {
                    var path = Path.Combine(saveDlg.SelectedPath, "Game" + DateTime.Now.ToString("_yyyyMMdd_hhmm") + ".xml");

                    if (_game != null) File.WriteAllText(path, _game.ToXml());
                }
            }
        }

        /// <summary>
        /// saves the current board to a chess file
        /// </summary>
        private void saveChessFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
<Game players="1" turn="White" whiteTime="" blackTime="" algorithm="" >
	<Selection row="0" column="0"/>
	<Moves>
		<Move>
			<Piece type= "King" player="White">
				<LastPosition row="0" column="0" /> 
			</Piece>
			<FromPosition row="0" column="0" /> 
			<ToPosition row="0" column="0" /> 
        </Move>
		<Move>
			<Piece type= "King" player="Black">
				<LastPosition row="0" column="0" /> 
			</Piece>
			<FromPosition row="0" column="0" /> 
			<ToPosition row="0" column="0" /> 
        </Move>
    </Moves>	
	<Pieces>
		<Piece type= "King" player="Black">
			<LastPosition row="0" column="0" /> 
		</Piece>
		<Piece type= "Queen" player="Black">
			<LastPosition row="0" column="0" /> 
		</Piece>
    </Pieces>
</Game>
             */

            SaveToFile();
        }

        /// <summary>
        /// opens an already created chess file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openChessFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                RestoreDirectory = true,
                Title = @"Open Chess XML",
                DefaultExt = "xml",
                Filter = @"Chass XML files (*.xml)|*.xml|All files (*.*)|*.*",
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = false
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var xmlString = File.ReadAllText(dialog.FileName);
                _game = Game.FromXml(this, xmlString);
                lblWhiteTime.Text = _game.WhiteTime.ToString("c");
                lblBlackTime.Text = _game.BlackTime.ToString("c");

                // Load History
                foreach (var moveHistory in _game.Moves)
                {
                    string move = string.Empty;

                    switch (moveHistory.Piece.PieceType)
                    {
                        case PieceType.Pawn:
                            move += "Pawn";
                            break;
                        case PieceType.Rook:
                            move += "Rook";
                            break;
                        case PieceType.Knight:
                            move += "Knight";
                            break;
                        case PieceType.Bishop:
                            move += "Bishop";
                            break;
                        case PieceType.Queen:
                            move += "Queen";
                            break;
                        case PieceType.King:
                            move += "King";
                            break;
                    }

                    move += ":\t";

                    // from Position
                    switch (moveHistory.Move.FromPosition.Letter)
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
                    move += (moveHistory.Move.FromPosition.Number + 1).ToString();
                    move += "-";

                    // letter
                    switch (moveHistory.Move.ToPosition.Letter)
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
                    move += (moveHistory.Move.ToPosition.Number + 1).ToString();

                    // To Position
                    //if (Board.Grid[m.ToPosition.Number][m.ToPosition.Letter].PieceType != PieceType.None || LegalMoveSet.IsEnPassant(Board, m))
                    //{
                    //    move += " + ";  // Is a Kill
                    //}

                    // if that move put someone in check
                    //if (LegalMoveSet.IsCheck(Board, (Turn == Player.White) ? Player.Black : Player.White))
                    //{
                    //    move += " # ";  // Check
                    //}

                    if (moveHistory.Piece.Player == Player.Black)
                    {
                        listViewLogBlack.Items.Add(new ListViewItem
                        {
                            ImageKey = moveHistory.Piece.PieceType.ToString(),
                            Text = move
                        });
                    }
                    else if (moveHistory.Piece.Player == Player.White)
                    {
                        listViewLogWhite.Items.Add(new ListViewItem
                        {
                            ImageKey = moveHistory.Piece.PieceType.ToString(),
                            Text = move
                        });
                    }
                }

                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        PlacePiece(_game.Board.Grid[i][j].PieceType, _game.Board.Grid[i][j].Player, j, i);
                    }
                }
                SetTurn(_game.Turn);

                ResizeBoard(null, null);
            }  
        }

        private void splitView_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
