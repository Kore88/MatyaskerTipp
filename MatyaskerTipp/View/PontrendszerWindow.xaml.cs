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
    /// Interaction logic for PontrendszerWindow.xaml
    /// </summary>
    public partial class PontrendszerWindow : Window
    {
        private Contest contest;
        public PontrendszerWindow()
        {
            InitializeComponent();
            contest = new Contest();
            cbxBajnoksagok.ItemsSource = contest.GetAllContestName();
        }

        private void btnModositas_Click(object sender, RoutedEventArgs e)
        {
            tbxHDVpont.IsEnabled = true;
        }

        private void btnJovahagyas_Click(object sender, RoutedEventArgs e)
        {
            tbxHDVpont.IsEnabled = false;
            if (cbxBajnoksagok.SelectedIndex != -1)
            {
                string selectedContestName = (string)cbxBajnoksagok.SelectedItem;
                Contest contest = new Contest();
                int selectedContestId = contest.GetContestIdByName(selectedContestName);

                ScoringRules scoringRules = new ScoringRules();
                ScoringRules retrievedRules = scoringRules.GetContestRules(selectedContestId);

                try
                {
                    MySqlConnection conn = new MySqlConnection(MySqlConn.connection);
                    conn.Open();
                    MySqlCommand cmd;

                    if (retrievedRules != null)
                    {
                        cmd = new MySqlCommand(SqlCommans.updateScoringRule, conn);

                        cmd.Parameters.AddWithValue("@description", lbHDV.Content);
                        cmd.Parameters.AddWithValue("@points", int.Parse(tbxHDVpont.Text));
                        cmd.Parameters.AddWithValue("@contestId", selectedContestId);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Sikeres frissítés!");
                    }
                    else
                    {
                        cmd = new MySqlCommand(SqlCommans.instertIntoScoringRules, conn);
   
                        cmd.Parameters.AddWithValue("@description", lbHDV.Content);
                        cmd.Parameters.AddWithValue("@points", int.Parse(tbxHDVpont.Text));
                        cmd.Parameters.AddWithValue("@contestId", selectedContestId);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Sikeres hozzáadás!");
                    }

                        conn.Close();
                }
                catch (Exception ex) {MessageBox.Show(ex.Message);}
            }
        }

        private void cbxBajnoksagok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxBajnoksagok.SelectedIndex != -1)
            {
                string selectedContestName = (string)cbxBajnoksagok.SelectedItem;
                Contest contest = new Contest();
                int selectedContestId = contest.GetContestIdByName(selectedContestName);



                ScoringRules scoringRules = new ScoringRules();
                ScoringRules retrievedRules = scoringRules.GetContestRules(selectedContestId);

                if (retrievedRules != null)
                {
                    lbHDV.Content = retrievedRules.Desciption;
                    tbxHDVpont.Text = retrievedRules.Points.ToString();
                }
                else
                {
                    MessageBox.Show("Még nincs hozzáadva pontszerzési lehetőség a bajnoksághoz!");
                }
            }
        }

        private void btnMegse_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
