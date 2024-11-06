using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatyaskerTipp.Model
{
    internal class Match
    {
        public int Id { get; set; }
        public string HomeName { get; set; }
        public string GuestName { get; set; }
        public DateTime Date { get; set; }
        public int HomeGoals { get; set; } = 0;
        public int GuestGoals { get; set; } = 0;
        public bool isAvailable { get; set; } = false;

        public Match(string homeName, string guestName, DateTime date, int homeGoals, int guestGoals, bool isAvailable)
        {
            HomeName = homeName;
            GuestName = guestName;
            Date = date;
            HomeGoals = homeGoals;
            GuestGoals = guestGoals;
            isAvailable = isAvailable;
        }
    }
}
