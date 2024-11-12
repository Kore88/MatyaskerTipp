using MatyaskerTipp.MySQL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MatyaskerTipp.Model
{
    internal class Match
    {
        public int Id { get; set; }
        public string HomeName { get; set; }
        public string GuestName { get; set; }
        public DateTime Date { get; set; }
        public int HomeGoals { get; set; } = -1;
        public int GuestGoals { get; set; } = -1;
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
                    //Match match = new Match
                    //{
                    //    Id = dr.GetInt32("id"),
                    //    HomeName = dr.GetString("homeName"),
                    //    GuestName = dr.GetString("guestName"),
                    //    Date = dr.GetDateTime("date"),
                    //    HomeGoals = dr.GetInt32("homeGoals"),
                    //    GuestGoals = dr.GetInt32("guestGoals"),
                    //    IsAvaliable = dr.GetBoolean("isAvaliable")
                    //};
                    string home = dr.GetString("homeName");
                    string guest = dr.GetString("guestName");

                    string match = home + " " + guest;

                    matches.Add(match);
                }

            }catch (Exception ex) { MessageBox.Show(ex.Message); }
           


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
                    //Match match = new Match
                    //{
                    //    Id = dr.GetInt32("id"),
                    //    HomeName = dr.GetString("homeName"),
                    //    GuestName = dr.GetString("guestName"),
                    //    Date = dr.GetDateTime("date"),
                    //    HomeGoals = dr.GetInt32("homeGoals"),
                    //    GuestGoals = dr.GetInt32("guestGoals"),
                    //    IsAvaliable = dr.GetBoolean("isAvaliable")
                    //};
                    string home = dr.GetString("homeName");
                    string guest = dr.GetString("guestName");

                    string match = home + "  VS  " + guest;

                    matches.Add(match);
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }



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

                    string home = dr.GetString("homeName");
                    string guest = dr.GetString("guestName");

                    string match = home + "  VS  " + guest;

                    matches.Add(match);
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }



            return matches;
        }


    }
}
