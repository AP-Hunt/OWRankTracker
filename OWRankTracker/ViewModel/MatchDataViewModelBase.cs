using GalaSoft.MvvmLight;
using OWRankTracker.Messages;
using OWRankTracker.Repositories;
using OWRankTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.ViewModel
{
    class MatchDataViewModelBase : ViewModelBase
    {
        protected IMatchRepository MatchRepository { get; set; }
        public string ProfileName { get; private set; }

        public MatchDataViewModelBase(IProfileManager profileManager)
        {
            MatchRepository = profileManager.ActiveProfile;
            ProfileName = profileManager.ActiveProfileName;

            MessengerInstance.Register<Messages.ActiveProfileChanged>(this, OnActiveProfileChanged);
        }

        protected virtual void ActiveProfileChanged()
        {
            // No op. Extenders should overide.
        }

        private void OnActiveProfileChanged(ActiveProfileChanged message)
        {
            MatchRepository = message.MatchRepository;
            ProfileName = message.ProfileName;
            ActiveProfileChanged();
        }
    }
}
