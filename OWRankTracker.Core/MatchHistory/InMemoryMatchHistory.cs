using OWRankTracker.Core.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Core.MatchHistory
{
    public class InMemoryMatchHistory : IMatchHistory
    {
        private List<Core.Model.MatchRecord> _records;

        public InMemoryMatchHistory()
        {
            _records = new List<Core.Model.MatchRecord>();
        }

        public InMemoryMatchHistory(IEnumerable<Core.Model.MatchRecord> records)
        {
            _records = records.ToList();
        }

        public MatchRecord LastMatch => _records.LastOrDefault();

        public void Add(MatchRecord record)
        {
            _records.Add(record);
        }

        public IEnumerator<MatchRecord> GetEnumerator()
        {
            return _records.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _records.GetEnumerator();
        }
    }
}
