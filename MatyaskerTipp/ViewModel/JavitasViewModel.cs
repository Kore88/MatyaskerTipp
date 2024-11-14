using MatyaskerTipp.Model;
using MatyaskerTipp.MySQL;
using MatyaskerTipp.View;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MatyaskerTipp.ViewModel
{
    public class JavitasViewModel : ObservableObjects
    {
        private Match match;
        public string hname;
        public string gname;
        public JavitasViewModel()
        {
        }
        public void GetMatchByID(int matchId)
        {
            match = new Match();
            try
            {
                MySqlConnection conn = new MySqlConnection(MySqlConn.connection);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(SqlCommans.selectedMatch, conn);
                cmd.Parameters.AddWithValue("@idx", matchId);
                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    match = new Match
                    {
                        Id = dr.GetInt32("id"),
                        HomeName = dr.GetString("homeName"),
                        GuestName = dr.GetString("guestName"),
                        Date = dr.GetDateTime("date"),
                        HomeGoals = dr.GetInt32("homeGoals"),
                        GuestGoals = dr.GetInt32("guestGoals"),
                        IsAvailable = dr.GetBoolean("isAvailable")
                    };
                }

                conn.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            gname = match.GuestName;
            hname = match.HomeName;
        }

        public void Javitas(string home, string guest)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(MySqlConn.connection);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(SqlCommans.updateMatch, conn);
                cmd.Parameters.AddWithValue("@homeGoals", int.Parse(home));
                cmd.Parameters.AddWithValue("@guestGoals", int.Parse(guest));
                cmd.Parameters.AddWithValue("@isAvailable", 0);
                cmd.Parameters.AddWithValue("@id", match.Id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Sikeres javítás!");

                match.GuestGoals = int.Parse(guest);
                match.HomeGoals = int.Parse(home);

                conn.Close();
                Notify();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
