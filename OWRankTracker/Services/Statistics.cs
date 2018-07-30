using MoreLinq;
using OWRankTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Services
{
    class Statistics
    {
        private readonly IEnumerable<Model.MatchRecord> _records;

        public Statistics(IEnumerable<Model.MatchRecord> records)
        {
            _records = records;
        }

        public IEnumerable<GameSession> FindGameSessions()
        {
            if(!_records.Any())
            {
                return Enumerable.Empty<GameSession>();
            }

            int averageDistance =
                (int)_records
                    .Pairwise((a, b) => Timestamp(b.Date) - Timestamp(a.Date))
                    .Average();

            List<GameSession> sessions = new List<GameSession>();
            int lastTimestamp = Timestamp(_records.First().Date);
            int prevCR = _records.First().CR;
            int prevOffset = 0;
            int i = 0;

            foreach (var match in _records)
            {
                var currentTimestamp = Timestamp(match.Date);
                var distance = currentTimestamp - lastTimestamp;
                if (distance > averageDistance)
                {
                    var sessionGames = _records.Skip(prevOffset).Take(i - prevOffset);
                    sessions.Add(new GameSession(prevCR, sessionGames));
                    prevOffset = i;
                    prevCR = sessionGames.Last().CR;
                }

                i++;
                lastTimestamp = currentTimestamp;
            }

            return sessions;
        }

        private int Timestamp(DateTime date)
        {
            TimeSpan ts = (date - new DateTime(1970, 1, 1, 0, 0, 0));
            return (int)ts.TotalSeconds;
        }

    }
}
