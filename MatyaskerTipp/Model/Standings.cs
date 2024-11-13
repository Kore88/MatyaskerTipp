using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatyaskerTipp.Model
{
    internal class Standings
    {
        public int Points { get; set; } = -1;
        public int ContestID { get; set; }
        public int UserID { get; set; }

        public Standings(int points, int contestID, int userID)
        {
            Points = points;
            ContestID = contestID;
            UserID = userID;
        }
    }
}
