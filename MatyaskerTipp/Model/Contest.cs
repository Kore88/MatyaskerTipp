using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatyaskerTipp.Model
{
    internal class Contest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsOpened { get; set; } = false;

        public Contest(int id, string name, DateTime startDate, DateTime endDate, bool isOpened)
        {
            Id = id;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            IsOpened = isOpened;
        }
    }
}
