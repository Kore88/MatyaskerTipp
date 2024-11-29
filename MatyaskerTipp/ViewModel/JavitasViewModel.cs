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

                // Frissítjük a mérkőzés eredményét
                MySqlCommand updateMatchCmd = new MySqlCommand(SqlCommans.updateMatch, conn);
                updateMatchCmd.Parameters.AddWithValue("@homeGoals", int.Parse(home));
                updateMatchCmd.Parameters.AddWithValue("@guestGoals", int.Parse(guest));
                updateMatchCmd.Parameters.AddWithValue("@isAvailable", 0);
                updateMatchCmd.Parameters.AddWithValue("@id", match.Id);
                updateMatchCmd.ExecuteNonQuery();

                // Kinyerjük a ContestId-t a match.Id alapján
                MySqlCommand selectContestIdCmd = new MySqlCommand("SELECT ContestId FROM matyaskert.incontest WHERE MatchId = @matchId", conn);
                selectContestIdCmd.Parameters.AddWithValue("@matchId", match.Id);
                int contestId = (int)selectContestIdCmd.ExecuteScalar();  // Az első értéket lekérjük

                match.GuestGoals = int.Parse(guest);
                match.HomeGoals = int.Parse(home);

                // Frissítjük a Standings táblát az adott ContestId-ra
                MySqlCommand updateStandingsCmd = new MySqlCommand(SqlCommans.updateStandings, conn);
                updateStandingsCmd.Parameters.AddWithValue("@contestId", contestId); // A contestId átadása
                updateStandingsCmd.ExecuteNonQuery();

                conn.Close();
                MessageBox.Show("Sikeres javítás és pontszám frissítés!");
                Notify();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt a javítás során: " + ex.Message);
            }
        }



    }
}
