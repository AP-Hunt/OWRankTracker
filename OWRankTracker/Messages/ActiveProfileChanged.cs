using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OWRankTracker.Repositories;

namespace OWRankTracker.Messages
{
    class ActiveProfileChanged
    {
        public string ProfileName { get; }
        public IMatchRepository MatchRepository { get; private set; }

        public ActiveProfileChanged(string profileName, IMatchRepository profileDataRepository)
        {
            ProfileName = profileName;
            MatchRepository = profileDataRepository;
        }
    }
}
