using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OWRankTracker.MatchHistory;

namespace OWRankTracker.Services.Storage
{
    class InMemoryProfileStorage : IProfileStorage
    {
        private Dictionary<string, IMatchHistory> _profiles;

        public string DefaultProfileName => "Default";

        public IEnumerable<string> AllProfileNames => _profiles.Keys;

        public InMemoryProfileStorage()
        {
            _profiles = new Dictionary<string, IMatchHistory>();
        }

        public InMemoryProfileStorage(Dictionary<string, IMatchHistory> profiles)
        {
            _profiles = profiles;
        }

        public IMatchHistory Create(string profileName)
        {
            var repo = new InMemoryMatchHistory();
            _profiles.Add(profileName, repo);
            return repo;
        }

        public bool Exists(string profileName)
        {
            return _profiles.ContainsKey(profileName);
        }

        public IMatchHistory Get(string profileName)
        {
            return _profiles[profileName];
        }
    }
}
