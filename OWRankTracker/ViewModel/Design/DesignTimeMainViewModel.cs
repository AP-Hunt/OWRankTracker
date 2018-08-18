using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using OWRankTracker.DesignTime;
using OWRankTracker.Services;
using OWRankTracker.Services.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.ViewModel.Design
{
    class DesignTimeMainViewModel : ViewModel.MainViewModel
    {
        private static IProfileManager _profileManager;
        private static IDialogService _dialogService;
        private static IWindowService _windowService;

        static DesignTimeMainViewModel()
        {
            _profileManager = new DesignTimeProfileManager();
            _dialogService = new Services.Wpf.MessageBoxService();
            _windowService = new DesignTimeWindowService();
        }

        public DesignTimeMainViewModel()
            :base(
            _profileManager,
            new ViewModel.RecordMatchViewModel(_profileManager, _dialogService),
            new ViewModel.MatchRecordsTableViewModel(_profileManager),
            new ViewModel.RankPlotViewModel(_profileManager),
            new ViewModel.MapWinRatesViewModel(_profileManager),
            new ViewModel.ProfileSelectViewModel(_profileManager, _windowService)
        )
        {
        }
    }
}
