using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MatyaskerTipp.Model
{
    internal class Bet
    {
        public int Id { get; set; }
        public int ContestId { get; set; }
        public int UserId { get; set; }
        public int MatchID { get; set; }
        public int HomeGoals { get; set; }
        public int GuestGoals { get; set; }
        public bool IsWon { get; set; } = false;

        public Bet(int id, int contestId, int userId, int matchID, int homeHeals, int guestGoals, bool isWon)
        {
            Id = id;
            ContestId = contestId;
            UserId = userId;
            MatchID = matchID;
            HomeGoals = homeHeals;
            GuestGoals = guestGoals;
            IsWon = isWon;
        }
        
    }
}
