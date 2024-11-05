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
        public int HomeGoals { get; set; }
        public int GuestGoals { get; set; }
        public bool IsAviable { get; set; } = false;

        public Match(int id, string homeName, string guestName, DateTime date, int homeGoals, int guestGoals, bool isAviable)
        {
            Id = id;
            HomeName = homeName;
            GuestName = guestName;
            Date = date;
            HomeGoals = homeGoals;
            GuestGoals = guestGoals;
            IsAviable = isAviable;
        }
    }
}
