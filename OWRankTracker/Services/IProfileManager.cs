using System.Collections.Generic;
using OWRankTracker.Core.MatchHistory;
using OWRankTracker.Core.Profile;

namespace OWRankTracker.Core.Services
{
    public interface IProfileManager
    {
        IEnumerable<IProfile> Profiles { get; }
        IProfile ActiveProfile { get; }

        void OpenProfile(string name, bool emitMessage = true);
        void OpenDefaultProfile(bool emitMessage = true);
    }
}