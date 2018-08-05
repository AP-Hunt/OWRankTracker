using OWRankTracker.Model;
using OWRankTracker.MatchHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.DesignTime.Profiles
{
    class Loser : InMemoryMatchHistory
    {
        public Loser() : base(GetRecords())
        {
        }

        private static IEnumerable<MatchRecord> GetRecords()
        {
            DateTime now = new DateTime();
            var matchOne = new Model.MatchRecord()
            {
                CR = 1000,
                Date = new DateTime(),
                Diff = 1000,
                Map = Maps.All.First(),
                Result = MatchResult.LOSE
            };

            var matchTwo = matchOne.NewRelativeRecord(975, now.AddMinutes(30), Maps.All.ElementAt(1));
            var matchThree = matchTwo.NewRelativeRecord(950, now.AddMinutes(60), Maps.All.ElementAt(2));

            return new List<Model.MatchRecord>()
            {
                matchOne,
                matchTwo,
                matchThree
            };
        }
    }
}
