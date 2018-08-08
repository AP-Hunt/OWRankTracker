using GalaSoft.MvvmLight;
using OWRankTracker.Services;
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

        public ProfileSelectViewModel(IProfileManager profileManager)
        {
            _profileManager = profileManager;
            AllProfiles = new ObservableCollection<string>(_profileManager.Profiles.Select(p => p.Name));
            _selectedProfile = AllProfiles.First();
        }

        private void ProfileChanged(string name)
        {
            _profileManager.OpenProfile(name);
        }
    }
}
