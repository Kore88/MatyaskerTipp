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
    /// Interaction logic for TabellaWindow.xaml
    /// </summary>
    public partial class TabellaWindow : Window
    {
        private Contest contest;
        private List<string> userNames;


        public TabellaWindow()
        {
            InitializeComponent();

            contest = new Contest();
            cbxTabella.ItemsSource = contest.GetAllContestName();
            

        }

        private void cbxTabella_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxTabella.SelectedIndex != -1)
            {
                userNames = new List<string>();
                try
                {
                    List<ContestPlayer> contestPlayers = new List<ContestPlayer>();

                    MySqlConnection conn = new MySqlConnection(MySqlConn.connection);
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(SqlCommans.selectContestUsers, conn);
                    cmd.Parameters.AddWithValue("@selectedContest", cbxTabella.SelectedItem.ToString());
                    MySqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        contestPlayers.Add(new ContestPlayer
                        {
                            RealName = dr.GetString("realName"),
                            Points = dr.GetInt32("points")
                        });
                    }
                    conn.Close();

                    dgTabella.ItemsSource = contestPlayers;
                }
                
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            }

        }
    }
}
