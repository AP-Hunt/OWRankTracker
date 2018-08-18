using OWRankTracker.Core.Model;
using OWRankTracker.Core.Services;
using OWRankTracker.Messages;
using System.Collections.ObjectModel;
using System.Linq;

namespace OWRankTracker.ViewModel
{
    class MatchRecordsTableViewModel : MatchDataViewModelBase
    {
        private ObservableCollection<MatchRecord> _records;
        public ObservableCollection<MatchRecord> Records
        {
            get
            {
                if (_records == null)
                {
                    _records = new ObservableCollection<MatchRecord>(
                        MatchHistory.OrderByDescending(r => r.Date)
                    );
                }

                return _records;
            }
            private set
            {
                Set(ref _records, value);
            }
        }

        private double _averageSessionCRChange;
        public double AverageSessionCRChange
        {
            get { return _averageSessionCRChange; }
            set { Set(ref _averageSessionCRChange, value); }
        }

        private double _totalSessions;
        public double TotalSessions
        {
            get { return _totalSessions; }
            set { Set(ref _totalSessions, value); }
        }

        public MatchRecordsTableViewModel(IProfileManager profileManager) : base(profileManager)
        {
            MessengerInstance.Register<Messages.NewMatchRecord>(this, OnNewRecord);
        }

        protected override void ActiveProfileChanged()
        {
            Records = new ObservableCollection<MatchRecord>(
                MatchHistory.OrderByDescending(r => r.Date)
            );
        }

        private void OnNewRecord(NewMatchRecord message)
        {
            Records = new ObservableCollection<MatchRecord>(
                MatchHistory.OrderByDescending(r => r.Date)
            );
        }
    }
}
