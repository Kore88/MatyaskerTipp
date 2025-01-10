using MatyaskerTipp.Model;
using MatyaskerTipp.MySQL;
using MatyaskerTipp.View;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Ink;
using System.Xml.Linq;

namespace MatyaskerTipp.ViewModel
{
    public class JavitasViewModel : ObservableObjects
    {
        internal Match match;
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
        }

        public void Javitas(string home, string guest, int matchId)
        {
            List<InContest> inContestList = new List<InContest>();
            List<ScoringRules> scoringRules = new List<ScoringRules>();
            List<Standings> standings = new List<Standings>();
            List<Contest> contests = new List<Contest>();
            List<ContestPlayer> contestPlayers = new List<ContestPlayer>();
            List<Bet> bets = new List<Bet>();
            List<Match> matchList = new List<Match>();

            try
            {
                MySqlConnection conn = new MySqlConnection(MySqlConn.connection);
                conn.Open();


                MySqlCommand updateMatchCmd = new MySqlCommand(SqlCommans.updateMatch, conn);
                updateMatchCmd.Parameters.AddWithValue("@homeGoals", int.Parse(home));
                updateMatchCmd.Parameters.AddWithValue("@guestGoals", int.Parse(guest));
                updateMatchCmd.Parameters.AddWithValue("@isAvailable", 0);
                updateMatchCmd.Parameters.AddWithValue("@id", matchId);
                updateMatchCmd.ExecuteNonQuery();


                MySqlCommand cmd = new MySqlCommand(SqlCommans.getAllInContest, conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    int contestId = dr.GetInt32("ContestId");
                    int thisMatchId = dr.GetInt32("MatchId");

                    InContest inContest = new InContest(contestId, thisMatchId);
                    inContestList.Add(inContest);
                }
                conn.Close();

                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand(SqlCommans.getAllScoringRules, conn);
                MySqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    string desc = dr1.GetString("description");
                    int points = dr1.GetInt32("points");
                    int contestId = dr1.GetInt32("contestId");

                    ScoringRules scoringRule = new ScoringRules(desc, points, contestId);
                    scoringRules.Add(scoringRule);
                }
                conn.Close();

                conn.Open();
                MySqlCommand cmd2 = new MySqlCommand(SqlCommans.getAllStandings, conn);
                MySqlDataReader dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    int points = dr2.GetInt32("Points");
                    int contestID = dr2.GetInt32("ContestID");
                    int userID = dr2.GetInt32("UserID");

                    Standings standing = new Standings(points, contestID, userID);
                    standings.Add(standing);
                }
                conn.Close();


                conn.Open();
                MySqlCommand cmd3 = new MySqlCommand(SqlCommans.selectAllContest, conn);
                MySqlDataReader dr3 = cmd3.ExecuteReader();
                while (dr3.Read())
                {
                    int id = dr3.GetInt32("Id");
                    string name = dr3.GetString("Name");
                    DateTime start = dr3.GetDateTime("StartDate");
                    DateTime end = dr3.GetDateTime("EndDate");
                    bool open = dr3.GetBoolean("IsOpened");

                    Contest contest = new Contest(id, name, start, end, open);
                    contests.Add(contest);
                }
                conn.Close();

                conn.Open();
                MySqlCommand cmd5 = new MySqlCommand(SqlCommans.getAllBets, conn);
                MySqlDataReader dr5 = cmd5.ExecuteReader();
                while (dr5.Read())
                {
                    int id = dr5.GetInt32("Id");
                    int contest = dr5.GetInt32("ContestId");
                    int user = dr5.GetInt32("UserId");
                    int match = dr5.GetInt32("MatchID");
                    int hgoals = dr5.GetInt32("HomeGoals");
                    int ggoals = dr5.GetInt32("GuestGoals");
                    bool iswon = dr5.GetBoolean("IsWon");

                    Bet bet = new Bet(id, contest, user, match, hgoals, ggoals, iswon);
                    bets.Add(bet);
                }
                conn.Close();



                foreach (InContest incontest in inContestList.Where(i => i.MatchId == matchId)) 
                {

                            foreach (Bet bet in bets.Where(b => b.MatchID == matchId && b.ContestId == incontest.ContestId))
                            {

                                if (EvaluateBet(bet, int.Parse(home), int.Parse(guest)))
                                {
                                    ScoringRules rule = scoringRules.FirstOrDefault(r => r.ContestId == incontest.ContestId);
                                    if (rule != null)
                                    {
                                        UpdateStandings(bet.UserId, incontest.ContestId, rule.Points, standings);
                                    }
                                }
                            }
                }


                SaveUpdatedStandings(standings);

                MessageBox.Show("Sikeres javítás és pontszám frissítés!");
                Notify();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt: " + ex.Message);
            }
        }


        private bool EvaluateBet(Bet bet, int hgoals, int ggoals)
        {
            return bet.HomeGoals == hgoals && bet.GuestGoals == ggoals;
        }

        private void UpdateStandings(int userId, int contestId, int points, List<Standings> standings)
        {
            Standings standing = standings.FirstOrDefault(s => s.UserID== userId && s.ContestID == contestId);
            if (standing != null)
            {
                standing.Points += points;
            }
            else
            {
                standings.Add(new Standings(points, contestId, userId));
            }
        }

        private void SaveUpdatedStandings(List<Standings> standings)
        {
            using (MySqlConnection conn = new MySqlConnection(MySqlConn.connection))
            {
                conn.Open();
                foreach (Standings standing in standings)
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE standings SET Points = @points WHERE UserId = @userId AND ContestId = @contestId", conn);
                    cmd.Parameters.AddWithValue("@points", standing.Points);
                    cmd.Parameters.AddWithValue("@contestId", standing.ContestID);
                    cmd.Parameters.AddWithValue("@userId", standing.UserID);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

