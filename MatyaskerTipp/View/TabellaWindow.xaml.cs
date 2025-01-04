using MatyaskerTipp.Model;
using MatyaskerTipp.MySQL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                try
                {
                    ObservableCollection<ContestPlayer> contestPlayers = new ObservableCollection<ContestPlayer>();
                    List<User> users = new List<User>();

                    MySqlConnection conn = new MySqlConnection(MySqlConn.connection);
                    {
                        conn.Open();

                        MySqlCommand cmd1 = new MySqlCommand(SqlCommans.selectAllUser, conn);
                        MySqlDataReader dr1 = cmd1.ExecuteReader();
                        {
                            while (dr1.Read())
                            {
                                // Populate User object
                                User user = new User
                                {
                                    Id = dr1.GetInt32("id"),
                                    RealName = dr1.GetString("realName"),
                                    IsAdmin = dr1.GetBoolean("isAdmin")
                                };
                                users.Add(user);
                            }
                        }

                        MySqlCommand cmd = new MySqlCommand(SqlCommans.selectContestUsers, conn);
                        cmd.Parameters.AddWithValue("@selectedContest", cbxTabella.SelectedItem.ToString());
                        MySqlDataReader dr = cmd.ExecuteReader();
                        {
                            while (dr.Read())
                            {
                                string realName = dr.GetString("realName");

                                User user = users.FirstOrDefault(u => u.RealName == realName);

                                if (user != null && !user.IsAdmin)
                                {
                                    contestPlayers.Add(new ContestPlayer
                                    {
                                        RealName = realName,
                                        Points = dr.GetInt32("points")
                                    });
                                }
                            }
                        }
                    }

                    dgTabella.ItemsSource = contestPlayers;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }



    }
}

