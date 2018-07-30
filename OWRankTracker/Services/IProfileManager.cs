using System.Collections.Generic;
using OWRankTracker.Repositories;

namespace OWRankTracker.Services
{
    interface IProfileManager
    {
        IMatchRepository ActiveProfile { get; }
        string ActiveProfileName { get; }

        IEnumerable<string> AllProfiles();
        void OpenProfile(string name, bool emitMessage = true);
        void OpenProfileDefaultProfile(bool emitMessage = true);
    }
}