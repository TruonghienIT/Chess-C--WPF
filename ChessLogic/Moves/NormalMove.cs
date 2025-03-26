namespace ChessLogic
{
    //Nước đi thông thường
    public class NormalMove : Move
    {
        public override MoveType Type => MoveType.Normal;

        public override Position FromPos { get; }


        public override Position ToPos { get; }
        public NormalMove (Position from, Position pos)
        {
            FromPos = from;
            ToPos = pos;
        }

        public override bool Execute(Board board)
        {
            Piece piece = board[FromPos];
            bool capture = !board.IsEmpty(ToPos);
            board[ToPos] = piece;
            board[FromPos] = null;
            piece.HasMoved = true;

            return capture || piece.Type == PieceType.Pawn;
        }
    }
}
