using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OWRankTracker.MatchHistory;
using OWRankTracker.Profile;

namespace OWRankTracker.Messages
{
    class ActiveProfileChanged
    {
        public IProfile Profile { get; private set; }

        public ActiveProfileChanged(IProfile profile)
        {
            Profile = profile;
        }
    }
}
