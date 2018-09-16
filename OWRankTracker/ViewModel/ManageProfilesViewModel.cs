using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Views;
using OWRankTracker.Core.Services;
using OWRankTracker.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.ViewModel
{
    class ManageProfilesViewModel : ViewModelBase
    {
        private readonly IProfileManager _profileManager;
        private readonly IDialogService _dialogService;

        public ObservableCollection<string> Profiles { get; private set; }

        public RelayCommand<string> AddProfileCommand => new RelayCommand<string>(AddProfile);

        public ManageProfilesViewModel(IProfileManager profileManager, IDialogService dialogService)
        {
            _profileManager = profileManager;
            _dialogService = dialogService;
            Profiles = new ObservableCollection<string>(_profileManager.Profiles.Select(p => p.Name));

            MessengerInstance.Register<Messages.NewProfile>(this, OnNewProfile);
        }

        private void AddProfile(string profileName)
        {
            try
            {
                _profileManager.Create(profileName);
            }
            catch(ProfileNameConflictException ex)
            {
                _dialogService.ShowError(ex, "Profile name conflict", "OK", (Action)null);
            }
        }

        private void OnNewProfile(NewProfile message)
        {
            Profiles.Add(message.Profile.Name);
        }
    }
}
