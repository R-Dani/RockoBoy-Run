using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class YourRecord : Window
    {
        TimeSpan gameTime;
        public YourRecord(TimeSpan gameTime)
        {
            InitializeComponent();
            this.gameTime = gameTime;
            lbl_ScoreTime.Content = gameTime.ToString(@"hh\:mm\:ss");
        }
        private void btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            string nombre, tiempo;
            nombre = txt_Name.Text;
            tiempo = gameTime.ToString(@"hh\:mm\:ss");
            

            string conexionString = "Data Source=DANIEL-PC;Initial Catalog=rocko_boy;Integrated Security=True; TrustServerCertificate=True;";
            

            using (SqlConnection conexion = new SqlConnection(conexionString))
            {
                string query = "INSERT INTO game_records(nombre, tiempo) VALUES (@nombre, @tiempo)";
                SqlCommand comando = new SqlCommand(query, conexion);

                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@tiempo", tiempo);

                try
                {
                    conexion.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Datos guardados");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar record" + ex.Message);
                }
            }
        }
    }
}
