using OWRankTracker.Core.MatchHistory;
using OWRankTracker.Core.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Test.Fixtures
{
    public class EmptyProfile : IProfile
    {
        public string Name => "Empty";
        public IMatchHistory MatchHistory { get; private set; }

        public EmptyProfile()
        {
            MatchHistory = new InMemoryMatchHistory();
        }
    }
}
