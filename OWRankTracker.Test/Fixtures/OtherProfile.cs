using OWRankTracker.MatchHistory;
using OWRankTracker.Model;
using OWRankTracker.Profile;
using OWRankTracker.Test.Fakers;
using System.Collections.Generic;
using System.Linq;

namespace OWRankTracker.Test.Fixtures
{
    public class OtherProfile : IProfile
    {
        public string Name => "Other";
        public IMatchHistory MatchHistory { get; private set; }

        public OtherProfile()
        {
            MatchHistory = new InMemoryMatchHistory(new List<MatchRecord>()
            {
                MatchRecordFaker.CreateRecord(result: MatchResult.LOSE, map: Maps.All.ElementAt(1)),
                MatchRecordFaker.CreateRecord(result: MatchResult.LOSE, map: Maps.All.ElementAt(1)),
                MatchRecordFaker.CreateRecord(result: MatchResult.LOSE, map: Maps.All.ElementAt(2)),
                MatchRecordFaker.CreateRecord(result: MatchResult.LOSE, map: Maps.All.ElementAt(3)),
                MatchRecordFaker.CreateRecord(result: MatchResult.WIN, map: "N/A"),
                MatchRecordFaker.CreateRecord(result: MatchResult.DRAW, map: "N/A"),
                MatchRecordFaker.CreateRecord(result: MatchResult.DRAW, map: Maps.All.ElementAt(4)),
            });
        }
    }
}
