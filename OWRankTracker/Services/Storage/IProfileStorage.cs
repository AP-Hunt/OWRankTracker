using OWRankTracker.MatchHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Services.Storage
{
    interface IProfileStorage
    {
        string DefaultProfileName { get; }
        IEnumerable<string> AllProfileNames { get; }

        bool Exists(string profileName);
        IMatchHistory Get(string profileName);
        IMatchHistory Create(string profileName);
    }
}
