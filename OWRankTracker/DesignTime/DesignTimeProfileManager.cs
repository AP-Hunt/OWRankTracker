using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OWRankTracker.MatchHistory;
using OWRankTracker.Profile;

namespace OWRankTracker.DesignTime
{
    class DesignTimeProfileManager : Services.IProfileManager
    {
        private List<IProfile> _profiles = new List<IProfile>()
        {
            new Profiles.Winner(),
            new Profiles.Loser(),
            new Profiles.FiveHundredMatches()
        };

        public IProfile ActiveProfile { get; private set; }

        public IEnumerable<IProfile> Profiles => _profiles;

        public DesignTimeProfileManager()
        {
            OpenProfileDefaultProfile(false);
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

        public void OpenProfileDefaultProfile(bool emitMessage = true)
        {
            OpenProfile(_profiles.First().Name, emitMessage);
        }
    }
}
