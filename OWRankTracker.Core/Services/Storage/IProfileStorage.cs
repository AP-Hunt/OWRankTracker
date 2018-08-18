using OWRankTracker.Core.MatchHistory;
using OWRankTracker.Core.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Core.Services.Storage
{
    public interface IProfileStorage : IEnumerable<IProfile>
    {
        bool Exists(string profileName);
        IProfile Get(string profileName);
        IProfile Create(string profileName);
    }
}
