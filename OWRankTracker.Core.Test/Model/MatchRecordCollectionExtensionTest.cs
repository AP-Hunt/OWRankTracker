using Microsoft.VisualStudio.TestTools.UnitTesting;
using OWRankTracker.Core.Model;
using OWRankTracker.Test.Fakers;
using System.Collections.Generic;

namespace OWRankTracker.Test.Model
{
    [TestClass]
    public class MatchRecordCollectionExtensionTest
    {
        [TestMethod]
        public void Wins_CountsNumberOfMatchesWithWinResult()
        {
            // Arrange
            List<MatchRecord> records = new List<MatchRecord>()
            {
                MatchRecordFaker.CreateRecord(result: MatchResult.WIN),
                MatchRecordFaker.CreateRecord(result: MatchResult.WIN),
                MatchRecordFaker.CreateRecord(result: MatchResult.LOSE),
                MatchRecordFaker.CreateRecord(result: MatchResult.DRAW),
            };

            // Act
            int actual = records.Wins();

            // Assert
            Assert.AreEqual(2, actual);
        }

        [TestMethod]
        public void Draws_CountsNumberOfMatchesWithDrawResult()
        {
            // Arrange
            List<MatchRecord> records = new List<MatchRecord>()
            {
                MatchRecordFaker.CreateRecord(result: MatchResult.DRAW),
                MatchRecordFaker.CreateRecord(result: MatchResult.DRAW),
                MatchRecordFaker.CreateRecord(result: MatchResult.WIN),
                MatchRecordFaker.CreateRecord(result: MatchResult.LOSE),
            };

            // Act
            int actual = records.Draws();

            // Assert
            Assert.AreEqual(2, actual);
        }

        [TestMethod]
        public void Losses_CountsNumberOfMatchesWithLoseResult()
        {
            // Arrange
            List<MatchRecord> records = new List<MatchRecord>()
            {
                MatchRecordFaker.CreateRecord(result: MatchResult.LOSE),
                MatchRecordFaker.CreateRecord(result: MatchResult.LOSE),
                MatchRecordFaker.CreateRecord(result: MatchResult.WIN),
                MatchRecordFaker.CreateRecord(result: MatchResult.DRAW),
            };

            // Act
            int actual = records.Losses();

            // Assert
            Assert.AreEqual(2, actual);
        }
    }
}
