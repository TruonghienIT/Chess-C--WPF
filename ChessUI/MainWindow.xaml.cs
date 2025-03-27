using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ChessLogic;
using System.Media;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Khởi tạo biến thời gian cho các bên quân
        private DispatcherTimer _whiteTimer;
        private DispatcherTimer _blackTimer;
        private TimeSpan _whiteTime;
        private TimeSpan _blackTime;

        private readonly Color moveHighlightColor = Color.FromArgb(150, 125, 255, 125); // Màu xanh cho di chuyển thông thường
        private readonly Color captureHighlightColor = Color.FromArgb(150, 255, 125, 125); // Màu đỏ cho ăn quân

        private readonly Image[,] pieceImages = new Image[8, 8];
        private readonly Rectangle[,] highlights = new Rectangle[8, 8];
        private readonly Dictionary<Position, Move> moveCache = new Dictionary<Position, Move>();

        private GameState gameState;
        private Position selectedPos = null;

        //Khởi tạo biến tên người chơi 
        private string whitePlayerName ;
        private string blackPlayerName ;

        //Khởi tạo biến thời gian ban đầu
        private int selectedGameTime ;

        // Tạo player toàn cục
        private MediaPlayer moveSoundPlayer = new MediaPlayer();
        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();

            ShowPlayerNameWindow();

            InitializeTimer();

            gameState = new GameState(Player.White, Board.Initial(), _whiteTime, _blackTime);
            DrawBoard(gameState.Board);
            setcursor(gameState.CurrentPlayer);

            SwitchTurn(true);

        }

        private void InitializeBoard()
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Image image = new Image();
                    pieceImages[r, c] = image;
                    PieceGrid.Children.Add(image);

                    Rectangle highlight = new Rectangle();
                    highlights[r, c] = highlight;
                    HighlightGrid.Children.Add(highlight); // Thêm Rectangle vào Grid
                }
            }
        }

        private void InitializeTimer()
        {
            // Timer cho quân trắng
            _whiteTimer = new DispatcherTimer();
            _whiteTimer.Interval = TimeSpan.FromSeconds(1);
            _whiteTimer.Tick += (s, e) => UpdateTimer(ref _whiteTime, WhiteTimerRun);

            // Timer cho quân đen
            _blackTimer = new DispatcherTimer();
            _blackTimer.Interval = TimeSpan.FromSeconds(1);
            _blackTimer.Tick += (s, e) => UpdateTimer(ref _blackTime, BlackTimerRun);
        }

        private void UpdateTimer(ref TimeSpan time, Run timerRun)
        {
            time = time.Subtract(TimeSpan.FromSeconds(1));
            timerRun.Text = $"{time:mm\\:ss}"; // Cập nhật thời gian

            if (time.TotalSeconds <= 0)
            {
                _whiteTimer.Stop();
                _blackTimer.Stop();

                // ⏰ Cập nhật trạng thái GameState
                if (gameState.Timeout())
                {
                    ShowGameOver(); // Hiển thị kết quả khi hết thời gian
                }
            }
        }

        private void DrawBoard(Board board)
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Piece piece = board[r, c];
                    pieceImages[r, c].Source = Images.GetImage(piece);
                }
            }
        }

        private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsMenuOnScreen())
            {
                return;
            }

            Point point = e.GetPosition(BoardGrid);
            Position pos = ToSquarePosition(point);

            if (selectedPos == null)
            {
                OnFromPositionSelected(pos);
            }
            else
            {
                OnToPositionSelected(pos);
            }
        }

        private Position ToSquarePosition(Point point)
        {
            double squareSize = BoardGrid.ActualWidth / 8;
            int row = (int)(point.Y / squareSize);
            int col = (int)(point.X / squareSize);
            return new Position(row, col);
        }

        private void OnFromPositionSelected(Position pos)
        {
            IEnumerable<Move> moves = gameState.LegalMovesForPiece(pos);

            if (moves.Any())
            {
                selectedPos = pos;
                CacheMoves(moves); // Lưu trữ các nước đi hợp lệ vào moveCache
                ShowHighlights();  // Hiển thị các ô màu xanh
            }
        }

        private void OnToPositionSelected(Position pos)
        {
            selectedPos = null;
            HideHighlights(); //ẩn ô xah

            if (moveCache.TryGetValue(pos, out Move move))
            {
                if (move.Type == MoveType.PawnPromtion)
                {
                    HandlePromotion(move.FromPos, move.ToPos);
                }
                else
                {
                    HandleMove(move);
                }
            }
        }

        private void HandlePromotion(Position from, Position to)
        {
            pieceImages[to.Row, to.Column].Source = Images.GetImage(gameState.CurrentPlayer, PieceType.Pawn);
            pieceImages[from.Row, from.Column].Source = null;

            PromotionMenu promMenu = new PromotionMenu(gameState.CurrentPlayer);
            MenuContainer.Content = promMenu;

            promMenu.PieceSelected += type =>
            {
                MenuContainer.Content = null;
                Move promMove = new PawnPromotion(from, to, type);
                HandleMove(promMove);
            };
        }

        private void HandleMove(Move move)
        {

            PlayMoveSound(); // 🔊 Phát âm thanh khi di chuyển quân
            gameState.MakeMove(move);
            DrawBoard(gameState.Board);
            setcursor(gameState.CurrentPlayer);

            UpdateCheckWarnings();

            SwitchTurn(gameState.CurrentPlayer == Player.White);

            if (gameState.IsGameOver())
            {
                ShowGameOver();
            }
        }

        private void CacheMoves(IEnumerable<Move> moves)
        {
            moveCache.Clear();

            foreach (Move move in moves)
            {
                moveCache[move.ToPos] = move;
            }
        }

        private void ShowHighlights()
        {
            foreach (var entry in moveCache)
            {
                Position to = entry.Key;
                Move move = entry.Value;

                // Kiểm tra thông thường
                bool isNormalCapture = gameState.Board[to.Row, to.Column] != null;

                // Kiểm tra bắt tốt qua đường (en passant)
                bool isEnPassant = move is EnPassant &&
                                  gameState.Board[((EnPassant)move).FromPos.Row, to.Column] is Pawn;

                Color color = (isNormalCapture || isEnPassant) ? captureHighlightColor : moveHighlightColor;
                highlights[to.Row, to.Column].Fill = new SolidColorBrush(color);
            }
        }

        private void HideHighlights() //Hàm này được gọi khi người chơi thực hiện xog nước đi hoặc bỏ chọn giúp ẩn các ô hiện thị nước đi hợp lệ
        {
            foreach (Position to in moveCache.Keys)
            {
                highlights[to.Row, to.Column].Fill = Brushes.Transparent;
            }
        }

        private void setcursor(Player player)
        {
            if (player == Player.White)
            {
                Cursor = ChessCursors.WhiteCursor;
            }
            else
            {
                Cursor = ChessCursors.BlackCursor;
            }
        }

        private bool IsMenuOnScreen()
        {
            return MenuContainer.Content != null;
        }

        private void ShowGameOver()
        {
            // Dừng cả hai bộ đếm thời gian
            _whiteTimer.Stop();
            _blackTimer.Stop();

            GameOverMenu gameOverMenu = new GameOverMenu(gameState, whitePlayerName, blackPlayerName);
            MenuContainer.Content = gameOverMenu;

            gameOverMenu.OptionSelected += option =>
            {
                if (option == Option.Restart)
                {
                    MenuContainer.Content = null;
                    RestartGame();
                }
                else
                {
                    Application.Current.Shutdown();
                }
            };
        }

        private void RestartGame()
        {
            selectedPos = null;
            HideHighlights();
            moveCache.Clear();

            // Sử dụng selectedGameTime 
            gameState = new GameState(Player.White, Board.Initial(), TimeSpan.FromMinutes(selectedGameTime), TimeSpan.FromMinutes(selectedGameTime));
            DrawBoard(gameState.Board);
            setcursor(gameState.CurrentPlayer);

            // ✅ Reset thời gian
            _whiteTime = TimeSpan.FromMinutes(selectedGameTime);
            _blackTime = TimeSpan.FromMinutes(selectedGameTime);
            WhiteTimerRun.Text = $"{_whiteTime:mm\\:ss}";
            BlackTimerRun.Text = $"{_blackTime:mm\\:ss}";

            // ✅ Đảm bảo tên người chơi vẫn hiển thị
            WhitePlayerName.Text = whitePlayerName;
            BlackPlayerName.Text = blackPlayerName;

            // Ẩn cảnh báo chiếu tướng khi bắt đầu lại game
            WhiteCheckWarning.Visibility = Visibility.Collapsed;
            BlackCheckWarning.Visibility = Visibility.Collapsed;

            // ✅ Bắt đầu bộ đếm thời gian
            SwitchTurn(true);
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!IsMenuOnScreen() && e.Key == Key.Escape)
            {
                ShowPauseMenu();
            }
        }

        private void ShowPauseMenu()
        {
            //dừng 2 bộ đếm thời gian
            _whiteTimer.Stop();
            _blackTimer.Stop();

            PauseMenu pauseMenu = new PauseMenu();
            MenuContainer.Content = pauseMenu;

            pauseMenu.OptionSelected += option =>
            {
                MenuContainer.Content = null;

                if (option == Option.Restart)
                {
                    RestartGame();
                }
                else
                {
                    //Tiếp tục bộ đếm thời gian nếu không chọn chơi lại
                    SwitchTurn(gameState.CurrentPlayer == Player.White);
                }
            };
        }

        private void SwitchTurn(bool isWhiteTurn)
        {
            if (isWhiteTurn)
            {
                _blackTimer.Stop(); //tgian quân đen tạm dừng
                _whiteTimer.Start(); //tgian quân trắng chạy
            }
            else
            {
                _whiteTimer.Stop(); //tgian quân đen tạm dừng
                _blackTimer.Start(); //tgian quân trắng chạy
            }
        }

        private void Emoji_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image clickedEmoji)
            {
                PlayEmojiAnimation(clickedEmoji);
            }
        }

        // Tạo hiệu ứng phóng to thu nhỏ khi nhấn icon
        private void PlayEmojiAnimation(Image emoji)
        {
            DoubleAnimation scaleAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 3,
                Duration = TimeSpan.FromMilliseconds(500),
                AutoReverse = true
            };

            // Đặt điểm gốc cho transform để phóng to đều theo trung tâm
            emoji.RenderTransformOrigin = new Point(0.5, 0.5);

            // Áp dụng ScaleTransform nếu chưa có
            if (emoji.RenderTransform is not ScaleTransform)
            {
                emoji.RenderTransform = new ScaleTransform(1, 1);
            }

            // Gán animation cho cả chiều X và Y
            (emoji.RenderTransform as ScaleTransform).BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            (emoji.RenderTransform as ScaleTransform).BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
        }

        private void ShowPlayerNameWindow()
        {
            PlayerNameWindow nameForm = new PlayerNameWindow();
            if (nameForm.ShowDialog() == true)
            {
                // Lưu tên người chơi
                whitePlayerName = nameForm.PlayerWhiteName;
                blackPlayerName = nameForm.PlayerBlackName;
                selectedGameTime = nameForm.SelectedGameTime;

                // ✅ Hiển thị tên người chơi trong TextBlock
                WhitePlayerName.Text = whitePlayerName;
                BlackPlayerName.Text = blackPlayerName;

                // Khởi tạo thời gian với giá trị đã chọn
                _whiteTime = TimeSpan.FromMinutes(selectedGameTime);
                _blackTime = TimeSpan.FromMinutes(selectedGameTime);

                // ✅ Hiển thị thời gian ban đầu

                WhiteTimerRun.Text = $"{_whiteTime:mm\\:ss}";
                BlackTimerRun.Text = $"{_blackTime:mm\\:ss}";
            }
            else
            {
                Close(); // Đóng game nếu không nhập tên
            }
        }

        private void PlayMoveSound()
        {
            try
            {
                //Thay bằng đường truyển tuyệt đối
                string soundPath = @"C:\RunPlayChess\RunChess\RunChess\Chess\ChessUI\Assets\sound_move.wav";
                moveSoundPlayer.Open(new Uri(soundPath, UriKind.Absolute));
                moveSoundPlayer.Play();
            }
            catch (Exception ex)
            {
            }
        }

        private void UpdateCheckWarnings()
        {
            // Kiểm tra và hiển thị cảnh báo cho quân trắng
            WhiteCheckWarning.Visibility = gameState.IsWhiteInCheck ? Visibility.Visible : Visibility.Collapsed;

            // Kiểm tra và hiển thị cảnh báo cho quân đen
            BlackCheckWarning.Visibility = gameState.IsBlackInCheck ? Visibility.Visible : Visibility.Collapsed;
        }

    }
}
