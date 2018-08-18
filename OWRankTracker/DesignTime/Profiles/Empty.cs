using OWRankTracker.Core.MatchHistory;
using OWRankTracker.Core.Profile;

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
