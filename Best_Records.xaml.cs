using Microsoft.Data.SqlClient;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RockoBoy_Run
{
    public partial class Best_Records : Window
    {
        SoundPlayer startSound = new SoundPlayer("Assets\\Sounds\\Instructions.wav");
        public Best_Records()
        {
            InitializeComponent();
            DataBase_SQL();
            startSound.PlayLooping();

        }

        private void DataBase_SQL()
        {
            string conexionString = "Data Source=DANIEL-PC;Initial Catalog=rocko_boy;Integrated Security=True; TrustServerCertificate=True;";
            string query = "SELECT TOP 5 nombre, tiempo FROM best_records";

            List<Label> lbl_Players = new List<Label> { lbl_player1, lbl_player2, lbl_player3, lbl_player4, lbl_player5 };
            List<Label> lbl_Times = new List<Label> { lbl_time1, lbl_time2, lbl_time3, lbl_time4, lbl_time5 };

            try
            {
                using (SqlConnection conexion = new SqlConnection(conexionString))
                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    conexion.Open();
                    SqlDataReader reader = comando.ExecuteReader();

                    int index = 0;

                    while (reader.Read() && index < 5)
                    {
                        lbl_Players[index].Content = reader["nombre"].ToString();
                        lbl_Times[index].Content = reader["tiempo"].ToString();
                        index++;
                    }

                    // Si la tabla tiene menos de 5 registros, rellena los demás
                    for (; index < 5; index++)
                    {
                        lbl_Players[index].Content = "AAAA";
                        lbl_Times[index].Content = "00:00:00";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            startSound.Stop();
            Start start = new Start();
            this.Close();
            start.Show();
        }
    }
}
