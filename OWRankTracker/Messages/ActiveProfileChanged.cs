using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OWRankTracker.MatchHistory;

namespace OWRankTracker.Messages
{
    class ActiveProfileChanged
    {
        public string ProfileName { get; }
        public IMatchHistory MatchHistory { get; private set; }

        public ActiveProfileChanged(string profileName, IMatchHistory profileMatchHistory)
        {
            ProfileName = profileName;
            MatchHistory = profileMatchHistory;
        }
    }
}
