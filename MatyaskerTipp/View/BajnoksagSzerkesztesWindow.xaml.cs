using MatyaskerTipp.Model;
using MatyaskerTipp.MySQL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
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

namespace MatyaskerTipp.View
{
    /// <summary>
    /// Interaction logic for BajnoksagSzerkesztesWindow.xaml
    /// </summary>
    public partial class BajnoksagSzerkesztesWindow : Window
    {
        private Contest contest;
        private Match match;
        public BajnoksagSzerkesztesWindow(int idx)
        {
            InitializeComponent();
            try
            {
                MySqlConnection conn = new MySqlConnection(MySqlConn.connection);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(SqlCommans.selectedContest, conn);
                cmd.Parameters.AddWithValue("@idx",idx);

                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read()) 
                {
                    contest = new Contest
                    {
                        Id = dr.GetInt32("id"),                
                        Name = dr.GetString("name"),
                        StartDate = dr.GetDateTime("startDate"),
                        EndDate = dr.GetDateTime("endDate")
                    };
                }


            } catch (Exception ex) { MessageBox.Show(ex.Message); }
            lbContest.Content = contest.Name;
            match = new Match();
            lbxInaktiv.ItemsSource = match.GetAllNotAviableMatches();
            lbxAktiv.ItemsSource = match.GetAllAviableMatches();

        }

        private void btnModositas_Click(object sender, RoutedEventArgs e)
        {
            btnEltavolitas.IsEnabled = true;
        }

        private void btnModositas2_Click(object sender, RoutedEventArgs e)
        {
            btnAktivalas.IsEnabled = true;
        }

        private void btnBajnoksagSzerkesztes_Click(object sender, RoutedEventArgs e)
        {
            btnLezaras.Visibility = Visibility.Visible;
            btnInditas.Visibility = Visibility.Visible;
        }
    }
}
