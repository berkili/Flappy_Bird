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
using Business_Layer.Functions;

namespace Flappy_Bird
{
    /// <summary>
    /// Interaction logic for Leaderboard.xaml
    /// </summary>
    public partial class Leaderboard : Window
    {
        /// <summary>
        /// Bu constructor leaderboard'ın içindeki datagride <see cref="LeadboardManagment.Leadboards"/> fonksiyonunu
        /// çağırarak tabloya veri ekler.
        /// </summary>
        public Leaderboard()
        {            
            InitializeComponent();
            //DataManager'ın içinde bulunan GetScoreList'i çağırmamın amacı ise dosyada bulunan tüm verileri çekmek.
            DataManager.GetScoreList();
            leaderboard.ItemsSource = LeadboardManagment.Leadboards();
        }

        /// <summary>
        /// Bu fonksiyon Ana Menü adındaki butona tıklandığında Ana Menüye dönülmesini sağlar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
