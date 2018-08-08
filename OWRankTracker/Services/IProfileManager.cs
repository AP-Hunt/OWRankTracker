using System.Collections.Generic;
using OWRankTracker.MatchHistory;
using OWRankTracker.Profile;

namespace OWRankTracker.Services
{
    interface IProfileManager
    {
        IEnumerable<IProfile> Profiles { get; }
        IProfile ActiveProfile { get; }

        void OpenProfile(string name, bool emitMessage = true);
        void OpenDefaultProfile(bool emitMessage = true);
    }
}