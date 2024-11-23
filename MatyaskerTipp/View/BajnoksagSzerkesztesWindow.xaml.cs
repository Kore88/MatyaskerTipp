using MatyaskerTipp.Model;
using MatyaskerTipp.MySQL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private string selectedMatch;
        private ObservableCollection<string> aktivMatches = new ObservableCollection<string>();
        private ObservableCollection<string> inaktivMatches = new ObservableCollection<string>();
        public BajnoksagSzerkesztesWindow(int idx)
        {
            InitializeComponent();
            try
            {
                MySqlConnection conn = new MySqlConnection(MySqlConn.connection);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(SqlCommans.selectedContest, conn);
                cmd.Parameters.AddWithValue("@idx", idx);

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
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            lbContest.Content = contest.Name;


            match = new Match();
            var notAvailableMatches = match.GetAllNotAviableMatches();
            foreach (var match in notAvailableMatches)
            {
                inaktivMatches.Add(match);
            }

            var availableMatches = match.GetAllAviableMatches();
            foreach (var match in availableMatches)
            {
                aktivMatches.Add(match);
            }

            lbxAktiv.ItemsSource = aktivMatches;
            lbxInaktiv.ItemsSource = inaktivMatches;
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

        private void btnEltavolitas_Click(object sender, RoutedEventArgs e)
        {
            if (lbxAktiv.SelectedItem != null)
            {
                string selectedMatch = lbxAktiv.SelectedItem.ToString();
                string[] matchDetails = selectedMatch.Split(' ');  // Assuming format "matchId homeName VS guestName"
                int matchId = Convert.ToInt32(matchDetails[0]);  // Extract the matchId from the string

                try
                {

                    MySqlConnection conn = new MySqlConnection(MySqlConn.connection);
                    conn.Open();

                    // Query to delete the match from the contest (InContest table)
                    string query = "DELETE FROM InContest WHERE ContestId = @contestId AND MatchId = @matchId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@contestId", contest.Id);  // Use the contest.Id from the form
                    cmd.Parameters.AddWithValue("@matchId", matchId);  // Use the matchId from the selected match
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    // Remove the match from the ObservableCollection (list view)
                    aktivMatches.Remove(lbxAktiv.SelectedItem.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error removing match from contest: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a match to remove.");
            }
        }

        private void btnAktivalas_Click(object sender, RoutedEventArgs e)
        {
            if (lbxInaktiv.SelectedItem != null)
            {
                string homeName = "";
                string guestName = "";
                string selectedMatch = lbxInaktiv.SelectedItem.ToString();

                try
                {
                    int firstSpaceIndex = selectedMatch.IndexOf(' ');
                    if (firstSpaceIndex == -1)
                    {
                        MessageBox.Show("Invalid match format.");
                        return;
                    }

                    string matchIdString = selectedMatch.Substring(0, firstSpaceIndex).Trim();
                    if (!int.TryParse(matchIdString, out int matchId))
                    {
                        MessageBox.Show("Invalid match ID.");
                        return;
                    }

                    string remainingString = selectedMatch.Substring(firstSpaceIndex + 1).Trim();
                    string[] teams = remainingString.Split(new string[] { " VS " }, StringSplitOptions.None);

                    if (teams.Length == 2)
                    {
                        homeName = teams[0].Trim();
                        guestName = teams[1].Trim();
                    }
                    else
                    {
                        MessageBox.Show("Invalid match format. Expected 'Home VS Guest'.");
                        return;
                    }

                    MySqlConnection conn = new MySqlConnection(MySqlConn.connection);
                    conn.Open();

                    MySqlCommand insertCmd = new MySqlCommand(SqlCommans.insertInContest, conn);
                    insertCmd.Parameters.AddWithValue("@contestId", contest.Id);
                    insertCmd.Parameters.AddWithValue("@matchId", matchId);
                    insertCmd.ExecuteNonQuery();

                    MySqlCommand updateMatchCmd = new MySqlCommand(SqlCommans.updateMatchQuery, conn);
                    updateMatchCmd.Parameters.AddWithValue("@matchId", matchId);
                    updateMatchCmd.ExecuteNonQuery();

                    conn.Close();

                    // Update ObservableCollections instead of directly manipulating ListBox.Items
                    inaktivMatches.Remove(selectedMatch); // Removes from ObservableCollection
                    aktivMatches.Add(selectedMatch);     // Adds to ObservableCollection
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error activating match: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a match to activate.");
            }
        }

        private int GetMatchId(string homeName, string guestName)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(MySqlConn.connection);
                conn.Open();

                
                MySqlCommand cmd = new MySqlCommand(SqlCommans.selectMatchId, conn);
                cmd.Parameters.AddWithValue("@homeName", homeName);
                cmd.Parameters.AddWithValue("@guestName", guestName);

                var result = cmd.ExecuteScalar();
                conn.Close();

                return result != null ? Convert.ToInt32(result) : 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching match ID: " + ex.Message);
                return 0;
            }
        }
    }
}
