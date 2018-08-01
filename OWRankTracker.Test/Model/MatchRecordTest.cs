using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OWRankTracker.Model;
using OWRankTracker.Test.Fakers;

namespace OWRankTracker.Test.Model
{
    [TestClass]
    public class MatchRecordTest
    {
        [TestMethod]
        public void NewRelativeRecord_CreatesANewMatchRecord_WithTheDifferencealculated_RelativeToTheOriginal()
        {
            // Arrange
            MatchRecord original = MatchRecordFaker.CreateRecord(cr: 2000);

            // Act
            MatchRecord relative = original.NewRelativeRecord(2100, DateTime.Now, MapFaker.Random());

            // Assert
            Assert.AreEqual(relative.Diff, 100);
        }

        [TestMethod]
        public void NewRelativeRecord_CreatesANewMatchRecord_WithWinResult_WhenNewRankIsHigherThanOldRank()
        {
            // Arrange
            MatchRecord original = MatchRecordFaker.CreateRecord(cr: 2000);

            // Act
            MatchRecord relative = original.NewRelativeRecord(2100, DateTime.Now, MapFaker.Random());

            // Assert
            Assert.AreEqual(relative.Result, MatchResult.WIN);
        }

        [TestMethod]
        public void NewRelativeRecord_CreatesANewMatchRecord_WitDrawResult_WhenNewRankIsEqualToOldRank()
        {
            // Arrange
            MatchRecord original = MatchRecordFaker.CreateRecord(cr: 2000);

            // Act
            MatchRecord relative = original.NewRelativeRecord(2000, DateTime.Now, MapFaker.Random());

            // Assert
            Assert.AreEqual(relative.Result, MatchResult.DRAW);
        }

        [TestMethod]
        public void NewRelativeRecord_CreatesANewMatchRecord_WitLoseResult_WhenNewRankIsLessThanOldRank()
        {
            // Arrange
            MatchRecord original = MatchRecordFaker.CreateRecord(cr: 2000);

            // Act
            MatchRecord relative = original.NewRelativeRecord(1900, DateTime.Now, MapFaker.Random());

            // Assert
            Assert.AreEqual(relative.Result, MatchResult.LOSE);
        }
    }
}
