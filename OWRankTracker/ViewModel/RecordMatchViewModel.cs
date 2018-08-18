using GalaSoft.MvvmLight.CommandWpf;
using OWRankTracker.Core.Model;
using OWRankTracker.Core.Services;
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
        private readonly GalaSoft.MvvmLight.Views.IDialogService _dialogService;

        private int? _cr;
        public int? CR
        {
            get { return _cr; }
            set
            {
                Set(ref _cr, value);
            }
        }

        private string[] _maps = Core.Model.Maps.All;
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

        public RecordMatchViewModel(IProfileManager profileManager, GalaSoft.MvvmLight.Views.IDialogService dialogService) : base(profileManager)
        {
            _dialogService = dialogService;
            SaveCommand = new RelayCommand(Save, () => CR.HasValue);
            SelectedMap = Maps.First();
        }

        private async void Save()
        {
            var lastMatch = MatchHistory.LastMatch;

            MatchRecord newMatch;
            if (lastMatch != null)
            {
                newMatch = lastMatch.NewRelativeRecord(CR.Value, DateTime.Now, SelectedMap);
            }
            else
            {
                await _dialogService.ShowMessage("Recording first game as a win, because there are no previous records to base it on", "Recording new match");
                newMatch = new MatchRecord()
                { 
                    CR = CR.Value,
                    Diff = CR.Value,
                    Date = DateTime.Now,
                    Map = SelectedMap,
                    Result = MatchResult.WIN
                };
            }


            MatchHistory.Add(newMatch);
            MessengerInstance.Send(new Messages.NewMatchRecord(newMatch));

            CR = null;
            SelectedMap = Maps.First();
        }
    }
}
