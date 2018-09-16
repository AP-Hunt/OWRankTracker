using System.Collections.Generic;
using OWRankTracker.Core.MatchHistory;
using OWRankTracker.Core.Profile;

namespace OWRankTracker.Core.Services
{
    public interface IProfileManager
    {
        IEnumerable<IProfile> Profiles { get; }
        IProfile ActiveProfile { get; }

        /// <summary>
        /// Opens a profile
        /// </summary>
        /// <param name="name"></param>
        /// <param name="emitMessage"></param>
        void OpenProfile(string name, bool emitMessage = true);

        /// <summary>
        /// Opens the default profile
        /// </summary>
        /// <param name="emitMessage"></param>
        void OpenDefaultProfile(bool emitMessage = true);

        /// <summary>
        /// Creates a new profile
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="ProfileNameConflictException">Thrown when a profile with the same name already exists.</exception>
        /// <returns></returns>
        IProfile Create(string name);
    }
}