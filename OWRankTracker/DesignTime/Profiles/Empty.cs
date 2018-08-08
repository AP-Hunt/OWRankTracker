using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OWRankTracker.Model;
using OWRankTracker.MatchHistory;
using OWRankTracker.Profile;

namespace OWRankTracker.DesignTime.Profiles
{
    class Empty : IProfile
    {
        public string Name { get; private set; }
        public IMatchHistory MatchHistory { get; private set; }

        public Empty(string name)
        {
            Name = name;
            MatchHistory = new InMemoryMatchHistory();
        }
    }
}
