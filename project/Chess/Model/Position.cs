
namespace Chess.Model
{
    public struct Position
    {
        public int Letter { get; set; }
        public int Number { get; set; }

        public Position(int letter, int number) : this()
        {
            Letter = letter;
            Number = number;
        }
        public Position(Position copy) : this()
        {
            Letter = copy.Letter;
            Number = copy.Number;
        }

        /// <summary>
        /// overrides the equal method, a struct can never equal a struct because they are not pointing to the same 
        /// memory location, so equals was needed to be overridden to be when two positions are the same
        /// </summary>
        /// <param name="obj">the object to equate</param>
        /// <returns>true if the letter and number is equal to the paramater letter and number</returns>
        public override bool Equals(object obj)
        {
            return Letter == ((Position)obj).Letter && Number == ((Position)obj).Number;
        }
    }
}
