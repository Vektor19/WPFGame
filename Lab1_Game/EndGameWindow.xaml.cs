using System.Windows;
using System.Windows.Input;

namespace Lab1_Game
{
    public partial class EndGameWindow : Window
    {
        MainWindow mainWindow;
        private bool isOpen = false;
        public bool IsOpen
        {
            get { return isOpen; }

        }
        public EndGameWindow(MainWindow window)
        {
            mainWindow = window;
            InitializeComponent();
        }
        public void SetIsOpen(bool value)
        {
            isOpen = value;
        }
        private void RestartBtn_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.RestartGame();
        }
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Exit();
        }
        private void RestartBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            RestartBtn.Focus();
        }
        private void ExitBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            ExitBtn.Focus();
        }
        public void PutScore(long score)
        {
            ScoreText.Text = "Score: " + score.ToString();
        }
    }
}
