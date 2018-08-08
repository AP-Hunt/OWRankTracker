using OWRankTracker.MatchHistory;
using OWRankTracker.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Services.Storage
{
    interface IProfileStorage : IEnumerable<IProfile>
    {
        bool Exists(string profileName);
        IProfile Get(string profileName);
        IProfile Create(string profileName);
    }
}
