namespace ChessLogic
{
    public class Queen : Piece
    {
        public override PieceType Type => PieceType.Queen; //thuộc tính chỉ đọc (read-only) trả về quân hậu

        public override Player Color { get; }

        private static readonly Direction[] dirs = new Direction[] //Mảng hướng di chuyển
        {
            Direction.North, //Phía bắc
            Direction.South, //Phía Nam
            Direction.East, //Phía đông
            Direction.West, //Phía tây
            Direction.NorthWest,
            Direction.NorthEast,
            Direction.SouthWest,
            Direction.SouthEast,
        };

        public Queen (Player color) //Constructor: phương thức khởi tạo một quân hậu với màu sắc chỉ định
        {
            Color = color;
        }
        public override Piece Copy() //Phương thức này tạo ra một bản sao của quân hậu hiện tại 
        {
            Queen copy = new Queen(Color);
            copy.HasMoved = HasMoved; //Đây là một thuộc tính (có thể được định nghĩa trong lớp Piece) để theo dõi xem quân cờ đã di chuyển hay chưa. Thuộc tính này được sao chép từ quân gốc sang quân mới.
            return copy;
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board) //phương thức trả về nước đi hợp lệ mà quân hậu có thể thực hiện ở nước đi hiện tại [from] đến vị trị [to]
        {
            return MovePositionsInDirs(from, board, dirs).Select(to => new NormalMove(from, to));
        }
    }
}
