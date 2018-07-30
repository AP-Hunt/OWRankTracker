using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OWRankTracker.Model;
using OWRankTracker.Services;

namespace OWRankTracker.Test.Services
{
    [TestClass]
    public class StatisticsTest
    {
        private FixtureFactories.MatchRecordFactory _matchRecordFactory = new FixtureFactories.MatchRecordFactory();
        
        [TestMethod]
        public void GameSessions_FindsGameSessionClusters_AcrossDifferentDays()
        {
            // Arrange
            var clusterOne = _matchRecordFactory.NMatchesBetweenDates(7, new DateTime(2018, 01, 01, 21, 25, 00));
            var clusterTwo = _matchRecordFactory.NMatchesBetweenDates(12, new DateTime(2018, 01, 02, 18, 00, 00));

            List<MatchRecord> matches = new List<MatchRecord>();
            matches.AddRange(clusterOne);
            matches.AddRange(clusterTwo);

            Statistics stats = new Statistics(matches);

            // Act
            IEnumerable<GameSession> sessions = stats.FindGameSessions();

            // Assert
            Assert.AreEqual(2, sessions.Count(), 1, "Didn't find the expected number of clusters, +/- 1.");
        }
    }
}
