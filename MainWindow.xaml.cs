using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Threading;
using System.Diagnostics;

namespace RockoBoy_Run
{
    public partial class MainWindow : Window
    {
        //Reloj del juego
        DispatcherTimer gameTimer = new DispatcherTimer();
        //Hitboxes
        Rect rockoHitBox;
        Rect streetHitBox;
        Rect enemy1HitBox;
        Rect enemy2HitBox;
        Rect enemy3HitBox;
        //Funciones de rocko
        bool jumping;
        bool slide;
        bool inStreet;
        bool gameOver = false;
        //Valores para el salto/jumping
        int jumpCount = 2;
        int force = 20;
        int speed = 0;
        //Animaciones
        double spritesIndex = 0;
        double enemySpriteIndex = 0;
        double gameSpeed = 12.0;
        //Random
        Random rnd = new Random();
        //Tiempo para record
        Stopwatch scoreTime = new Stopwatch();
        //Sprites del juego
        ImageBrush rockoSprite = new ImageBrush();
        ImageBrush city1Sprite = new ImageBrush();
        ImageBrush street1Sprite = new ImageBrush();
        ImageBrush enemy1Sprite = new ImageBrush();
        ImageBrush enemy2Sprite = new ImageBrush();
        ImageBrush enemy3Sprite = new ImageBrush();
        //Pocisiones aleatorias del enemigo
        int[] enemy1Position = { 1600, 2000, 2400, 2800, 3200};
        int[] enemy2Position = { 2200, 2600, 3100, 3500};
        int[] enemy3Position = { 3000, 3400, 3900, 4300, 4700 };
        int[] enemy3PositionUp = { 375, 300, 250, 200, 150 };

