using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using OWRankTracker.MatchHistory;
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

        public RankPlotSettingViewModel(DateTime min, DateTime max)
        {
            _minDateTime = min;
            _startDateTime = min;

            _maxDateTime = max;
            _endDateTime = max;

            ShowGameSessions = false;
        }

        public void ExtendDateRangeEnding(DateTime end)
        {
            DateTime previousMax = MaxDateTime;
            MaxDateTime = end;

            if(previousMax == EndDateTime)
            {
                EndDateTime = end;
            }
        }

        public void ChangeProfile(IMatchHistory profile)
        {
            _isProfileChanging = true;

            // Set the min and max values to ensure
            // the new start and end will be in range
            MinDateTime = DateTime.MinValue;
            MaxDateTime = DateTime.MaxValue;

            DateTime start = profile.FirstOrDefault()?.Date ?? DateTime.Today;
            DateTime end = profile.LastOrDefault()?.Date ?? DateTime.Today;

            StartDateTime = start;
            EndDateTime = end;

            // Bring the min and max inwards
            MinDateTime = start;
            MaxDateTime = end;

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
    }
}
