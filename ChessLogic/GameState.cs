namespace ChessLogic
{
        public class GameState
        {
            public Board Board { get; }
            public event Action PieceCaptured;

            public bool IsWhiteInCheck => Board.IsInCheck(Player.White);
            public bool IsBlackInCheck => Board.IsInCheck(Player.Black);
            public Player CurrentPlayer { get; private set; }

            public Result Result { get; private set; } = null;

            private int noCaptureOrPawnMoves = 0;
            private string stateString;

            private readonly Dictionary<string, int> stateHistory = new Dictionary<string, int>();

            // ✅ Thêm biến thời gian cho từng bên
            public TimeSpan WhiteTime { get; private set; }
            public TimeSpan BlackTime { get; private set; }

            private DateTime lastMoveTime;
            public GameState(Player player, Board board, TimeSpan whiteTime, TimeSpan blackTime)
            {
                CurrentPlayer = player;
                Board = board;

                WhiteTime = whiteTime;
                BlackTime = blackTime;

                stateString = new StateString(CurrentPlayer, board).ToString();
                stateHistory[stateString] = 1;
            }

            public IEnumerable<Move> LegalMovesForPiece(Position pos)
            {
                if (Board.IsEmpty(pos) || Board[pos].Color != CurrentPlayer)
                {
                    return Enumerable.Empty<Move>();
                }

                Piece piece = Board[pos];
                IEnumerable<Move> moveCandidates = piece.GetMoves(pos, Board);
                return moveCandidates.Where(move => move.IsLegal(Board));
            }

            public void MakeMove(Move move)
            {
                Board.SetPawnSkipPosition(CurrentPlayer, null);
                bool captureOrPawn = move.Execute(Board);

                if (captureOrPawn || move is PawnPromotion)
                {
                    noCaptureOrPawnMoves = 0;
                    stateHistory.Clear();
                }
                else
                {
                    noCaptureOrPawnMoves++;
                }

                CurrentPlayer = CurrentPlayer.Opponent();
                UpdateStateString();
                CheckForGameOver();
            }

            public IEnumerable<Move> AllLegalMovesFor(Player player)
            {
                IEnumerable<Move> moveCandidates = Board.PiecePositionsFor(player).SelectMany(pos =>
                {
                    Piece piece = Board[pos];
                    return piece.GetMoves(pos, Board);
                });

                return moveCandidates.Where(move => move.IsLegal(Board));
            }

            private void CheckForGameOver()
            {   
                if (!AllLegalMovesFor(CurrentPlayer).Any())
                {
                    if (Board.IsInCheck(CurrentPlayer))
                    {
                        Result = Result.Win(CurrentPlayer.Opponent());
                    }
                    else
                    {
                        Result = Result.Draw(EndReason.Stalemate);
                    }
                }
                else if (Board.InsufficientMaterial())
                {
                    Result = Result.Draw(EndReason.InsufficientMaterial);
                }
                else if (FiftyMoveRule())
                {
                    Result = Result.Draw(EndReason.FiftyMoveRule);
                }
                else if (ThreefoldRepetition())
                {
                    Result = Result.Draw(EndReason.ThreefoldRepetition);
                }  
            }

            public bool IsGameOver()
            {
                return Result != null;
            }

            private bool FiftyMoveRule()
            {
            return noCaptureOrPawnMoves >= 100;
            }

            private void UpdateStateString()
            {
                stateString = new StateString(CurrentPlayer, Board).ToString();

                if(!stateHistory.ContainsKey(stateString))
                {
                    stateHistory[stateString] = 1;
                }
                else
                {
                    stateHistory[stateString]++;
                }
            }

            private bool ThreefoldRepetition()
            {
                return stateHistory[stateString] == 3;
            }
            public bool Timeout()
            {
                if (IsGameOver()) return false; // Đảm bảo game chỉ kết thúc một lần

                Player loser = CurrentPlayer;
                Player winner = (loser == Player.White) ? Player.Black : Player.White;

                Result = Result.WinTimeout(winner);

                Console.WriteLine("Hết thời gian");

                return true;
            }
        }
}
