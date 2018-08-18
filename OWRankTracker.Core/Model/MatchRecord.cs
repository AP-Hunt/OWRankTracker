using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Core.Model
{
    public class MatchRecord
    {
        public int CR { get; set; }
        public DateTime Date { get; set; }
        public MatchResult Result { get; set; }
        public int Diff{ get; set; }
        public string Map { get; set; }

        public MatchRecord NewRelativeRecord(int newCr, DateTime dateTime, string map)
        {
            return new MatchRecord()
            {
                CR = newCr,
                Date = dateTime,
                Result = ComparerCR(newCr, CR),
                Diff = newCr - CR,
                Map = map
            };
        }

        internal static MatchResult ComparerCR(int newCR, int oldCR)
        {
            int diff = newCR - oldCR;

            if(diff == 0)
            {
                return MatchResult.DRAW;
            }
            else if(diff > 0)
            {
                return MatchResult.WIN;
            }
            else
            {
                return MatchResult.LOSE;
            }
        }
    }
}
