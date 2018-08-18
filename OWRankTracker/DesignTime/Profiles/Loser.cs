using OWRankTracker.Core.MatchHistory;
using OWRankTracker.Core.Model;
using OWRankTracker.Core.Profile;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OWRankTracker.DesignTime.Profiles
{
    class Loser : IProfile
    {
        public string Name { get; private set; }
        public IMatchHistory MatchHistory { get; private set; }

        public Loser()
        {
            Name = "Loser";
            MatchHistory = new InMemoryMatchHistory(GetRecords());
        }

        private static IEnumerable<MatchRecord> GetRecords()
        {
            DateTime now = new DateTime();
            var matchOne = new MatchRecord()
            {
                CR = 1000,
                Date = new DateTime(),
                Diff = 1000,
                Map = Maps.All.First(),
                Result = MatchResult.LOSE
            };

            var matchTwo = matchOne.NewRelativeRecord(975, now.AddMinutes(30), Maps.All.ElementAt(1));
            var matchThree = matchTwo.NewRelativeRecord(950, now.AddMinutes(60), Maps.All.ElementAt(2));

            return new List<MatchRecord>()
            {
                matchOne,
                matchTwo,
                matchThree
            };
        }
    }
}
