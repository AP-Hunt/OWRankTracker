using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OWRankTracker.Core.MatchHistory;
using OWRankTracker.Core.Profile;

namespace OWRankTracker.DesignTime
{
    class DesignTimeProfileManager : Core.Services.IProfileManager
    {
        private List<IProfile> _profiles = new List<IProfile>()
        {
            new Profiles.FiveHundredMatches(),
            new Profiles.Winner(),
            new Profiles.Loser()
        };

        public IProfile ActiveProfile { get; private set; }

        public IEnumerable<IProfile> Profiles => _profiles;

        public DesignTimeProfileManager()
        {
            OpenDefaultProfile();
        }

        public void OpenProfile(string name, bool emitMessage = true)
        {
            IProfile profile = _profiles.FirstOrDefault(p => p.Name == name);

            if (profile == null)
            {
                throw new KeyNotFoundException();
            }


            ActiveProfile = profile;
        }

        public void OpenDefaultProfile(bool emitMessage = true)
        {
            OpenProfile(_profiles.First().Name, emitMessage);
        }
    }
}
