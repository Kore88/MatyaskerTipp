﻿using MatyaskerTipp.Model;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MatyaskerTipp.MySQL
{
    internal class SqlCommans
    {

        public static string selectAllUser = "SELECT * FROM User";
        public static string addMatch = "INSERT INTO matyaskert.match (id, homeName, guestName, date, homeGoals, guestGoals, isAvailable) VALUES (NULL, @homeName, @guestName, @date, @homeGoals, @guestGoals, @isAvailable)";
        public static string addContest = "INSERT INTO matyaskert.contest (id, name, startDate, endDate, isOpened) VALUES (NULL, @name, @startDate, @endDate, @isOpened)";
        public static string selectConestNames = "SELECT name FROM matyaskert.Contest";
        public static string selectedContest = "SELECT id, name, startDate, endDate FROM matyaskert.Contest WHERE id = @idx";
        public static string selectAllNotAvailableMatch = "SELECT id, homeName,guestName FROM matyaskert.match WHERE isAvailable = 0";
        public static string selectAllAvailableMatch = "SELECT id, homeName,guestName FROM matyaskert.match WHERE isAvailable = 1";
        //public static string selectAllNotAvailableMatch = @"
        //                    SELECT homeName, guestName 
        //                    FROM matyaskert.match 
        //                    WHERE isAvailable = 0 
        //                    AND id NOT IN (SELECT incontest.matchId FROM matyaskert.incontest WHERE incontest.contestId = @contestId)";
        //public static string selectAllAvailableMatch = @"
        //                    SELECT homeName, guestName 
        //                    FROM matyaskert.match 
        //                    WHERE isAvailable = 1 
        //                    AND id NOT IN (SELECT incontest.matchId FROM matyaskert.incontest WHERE incontest.contestId = @contestId)";

        public static string selectAllNonCheckedMatch = "SELECT id, homeName, guestName FROM matyaskert.match WHERE homeGoals = -1 AND guestGoals = -1";
        public static string selectContestRules = "SELECT scoringrules.description, scoringrules.points, scoringrules.contestId " +
                                                  "FROM matyaskert.scoringrules " +
                                                  "JOIN matyaskert.contest ON scoringrules.contestId = contest.id " +
                                                  "WHERE contest.id = @contestId";
        public static string selectContestIdByName = "SELECT id FROM matyaskert.contest WHERE name = @name";

        public static string instertIntoScoringRules = "INSERT INTO matyaskert.scoringrules (description, points, contestId) VALUES (@description, @points, @contestId)";
        public static string updateScoringRule = "UPDATE matyaskert.scoringrules SET description = @description, points = @points WHERE contestId = @contestId";
        public static string selectedMatch = "SELECT id, homeName, guestName, date, homeGoals,guestGoals,isAvailable FROM matyaskert.match WHERE id = @idx";
        public static string updateMatch = "UPDATE matyaskert.match SET homeGoals = @homeGoals, guestGoals = @guestGoals, isAvailable = @isAvailable WHERE id = @id";

        public static string selectAllUsers = "SELECT id, username, realname, isActive FROM matyaskert.User WHERE isAdmin = 0";
        public static string updateStatus = "UPDATE matyaskert.User SET isActive = @isActive WHERE id = @id";

        public static string selectContestUsers = @"
        SELECT User.realName, Standings.points 
        FROM matyaskert.User 
        JOIN matyaskert.Standings ON Standings.userId = User.id 
        JOIN matyaskert.Contest ON Contest.id = Standings.contestId 
        WHERE Contest.name = @selectedContest
        ORDER BY Standings.points DESC";

        public static string selectMatchesByContest = @"
        SELECT Match.id, Match.homeName, Match.guestName, Match.date
        FROM matyaskert.Match
        JOIN matyaskert.InContest ON InContest.matchId = Match.id
        JOIN matyaskert.Contest ON Contest.id = InContest.contestId
        WHERE Contest.name = @contestName";

        public static string selectMatchId = "SELECT id FROM matyaskert.match WHERE homeName = @homeName AND guestName = @guestName";
        public static string insertInContest = "INSERT INTO InContest (ContestId, MatchId) VALUES (@contestId, @matchId)";
        public static string updateMatchQuery = "UPDATE matyaskert.match SET isAvailable = 1 WHERE id = @matchId";

    }
}
