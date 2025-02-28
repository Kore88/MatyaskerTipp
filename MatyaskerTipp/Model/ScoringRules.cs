﻿using MatyaskerTipp.MySQL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MatyaskerTipp.Model
{
    internal class ScoringRules
    {
        public string Description { get; set; }
        public int Points { get; set; }
        public int ContestId { get; set; }

        public ScoringRules(string description, int points, int contestId)
        {
            Description = description;
            Points = points;
            ContestId = contestId;
        }

        public ScoringRules()
        {
        }

        public ScoringRules GetContestRules(int conId)
        {
            ScoringRules rule = null;

            try
            {
                MySqlConnection conn = new MySqlConnection(MySqlConn.connection);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(SqlCommans.selectContestRules, conn);
                cmd.Parameters.AddWithValue("@contestId", conId);
                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read()) 
                {
                    rule = new ScoringRules();

                    rule.Description = dr.GetString("description");
                    rule.Points = dr.GetInt32("points");
                    rule.ContestId = dr.GetInt32("contestId");
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            return rule;
        }

    }
}
