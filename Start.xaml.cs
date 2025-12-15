using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

namespace RockoBoy_Run
{
    /// <summary>
    /// Lógica de interacción para Start.xaml
    /// </summary>
    public partial class Start : Window
    {
        SoundPlayer startSound = new SoundPlayer("Assets\\Sounds\\twincraft.wav");
        public Start()
        {
            InitializeComponent();
            startSound.PlayLooping();
        }

        private void HowToPlay_Click(object sender, RoutedEventArgs e)
        {
            startSound.Stop();
            Instructions instructions = new Instructions();
            this.Close();
            instructions.Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            startSound.Stop();
            this.Close();
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            startSound.Stop();
            Select_Level select_Level = new Select_Level();
            select_Level.Show();
            this.Close();
        }

        private void btn_Records_Click(object sender, RoutedEventArgs e)
        {
            startSound.Stop();
            Best_Records bestRecords = new Best_Records();
            this.Close();
            bestRecords.Show();
        }
    }
}
