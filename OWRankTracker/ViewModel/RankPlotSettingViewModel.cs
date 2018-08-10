using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using OWRankTracker.MatchHistory;
using OWRankTracker.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.ViewModel
{
    class RankPlotSettingViewModel : ViewModelBase
    {
        private bool _isProfileChanging = false;

        private bool _showGameSessions;
        public bool ShowGameSessions
        {
            get { return _showGameSessions; }
            set { Set(ref _showGameSessions, value); }
        }

        private DateTime _minDateTime;
        public DateTime MinDateTime
        {
            get { return _minDateTime; }
            set
            {
                try
                {
                    Set(ref _minDateTime, value);
                }
                catch (ArgumentOutOfRangeException)
                { }
            }
        }

        private DateTime _startDateTime;
        public DateTime StartDateTime
        {
            get { return _startDateTime; }
            set 
            { 
                Set(ref _startDateTime, value);
                this.DispatchDateRangeChanged();
            }
        }

        private DateTime _maxDateTime;
        public DateTime MaxDateTime
        {
            get { return _maxDateTime; }
            set
            {
                try
                {
                    Set(ref _maxDateTime, value);
                }
                catch (ArgumentOutOfRangeException)
                { }
            }
        }

        private DateTime _endDateTime;
        public DateTime EndDateTime
        {
            get { return _endDateTime; }
            set 
            { 
                Set(ref _endDateTime, value);
                this.DispatchDateRangeChanged();
            }
        }

        public RankPlotSettingViewModel(IProfile profile)
        {
            SetDateRanges(profile);
            ShowGameSessions = false;
        }

        public void ExtendDateRangeEnding(DateTime end)
        {
            DateTime endOfDate = end.Date + new TimeSpan(23, 59, 59);
            MaxDateTime = endOfDate;
            EndDateTime = endOfDate;
        }

        public void ChangeProfile(IProfile profile)
        {
            _isProfileChanging = true;
            SetDateRanges(profile);
            _isProfileChanging = false;

            this.DispatchDateRangeChanged();
        }

        private void DispatchDateRangeChanged()
        {
            if (!_isProfileChanging)
            {
                MessengerInstance.Send(new Messages.PlotDateRangeChanged(this, StartDateTime, EndDateTime));
            }
        }

        private void SetDateRanges(IProfile profile)
        {
            if (profile.MatchHistory.Any())
            {
                DateTime start = profile.MatchHistory.First().Date;
                DateTime end = profile.MatchHistory.Last().Date;

                DateTime midnightOfStart = start.Date + new TimeSpan(0, 0, 0);
                DateTime endOfDayOfEnd = end.Date + new TimeSpan(23, 59, 59);

                _minDateTime = midnightOfStart;
                _startDateTime = start;

                _maxDateTime = endOfDayOfEnd;
                _endDateTime = end;
            }
            else
            {
                DateTime start = DateTime.Today + new TimeSpan(0, 0, 0); ;
                DateTime end = DateTime.Today + new TimeSpan(23, 59, 59); ;

                _minDateTime = start;
                _startDateTime = start;

                _maxDateTime = end;
                _endDateTime = end;
            }
        }
    }
}
