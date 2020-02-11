namespace Chess.Model
{
    public class MoveHistory
    {
        public Move Move { get; set; }
        public Piece Piece { get; set; }

        /// <summary>
        /// overrides the ToString() method so it can be printed in the correct format 
        /// </summary>
        /// <returns>a string correctly formatted</returns>
        public override string ToString()
        {
            var move = string.Format("MOVE:{0}", Move);
            var piece = string.Format("PIECE:{0}", Piece);
            return string.Format("{0}|{1}",move, piece);
        }
    }
}
