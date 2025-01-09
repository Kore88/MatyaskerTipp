using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatyaskerTipp.Model
{
    public class ContestPlayer
    {
        public string UserName { get; set; }
        public string RealName { get; set; }
        public int Points { get; set; }

        public ContestPlayer(string realName,int points)
        {
            RealName = realName;
            Points = points;
        }

        public ContestPlayer(string userName, string realName, int points) 
        {
            UserName = userName;
            RealName = realName;
            Points = points;

        }
    }


}
