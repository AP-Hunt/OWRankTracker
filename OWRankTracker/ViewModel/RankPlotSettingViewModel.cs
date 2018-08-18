using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using OWRankTracker.Core.MatchHistory;
using OWRankTracker.Core.Profile;
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

        private DateTime _minDate;
        /// <summary>
        /// Sets the minimum date selectable
        /// </summary>
        /// <remarks>
        /// Ignores time component, and uses midnight
        /// of the given date
        /// </remarks>
        public DateTime MinDate
        {
            get { return _minDate; }
            set
            {
                try
                {
                    DateTime newDT =
                        value.Date
                        + new TimeSpan(0, 0, 0);

                    Set(ref _minDate, newDT);
                    this.DispatchDateRangeChanged();
                }
                catch (ArgumentOutOfRangeException)
                { }
            }
        }

        private DateTime _startDate;
        /// <summary>
        /// Sets the starting date of the rank plot
        /// </summary>
        /// <remarks>
        /// Ignores time component, and uses midnight
        /// of the given date
        /// </remarks>
        public DateTime StartDate
        {
            get { return _startDate; }
            set 
            {
                DateTime newDT =
                    value.Date
                    + new TimeSpan(0, 0, 0);

                Set(ref _startDate, newDT, broadcast: !_isProfileChanging);
                this.DispatchDateRangeChanged();
            }
        }

        private DateTime _maxDate;
        /// <summary>
        /// Sets the maximum date selectable
        /// </summary>
        /// <remarks>
        /// Ignores time component, and uses midnight
        /// of the given date
        /// </remarks>
        public DateTime MaxDate
        {
            get { return _maxDate; }
            set
            {
                try
                {
                    DateTime newDT =
                        value.Date
                        + new TimeSpan(23, 59, 59);

                    Set(ref _maxDate, newDT);
                    this.DispatchDateRangeChanged();
                }
                catch (ArgumentOutOfRangeException)
                { }
            }
        }

        private DateTime _endDate;
        /// <summary>
        /// Sets the end date of the rank plot
        /// </summary>
        /// <remarks>
        /// Ignores time component, and uses midnight
        /// of the given date
        /// </remarks>
        public DateTime EndDate
        {
            get { return _endDate; }
            set 
            {
                DateTime newDT =
                    value.Date
                    + new TimeSpan(23, 59, 59);

                Set(ref _endDate, newDT, broadcast: !_isProfileChanging);
                this.DispatchDateRangeChanged();
            }
        }

        public RankPlotSettingViewModel(IProfile profile)
        {
            SetDateRanges(profile);
        }

        public void ExtendDateRangeEnding(DateTime end)
        {
            DateTime endOfDate = end.Date + new TimeSpan(23, 59, 59);
            MaxDate = endOfDate;
            EndDate = endOfDate;
        }

        public void ChangeProfile(IProfile profile)
        {
            _isProfileChanging = true;
            SetDateRanges(profile);
            _isProfileChanging = false;

            this.DispatchDateRangeChanged();
            RaisePropertyChanged(nameof(EndDate));
            RaisePropertyChanged(nameof(StartDate));
        }

        private void DispatchDateRangeChanged()
        {
            if (!_isProfileChanging)
            {
                MessengerInstance.Send(new Messages.PlotDateRangeChanged(this, StartDate, EndDate));
            }
        }

        private void SetDateRanges(IProfile profile)
        {
            DateTime start;
            DateTime end;
            if (profile.MatchHistory.Any())
            {
                start = profile.MatchHistory.First().Date;
                end = profile.MatchHistory.Last().Date;
            }
            else
            {
                start = DateTime.Today;
                end = DateTime.Today;
                
            }

            // Set min and max first
            // to prevent start and end
            // being out of bounds
            MinDate = start;
            MaxDate = end;

            StartDate = start;
            EndDate = end;

        }
    }
}
