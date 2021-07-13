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
using System.Windows.Threading;
using Business_Layer.Functions;
using Entities_Layer.Concrete;

namespace Flappy_Bird
{
    /// <summary>
    /// Interaction logic for GameScreen.xaml
    /// </summary>
    public partial class GameScreen : Window
    {
                
        /// <summary>
        /// DispatcherTime classından gamerTimer adında yeni bir obje oluşturuldu.
        /// </summary>
        DispatcherTimer gameTimer = new DispatcherTimer();        
        /// <summary>
        /// Yerçekiminin sayısal değerini tutar.
        /// </summary>
        int gravity = 8;
        /// <summary>
        /// Skoru tutar.
        /// </summary>
        double score;
        /// <summary>
        /// Rect classından collision'ları tutar.
        /// </summary>        
        Rect FlappyRect;
        /// <summary>
        /// Oyunun bitip bitmediğini true false olarak değer tutar.
        /// </summary>
        bool gameover = false;
        /// <summary>
        /// Oyun sona erdiğinde eğer oyuncu verilerini kaydetmek isterse diye username'i yazması gereken modülün
        /// açılıp açılmayacağını true false olarak tutar.
        /// </summary>
        bool hold = false;

        /// <summary>
        /// Bu constructorda oyunu başlatan fonksiyonu çağırıyor ve <see cref="gameTimer"/>'ın tick özelliğine
        /// <see cref="gameEngine(object, EventArgs)"/> fonksiyonu ve <see cref="gameTimer"/>'ın interval özelliğine 20 milisaniye tanımlanıyor.
        /// </summary>
        public GameScreen()
        {
            InitializeComponent();
            
            gameTimer.Tick += gameEngine; 
            gameTimer.Interval = TimeSpan.FromMilliseconds(20); 
            
            startGame();

        }

