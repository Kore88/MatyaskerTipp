using MatyaskerTipp.Model;
using MatyaskerTipp.MySQL;
using MySql.Data.MySqlClient;
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

namespace MatyaskerTipp.View
{
    /// <summary>
    /// Interaction logic for BajnoksagWindow.xaml
    /// </summary>
    public partial class BajnoksagWindow : Window
    {
        Contest contest;

        public BajnoksagWindow()
        {
            InitializeComponent();
            contest = new Contest();
            cbxBajnoksagok.ItemsSource = contest.GetAllContestName();
        }

        private void btnSzerkesztes_Click(object sender, RoutedEventArgs e)
        {
            if (cbxBajnoksagok.SelectedIndex != -1)
            {
                BajnoksagSzerkesztesWindow window = new BajnoksagSzerkesztesWindow(cbxBajnoksagok.SelectedIndex+1);
                window.Show();
            }

        }

        private void btnUjBajnoksag_Click(object sender, RoutedEventArgs e)
        {
            UjBajknoksagWindow window = new UjBajknoksagWindow();
            window.Show();
        }

        private void cbxBajnoksagok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxBajnoksagok.SelectedIndex != -1)
            {
                string selectedContest = cbxBajnoksagok.SelectedItem.ToString();
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
                using (MySqlConnection conn = new MySqlConnection(MySqlConn.connection))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(SqlCommans.selectMatchesByContest, conn))
                    {
                        cmd.Parameters.AddWithValue("@contestName", contestName);

                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
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
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt a meccsek betöltése során: {ex.Message}", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return matches;
        }
    }
}
