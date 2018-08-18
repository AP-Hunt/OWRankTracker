using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using OWRankTracker.Services;
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
        public RelayCommand ManageProfilesCommand => new RelayCommand(OpenManageProfilesWindow);

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
        }

        private void ProfileChanged(string name)
        {
            _profileManager.OpenProfile(name);
        }

        private void OpenManageProfilesWindow()
        {
            throw new NotImplementedException("Not implemented yet");
        }
    }
}
