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
    class DefaultProfile : InMemoryMatchRepository
    {
        public static readonly string ProfileName = "Default";

        public static readonly int Wins = 2;
        public static readonly int Draws = 2;
        public static readonly int Losses = 1;

        public DefaultProfile()
        :base (
            new List<MatchRecord>()
            {
                MatchRecordFaker.CreateRecord(result: MatchResult.WIN, map: Maps.All.ElementAt(1)),
                MatchRecordFaker.CreateRecord(result: MatchResult.WIN, map: Maps.All.ElementAt(1)),
                MatchRecordFaker.CreateRecord(result: MatchResult.LOSE, map: Maps.All.ElementAt(2)),
                MatchRecordFaker.CreateRecord(result: MatchResult.LOSE, map: Maps.All.ElementAt(3)),
                MatchRecordFaker.CreateRecord(result: MatchResult.DRAW, map: "N/A"),
            }
        )
        {
        }
    }
}
