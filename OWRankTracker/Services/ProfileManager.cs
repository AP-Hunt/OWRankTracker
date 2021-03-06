﻿using OWRankTracker.Core.Profile;
using OWRankTracker.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OWRankTracker.Core.Services
{
    class ProfileManager : IProfileManager, Validation.IProfileNameValidator
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
        }

        /// <summary>
        /// Opens the default profile, or if that doesn't exist the first alphabetically
        /// </summary>
        public void OpenDefaultProfile(bool emitMessage = true)
        {
            string profileName = DEFAULT_PROFILE_NAME;

            if(!_profileStorage.Exists(profileName))
            {
                profileName = _profileStorage.First().Name;
            }

            OpenProfile(profileName, emitMessage);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public IProfile Create(string name)
        {
            if (_profileStorage.Exists(name))
            {
                throw new ProfileNameConflictException(name);
            }

            IProfile newProfile = _profileStorage.Create(name);
            _messenger.Send(new Messages.NewProfile(newProfile));
            return newProfile;
        }

        bool IProfileNameValidator.Validate(string profileName)
        {
            if(Profiles.Any(p => p.Name.Equals(profileName, StringComparison.CurrentCultureIgnoreCase)))
            {
                return false;
            }

            return true;
        }
    }
}
