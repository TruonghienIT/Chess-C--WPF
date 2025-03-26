namespace ChessLogic
{
    public abstract class Move
    {
        public abstract MoveType Type { get; }
        public abstract Position FromPos { get; }
        public abstract Position ToPos { get; }
        public Piece CapturedPiece { get; private set; }  // Thêm biến này
        public abstract bool Execute(Board board);

        public virtual bool IsLegal(Board board)
        {
            Player player = board[FromPos].Color;
            Board boardCopy = board.Copy();
            Execute(boardCopy);
            return !boardCopy.IsInCheck(player);
        }
        // Phương thức này cần được gọi trong Execute() của các Move con
        protected void CapturePiece(Board board)
        {
            CapturedPiece = board[ToPos]; // Lấy quân cờ bị ăn
        }
    }
}
