using System.Collections.Generic;
using OWRankTracker.Model;

namespace OWRankTracker.MatchHistory
{
    public interface IMatchHistory : IEnumerable<MatchRecord>
    {
        MatchRecord LastMatch { get; }

        void Add(MatchRecord record);
    }
}