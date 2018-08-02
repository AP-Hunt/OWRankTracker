using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OWRankTracker.Model;

namespace OWRankTracker.Test.Model
{
    [TestClass]
    public class MapStatisticsTest
    {
        private MapStatistics _stats;

        [TestInitialize]
        public void SetUp()
        {
            _stats = new MapStatistics()
            {
                TotalPlayed = 10,
                TotalWon = 4,
                TotalLost = 4,
                TotalDrawn = 2,
                Map = "Foo"
            };
        }

        [TestMethod]
        public void CalculatesWinPc_AsPercentageOfTotalPlayed()
        {
            // Act
            double pct = _stats.WinPc;

            // Assert
            Assert.AreEqual(0.4, pct);
        }

        [TestMethod]
        public void CalculatesDrawnPc_AsPercentageOfTotalPlayed()
        {
            // Act
            double pct = _stats.DrawnPc;

            // Assert
            Assert.AreEqual(0.2, pct);
        }

        [TestMethod]
        public void CalculatesLostPc_AsPercentageOfTotalPlayed()
        {
            // Act
            double pct = _stats.LostPc;

            // Assert
            Assert.AreEqual(0.4, pct);
        }
    }
}
