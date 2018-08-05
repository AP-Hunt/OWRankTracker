using OWRankTracker.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Services
{
    class ProfileManager : IProfileManager
    {
        private static string DEFAULT_PROFILE_NAME = "Default";
        private static string PROFILE_FOLDER_PATH =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "OverwatchRankTracker",
                "Profiles"
            );

        private readonly Storage.IProfileStorage _profileStorage;
        private readonly GalaSoft.MvvmLight.Messaging.IMessenger _messenger;

        public MatchHistory.IMatchHistory ActiveProfile { get; private set; }
        public string ActiveProfileName { get; private set; }

        public ProfileManager(
            Storage.IProfileStorage profileStorage,
            GalaSoft.MvvmLight.Messaging.IMessenger messenger
        ){
            _profileStorage = profileStorage;
            _messenger = messenger;
        }

        /// <summary>
        /// Opens the default profile, or if that doesn't exist the first alphabetically
        /// </summary>
        public void OpenProfileDefaultProfile(bool emitMessage = true)
        {
            var allProfiles = this.AllProfiles();
            string profileName = _profileStorage.DefaultProfileName;

            if(!_profileStorage.Exists(profileName))
            {
                profileName = allProfiles.First();
            }

            OpenProfile(profileName, emitMessage);
        }

        public void OpenProfile(string name, bool emitMessage = true)
        {
            if(!_profileStorage.Exists(name))
            {
                throw new ArgumentException($"{name} is not a valid profile name", nameof(name));
            }

            var repo = _profileStorage.Get(name);
            ActiveProfile = repo;
            ActiveProfileName = name;

            if (emitMessage)
            {
                _messenger.Send<Messages.ActiveProfileChanged>(new Messages.ActiveProfileChanged(name, repo));
            }
        }

        public IEnumerable<string> AllProfiles()
        {
            return _profileStorage.AllProfileNames;
        }
    }
}
