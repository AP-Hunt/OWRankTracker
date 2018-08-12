using Microsoft.VisualStudio.TestTools.UnitTesting;
using OWRankTracker.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Test.ViewModel
{
    [TestClass]
    public class RankPlotVIewModelTest : ViewModelTestBase
    {
        /// <summary>
        /// A bug in the LiveCharts library
        /// causes an exception when clearing
        /// the data series, if it hasn't
        /// been bound to a chart.
        /// 
        /// To avoid this, we create a chart
        /// and bind the series to it in tests.
        /// </summary>
        private LiveCharts.Wpf.CartesianChart _chart;

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            // Set up a small number of date dispersed
            // matches to make it easy to see
            // when each has been included/excluded
            _emptyProfile.MatchHistory.Add(
                Fakers.MatchRecordFaker.CreateRecord(cr: 1010, date: new DateTime(2018, 01, 01, 14, 00, 00))
            );

            _emptyProfile.MatchHistory.Add(
                Fakers.MatchRecordFaker.CreateRecord(cr: 1020, date: new DateTime(2018, 01, 11, 14, 00, 00))
            );

            _emptyProfile.MatchHistory.Add(
                Fakers.MatchRecordFaker.CreateRecord(cr: 1030, date: new DateTime(2018, 01, 21, 14, 00, 00))
            );

            _profileManager.OpenProfile(_emptyProfile.Name);
            _chart = new LiveCharts.Wpf.CartesianChart();
        }

        [TestMethod]
        public void ByDefault_PlotShowsAllMatches()
        {
            // Act
            RankPlotViewModel vm = new RankPlotViewModel(_profileManager);
            _chart.Series = vm.DataSeries;

            // Assert
            var series = vm.DataSeries;
            var values = (LiveCharts.ChartValues<LiveCharts.Defaults.ObservableValue>)series.First().ActualValues;
            Assert.AreEqual(3, series.First().ActualValues.Count);
            CollectionAssert.AreEquivalent(
                new double[] { 1010, 1020, 1030 },
                values.Select(v => v.Value).ToArray()
            );
        }

        [TestMethod]
        public void WhenPlotSettingsDateRangeChanges_RedrawsPlotToIncludeOnlyPointsInsideTheDateRange()
        {
            // Arrange
            RankPlotViewModel vm = new RankPlotViewModel(_profileManager);
            _chart.Series = vm.DataSeries;

            // Act
            vm.Settings.StartDate = new DateTime(2018, 01, 05);

            // Assert
            var series = vm.DataSeries;
            var values = (LiveCharts.ChartValues<LiveCharts.Defaults.ObservableValue>)series.First().ActualValues;
            Assert.AreEqual(2, series.First().ActualValues.Count);
            CollectionAssert.AreEquivalent(
                new double[] { 1020, 1030 },
                values.Select(v => v.Value).ToArray()
            );
        }
    }
}
