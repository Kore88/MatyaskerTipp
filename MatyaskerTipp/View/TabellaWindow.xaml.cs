﻿using MatyaskerTipp.Model;
using MatyaskerTipp.MySQL;
using MatyaskerTipp.ViewModel;
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
        public ObservableCollection<ContestPlayer> contestPlayers;

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
                contestPlayers = new ObservableCollection<ContestPlayer>();
                try
                {
                    
                    List<User> users = new List<User>();

                    MySqlConnection conn = new MySqlConnection(MySqlConn.connection);
                    {
                        conn.Open();

                        MySqlCommand cmd1 = new MySqlCommand(SqlCommans.selectAllUser, conn);
                        MySqlDataReader dr1 = cmd1.ExecuteReader();
                        
                            while (dr1.Read())
                            {
                                User user = new User
                                {
                                    Id = dr1.GetInt32("id"),
                                    RealName = dr1.GetString("realName"),
                                    IsAdmin = dr1.GetBoolean("isAdmin"),
                                    UserName = dr1.GetString("userName"),
                                };
                                users.Add(user);
                            }
                        conn.Close();
                        conn.Open();

                        MySqlCommand cmd = new MySqlCommand(SqlCommans.selectContestUsers, conn);
                        cmd.Parameters.AddWithValue("@selectedContest", cbxTabella.SelectedItem.ToString());
                        MySqlDataReader dr = cmd.ExecuteReader();
                        
                            while (dr.Read())
                            {
                                string userName = dr.GetString("UserName");
                                string realName = dr.GetString("RealName");

                                User user = users.FirstOrDefault(u => u.RealName == realName && u.UserName == userName);

                                if (user != null && !user.IsAdmin)
                                {
                                ContestPlayer contestPlayer = new ContestPlayer(userName,realName,dr.GetInt32("Points"));
                                contestPlayers.Add(contestPlayer);
                                }
                            }
                        conn.Close();
                    };

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

