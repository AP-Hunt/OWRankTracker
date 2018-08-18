using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Core.Model
{
    public class MapStatistics
    {
        public string Map { get; set; }
        public int TotalPlayed { get; set; }
        
        public int TotalWon { get; set; }
        public double WinPc => (double)TotalWon / (double)TotalPlayed;
        
        
        public int TotalDrawn { get; set; }
        public double DrawnPc => (double)TotalDrawn / (double)TotalPlayed;

        public int TotalLost { get; set; }
        public double LostPc => (double)TotalLost / (double)TotalPlayed;
    }
}
