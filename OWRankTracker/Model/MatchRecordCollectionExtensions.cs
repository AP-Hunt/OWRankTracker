using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Model
{
    static class MatchRecordCollectionExtensions
    {
        public static int Wins(this IEnumerable<MatchRecord> records)
        {
            return records.Count(m => m.Result == MatchResult.WIN);
        }

        public static int Draws(this IEnumerable<MatchRecord> records)
        {
            return records.Count(m => m.Result == MatchResult.DRAW);
        }

        public static int Losses(this IEnumerable<MatchRecord> records)
        {
            return records.Count(m => m.Result == MatchResult.LOSE);
        }
    }
}
