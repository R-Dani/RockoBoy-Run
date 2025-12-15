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

namespace RockoBoy_Run
{
    /// <summary>
    /// Lógica de interacción para Select_Level.xaml
    /// </summary>
    public partial class Select_Level : Window
    {
        public Select_Level()
        {
            InitializeComponent();
        }

        private void btn_Normal_Click(object sender, RoutedEventArgs e)
        {
            MainWindow2 mainWindow2 = new MainWindow2();
            mainWindow2.Show();
            this.Close();
        }

        private void btn_Dificil_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
