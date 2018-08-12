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
        public void OnCreation_SetsMinDate_ToMidnightOfFirstMatchInMatchHistory()
        {
            // Arrange
            DateTime expected = 
                (_defaultProfile.MatchHistory.First().Date.Date) 
                + new TimeSpan(0, 0, 0);
        
            // Act
            RankPlotSettingViewModel vm = new RankPlotSettingViewModel(_defaultProfile);

            // Assert
            Assert.AreEqual(expected, vm.MinDate);
        }

        [TestMethod]
        public void OnCreation_SetsStartDate_ToTheDateOfTheFirstMatch()
        {
            // Arrange
            DateTime expected = _defaultProfile.MatchHistory.First().Date.Date;

            // Act
            RankPlotSettingViewModel vm = new RankPlotSettingViewModel(_defaultProfile);

            // Assert
            Assert.AreEqual(expected, vm.StartDate);
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
            Assert.AreEqual(expected, vm.MaxDate);
        }

        [TestMethod]
        public void OnCreation_SetsEndDateTime_TheTheDateOfTheLastMatch()
        {
            // Arrange
            DateTime expected = _defaultProfile.MatchHistory.Last().Date.Date + new TimeSpan(23, 59, 59);

            // Act
            RankPlotSettingViewModel vm = new RankPlotSettingViewModel(_defaultProfile);

            // Assert
            Assert.AreEqual(expected, vm.EndDate);
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
            Assert.AreEqual(expected, vm.StartDate);
            Assert.AreEqual(expected, vm.MinDate);
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
            Assert.AreEqual(expected, vm.EndDate);
            Assert.AreEqual(expected, vm.MaxDate);
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
            Assert.AreEqual(endOfDate, vm.MaxDate);
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
            Assert.AreEqual(endOfDate, vm.EndDate);
        }

        [TestMethod]
        public void OnProfileChange_RecalculatesTheDatesForTheNewProfile()
        {
            // Arrange
            RankPlotSettingViewModel vm = new RankPlotSettingViewModel(_defaultProfile);

            DateTime start = _alternateProfile.MatchHistory.First().Date.Date;
            DateTime min = start.Date + new TimeSpan(0, 0, 0);
            DateTime end = _alternateProfile.MatchHistory.Last().Date.Date + new TimeSpan(23, 59, 59);
            DateTime max = end.Date + new TimeSpan(23, 59, 59);

            // Act
            vm.ChangeProfile(_alternateProfile);

            // Assert
            Assert.AreEqual(start, vm.StartDate);
            Assert.AreEqual(min, vm.MinDate);
            Assert.AreEqual(end, vm.EndDate);
            Assert.AreEqual(max, vm.MaxDate);
        }

        [TestMethod]
        public void SettingStartDate_AlwaysMovesToMidnightOfThatDate()
        {
            // Arrange
            RankPlotSettingViewModel vm = new RankPlotSettingViewModel(_defaultProfile);
            DateTime dt = new DateTime(2017, 02, 03, 03, 00, 00);

            // Act
            vm.StartDate = dt;

            // Assert
            Assert.AreEqual(new DateTime(2017, 02, 03, 00, 00, 00), vm.StartDate);
        }

        [TestMethod]
        public void SettingMidDate_AlwaysMovesToMidnightOfThatDate()
        {
            // Arrange
            RankPlotSettingViewModel vm = new RankPlotSettingViewModel(_defaultProfile);
            DateTime dt = new DateTime(2017, 02, 03, 03, 00, 00);

            // Act
            vm.MinDate = dt;

            // Assert
            Assert.AreEqual(new DateTime(2017, 02, 03, 00, 00, 00), vm.MinDate);
        }

        [TestMethod]
        public void SettingEndDate_AlwaysMovesToEndOfTheGivenDate()
        {
            // Arrange
            RankPlotSettingViewModel vm = new RankPlotSettingViewModel(_defaultProfile);
            DateTime dt = new DateTime(2017, 02, 03, 03, 00, 00);

            // Act
            vm.EndDate = dt;

            // Assert
            Assert.AreEqual(new DateTime(2017, 02, 03, 23, 59, 59), vm.EndDate);
        }

        [TestMethod]
        public void SettingMaxDate_AlwaysMovesToEndOfTheGivenDate()
        {
            // Arrange
            RankPlotSettingViewModel vm = new RankPlotSettingViewModel(_defaultProfile);
            DateTime dt = new DateTime(2017, 02, 03, 03, 00, 00);

            // Act
            vm.MaxDate = dt;

            // Assert
            Assert.AreEqual(new DateTime(2017, 02, 03, 23, 59, 59), vm.MaxDate);
        }
    }
}
