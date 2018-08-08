using GalaSoft.MvvmLight;
using OWRankTracker.Messages;
using OWRankTracker.Model;
using OWRankTracker.MatchHistory;
using OWRankTracker.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.ViewModel
{
    class MapWinRatesViewModel : MatchDataViewModelBase
    {
        private ObservableCollection<MapStatistics> _stats;
        public ObservableCollection<MapStatistics> Stats
        {
            get
            {
                return _stats;
            }
            set
            {
                Set(ref _stats, value);
            }
        }

        private int _totalPlayed;
        public int TotalPlayed
        {
            get { return _totalPlayed; }
            set { Set(ref _totalPlayed, value); }
        }

        private int _totalWon;
        public int TotalWon
        {
            get { return _totalWon; }
            set { Set(ref _totalWon, value); }
        }

        private int _totalDrawn;
        public int TotalDrawn
        {
            get { return _totalDrawn; }
            set { Set(ref _totalDrawn, value); }
        }

        private int _totalLost;
        public int TotalLost
        {
            get { return _totalLost; }
            set { Set(ref _totalLost, value); }
        }


        private int _totalWithMaps;
        public int TotalWithMaps
        {
            get { return _totalWithMaps; }
            set { Set(ref _totalWithMaps, value); }
        }

        public MapWinRatesViewModel(IProfileManager profileManager) : base(profileManager)
        {
            GenerateStatistics();
            MessengerInstance.Register<Messages.NewMatchRecord>(this, OnNewMatchRecord);
        }

        private void OnNewMatchRecord(NewMatchRecord message)
        {
            GenerateStatistics();
        }

        protected override void ActiveProfileChanged()
        {
            GenerateStatistics();
        }

        private void GenerateStatistics()
        {
            string[] maps = Maps.All.Where(m => m != "N/A").ToArray();

            TotalPlayed = MatchHistory.Count();
            TotalWon = MatchHistory.Wins();
            TotalDrawn = MatchHistory.Draws();
            TotalLost = MatchHistory.Losses();
            TotalWithMaps = MatchHistory.Count(r => r.Map != "N/A");
            Stats = new ObservableCollection<MapStatistics>(
                from map in maps
                let matches = MatchHistory.Where(match => match.Map == map)
                orderby matches.Wins() descending, matches.Count() descending
                select new MapStatistics()
                {
                    Map = map,
                    TotalPlayed = matches.Count(),
                    TotalWon = matches.Wins(),
                    TotalDrawn = matches.Draws(),
                    TotalLost = matches.Losses()
                }
            );
        }
    }
}
