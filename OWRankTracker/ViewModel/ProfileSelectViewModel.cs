using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using OWRankTracker.Core.Services;
using OWRankTracker.Messages;
using OWRankTracker.Services.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.ViewModel
{
    class ProfileSelectViewModel : ViewModelBase
    {
        private readonly IProfileManager _profileManager;
        private readonly IWindowService _windowService;

        public ObservableCollection<string> AllProfiles { get; set; }

        private string _selectedProfile;
        public string SelectedProfile
        {
            get { return _selectedProfile; }
            set
            {
                Set(ref _selectedProfile, value);
                ProfileChanged(value);
            }
        }

        public ProfileSelectViewModel(IProfileManager profileManager, IWindowService windowService)
        {
            _profileManager = profileManager;
            _windowService = windowService;

            AllProfiles = new ObservableCollection<string>(_profileManager.Profiles.Select(p => p.Name));
            _selectedProfile = AllProfiles.First();

            MessengerInstance.Register<Messages.NewProfile>(this, OnNewProfile);
        }

        private void OnNewProfile(NewProfile msg)
        {
            AllProfiles.Add(msg.Profile.Name);
        }

        private void ProfileChanged(string name)
        {
            _profileManager.OpenProfile(name);
        }
    }
}
