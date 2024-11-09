using MatyaskerTipp.Model;
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
    }
}
