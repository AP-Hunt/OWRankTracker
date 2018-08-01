using OWRankTracker.Repositories;
using OWRankTracker.Services.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Test.Services
{
    class FakeProfileStorage : IProfileStorage
    {
        private Dictionary<string, IMatchRepository> _profiles;

        public string DefaultProfileName { get; set; }

        public IEnumerable<string> AllProfileNames => _profiles.Keys;

        public FakeProfileStorage() : this(new Dictionary<string, IMatchRepository>())
        {
        }

        public FakeProfileStorage(Dictionary<string, IMatchRepository> profiles)
        {
            _profiles = profiles;
            DefaultProfileName = "Default";
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
