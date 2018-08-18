using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OWRankTracker.Core.MatchHistory;
using OWRankTracker.Core.Profile;

namespace OWRankTracker.Core.Services.Storage
{
    public class InMemoryProfileStorage : IProfileStorage
    {
        private ICollection<IProfile> _profiles;

        public InMemoryProfileStorage()
        {
            _profiles = new List<IProfile>();
        }

        public InMemoryProfileStorage(ICollection<IProfile> profiles)
        {
            _profiles = profiles;
        }

        public IProfile Create(string profileName)
        {
            var profile = new InMemoryProfile(profileName);
            _profiles.Add(profile);
            return profile;
        }

        public bool Exists(string profileName)
        {
            return _profiles.Any(p => p.Name == profileName);
        }

        public IProfile Get(string profileName)
        {
            return _profiles.FirstOrDefault(p => p.Name == profileName);
        }

        #region IEnumerable<IProfile>
        public IEnumerator<IProfile> GetEnumerator()
        {
            return _profiles.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _profiles.GetEnumerator();
        }
        #endregion

        /// <summary>
        /// Implementation of IProfile for use with InMemoryProfileStorage
        /// </summary>
        class InMemoryProfile : IProfile
        {
            public string Name { get; private set; }
            public IMatchHistory MatchHistory { get; private set; }

            public InMemoryProfile(string name)
                : this(name, null)
            {
            }

            public InMemoryProfile(string name, InMemoryMatchHistory matchHistory)
            {
                Name = name;
                MatchHistory = matchHistory ?? new InMemoryMatchHistory();
            }
        }
    }
}
