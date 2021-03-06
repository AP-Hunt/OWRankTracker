﻿using GalaSoft.MvvmLight;
using OWRankTracker.Messages;
using OWRankTracker.Core.MatchHistory;
using OWRankTracker.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OWRankTracker.Core.Profile;

namespace OWRankTracker.ViewModel
{
    class MatchDataViewModelBase : ViewModelBase
    {
        protected IMatchHistory MatchHistory { get; set; }
        public string ProfileName { get; private set; }
        protected IProfile ActiveProfile { get; private set; }

        public MatchDataViewModelBase(IProfileManager profileManager)
        {
            MatchHistory = profileManager.ActiveProfile.MatchHistory;
            ProfileName = profileManager.ActiveProfile.Name;
            ActiveProfile = profileManager.ActiveProfile;

            MessengerInstance.Register<Messages.ActiveProfileChanged>(this, OnActiveProfileChanged);
        }

        protected virtual void ActiveProfileChanged()
        {
            // No op. Extenders should overide.
        }

        private void OnActiveProfileChanged(ActiveProfileChanged message)
        {
            MatchHistory = message.Profile.MatchHistory;
            ProfileName = message.Profile.Name;
            ActiveProfile = message.Profile;
            ActiveProfileChanged();
        }
    }
}
