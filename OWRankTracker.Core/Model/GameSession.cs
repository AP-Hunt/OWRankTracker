using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Core.Model
{
    public class GameSession : IEnumerable<MatchRecord>
    {
        private readonly int _startingCR;
        private readonly IEnumerable<MatchRecord> _matches;

        public int CRChange
        {
            get
            {
                return _matches.Last().CR - _startingCR;
            }
        }

        public GameSession(int startingCR, IEnumerable<MatchRecord> matchesInSession)
        {
            _startingCR = startingCR;
            _matches = matchesInSession;
        }

        #region IEnumerable<MatchRecord>
        public IEnumerator<MatchRecord> GetEnumerator()
        {
            return _matches.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _matches.GetEnumerator();
        }
        #endregion
    }
}
