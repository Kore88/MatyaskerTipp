using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatyaskerTipp.Model
{
    internal class InContest
    {
        public int ContestId { get; set; }
        public int MatchId { get; set; }

        public InContest(int contestId, int matchId)
        {
            ContestId = contestId;
            MatchId = matchId;
        }
    }
}
