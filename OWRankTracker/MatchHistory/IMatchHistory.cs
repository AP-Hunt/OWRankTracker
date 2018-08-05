using System.Collections.Generic;
using OWRankTracker.Model;

namespace OWRankTracker.MatchHistory
{
    interface IMatchHistory : IEnumerable<MatchRecord>
    {
        MatchRecord LastMatch { get; }

        void Add(MatchRecord record);
    }
}