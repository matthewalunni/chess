namespace Chess
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newAIGame = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.new2PlayerGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.endCurrentGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.openChessFileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saceChessFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.difficultyDepthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDif1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDif2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDif3 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDif4 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDif5 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.prgThinking = new System.Windows.Forms.ToolStripProgressBar();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitView = new System.Windows.Forms.SplitContainer();
            this.panelBlack = new System.Windows.Forms.Panel();
            this.listViewBlackKills = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageListWhite = new System.Windows.Forms.ImageList(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.panelBlackTurn = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblBlackTime = new System.Windows.Forms.Label();
            this.listViewLogBlack = new System.Windows.Forms.ListView();
            this.columnFrom = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageListBlack = new System.Windows.Forms.ImageList(this.components);
            this.lblBlackCheck = new System.Windows.Forms.Label();
            this.panelWhite = new System.Windows.Forms.Panel();
            this.listViewWhiteKills = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.panelWhiteTurn = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.listViewLogWhite = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblWhiteTime = new System.Windows.Forms.Label();
            this.lblWhiteCheck = new System.Windows.Forms.Label();
            this.timerWhitePlayer = new System.Windows.Forms.Timer(this.components);
            this.timerBlackPlayer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitView)).BeginInit();
            this.splitView.Panel2.SuspendLayout();
            this.splitView.SuspendLayout();
            this.panelBlack.SuspendLayout();
            this.panelWhite.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.difficultyDepthToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(921, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newAIGame,
            this.newGameToolStripMenuItem,
            this.new2PlayerGameToolStripMenuItem,
            this.endCurrentGameToolStripMenuItem,
            this.toolStripMenuItem2,
            this.openChessFileToolStripMenuItem1,
            this.saceChessFileToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newAIGame
            // 
            this.newAIGame.Name = "newAIGame";
            this.newAIGame.Size = new System.Drawing.Size(262, 26);
            this.newAIGame.Text = "New AI vs AI Game";
            this.newAIGame.Click += new System.EventHandler(this.NewGame);
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(262, 26);
            this.newGameToolStripMenuItem.Text = "New AI vs Player Game";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.NewGame);
            // 
            // new2PlayerGameToolStripMenuItem
            // 
            this.new2PlayerGameToolStripMenuItem.Name = "new2PlayerGameToolStripMenuItem";
            this.new2PlayerGameToolStripMenuItem.Size = new System.Drawing.Size(262, 26);
            this.new2PlayerGameToolStripMenuItem.Text = "New Player vs Player Game";
            this.new2PlayerGameToolStripMenuItem.Click += new System.EventHandler(this.NewGame);
            // 
            // endCurrentGameToolStripMenuItem
            // 
            this.endCurrentGameToolStripMenuItem.Name = "endCurrentGameToolStripMenuItem";
            this.endCurrentGameToolStripMenuItem.Size = new System.Drawing.Size(262, 26);
            this.endCurrentGameToolStripMenuItem.Text = "End current game";
            this.endCurrentGameToolStripMenuItem.Click += new System.EventHandler(this.EndGame);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(259, 6);
            // 
            // openChessFileToolStripMenuItem1
            // 
            this.openChessFileToolStripMenuItem1.Name = "openChessFileToolStripMenuItem1";
            this.openChessFileToolStripMenuItem1.Size = new System.Drawing.Size(262, 26);
            this.openChessFileToolStripMenuItem1.Text = "Open Chess File";
            this.openChessFileToolStripMenuItem1.Click += new System.EventHandler(this.openChessFileToolStripMenuItem1_Click);
            // 
            // saceChessFileToolStripMenuItem
            // 
            this.saceChessFileToolStripMenuItem.Name = "saceChessFileToolStripMenuItem";
            this.saceChessFileToolStripMenuItem.Size = new System.Drawing.Size(262, 26);
            this.saceChessFileToolStripMenuItem.Text = "Save Chess File";
            this.saceChessFileToolStripMenuItem.Click += new System.EventHandler(this.saveChessFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(259, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(262, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.Shutdown);
            // 
            // difficultyDepthToolStripMenuItem
            // 
            this.difficultyDepthToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDif1,
            this.mnuDif2,
            this.mnuDif3,
            this.mnuDif4,
            this.mnuDif5});
            this.difficultyDepthToolStripMenuItem.Name = "difficultyDepthToolStripMenuItem";
            this.difficultyDepthToolStripMenuItem.Size = new System.Drawing.Size(136, 24);
            this.difficultyDepthToolStripMenuItem.Text = "Difficulty (Depth)";
            // 
            // mnuDif1
            // 
            this.mnuDif1.Name = "mnuDif1";
            this.mnuDif1.Size = new System.Drawing.Size(173, 26);
            this.mnuDif1.Tag = "1";
            this.mnuDif1.Text = "Beginner ( 1 )";
            this.mnuDif1.Click += new System.EventHandler(this.Difficulty);
            // 
            // mnuDif2
            // 
            this.mnuDif2.Name = "mnuDif2";
            this.mnuDif2.Size = new System.Drawing.Size(173, 26);
            this.mnuDif2.Tag = "2";
            this.mnuDif2.Text = "Easy ( 2 )";
            this.mnuDif2.Click += new System.EventHandler(this.Difficulty);
            // 
            // mnuDif3
            // 
            this.mnuDif3.Checked = true;
            this.mnuDif3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuDif3.Name = "mnuDif3";
            this.mnuDif3.Size = new System.Drawing.Size(173, 26);
            this.mnuDif3.Tag = "3";
            this.mnuDif3.Text = "Medium ( 3 )";
            this.mnuDif3.Click += new System.EventHandler(this.Difficulty);
            // 
            // mnuDif4
            // 
            this.mnuDif4.Name = "mnuDif4";
            this.mnuDif4.Size = new System.Drawing.Size(173, 26);
            this.mnuDif4.Tag = "4";
            this.mnuDif4.Text = "Hard ( 4 )";
            this.mnuDif4.Click += new System.EventHandler(this.Difficulty);
            // 
            // mnuDif5
            // 
            this.mnuDif5.Name = "mnuDif5";
            this.mnuDif5.Size = new System.Drawing.Size(173, 26);
            this.mnuDif5.Tag = "5";
            this.mnuDif5.Text = "Insane ( 5 )";
            this.mnuDif5.Click += new System.EventHandler(this.Difficulty);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.prgThinking,
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 572);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(921, 26);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // prgThinking
            // 
            this.prgThinking.Name = "prgThinking";
            this.prgThinking.Size = new System.Drawing.Size(533, 20);
            this.prgThinking.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(74, 21);
            this.lblStatus.Text = "Thinking...";
            // 
            // splitView
            // 
            this.splitView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitView.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitView.IsSplitterFixed = true;
            this.splitView.Location = new System.Drawing.Point(12, 33);
            this.splitView.Margin = new System.Windows.Forms.Padding(4);
            this.splitView.Name = "splitView";
            // 
            // splitView.Panel1
            // 
            this.splitView.Panel1.BackColor = System.Drawing.Color.LightGray;
            this.splitView.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitView_Panel1_Paint);
            this.splitView.Panel1.Resize += new System.EventHandler(this.ResizeBoard);
            this.splitView.Panel1MinSize = 400;
            // 
            // splitView.Panel2
            // 
            this.splitView.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitView.Panel2.Controls.Add(this.panelBlack);
            this.splitView.Panel2.Controls.Add(this.panelWhite);
            this.splitView.Panel2MinSize = 200;
            this.splitView.Size = new System.Drawing.Size(893, 503);
            this.splitView.SplitterDistance = 620;
            this.splitView.SplitterWidth = 5;
            this.splitView.TabIndex = 2;
            // 
            // panelBlack
            // 
            this.panelBlack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelBlack.BackColor = System.Drawing.Color.DimGray;
            this.panelBlack.Controls.Add(this.listViewBlackKills);
            this.panelBlack.Controls.Add(this.label4);
            this.panelBlack.Controls.Add(this.panelBlackTurn);
            this.panelBlack.Controls.Add(this.label2);
            this.panelBlack.Controls.Add(this.lblBlackTime);
            this.panelBlack.Controls.Add(this.listViewLogBlack);
            this.panelBlack.Controls.Add(this.lblBlackCheck);
            this.panelBlack.Location = new System.Drawing.Point(184, 4);
            this.panelBlack.Margin = new System.Windows.Forms.Padding(4);
            this.panelBlack.Name = "panelBlack";
            this.panelBlack.Size = new System.Drawing.Size(167, 496);
            this.panelBlack.TabIndex = 11;
            // 
            // listViewBlackKills
            // 
            this.listViewBlackKills.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewBlackKills.BackColor = System.Drawing.Color.White;
            this.listViewBlackKills.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.listViewBlackKills.ForeColor = System.Drawing.Color.Black;
            this.listViewBlackKills.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewBlackKills.LargeImageList = this.imageListWhite;
            this.listViewBlackKills.Location = new System.Drawing.Point(5, 402);
            this.listViewBlackKills.Margin = new System.Windows.Forms.Padding(4);
            this.listViewBlackKills.Name = "listViewBlackKills";
            this.listViewBlackKills.Size = new System.Drawing.Size(156, 89);
            this.listViewBlackKills.SmallImageList = this.imageListWhite;
            this.listViewBlackKills.TabIndex = 13;
            this.listViewBlackKills.TileSize = new System.Drawing.Size(24, 24);
            this.listViewBlackKills.UseCompatibleStateImageBehavior = false;
            this.listViewBlackKills.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Move";
            this.columnHeader3.Width = 80;
            // 
            // imageListWhite
            // 
            this.imageListWhite.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListWhite.ImageSize = new System.Drawing.Size(24, 24);
            this.imageListWhite.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(3, 384);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "Kills";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelBlackTurn
            // 
            this.panelBlackTurn.BackColor = System.Drawing.Color.Yellow;
            this.panelBlackTurn.Location = new System.Drawing.Point(3, 2);
            this.panelBlackTurn.Margin = new System.Windows.Forms.Padding(4);
            this.panelBlackTurn.Name = "panelBlackTurn";
            this.panelBlackTurn.Size = new System.Drawing.Size(161, 12);
            this.panelBlackTurn.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 17);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 28);
            this.label2.TabIndex = 1;
            this.label2.Text = "Black";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBlackTime
            // 
            this.lblBlackTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBlackTime.Location = new System.Drawing.Point(4, 48);
            this.lblBlackTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBlackTime.Name = "lblBlackTime";
            this.lblBlackTime.Size = new System.Drawing.Size(159, 16);
            this.lblBlackTime.TabIndex = 2;
            this.lblBlackTime.Text = "00:00:00.0";
            this.lblBlackTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listViewLogBlack
            // 
            this.listViewLogBlack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewLogBlack.BackColor = System.Drawing.Color.White;
            this.listViewLogBlack.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnFrom});
            this.listViewLogBlack.ForeColor = System.Drawing.Color.Black;
            this.listViewLogBlack.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewLogBlack.LargeImageList = this.imageListBlack;
            this.listViewLogBlack.Location = new System.Drawing.Point(5, 86);
            this.listViewLogBlack.Margin = new System.Windows.Forms.Padding(4);
            this.listViewLogBlack.Name = "listViewLogBlack";
            this.listViewLogBlack.Size = new System.Drawing.Size(155, 285);
            this.listViewLogBlack.SmallImageList = this.imageListBlack;
            this.listViewLogBlack.TabIndex = 8;
            this.listViewLogBlack.UseCompatibleStateImageBehavior = false;
            this.listViewLogBlack.View = System.Windows.Forms.View.Details;
            // 
            // columnFrom
            // 
            this.columnFrom.Text = "Move";
            this.columnFrom.Width = 80;
            // 
            // imageListBlack
            // 
            this.imageListBlack.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListBlack.ImageSize = new System.Drawing.Size(24, 24);
            this.imageListBlack.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // lblBlackCheck
            // 
            this.lblBlackCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBlackCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBlackCheck.ForeColor = System.Drawing.Color.Red;
            this.lblBlackCheck.Location = new System.Drawing.Point(4, 66);
            this.lblBlackCheck.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBlackCheck.Name = "lblBlackCheck";
            this.lblBlackCheck.Size = new System.Drawing.Size(159, 16);
            this.lblBlackCheck.TabIndex = 6;
            this.lblBlackCheck.Text = "In Check";
            this.lblBlackCheck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBlackCheck.Visible = false;
            // 
            // panelWhite
            // 
            this.panelWhite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelWhite.BackColor = System.Drawing.Color.DimGray;
            this.panelWhite.Controls.Add(this.listViewWhiteKills);
            this.panelWhite.Controls.Add(this.label3);
            this.panelWhite.Controls.Add(this.panelWhiteTurn);
            this.panelWhite.Controls.Add(this.label1);
            this.panelWhite.Controls.Add(this.listViewLogWhite);
            this.panelWhite.Controls.Add(this.lblWhiteTime);
            this.panelWhite.Controls.Add(this.lblWhiteCheck);
            this.panelWhite.Location = new System.Drawing.Point(4, 4);
            this.panelWhite.Margin = new System.Windows.Forms.Padding(4);
            this.panelWhite.Name = "panelWhite";
            this.panelWhite.Size = new System.Drawing.Size(172, 496);
            this.panelWhite.TabIndex = 10;
            // 
            // listViewWhiteKills
            // 
            this.listViewWhiteKills.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewWhiteKills.BackColor = System.Drawing.Color.White;
            this.listViewWhiteKills.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listViewWhiteKills.ForeColor = System.Drawing.Color.Black;
            this.listViewWhiteKills.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewWhiteKills.LargeImageList = this.imageListBlack;
            this.listViewWhiteKills.Location = new System.Drawing.Point(8, 402);
            this.listViewWhiteKills.Margin = new System.Windows.Forms.Padding(4);
            this.listViewWhiteKills.Name = "listViewWhiteKills";
            this.listViewWhiteKills.Size = new System.Drawing.Size(156, 89);
            this.listViewWhiteKills.SmallImageList = this.imageListBlack;
            this.listViewWhiteKills.TabIndex = 12;
            this.listViewWhiteKills.TileSize = new System.Drawing.Size(22, 22);
            this.listViewWhiteKills.UseCompatibleStateImageBehavior = false;
            this.listViewWhiteKills.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Move";
            this.columnHeader2.Width = 80;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(5, 383);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Kills";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelWhiteTurn
            // 
            this.panelWhiteTurn.BackColor = System.Drawing.Color.Yellow;
            this.panelWhiteTurn.Location = new System.Drawing.Point(5, 2);
            this.panelWhiteTurn.Margin = new System.Windows.Forms.Padding(4);
            this.panelWhiteTurn.Name = "panelWhiteTurn";
            this.panelWhiteTurn.Size = new System.Drawing.Size(161, 12);
            this.panelWhiteTurn.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(4, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "White";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listViewLogWhite
            // 
            this.listViewLogWhite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewLogWhite.BackColor = System.Drawing.Color.White;
            this.listViewLogWhite.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewLogWhite.ForeColor = System.Drawing.Color.Black;
            this.listViewLogWhite.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewLogWhite.LargeImageList = this.imageListWhite;
            this.listViewLogWhite.Location = new System.Drawing.Point(8, 86);
            this.listViewLogWhite.Margin = new System.Windows.Forms.Padding(4);
            this.listViewLogWhite.Name = "listViewLogWhite";
            this.listViewLogWhite.Size = new System.Drawing.Size(156, 285);
            this.listViewLogWhite.SmallImageList = this.imageListWhite;
            this.listViewLogWhite.TabIndex = 9;
            this.listViewLogWhite.UseCompatibleStateImageBehavior = false;
            this.listViewLogWhite.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Move";
            this.columnHeader1.Width = 80;
            // 
            // lblWhiteTime
            // 
            this.lblWhiteTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWhiteTime.ForeColor = System.Drawing.Color.White;
            this.lblWhiteTime.Location = new System.Drawing.Point(7, 50);
            this.lblWhiteTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWhiteTime.Name = "lblWhiteTime";
            this.lblWhiteTime.Size = new System.Drawing.Size(161, 16);
            this.lblWhiteTime.TabIndex = 2;
            this.lblWhiteTime.Text = "00:00:00.0";
            this.lblWhiteTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWhiteCheck
            // 
            this.lblWhiteCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWhiteCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWhiteCheck.ForeColor = System.Drawing.Color.Red;
            this.lblWhiteCheck.Location = new System.Drawing.Point(3, 68);
            this.lblWhiteCheck.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWhiteCheck.Name = "lblWhiteCheck";
            this.lblWhiteCheck.Size = new System.Drawing.Size(165, 15);
            this.lblWhiteCheck.TabIndex = 6;
            this.lblWhiteCheck.Text = "In Check";
            this.lblWhiteCheck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblWhiteCheck.Visible = false;
            // 
            // timerWhitePlayer
            // 
            this.timerWhitePlayer.Tick += new System.EventHandler(this.tmrWhite_Tick);
            // 
            // timerBlackPlayer
            // 
            this.timerBlackPlayer.Tick += new System.EventHandler(this.tmrBlack_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 598);
            this.Controls.Add(this.splitView);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(861, 605);
            this.Name = "MainForm";
            this.Text = "Chess - Minmax Algorithm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WindowClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitView.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitView)).EndInit();
            this.splitView.ResumeLayout(false);
            this.panelBlack.ResumeLayout(false);
            this.panelWhite.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar prgThinking;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.SplitContainer splitView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblWhiteTime;
        private System.Windows.Forms.Label lblBlackTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem new2PlayerGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem difficultyDepthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuDif1;
        private System.Windows.Forms.ToolStripMenuItem mnuDif2;
        private System.Windows.Forms.ToolStripMenuItem mnuDif3;
        private System.Windows.Forms.ToolStripMenuItem mnuDif4;
        private System.Windows.Forms.ToolStripMenuItem mnuDif5;
        private System.Windows.Forms.Label lblBlackCheck;
        private System.Windows.Forms.Label lblWhiteCheck;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem endCurrentGameToolStripMenuItem;
        private System.Windows.Forms.Timer timerWhitePlayer;
        private System.Windows.Forms.Timer timerBlackPlayer;
        private System.Windows.Forms.ToolStripMenuItem newAIGame;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem saceChessFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openChessFileToolStripMenuItem1;
        private System.Windows.Forms.ListView listViewLogBlack;
        private System.Windows.Forms.ColumnHeader columnFrom;
        private System.Windows.Forms.ImageList imageListBlack;
        private System.Windows.Forms.ListView listViewLogWhite;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ImageList imageListWhite;
        private System.Windows.Forms.Panel panelWhite;
        private System.Windows.Forms.Panel panelBlack;
        private System.Windows.Forms.Panel panelBlackTurn;
        private System.Windows.Forms.Panel panelWhiteTurn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listViewBlackKills;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ListView listViewWhiteKills;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}

