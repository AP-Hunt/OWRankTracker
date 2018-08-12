using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using MoreLinq;
using OWRankTracker.Messages;
using OWRankTracker.Model;
using OWRankTracker.Services;

namespace OWRankTracker.ViewModel
{
    class RankPlotViewModel : MatchDataViewModelBase
    {
        private SeriesCollection _dataSeries;
        public SeriesCollection DataSeries
        {
            get { return _dataSeries; }
            private set
            {
                Set(ref _dataSeries, value);
            }
        }

        private RankPlotSettingViewModel _settings;
        public RankPlotSettingViewModel Settings
        {
            get { return _settings; }
            set { Set(ref _settings, value); }
        }

        private string[] _labels;
        public string[] Labels
        {
            get { return _labels; }
            private set
            {
                Set(ref _labels, value);
            }
        }

        private Func<double, string> _yFormatter;
        public Func<double, string> YFormatter
        {
            get { return _yFormatter; }
            private set
            {
                Set(ref _yFormatter, value);
            }
        }

        private Func<double, string> _xFormatter;
        public Func<double, string> XFormatter
        {
            get { return _xFormatter; }
            private set
            {
                Set(ref _xFormatter, value);
            }
        }

        private double _yAxisStartValue = 0;
        public double YAxisStartValue
        {
            get { return _yAxisStartValue; }
            set { Set(ref _yAxisStartValue, value); }
        }

        private IChartLegend _legend = null;
        public IChartLegend Legend
        {
            get { return _legend; }
            set { Set(ref _legend, value); }
        }

        private SectionsCollection _xAxisSections;
        public SectionsCollection XAxisSections
        {
            get { return _xAxisSections; }
            set { Set(ref _xAxisSections, value); }
        }

        public RankPlotViewModel(IProfileManager profileManager) : base(profileManager)
        {
            Settings = new RankPlotSettingViewModel(ActiveProfile);
            GeneratePlot();
            MessengerInstance.Register<Messages.NewMatchRecord>(this, OnNewRecord);
            MessengerInstance.Register<Messages.PlotDateRangeChanged>(this, OnPlotDateRangeChange);
        }

        private void OnPlotDateRangeChange(Messages.PlotDateRangeChanged message)
        {
            if (message.Sender == Settings)
            {
                GeneratePlot();
            }
        }

        private void OnNewRecord(NewMatchRecord message)
        {
            Settings.ExtendDateRangeEnding(MatchHistory.Last().Date);
            AddRecordToPlot(message.Record);
        }

        private void AddRecordToPlot(MatchRecord record)
        {
            if(DataSeries != null)
            {
                DataSeries.First().Values.Add(new LiveCharts.Defaults.ObservableValue(record.CR));
            }
        }

        protected override void ActiveProfileChanged()
        {
            Settings.ChangeProfile(ActiveProfile);
            GeneratePlot();
        }

        private void GeneratePlot()
        {
            var matchesInDateRange = MatchHistory.Where(m => Between(m, Settings.StartDate, Settings.EndDate));

            var lineSeries = new LineSeries
            {
                Title = "SR",
                Values = new ChartValues<LiveCharts.Defaults.ObservableValue>(
                    from r in matchesInDateRange
                    select new LiveCharts.Defaults.ObservableValue(r.CR)
                ),
                LineSmoothness = 0,
                PointGeometry = DefaultGeometries.Circle,
                Fill = System.Windows.Media.Brushes.Transparent,
                DataLabels = false
            };

            Brush weekendBrush = Brushes.DodgerBlue;

            lineSeries.Configuration = LiveCharts.Configurations.Mappers.Xy<LiveCharts.Defaults.ObservableValue>()
                .X((item, index) => index)
                .Y(item => item.Value)
                .Fill(ShouldBeWeekendColoured(weekendBrush))
                .Stroke(ShouldBeWeekendColoured(weekendBrush));

            var xLabels =
                from r in MatchHistory
                select r.Date.ToString("G");


            Func<double, string> yFormatter = (i) => i.ToString();
            Func<double, string> xFormatter = (i) => i.ToString("C");

            if(DataSeries != null)
            {
                DataSeries.Clear();
            }
            else
            {
                DataSeries = new SeriesCollection();
            }
            DataSeries.Add(lineSeries);
            Labels = xLabels.ToArray();
            YFormatter = yFormatter;
            XFormatter = xFormatter;
            YAxisStartValue = matchesInDateRange.FirstOrDefault()?.CR ?? 0;

            if(XAxisSections != null)
            {
                XAxisSections.Clear();
            }
            else
            {
                XAxisSections = new SectionsCollection();
            }

            if (Settings.ShowGameSessions)
            {
                XAxisSections.AddRange(CreateXAxisSections());
            }
        }

        private bool Between(Model.MatchRecord record, DateTime start, DateTime end)
        {
            return record.Date >= start && record.Date <= end;
        }

        private Func<LiveCharts.Defaults.ObservableValue, int, object> ShouldBeWeekendColoured(Brush weekendBrush)
        {
            return (item, index) =>
            {
                if (index > MatchHistory.Count() - 1)
                {
                    return null;
                }
                return IsWeekendDay(MatchHistory.ElementAt(index).Date) ? weekendBrush : null;
            };
        }

        private SectionsCollection CreateXAxisSections()
        {
            var stats = new Statistics(MatchHistory);
            var sessions = stats.FindGameSessions();

            SectionsCollection sections = new SectionsCollection();

            Brush[] brushes = new Brush[]
            {
                Brushes.AntiqueWhite,
                Brushes.Beige,
                Brushes.Lavender
            };

            int start = 0;
            int brushIndex = 0;
            foreach(Model.GameSession session in sessions)
            {
                sections.Add(new AxisSection()
                {
                    Value = start,
                    SectionWidth = session.Count(),
                    Fill = brushes[brushIndex]
                });

                start += session.Count();
                brushIndex = (brushIndex+1)% brushes.Length;
            }


            return sections;
        }

        private bool IsWeekendDay(DateTime date)
        {
            if(date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                return true;
            }

            return false;
        }
    }
}
