using MatyaskerTipp.MySQL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MatyaskerTipp.Model
{
    internal class Contest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsOpened { get; set; } = false;

        public Contest(int id, string name, DateTime startDate, DateTime endDate, bool isOpened)
        {
            Id = id;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            IsOpened = isOpened;
        }

        public Contest()
        {
        }

        public List<string> GetAllContestName()
        {
            List<string> contestNames = new List<string>();
            try
            {
                MySqlConnection conn = new MySqlConnection(MySqlConn.connection);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(SqlCommans.selectConestNames, conn);
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contestNames.Add(dr.GetString(0));
                }
                conn.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            return contestNames;
        }
    }
}
