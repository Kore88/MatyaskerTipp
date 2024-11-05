using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatyaskerTipp.Model
{
    internal class ScoringRules
    {
        public string Desciption { get; set; }
        public int Points { get; set; }
        public int ContestId { get; set; }

        public ScoringRules(string desciption, int points, int contestId)
        {
            Desciption = desciption;
            Points = points;
            ContestId = contestId;
        }
    }
}
