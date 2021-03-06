﻿using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OWRankTracker.Core.Model;
using OWRankTracker.Core.Profile;
using OWRankTracker.Core.Services;
using OWRankTracker.Test.Fakers;
using OWRankTracker.ViewModel;

namespace OWRankTracker.Test.ViewModel
{
    [TestClass]
    public class MapWinRatesViewModelTest : ViewModelTestBase
    {
        [TestMethod]
        public void OnCreation_CalculatesTotalPlayed()
        {
            // Arrange
            MapWinRatesViewModel vm = new MapWinRatesViewModel(_profileManager);

            // Act
            int actual = vm.TotalPlayed;

            // Assert
            Assert.AreEqual(_defaultProfile.MatchHistory.Count(), actual);
        }

        [TestMethod]
        public void OnCreation_CalculatesNumberOfWins()
        {
            // Arrange
            MapWinRatesViewModel vm = new MapWinRatesViewModel(_profileManager);

            // Act
            int actual = vm.TotalWon;

            // Assert
            Assert.AreEqual(2, actual);
        }

        [TestMethod]
        public void OnCreation_CalculatesNumberOfDraws()
        {
            // Arrange
            MapWinRatesViewModel vm = new MapWinRatesViewModel(_profileManager);

            // Act
            int actual = vm.TotalDrawn;

            // Assert
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void OnCreation_CalculatesNumberOfLosses()
        {
            // Arrange
            MapWinRatesViewModel vm = new MapWinRatesViewModel(_profileManager);

            // Act
            int actual = vm.TotalLost;

            // Assert
            Assert.AreEqual(2, actual);
        }

        [TestMethod]
        public void OnCreation_CalculatesTheNumberOfGamesPlayedWithARecordedMap()
        {
            // Arrange
            MapWinRatesViewModel vm = new MapWinRatesViewModel(_profileManager);

            // Act
            int actual = vm.TotalWithMaps;

            // Assert
            Assert.AreEqual(_defaultProfile.MatchHistory.Count() - 1, actual);
        }

        [TestMethod]
        public void OnCreation_CalculatesStatisticsPerMap_ForAllMaps_ExceptNA_WithCorrectValues()
        {
            // Arrange
            MapWinRatesViewModel vm = new MapWinRatesViewModel(_profileManager);

            // Act
            IEnumerable<MapStatistics> stats = vm.Stats;

            // Assert
            Assert.AreEqual(Maps.All.Length - 1, stats.Count());

            MapStatistics matchOneStats = stats.First(s => s.Map == Maps.All.ElementAt(1));
            Assert.AreEqual(2, matchOneStats.TotalWon);
            Assert.AreEqual(0, matchOneStats.TotalLost);
            Assert.AreEqual(0, matchOneStats.TotalDrawn);
        }

        [TestMethod]
        public void OnNewMatchRecord_RecalculatesTotalPlayed()
        {
            // Arrange
            MatchRecord newRecord = MatchRecordFaker.CreateRecord();
            MapWinRatesViewModel vm = new MapWinRatesViewModel(_profileManager);

            // Act
            NewMatchRecord(newRecord);

            // Assert
            Assert.AreEqual(_profileManager.ActiveProfile.MatchHistory.Count(), vm.TotalPlayed);
        }

        [TestMethod]
        public void OnNewMatchRecord_RecalculatesTotalWon()
        {
            // Arrange
            MatchRecord newRecord = MatchRecordFaker.CreateRecord(result: MatchResult.WIN);
            MapWinRatesViewModel vm = new MapWinRatesViewModel(_profileManager);
            int won = vm.TotalWon;

            // Act
            NewMatchRecord(newRecord);

            // Assert
            Assert.AreEqual(won + 1, vm.TotalWon);
        }

        [TestMethod]
        public void OnNewMatchRecord_RecalculatesTotalDrawn()
        {
            // Arrange
            MatchRecord newRecord = MatchRecordFaker.CreateRecord(result: MatchResult.DRAW);
            MapWinRatesViewModel vm = new MapWinRatesViewModel(_profileManager);
            int drawn = vm.TotalDrawn;

            // Act
            NewMatchRecord(newRecord);

            // Assert
            Assert.AreEqual(drawn + 1, vm.TotalDrawn);
        }

        [TestMethod]
        public void OnNewMatchRecord_RecalculatesTotalLost()
        {
            // Arrange
            MatchRecord newRecord = MatchRecordFaker.CreateRecord(result: MatchResult.LOSE);
            MapWinRatesViewModel vm = new MapWinRatesViewModel(_profileManager);
            int lost = vm.TotalLost;

            // Act
            NewMatchRecord(newRecord);

            // Assert
            Assert.AreEqual(lost + 1, vm.TotalLost);
        }

        [TestMethod]
        public void OnProfileChange_RecalculatesStatistics_ForNewActiveProfile()
        {
            // Arrange
            MapWinRatesViewModel vm = new MapWinRatesViewModel(_profileManager);

            // Act
            _profileManager.OpenProfile(_alternateProfile.Name);

            // Assert
            Assert.AreEqual(_alternateProfile.MatchHistory.Count(), vm.TotalPlayed);
            Assert.AreEqual(_alternateProfile.MatchHistory.Wins(), vm.TotalWon);
            Assert.AreEqual(_alternateProfile.MatchHistory.Draws(), vm.TotalDrawn);
            Assert.AreEqual(_alternateProfile.MatchHistory.Losses(), vm.TotalLost);
            Assert.AreEqual(_alternateProfile.MatchHistory.Count(p => p.Map != "N/A"), vm.TotalWithMaps);
        }

        private void NewMatchRecord(MatchRecord record)
        {
            _profileManager.ActiveProfile.MatchHistory.Add(record);
            _messenger.Send(new Messages.NewMatchRecord(record));
        }
    }
}
