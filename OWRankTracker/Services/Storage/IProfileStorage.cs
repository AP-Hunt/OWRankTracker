using OWRankTracker.Repositories;
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
        IMatchRepository Get(string profileName);
        IMatchRepository Create(string profileName);
    }
}
