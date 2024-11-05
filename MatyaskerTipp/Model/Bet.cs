using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatyaskerTipp.Model
{
    internal class Bet
    {
        public int Id { get; set; }
        public int ContestId { get; set; }
        public int UserId { get; set; }
        public int MatchID { get; set; }
        public int HomeHeals { get; set; }
        public int GuestGoals { get; set; }
        public bool IsWon { get; set; } = false;

        public Bet(int id, int contestId, int userId, int matchID, int homeHeals, int guestGoals, bool isWon)
        {
            Id = id;
            ContestId = contestId;
            UserId = userId;
            MatchID = matchID;
            HomeHeals = homeHeals;
            GuestGoals = guestGoals;
            IsWon = isWon;
        }
    }
}
