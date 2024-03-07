using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lab1_Game
{
    /// <summary>
    /// Логика взаимодействия для PauseWindow.xaml
    /// </summary>
    public partial class PauseWindow : Window
    {
        MainWindow mainWindow;
        private bool isOpen = false;
        public bool IsOpen
        {
            get { return isOpen; }

        }
        public PauseWindow(MainWindow window)
        {
            mainWindow= window;
            InitializeComponent();
        }
        public void SetIsOpen(bool value)
        {
            isOpen = value;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                mainWindow.ResumeGame();
                this.Hide();
                
            }
        }

        private void ContinueBtn_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.ResumeGame();
            this.Hide();
        }

        private void RestartBtn_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.RestartGame();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Exit();
        }

        private void ContinueBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            ContinueBtn.Focus();
        }

        private void RestartBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            RestartBtn.Focus();
        }

        private void ExitBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            ExitBtn.Focus();
        }
    }
}
