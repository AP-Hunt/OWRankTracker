using Bogus;
using OWRankTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Test.FixtureFactories
{
    class MatchRecordFactory
    {
        public IEnumerable<MatchRecord> NMatchesBetweenDates(
            int n,
            DateTime start,
            int initialCR = 1000,
            int crMaxDifference = 500)
        {
            var valueFaker = new Faker();

            IEnumerable<DateTime> orderedDates = GenerateRealisticMatchTimes(n, start);

            List<MatchRecord> matches = new List<MatchRecord>();
            var i = 0;
            var firstCR = valueFaker.Random.Number(initialCR - crMaxDifference, initialCR + crMaxDifference);
            var firstMatch = new MatchRecord()
            {
                CR = firstCR,
                Date = orderedDates.ElementAt(i),
                Diff = firstCR - initialCR,
                Map = valueFaker.Random.ArrayElement(Maps.All),
                Result = MatchRecord.ComparerCR(firstCR, initialCR)
            };
            matches.Add(firstMatch);
            i++;


            MatchRecord lastMatch = firstMatch;
            while(i < n)
            {
                var newMatch = lastMatch.NewRelativeRecord(
                    valueFaker.Random.Number(initialCR - crMaxDifference, initialCR + crMaxDifference),
                    orderedDates.ElementAt(i),
                    valueFaker.Random.ArrayElement(Maps.All)
                );
                matches.Add(newMatch);
                i++;
            }

            return matches;
        }

        /// <summary>
        /// Attempts to generate realistic match times
        /// by randomizing time between games within a 
        /// reasonable range
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private IEnumerable<DateTime> GenerateRealisticMatchTimes(int n, DateTime start)
        {
            int minLengthMins = 10;
            int maxLengthMins = 45;

            DateTime previous = start;
            List<DateTime> times = new List<DateTime>();
            Random random = new Random();
            for(int i = 0; i <= n; i++)
            {
                int length = random.Next(minLengthMins, maxLengthMins);
                TimeSpan ts = TimeSpan.FromMinutes(length);
                DateTime dt = previous.Add(ts);
                times.Add(dt);
                previous = dt;
            }

            return times;
        }
    }
}
