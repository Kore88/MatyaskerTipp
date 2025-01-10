using MatyaskerTipp.Model;
using MatyaskerTipp.MySQL;
using MySql.Data.MySqlClient;
using MatyaskerTipp.ViewModel;
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
using K4os.Compression.LZ4.Internal;

namespace MatyaskerTipp.View
{
    /// <summary>
    /// Interaction logic for BajnoksagWindow.xaml
    /// </summary>
    public partial class BajnoksagWindow : Window
    {
        private BajnoksagViewModel bvm;
        string selectedContest;

        public BajnoksagWindow()
        {
            InitializeComponent();
            bvm = new BajnoksagViewModel();
            bvm.PropertyChanged += PropertyChanged;
            bvm.items = new Contest().GetAllContestName();
            cbxBajnoksagok.ItemsSource = bvm.items;
        }

        private void btnSzerkesztes_Click(object sender, RoutedEventArgs e)
        {
            if (cbxBajnoksagok.SelectedIndex != -1)
            {
                bvm.BajnoksagSzerkesztes(cbxBajnoksagok.SelectedIndex + 1, this.Left, this.Width, this.Top, this.Height);
            }

        }

        private void btnUjBajnoksag_Click(object sender, RoutedEventArgs e)
        {
            bvm.UjBajnoksag(this.Left, this.Width, this.Top, this.Height);
        }

        private void cbxBajnoksagok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxBajnoksagok.SelectedIndex != -1)
            {
                selectedContest = cbxBajnoksagok.SelectedItem.ToString();
                var matches = GetMatchesByContest(selectedContest)
                    .Select(m => $"{m.HomeName} vs {m.GuestName}").ToList();

                lbxMeccsek.ItemsSource = matches;
            }
        }
        private List<Match> GetMatchesByContest(string contestName)
        {
            List<Match> matches = new List<Match>();

            try
            {
                MySqlConnection conn = new MySqlConnection(MySqlConn.connection);
                
                    conn.Open();

                MySqlCommand cmd = new MySqlCommand(SqlCommans.selectMatchesByContest, conn);
                    
                        cmd.Parameters.AddWithValue("@contestName", contestName);

                MySqlDataReader dr = cmd.ExecuteReader();
                        
                            while (dr.Read())
                            {
                                matches.Add(new Match
                                {
                                    Id = dr.GetInt32("id"),
                                    HomeName = dr.GetString("homeName"),
                                    GuestName = dr.GetString("guestName"),
                                    Date = dr.GetDateTime("date")
                                });
                            }
                            conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt a meccsek betöltése során: {ex.Message}", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return matches;
        }

        private void PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var matches = GetMatchesByContest(selectedContest)
                .Select(m => $"{m.HomeName} vs {m.GuestName}").ToList();
            lbxMeccsek.ItemsSource = matches;
            bvm.items.Clear();
            bvm.items = new Contest().GetAllContestName();
            cbxBajnoksagok.ItemsSource = bvm.items;
            this.UpdateLayout();
        }
    }
}
