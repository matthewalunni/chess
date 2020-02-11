asaanamespace Chess.Model
{
    public struct Move
    {
        public Position FromPosition;
        public Position ToPosition;

        public Move(Position from, Position to)
        {
            FromPosition = from;
            ToPosition = to;
        }

        /// <summary>
        /// overrides the ToString method
        /// </summary>
        /// <returns>coordinate in correct string format</returns>
        public override string ToString()
        {
            return string.Format("{0}{1}:{2}{3}", FromPosition.Letter, FromPosition.Number, ToPosition.Letter, ToPosition.Number);
        }
    }
}
