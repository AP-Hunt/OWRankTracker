using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OWRankTracker.MatchHistory;

namespace OWRankTracker.DesignTime
{
    class DesignTimeProfileManager : Services.IProfileManager
    {
        private Dictionary<string, IMatchHistory> _profiles = new Dictionary<string, IMatchHistory>()
        {
            { "Winner", new Profiles.Winner() },
            { "Loser", new Profiles.Loser() },
            { "500 Records", new Profiles.FiveHundredMatches() }
        };


        public IMatchHistory ActiveProfile { get; private set; }
        public string ActiveProfileName { get; private set; }

        public DesignTimeProfileManager()
        {
            OpenProfileDefaultProfile(false);
        }

        public IEnumerable<string> AllProfiles()
        {
            return _profiles.Keys;
        }

        public void OpenProfile(string name, bool emitMessage = true)
        {
            if(!_profiles.ContainsKey(name))
            {
                throw new KeyNotFoundException();
            }

            ActiveProfile = _profiles[name];
            ActiveProfileName = name;
        }

        public void OpenProfileDefaultProfile(bool emitMessage = true)
        {
            OpenProfile(_profiles.Keys.First(), emitMessage);
        }
    }
}
