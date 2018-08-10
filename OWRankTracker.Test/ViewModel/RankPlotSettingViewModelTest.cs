using Microsoft.VisualStudio.TestTools.UnitTesting;
using OWRankTracker.Profile;
using OWRankTracker.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Test.ViewModel
{
    [TestClass]
    public class RankPlotSettingViewModelTest : ViewModelTestBase
    {
        [TestMethod]
        public void OnCreation_SetsMinDateTime_ToMidnightOfFirstMatchInMatchHistory()
        {
            // Arrange
            DateTime expected = 
                (_defaultProfile.MatchHistory.First().Date.Date) 
                + new TimeSpan(0, 0, 0);
        
            // Act
            RankPlotSettingViewModel vm = new RankPlotSettingViewModel(_defaultProfile);

            // Assert
            Assert.AreEqual(expected, vm.MinDateTime);
        }

        [TestMethod]
        public void OnCreation_SetsStartDateTime_ToTheDateOfTheFirstMatch()
        {
            // Arrange
            DateTime expected = _defaultProfile.MatchHistory.First().Date;

            // Act
            RankPlotSettingViewModel vm = new RankPlotSettingViewModel(_defaultProfile);

            // Assert
            Assert.AreEqual(expected, vm.StartDateTime);
        }

        [TestMethod]
        public void OnCreation_SetsMaxDateTime_ToEndOfTheDayOfTheLastRecordedMatch()
        {
            // Arrange
            DateTime expected =
                (_defaultProfile.MatchHistory.Last().Date.Date)
                + new TimeSpan(23, 59, 59);

            // Act
            RankPlotSettingViewModel vm = new RankPlotSettingViewModel(_defaultProfile);

            // Assert
            Assert.AreEqual(expected, vm.MaxDateTime);
        }

        [TestMethod]
        public void OnCreation_SetsEndDateTime_TheTheDateOfTheLastMatch()
        {
            // Arrange
            DateTime expected = _defaultProfile.MatchHistory.Last().Date;

            // Act
            RankPlotSettingViewModel vm = new RankPlotSettingViewModel(_defaultProfile);

            // Assert
            Assert.AreEqual(expected, vm.EndDateTime);
        }

        [TestMethod]
        public void OnCreation_IfMatchHistoryIsEmpty_SetsMinAndStartToMidnightOfToday()
        {
            // Arrange
            DateTime expected = DateTime.Today + new TimeSpan(0, 0, 0);
            IProfile emptyProfile = new Fixtures.EmptyProfile();

            // Act
            RankPlotSettingViewModel vm = new RankPlotSettingViewModel(emptyProfile);

            // Assert
            Assert.AreEqual(expected, vm.StartDateTime);
            Assert.AreEqual(expected, vm.MinDateTime);
        }

        [TestMethod]
        public void OnCreation_IfMatchHistoryIsEmpty_SetsMaxAndEndToEndOfToday()
        {
            // Arrange
            DateTime expected = DateTime.Today + new TimeSpan(23, 59, 59);
            IProfile emptyProfile = new Fixtures.EmptyProfile();

            // Act
            RankPlotSettingViewModel vm = new RankPlotSettingViewModel(emptyProfile);

            // Assert
            Assert.AreEqual(expected, vm.EndDateTime);
            Assert.AreEqual(expected, vm.MaxDateTime);
        }

        [TestMethod]
        public void ExtendDateRangeEnding_SetsTheMaxDateTimeToEndOfTheGivenDate()
        {
            // Arrange
            RankPlotSettingViewModel vm = new RankPlotSettingViewModel(_defaultProfile);
            DateTime date = new DateTime(2020, 01, 01);
            DateTime endOfDate = date.Date + new TimeSpan(23, 59, 59);

            // Act
            vm.ExtendDateRangeEnding(date);

            // Assert
            Assert.AreEqual(endOfDate, vm.MaxDateTime);
        }

        [TestMethod]
        public void ExtendDateRangeEnding_SetsTheEndDateTimeToTheEndOfTheGivenDate()
        {
            // Arrange
            RankPlotSettingViewModel vm = new RankPlotSettingViewModel(_defaultProfile);
            DateTime date = new DateTime(2020, 01, 01);
            DateTime endOfDate = date.Date + new TimeSpan(23, 59, 59);

            // Act
            vm.ExtendDateRangeEnding(date);

            // Assert
            Assert.AreEqual(endOfDate, vm.EndDateTime);
        }

        [TestMethod]
        public void OnProfileChange_RecalculatesTheDatesForTheNewProfile()
        {
            // Arrange
            RankPlotSettingViewModel vm = new RankPlotSettingViewModel(_defaultProfile);

            DateTime start = _alternateProfile.MatchHistory.First().Date;
            DateTime min = start.Date + new TimeSpan(0, 0, 0);
            DateTime end = _alternateProfile.MatchHistory.Last().Date;
            DateTime max = end.Date + new TimeSpan(23, 59, 59);

            // Act
            vm.ChangeProfile(_alternateProfile);

            // Assert
            Assert.AreEqual(start, vm.StartDateTime);
            Assert.AreEqual(min, vm.MinDateTime);
            Assert.AreEqual(end, vm.EndDateTime);
            Assert.AreEqual(max, vm.MaxDateTime);
        }
    }
}
