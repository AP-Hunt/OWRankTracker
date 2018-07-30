using GalaSoft.MvvmLight;
using OWRankTracker.Messages;
using OWRankTracker.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.ViewModel
{
    class MatchRecordsTableViewModel : MatchDataViewModelBase
    {
        private ObservableCollection<Model.MatchRecord> _records;
        public ObservableCollection<Model.MatchRecord> Records
        {
            get
            {
                if (_records == null)
                {
                    _records = new ObservableCollection<Model.MatchRecord>(
                        MatchRepository.OrderByDescending(r => r.Date)
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
            GenerateStatistics();
        }

        protected override void ActiveProfileChanged()
        {
            Records = new ObservableCollection<Model.MatchRecord>(
                MatchRepository.OrderByDescending(r => r.Date)
            );
            GenerateStatistics();
        }

        private void OnNewRecord(NewMatchRecord message)
        {
            Records = new ObservableCollection<Model.MatchRecord>(
                MatchRepository.OrderByDescending(r => r.Date)
            );
            GenerateStatistics();
        }

        private void GenerateStatistics()
        {
            var stats = new Statistics(MatchRepository);
            var sessions = stats.FindGameSessions();

            AverageSessionCRChange = 0;
            if (sessions.Any())
            {
                sessions.Select(s => s.CRChange).Average();
            }
            TotalSessions = sessions.Count();
        }
    }
}
