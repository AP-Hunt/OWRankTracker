using OWRankTracker.Core.MatchHistory;
using OWRankTracker.Core.Model;
using OWRankTracker.Core.Profile;
using OWRankTracker.Test.Fakers;
using System.Collections.Generic;
using System.Linq;

namespace OWRankTracker.Test.Fixtures
{
    public class DefaultProfile : IProfile
    {
        public string Name => "Default";
        public IMatchHistory MatchHistory { get; private set; }

        public DefaultProfile()
        {
            MatchHistory = new InMemoryMatchHistory(new List<MatchRecord>()
            {
                MatchRecordFaker.CreateRecord(result: MatchResult.WIN, map: Maps.All.ElementAt(1)),
                MatchRecordFaker.CreateRecord(result: MatchResult.WIN, map: Maps.All.ElementAt(1)),
                MatchRecordFaker.CreateRecord(result: MatchResult.LOSE, map: Maps.All.ElementAt(2)),
                MatchRecordFaker.CreateRecord(result: MatchResult.LOSE, map: Maps.All.ElementAt(3)),
                MatchRecordFaker.CreateRecord(result: MatchResult.DRAW, map: "N/A"),
            });
        }
    }
}
