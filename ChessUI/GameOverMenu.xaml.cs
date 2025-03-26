using ChessLogic;
using System.Windows;
using System.Windows.Controls;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class GameOverMenu : UserControl
    {
        public event Action<Option> OptionSelected;

        private string whitePlayerName;
        private string blackPlayerName;

        public GameOverMenu(GameState gamestate, string whitePlayer, string blackPlayer)
        {
            InitializeComponent();

            whitePlayerName = whitePlayer;
            blackPlayerName = blackPlayer;

            Result result = gamestate.Result;

            WinnerText.Text = GetWinnerText(result.Winner);
            ReasonText.Text = GetReasonText(result.Reason, gamestate.CurrentPlayer);
        }

        private string GetWinnerText(Player winner)
        {
            return winner switch
            {
                Player.White => $"{whitePlayerName} THẮNG!",
                Player.Black => $"{blackPlayerName} THẮNG!",
                _ => "HÒA"
            };
        }

        private static string GetReasonText(EndReason reason, Player currentPlayer)
        {
            return reason switch
            {
                EndReason.Stalemate => $"HẾT NƯỚC ĐI",
                EndReason.Checkmate => $"CHIẾU TƯỚNG",
                EndReason.FiftyMoveRule => "LUẬT 50 NƯỚC ĐI",
                EndReason.InsufficientMaterial => "KHÔNG ĐỦ QUÂN CHIẾU TƯỚNG",
                EndReason.ThreefoldRepetition => "LẶP LẠI THẾ CỜ 3 LẦN",
                EndReason.Timeout => "ĐỐI THỦ HẾT THỜI GIAN",
                _ => ""
            };
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Restart);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Exit);
        }
    }
}
