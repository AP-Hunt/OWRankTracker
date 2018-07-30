using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OWRankTracker.Repositories;

namespace OWRankTracker.Services.Storage
{
    class InMemoryProfileStorage : IProfileStorage
    {
        private Dictionary<string, IMatchRepository> _profiles;

        public string DefaultProfileName => "Default";

        public IEnumerable<string> AllProfileNames => _profiles.Keys;

        public InMemoryProfileStorage()
        {
            _profiles = new Dictionary<string, IMatchRepository>();
        }

        public InMemoryProfileStorage(Dictionary<string, IMatchRepository> profiles)
        {
            _profiles = profiles;
        }

        public IMatchRepository Create(string profileName)
        {
            var repo = new InMemoryMatchRepository();
            _profiles.Add(profileName, repo);
            return repo;
        }

        public bool Exists(string profileName)
        {
            return _profiles.ContainsKey(profileName);
        }

        public IMatchRepository Get(string profileName)
        {
            return _profiles[profileName];
        }
    }
}
