﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatyaskerTipp.MySQL
{
    internal class SqlCommans
    {

        public static string selectAllUser = "SELECT * FROM User";
        public static string addMatch = "INSERT INTO matyaskert.match (id, homeName, guestName, date, homeGoals, guestGoals, isAvailable) VALUES (NULL, @homeName, @guestName, @date, @homeGoals, @guestGoals, @isAvailable)";
        public static string addContest = "INSERT INTO matyaskert.contest (id, name, startDate, endDate, isOpened) VALUES (NULL, @name, @startDate, @endDate, @isOpened)";
        public static string selectConestNames = "SELECT name FROM matyaskert.Contest";
        public static string selectedContest = "SELECT id, name, startDate, endDate FROM matyaskert.Contest WHERE id = @idx";
        public static string selectAllNotAvailableMatch = "SELECT homeName,guestName FROM matyaskert.match WHERE isAvailable = 0";
        public static string selectAllAvailableMatch = "SELECT homeName,guestName FROM matyaskert.match WHERE isAvailable = 1";
        public static string selectAllNonCheckedMatch = "SELECT homeName, guestName FROM matyaskert.match WHERE homeGoals = -1 AND guestGoals = -1";
        public static string selectContestRules = "SELECT scoringrules.description, scoringrules.points, scoringrules.contestId " +
                                                  "FROM matyaskert.scoringrules " +
                                                  "JOIN matyaskert.contest ON scoringrules.contestId = contest.id " +
                                                  "WHERE contest.id = @contestId";
        public static string selectContestIdByName = "SELECT id FROM matyaskert.contest WHERE name = @name";


    }
}
