using Bogus;
using OWRankTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Test.Fakers
{
    class MatchRecordFaker
    {
        /// <summary>
        /// Creates fake match records. Use named arguments to override any field.
        /// </summary>
        /// <param name="cr"></param>
        /// <param name="diff"></param>
        /// <param name="map"></param>
        /// <param name="result"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static MatchRecord CreateRecord(
            int cr = 1000, 
            int diff = 0, 
            string map = null, 
            MatchResult result = MatchResult.WIN, 
            DateTime? date = null
        ){
            var valueFaker = new Faker();

            return new MatchRecord()
            {
                CR = cr,
                Date = date ?? DateTime.Now,
                Diff = diff,
                Map = map ?? MapFaker.Random(),
                Result = result
            };
        }

        public static IEnumerable<MatchRecord> NMatchesBetweenDates(
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
                Map = MapFaker.Random(),
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
                    MapFaker.Random()
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
        private static IEnumerable<DateTime> GenerateRealisticMatchTimes(int n, DateTime start)
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
