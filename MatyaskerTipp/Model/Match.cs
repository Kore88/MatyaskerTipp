using MatyaskerTipp.MySQL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace MatyaskerTipp.Model
{
    internal class Match : ObservableObjects
    {
        private int _GuestGoals;
        private int _HomeGoals;
        public int Id { get; set; }
        public string HomeName { get; set; }
        public string GuestName { get; set; }
        public DateTime Date { get; set; }
        public int HomeGoals {
            get => _HomeGoals;
            set {
                if (_HomeGoals != value)
                {
                    _HomeGoals = value;
                    Notify(nameof(HomeGoals));
                }
            }
        }
        public int GuestGoals
        {
            get => _GuestGoals;
            set
            {
                if (_GuestGoals != value)
                {
                    _GuestGoals = value;
                    Notify(nameof(GuestGoals));
                }
            }
        }
        public bool IsAvailable { get; set; } = false;

        public Match(string homeName, string guestName, DateTime date, int homeGoals, int guestGoals, bool isAvailable)
        {
            HomeName = homeName;
            GuestName = guestName;
            Date = date;
            HomeGoals = homeGoals;
            GuestGoals = guestGoals;
            IsAvailable = isAvailable;
        }

        public Match()
        {
            GuestGoals = -1;
            HomeGoals = -1;
        }

        public List<string> GetAllNotAviableMatches()
        {
            List<string> matches = new List<string>();
            try
            {
                MySqlConnection conn = new MySqlConnection(MySqlConn.connection);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(SqlCommans.selectAllNotAvailableMatch, conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    int matchId = dr.GetInt32("Id");
                    string home = dr.GetString("homeName");
                    string guest = dr.GetString("guestName");

                    string match = $"{matchId} {home} VS {guest}";

                    matches.Add(match);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching not available matches: " + ex.Message);
            }

            return matches;
        }

        public List<string> GetAllAviableMatches()
        {
            List<string> matches = new List<string>();
            try
            {
                MySqlConnection conn = new MySqlConnection(MySqlConn.connection);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(SqlCommans.selectAllAvailableMatch, conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    int matchId = dr.GetInt32("Id");
                    string home = dr.GetString("homeName");
                    string guest = dr.GetString("guestName");

                    string match = $"{matchId} {home} VS {guest}";

                    matches.Add(match);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching available matches: " + ex.Message);
            }

            return matches;
        }

        public List<string> GetAllNonCheckedMatches()
        {
            List<string> matches = new List<string>();
            try
            {
                MySqlConnection conn = new MySqlConnection(MySqlConn.connection);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(SqlCommans.selectAllNonCheckedMatch, conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string id = dr.GetInt32("id").ToString();
                    string home = dr.GetString("homeName");
                    string guest = dr.GetString("guestName");

                    string match =id+"\t"+ home + "  VS  " + guest;

                    matches.Add(match);
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }



            return matches;
        }


    }
}
