using OWRankTracker.Core.Services;

namespace OWRankTracker.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    class MainViewModel : MatchDataViewModelBase
    {
        private readonly IProfileManager _profileManager;

        public RecordMatchViewModel RecordMatchViewModel { get; private set; }
        public MatchRecordsTableViewModel MatchRecordsTableViewModel { get; private set; }
        public RankPlotViewModel RankPlotViewModel { get; private set; }
        public MapWinRatesViewModel MapWinRatesViewModel { get; private set; }
        public ProfileSelectViewModel ProfileSelectViewModel { get; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(
            IProfileManager profileManager,
            RecordMatchViewModel recordMatchViewModel,
            MatchRecordsTableViewModel matchRecordsTableViewModel,
            RankPlotViewModel rankPlotViewModel,
            MapWinRatesViewModel mapWinRatesViewModel,
            ProfileSelectViewModel profileSelectViewModel
        ) : base(profileManager)
        {
            _profileManager = profileManager;
            RecordMatchViewModel = recordMatchViewModel;
            MatchRecordsTableViewModel = matchRecordsTableViewModel;
            RankPlotViewModel = rankPlotViewModel;
            MapWinRatesViewModel = mapWinRatesViewModel;
            ProfileSelectViewModel = profileSelectViewModel;
        }
    }
}