using OWRankTracker.Model;
using OWRankTracker.MatchHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.DesignTime.Profiles
{
    class FiveHundredMatches : InMemoryMatchHistory
    {
        public FiveHundredMatches() : base(GetRecords())
        {

        }

        private static IEnumerable<MatchRecord> GetRecords()
        {
            TimeSpan interval = TimeSpan.FromMinutes(18);
            List<MatchRecord> records = new List<MatchRecord>();

            Random rand = new Random();
            int SR = rand.Next(1500, 2500);
            MatchRecord previousRecord = new MatchRecord()
            {
                CR = SR,
                Date = DateTime.Now,
                Diff = SR,
                Map = RandomMap(rand),
                Result = MatchResult.WIN
            };
            records.Add(previousRecord);

            for(int i = 0; i <= 499; i++)
            {
                int newSR = previousRecord.CR + (rand.Next(15, 30) * SRChangeModifier(rand));
                MatchRecord newRecord = previousRecord.NewRelativeRecord(newSR, previousRecord.Date.Add(interval), RandomMap(rand));
                records.Add(newRecord);
                previousRecord = newRecord;
            }

            return records;
        }

        private static int SRChangeModifier(Random rand)
        {
            double n = Math.Round(rand.NextDouble());

            return n == 0 ? -1 : 1;
        }

        private static string RandomMap(Random rand)
        {
            return Maps.All.ElementAt(rand.Next(0, Maps.All.Count()));
        }
    }   
}
