using OWRankTracker.Model;
using OWRankTracker.Repositories;
using OWRankTracker.Test.Fakers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Test.Fixtures
{
    class AlternateProfile : InMemoryMatchRepository
    {
        public static readonly string ProfileName = "Alternate";

        public static readonly int Wins = 1;
        public static readonly int Draws = 2;
        public static readonly int Losses = 4;

        public AlternateProfile()
        : base(
            new List<MatchRecord>()
            {
                MatchRecordFaker.CreateRecord(result: MatchResult.LOSE, map: Maps.All.ElementAt(1)),
                MatchRecordFaker.CreateRecord(result: MatchResult.LOSE, map: Maps.All.ElementAt(1)),
                MatchRecordFaker.CreateRecord(result: MatchResult.LOSE, map: Maps.All.ElementAt(2)),
                MatchRecordFaker.CreateRecord(result: MatchResult.LOSE, map: Maps.All.ElementAt(3)),
                MatchRecordFaker.CreateRecord(result: MatchResult.WIN, map: "N/A"),
                MatchRecordFaker.CreateRecord(result: MatchResult.DRAW, map: "N/A"),
                MatchRecordFaker.CreateRecord(result: MatchResult.DRAW, map: Maps.All.ElementAt(4)),
            }
        )
        {
        }
    }
}
