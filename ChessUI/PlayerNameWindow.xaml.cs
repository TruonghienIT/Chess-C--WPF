using System.Windows;
using System.Windows.Controls;

namespace ChessUI
{
    public partial class PlayerNameWindow : Window
    {
        public string PlayerWhiteName { get; private set; }
        public string PlayerBlackName { get; private set; }
        public int SelectedGameTime { get; private set; }

        public PlayerNameWindow()
        {
            InitializeComponent();
            // Consider setting default selection if needed
            // GameTimeComboBox.SelectedIndex = 0;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            PlayerBlackName = BlackPlayerName.Text.Trim();
            PlayerWhiteName = WhitePlayerName.Text.Trim();

            // Validate player names
            if (string.IsNullOrEmpty(PlayerBlackName) || string.IsNullOrEmpty(PlayerWhiteName))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên người chơi!",
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validate time selection
            if (GameTimeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn thời gian chơi!",
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Parse selected time
            if (GameTimeComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                if (!int.TryParse(selectedItem.Content.ToString(), out int selectedTime))
                {
                    MessageBox.Show("Thời gian chơi không hợp lệ!",
                        "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                SelectedGameTime = selectedTime;
                DialogResult = true;
                Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void GameTimeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // You could add logic here if needed when selection changes
        }
    }
}