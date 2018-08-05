using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OWRankTracker.Model;
using OWRankTracker.MatchHistory;

namespace OWRankTracker.DesignTime.Profiles
{
    class Winner : InMemoryMatchHistory
    {
        public Winner() : base(GetRecords())
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
                Result = MatchResult.WIN
            };

            var matchTwo = matchOne.NewRelativeRecord(1025, now.AddMinutes(30), Maps.All.ElementAt(1));
            var matchThree = matchTwo.NewRelativeRecord(1061, now.AddMinutes(60), Maps.All.ElementAt(2));

            return new List<Model.MatchRecord>()
            {
                matchOne,
                matchTwo,
                matchThree
            };
        }
    }
}
