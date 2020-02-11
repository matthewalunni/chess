namespace Chess.Model
{
    public interface IBoardView
    {
        void SetBoard(Board board);
        void LogMove(string line, Player turn, Piece piece);
        void SetStatus(bool thinking, string message);
        void SetTurn(Player p);
        void LogKill(Player turn, Piece piece);
    }
}