        public MainWindow()
        {
            InitializeComponent();
            //Detecta las teclas
            myCanvas.Focus();
            //Configura reloj del juego
            gameTimer.Tick += GameEngine;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            //Imagen de city y street
            city1Sprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/City/City_1.png"));
            city1.Fill = city1Sprite;
            city2.Fill = city1Sprite;
            street1Sprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/City/Street.png"));
            street1.Fill = street1Sprite;
            street2.Fill = street1Sprite;
            //Comenzar juego
            StartGame();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            //Tecla up para que salte
            if (e.Key == Key.Up && jumpCount > 0)
            {
                slide = false;
                rocko.Height = 145;
                jumping = true;
                force = 20;
                speed = -3;
                jumpCount--;
                //Cambiar sprites de salto
                if (jumpCount == 1)
                {
                    rockoSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Rocko/Rocko_Jump.png"));
                }
                else
                {
                    rockoSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Rocko/Rocko_Jump_2.png"));
                }
            }
            // Tecla down para que se deslize
            if (e.Key == Key.Down && !inStreet && !slide)
            {
                slide = true;
                rockoSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Rocko/Rocko_Slide.png"));
                rocko.Height = 100;
                Canvas.SetTop(rocko, Canvas.GetTop(street3) - rocko.Height);
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            //Detecta cuando se suelta la tecla down
            if (e.Key == Key.Down)
            {
                if (slide)
                {
                    slide = false;
                    RunSprite(spritesIndex);
                    rocko.Height = 145;
                    Canvas.SetTop(rocko, Canvas.GetTop(street3) - rocko.Height);
                }
               
            }
        }

        private void StartGame()
        {
            //Pocisiones iniciales de rocko, enemies, city y street
            Canvas.SetLeft(city1, 0);
            Canvas.SetLeft(city2, 1150);

            Canvas.SetLeft(street1, 0);
            Canvas.SetLeft(street2, 1300);

            Canvas.SetLeft(rocko, 200);
            Canvas.SetTop(rocko, 300);

            Canvas.SetLeft(enemy1, 1600);
            Canvas.SetTop(enemy1, 383);

            Canvas.SetLeft(enemy2, 2200);
            Canvas.SetTop(enemy2, 470);

            Canvas.SetLeft(enemy3, 3000);
            Canvas.SetTop(enemy3, 375);
            //Animacion de rocko
            RunSprite(1);
            //Sprites de enemies
            enemy1Sprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Enemy/Enemy_1.png"));
            enemy1.Fill = enemy1Sprite;
            Enemy_2Sprite(1);
            enemy3Sprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Enemy/Enemy_3.png"));
            enemy3.Fill = enemy3Sprite;
            //Valores iniciales del juego
            jumping = false;
            gameOver = false;
            gameSpeed = 12.0;
            gameTimer.Start();
            scoreTime.Restart();
            //Ocultar botones restart y back
            restart.Visibility = Visibility.Hidden;
            backStart.Visibility = Visibility.Hidden;
            screen2.Visibility = Visibility.Hidden;
        }

        private void GameOver()
        {
            //Perder el juego
            gameOver = true;
            gameTimer.Stop();
            scoreTime.Stop();
            rockoSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Rocko/Death_1.png"));
            restart.Visibility = Visibility.Visible;
            backStart.Visibility = Visibility.Visible;
            screen2.Visibility = Visibility.Visible;
            
        }

        private void RunSprite(double i)
        {
            //Switch para cambiar frames de rocko para correr
            switch (i)
            {
                case 1:
                    rockoSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Rocko/Rocko_1.png"));
                    break;
                case 2:
                    rockoSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Rocko/Rocko_2.png"));
                    break;
                case 3:
                    rockoSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Rocko/Rocko_3.png"));
                    break;
                case 4:
                    rockoSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Rocko/Rocko_4.png"));
                    break;
                case 5:
                    rockoSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Rocko/Rocko_5.png"));
                    break;
                case 6:
                    rockoSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Rocko/Rocko_6.png"));
                    break;
                case 7:
                    rockoSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Rocko/Rocko_7.png"));
                    break;
                case 8:
                    rockoSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Rocko/Rocko_8.png"));
                    break;
                case 9:
                    rockoSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Rocko/Rocko_9.png"));
                    break;
                case 10:
                    rockoSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Rocko/Rocko_10.png"));
                    break;
                case 11:
                    rockoSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Rocko/Rocko_11.png"));
                    break;
            }
            rocko.Fill = rockoSprite;
        }

        private void Enemy_2Sprite(double j)
        {
            //Switch para cambiar frames de rocko para correr
            switch (j)
            {
                case 1:
                    enemy2Sprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Enemy/Enemy_2_1.png"));
                    break;
                case 2:
                    enemy2Sprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Enemy/Enemy_2_2.png"));
                    break;
                case 3:
                    enemy2Sprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Enemy/Enemy_2_3.png"));
                    break;
                case 4:
                    enemy2Sprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Enemy/Enemy_2_4.png"));
                    break;
                case 5:
                    enemy2Sprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Enemy/Enemy_2_5.png"));
                    break;
                case 6:
                    enemy2Sprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Enemy/Enemy_2_6.png"));
                    break;
                case 7:
                    enemy2Sprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Enemy/Enemy_2_7.png"));
                    break;
                case 8:
                    enemy2Sprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Enemy/Enemy_2_8.png"));
                    break;
                case 9:
                    enemy2Sprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Enemy/Enemy_2_9.png"));
                    break;
                case 10:
                    enemy2Sprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Enemy/Enemy_2_10.png"));
                    break;
                case 11:
                    enemy2Sprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Enemy/Enemy_2_11.png"));
                    break;
                case 12:
                    enemy2Sprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Enemy/Enemy_2_12.png"));
                    break;

            }
            enemy2.Fill = enemy2Sprite;
        }

        private void GameEngine(object sender, EventArgs e)
        {
            //Movimiento de rocko
            Canvas.SetTop(rocko, Canvas.GetTop(rocko) + speed);
            //Movimiento de street
            Canvas.SetLeft(street1, Canvas.GetLeft(street1) - gameSpeed);
            Canvas.SetLeft(street2, Canvas.GetLeft(street2) - gameSpeed);
            //Movimiento de city
            Canvas.SetLeft(city1, Canvas.GetLeft(city1) - (gameSpeed - 11));
            Canvas.SetLeft(city2, Canvas.GetLeft(city2) - (gameSpeed - 11));
            //Movimiento de enemies
            Canvas.SetLeft(enemy1, Canvas.GetLeft(enemy1) - gameSpeed);
            Canvas.SetLeft(enemy2, Canvas.GetLeft(enemy2) - (gameSpeed + 6));
            Canvas.SetLeft(enemy3, Canvas.GetLeft(enemy3) - (gameSpeed + 4));
            //Sprites de enemy2
            enemySpriteIndex += 0.5;
            if (enemySpriteIndex > 12)
            {
                enemySpriteIndex = 1;
            }
            Enemy_2Sprite(enemySpriteIndex);
            //Tiempo record 
            TimeSpan elapsed = scoreTime.Elapsed;
            scoreText.Content = "Time: " + elapsed.ToString(@"hh\:mm\:ss");
            
            if (slide)
            {
                rockoHitBox = new Rect(Canvas.GetLeft(rocko), Canvas.GetTop(rocko), rocko.Width - 50, rocko.Height);
            }
            else
            {
                rockoHitBox = new Rect(Canvas.GetLeft(rocko), Canvas.GetTop(rocko), rocko.Width - 50, rocko.Height);
            }
            //Hitboxes de colision
            streetHitBox = new Rect(Canvas.GetLeft(street3), Canvas.GetTop(street3), street3.Width, street3.Height);
            enemy1HitBox = new Rect(Canvas.GetLeft(enemy1), Canvas.GetTop(enemy1), enemy1.Width - 50, enemy1.Height);
            enemy2HitBox = new Rect(Canvas.GetLeft(enemy2), Canvas.GetTop(enemy2), enemy2.Width - 50, enemy2.Height - 40);
            enemy3HitBox = new Rect(Canvas.GetLeft(enemy3), Canvas.GetTop(enemy3), enemy3.Width, enemy3.Height);
            //Si rocko toca street se reinicia el jumpcount
            if (rockoHitBox.IntersectsWith(streetHitBox))
            {
                inStreet = false;
                speed = 0;
                jumping = false;
                jumpCount = 2;

                Canvas.SetTop(rocko, Canvas.GetTop(street3) - rocko.Height);

                if (!slide)
                {
                    spritesIndex += 0.5;
                    if (spritesIndex > 11)
                    {
                        spritesIndex = 1;
                    }
                    RunSprite(spritesIndex);
                } 
            }
            else
            {
                inStreet = true;
            }
            //Si rocko choca con el enemigo se reproduce el game over
            if (rockoHitBox.IntersectsWith(enemy1HitBox) || rockoHitBox.IntersectsWith(enemy2HitBox) || rockoHitBox.IntersectsWith(enemy3HitBox))
            {
                GameOver();
            }
            //Funcion de saltar 
            if (jumping == true)
            {
                speed--;
                force--;
                if (force <= 0)
                {
                    jumping = false;
                }
            }
            else
            {
                speed = 12;
            }
            //Reaparecer street en pantalla
            if (Canvas.GetLeft(street1) < -2167)
            {
                Canvas.SetLeft(street1, 867);
            }

            if (Canvas.GetLeft(street2) < -2167)
            {
                Canvas.SetLeft(street2, 867);
            }
            //Reaparecer city en pantalla
            if (Canvas.GetLeft(city1) < -2300)
            {
                Canvas.SetLeft(city1, 1150);
            }

            if (Canvas.GetLeft(city2) < -2300)
            {
                Canvas.SetLeft(city2, 1150);
            }
            //Reaparecer enemies en pantalla
            if (Canvas.GetLeft(enemy1) < -200)
            {
                Canvas.SetLeft(enemy1, enemy1Position[rnd.Next(enemy1Position.Length)]);
                Canvas.SetTop(enemy1, 383);
            }

            if (Canvas.GetLeft(enemy2) < -200)
            {
                Canvas.SetLeft(enemy2, enemy2Position[rnd.Next(enemy2Position.Length)]);
                Canvas.SetTop(enemy2, 475);
            }

            if (Canvas.GetLeft(enemy3) < -200)
            {
                Canvas.SetLeft(enemy3, enemy3Position[rnd.Next(enemy3Position.Length)]);
                Canvas.SetTop(enemy3, enemy3PositionUp[rnd.Next(enemy3PositionUp.Length)]);
            }

            if (gameOver == true)
            {
                TimeSpan finalTime = scoreTime.Elapsed;
                scoreText.Content = "Time: " + finalTime.ToString(@"hh\:mm\:ss");
            }
            //Aumentar velocidad cada 30 segundos
            if (!gameOver)
            {
                gameSpeed = 12.0 + scoreTime.Elapsed.TotalSeconds / 30;

                if (gameSpeed > 30)
                {
                    gameSpeed = 30;
                }
            }

        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }

        private void backStart_Click(object sender, RoutedEventArgs e)
        {
            Start start = new Start();
            this.Close();
            start.Show();
        }
    }
}