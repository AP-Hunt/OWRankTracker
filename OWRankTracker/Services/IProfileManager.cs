using System.Collections.Generic;
using OWRankTracker.MatchHistory;

namespace OWRankTracker.Services
{
    interface IProfileManager
    {
        IMatchHistory ActiveProfile { get; }
        string ActiveProfileName { get; }

        IEnumerable<string> AllProfiles();
        void OpenProfile(string name, bool emitMessage = true);
        void OpenProfileDefaultProfile(bool emitMessage = true);
    }
}