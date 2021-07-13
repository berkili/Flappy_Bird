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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Flappy_Bird
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Bu fonksiyon Oyna isimli butona bastığımızda GameScreen'i ekrana getirir ve Ana Menüyü kapatır.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GameScreen gameScreen = new GameScreen();
            gameScreen.Show();
            this.Close();
        }

        /// <summary>
        /// Bu fonksiyon Liderlik Tablosu isimli butona bastığımızda Leaderboard'ı ekrana getirir ve Ana Menüyü kapatır.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void leaderboardButton_Click(object sender, RoutedEventArgs e)
        {
            Leaderboard leaderboard = new Leaderboard();
            leaderboard.Show();
            this.Close();
        }

        /// <summary>
        /// Bu fonksiyon Çıkış isimli butona bastığımızda uygulamayı kapatır.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
