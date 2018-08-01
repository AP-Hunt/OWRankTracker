using System.Collections.Generic;
using OWRankTracker.Model;

namespace OWRankTracker.Repositories
{
    internal interface IMatchRepository : IEnumerable<MatchRecord>
    {
        MatchRecord LastMatch { get; }

        void Add(MatchRecord record);
    }
}