        /// <summary>
        /// Bu fonksiyon boşluk tuşuna basıldığında ve oyun sona ermemişse bird'in resmini orta pozisyonundan
        /// -20 derece döndürerek ve yerçekimini de -8 yaparak yukarı hareketini sağlar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Tıklanan tuşu parametre alır.</param>
        private void Canvas_KeyisDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && gameover == false)
            {
                flappyBird.RenderTransform = new RotateTransform(-20, flappyBird.Width / 2, flappyBird.Height / 2);
                
                gravity = -8;
            }
        }

        /// <summary>
        /// Bu fonksiyon oyun sona erdiğinde Tekrar Oyna adlı butona tıklandığında oyunu tekrar başlatıcak olan fonksiyonu çağırır.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void restartButton_Click(object sender, RoutedEventArgs e)
        {
            if (gameover == true)
            {
                startGame();
            }
        }

        /// <summary>
        /// Bu fonksiyon oyun sona erdiğinde Ana Menü adlı butona tıklandığında ana menüyü açılmasını sağlar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        /// <summary>
        /// Bu fonksiyonda tuşa basılmadığında bird'in resmini orta pozisyondan 5 derece döndürerek ve
        /// yerçekimini 8 yaptığımızda bird aşağı doğru hareket edicek.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_KeyisUp(object sender, KeyEventArgs e)
        {
            flappyBird.RenderTransform = new RotateTransform(5, flappyBird.Width / 2, flappyBird.Height / 2);
           
            gravity = 8;
        }

        /// <summary>
        /// Bu fonksiyon oyunu başlatmak için değerleri default haline getirir.
        /// </summary>
        private void startGame()
        {

            
            int temp = 200;
            
            score = 0;
            
            Canvas.SetTop(flappyBird, 100);
            
            gameover = false;
            
            CheckVisibility();

            //Bu döngüde bu oyundaki her resmi kontrol ediyor ve onları varsayılan konumlarına ayarlıyor.
            foreach (var x in GameCanvas.Children.OfType<Image>())
            {
                
                if (x is Image && ((string)x.Tag == "obs1-top" || (string)x.Tag == "obs1-bot"))
                {
                    Canvas.SetLeft(x, 500);
                }
                
                if (x is Image && ((string)x.Tag == "obs2-top" || (string)x.Tag == "obs2-bot"))
                {
                    Canvas.SetLeft(x, 800);
                }
                
                if (x is Image && ((string)x.Tag == "obs3-top" || (string)x.Tag == "obs3-bot"))
                {
                    Canvas.SetLeft(x, 1100);
                }
                
                if (x is Image && (string)x.Tag == "clouds")
                {
                    Canvas.SetLeft(x, (300 + temp));
                    temp = 800;
                }

            }
            
            gameTimer.Start();

        }

        /// <summary>
        /// Bu fonksiyon boruların isimlerini parametre alarak random borular üretir.
        /// </summary>
        /// <param name="pipeTag">Boruların isimlerini parametre alır.</param>
        private void ResetPipe(string pipeTag)
        {
            Random random = new Random();
            double offset = 150;
            double distanceBetweenPipes = 100;

            double minY = offset;
            double maxY = GameCanvas.ActualHeight - offset;
            double randomPoint = random.NextDouble() * (maxY-minY) + minY;
            //Bu döngüde canvasın içinde bulunan her resmi alıp bunların boru olup olmadğını kontrol ettirerek belirli aralıklarla random konumlara boru konulmasını sağlar.
            foreach(var pipe in GameCanvas.Children.OfType<Image>())
            {
                if (pipe.Tag != null && pipe.Tag.ToString().StartsWith(pipeTag))
                {
                    bool isTop = pipe.Tag.ToString().Split("-")[1] == "top";

                    double newY = isTop ? randomPoint - distanceBetweenPipes / 2 - pipe.Height : randomPoint + distanceBetweenPipes / 2;
                    Canvas.SetTop(pipe, newY);
                    Canvas.SetLeft(pipe, isTop ? 805 : 800);
                }
            }
        }

        /// <summary>
        /// Bu fonksiyonda oyunun nerelerde sona ereceğini, nasıl skorlarının hesaplanacağını ve boruların random olarak yerleşmesini sağlayan fonksiyonları çağırma işlevlerinin yapıldığı yerdir.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameEngine(object sender, EventArgs e)
        {            
            scoreText.Content = "Score: " + score;
            
            FlappyRect = new Rect(Canvas.GetLeft(flappyBird), Canvas.GetTop(flappyBird), flappyBird.Width - 12, flappyBird.Height - 6);
            
            Canvas.SetTop(flappyBird, Canvas.GetTop(flappyBird) + gravity);

            // Bird ekranın dışına çıktığı zaman Failure fonksiyonunu çağırır ve Save buttonunu enable haline getirir.
            if (Canvas.GetTop(flappyBird) + flappyBird.Height > 490 || Canvas.GetTop(flappyBird) < -30)
            {
                Failure();
                openSaveButton.IsEnabled = true;
            }

            //Bu döngüde canvasın içinde bulunan her resmi alıp bunlar boru ise isimlerini kontrol ettirerek random olarak oluşmasını, collisionların ayarını ve skorları hesaplar.
            //Eğer canvas içindeki resimler bulut ise onların ekran hareket ettikçe aynı konumlarında kalmalarını sağlar
            foreach (var x in GameCanvas.Children.OfType<Image>())
            {
                if (x.Tag != null && (x.Tag.ToString().StartsWith("obs1-") || x.Tag.ToString().StartsWith("obs2-") || x.Tag.ToString().StartsWith("obs3-")))
                {
                                       

                    Canvas.SetLeft(x, Canvas.GetLeft(x) - 5);

                    Rect pillars = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (FlappyRect.IntersectsWith(pillars))
                    {
                        Failure();
                        openSaveButton.IsEnabled = true;

                    }
                    else
                    {
                        if (Canvas.GetLeft(x) < -50)
                        {
                            ResetPipe(x.Tag.ToString().Split("-")[0]);
                            score = score + 1;

                        }
                    }
                }
                else { 
                    if (x.Tag != null &&(string)x.Tag == "clouds")
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - .6);


                        if (Canvas.GetLeft(x) < -220)
                        {
                            Canvas.SetLeft(x, 550);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Bu fonksiyonda Kaydet butonuna basıldığında hold değişkenini true yaparak nickname girilen paneli aktifleştirir.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openSaveButton_Click(object sender, RoutedEventArgs e)
        {
            hold = true;
            CheckVisibility();
        }

        /// <summary>
        /// Bu fonksiyon textboxda girilen nickname ile oyundan gelen skoru, Entities katmanındaki Score classında oluşan propertylere bağlıyarak
        /// Data katmanındaki AddData fonksiyonuna göndererek dosyaya kaydedilmesini sağlar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Scores scores = new Scores();
            scores.Score = score;
            scores.Name = adTextBox.Text;
            DataManager.AddData(scores);

            hold = false;
            openSaveButton.IsEnabled = false;
            CheckVisibility();
        } 

        /// <summary>
        /// Bu fonksiyon oyun bittiğinde gameover panelini yada gameover panelinden Skor Kaydet butonuna basıldığında da nickname girilen diğer paneli açan kodları barındırır.
        /// </summary>
        private void CheckVisibility()
        {
            if (gameover == true)
            {
                GameOverCanvas.Visibility = Visibility.Visible;

                SaveCanvas.Visibility = Visibility.Hidden;
            }
            if (gameover == true && hold == true)
            {
                GameOverCanvas.Visibility = Visibility.Hidden;

                SaveCanvas.Visibility = Visibility.Visible;
            }
            if (gameover == false)
            {
                GameOverCanvas.Visibility = Visibility.Hidden;

                SaveCanvas.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Bu fonksiyon oyun belli durumlardan dolayı sona erdiğinde yapılacak olan işlemleri içerir.
        /// </summary>
        private void Failure()
        {
            gameTimer.Stop();
            gameover = true;
            CheckVisibility();
            score1Text.Content = "Score: " + score;
        }
    }
}
