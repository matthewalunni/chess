namespace Chess.Model
{
    public struct Piece
    {
        public PieceType PieceType;
        public Player Player;
        public Position LastPosition;

        

        public Piece(PieceType pieceType, Player player)
        {
            PieceType = pieceType;
            Player = player;
            LastPosition = new Position(-1, -1);
        }


        public Piece(Piece copy)
        {
            PieceType = copy.PieceType;
            Player = copy.Player;
            LastPosition = copy.LastPosition;
        }

        /// <summary>
        /// overrides the ToString method so it can be printed in the correct format
        /// </summary>
        /// <returns>a string in the correct format</returns>
        public override string ToString()
        {
            return string.Format("{0},{1}", Player, PieceType);
        }
    }
}
