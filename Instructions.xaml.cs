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
    /// Lógica de interacción para Instructions.xaml
    /// </summary>
    public partial class Instructions : Window
    {
        public Instructions()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Start start = new Start();
            this.Close();
            start.Show();
        }
    }
}
