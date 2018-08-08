using GalaSoft.MvvmLight.CommandWpf;
using OWRankTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OWRankTracker.ViewModel
{
    class RecordMatchViewModel : MatchDataViewModelBase
    {
        private int? _cr;
        public int? CR
        {
            get { return _cr; }
            set
            {
                Set(ref _cr, value);
            }
        }

        private string[] _maps = Model.Maps.All;
        public string[] Maps
        {
            get { return _maps; }
        }

        private string _selectedMap;
        public string SelectedMap
        {
            get { return _selectedMap; }
            set 
            {
                Set(ref _selectedMap, value);
            }
        }

        public RelayCommand SaveCommand { get; private set; }

        public RecordMatchViewModel(IProfileManager profileManager) : base(profileManager)
        {
            SaveCommand = new RelayCommand(Save);
            SelectedMap = Maps.First();
        }

        private void Save()
        {
            if(!CR.HasValue)
            {
                MessageBox.Show("CR must be a number");
                return;
            }

            var lastMatch = MatchHistory.LastMatch;

            Model.MatchRecord newMatch;
            if (lastMatch != null)
            {
                newMatch = lastMatch.NewRelativeRecord(CR.Value, DateTime.Now, SelectedMap);
            }
            else
            {
                MessageBox.Show("Recording first game as a win, because there are no previous records to base it on");
                newMatch = new Model.MatchRecord()
                { 
                    CR = CR.Value,
                    Diff = CR.Value,
                    Date = DateTime.Now,
                    Map = SelectedMap,
                    Result = Model.MatchResult.WIN
                };
            }


            MatchHistory.Add(newMatch);
            MessengerInstance.Send(new Messages.NewMatchRecord(newMatch));

            CR = null;
            SelectedMap = Maps.First();
        }
    }
}
