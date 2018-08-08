using OWRankTracker.Model;
using OWRankTracker.Profile;
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

        private readonly Storage.IProfileStorage _profileStorage;
        private readonly GalaSoft.MvvmLight.Messaging.IMessenger _messenger;

        public IProfile ActiveProfile { get; private set; }

        public IEnumerable<IProfile> Profiles => _profileStorage;

        public ProfileManager(
            Storage.IProfileStorage profileStorage,
            GalaSoft.MvvmLight.Messaging.IMessenger messenger
        ){
            _profileStorage = profileStorage;
            _messenger = messenger;

            EnsureDefaultProfileExistsIfNeeded();
            OpenInitialProfile();
        }

        /// <summary>
        /// Opens the default profile, or if that doesn't exist the first alphabetically
        /// </summary>
        public void OpenProfileDefaultProfile(bool emitMessage = true)
        {
            string profileName = DEFAULT_PROFILE_NAME;

            if(!_profileStorage.Exists(profileName))
            {
                profileName = _profileStorage.First().Name;
            }

            OpenProfile(profileName, emitMessage);
        }

        public void OpenProfile(string name, bool emitMessage = true)
        {
            if (!_profileStorage.Exists(name))
            {
                throw new ArgumentException($"{name} is not a valid profile name", nameof(name));
            }

            IProfile profile = _profileStorage.Get(name);
            ActiveProfile = profile;

            if (emitMessage)
            {
                _messenger.Send<Messages.ActiveProfileChanged>(new Messages.ActiveProfileChanged(profile));
            }
        }

        private void EnsureDefaultProfileExistsIfNeeded()
        {
            if(_profileStorage.Count() == 0)
            {
                _profileStorage.Create(DEFAULT_PROFILE_NAME);
            }
        }

        private void OpenInitialProfile()
        {
            OpenProfile(_profileStorage.First().Name);
        }
    }
}
