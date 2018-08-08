using GalaSoft.MvvmLight.Messaging;
using OWRankTracker.DesignTime;
using OWRankTracker.Services;
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

        static DesignTimeMainViewModel()
        {
            _profileManager = new DesignTimeProfileManager();
        }

        public DesignTimeMainViewModel()
            :base(
            _profileManager,
            new ViewModel.RecordMatchViewModel(_profileManager),
            new ViewModel.MatchRecordsTableViewModel(_profileManager),
            new ViewModel.RankPlotViewModel(_profileManager),
            new ViewModel.MapWinRatesViewModel(_profileManager),
            new ViewModel.ProfileSelectViewModel(_profileManager)
        )
        {
        }
    }
}
