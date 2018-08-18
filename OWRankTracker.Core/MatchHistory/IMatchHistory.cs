using System.Collections.Generic;
using OWRankTracker.Core.Model;

namespace OWRankTracker.Core.MatchHistory
{
    public interface IMatchHistory : IEnumerable<MatchRecord>
    {
        MatchRecord LastMatch { get; }

        void Add(MatchRecord record);
    }
}