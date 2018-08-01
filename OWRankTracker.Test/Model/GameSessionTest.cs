using Microsoft.VisualStudio.TestTools.UnitTesting;
using OWRankTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Test.Model
{
    [TestClass]
    public class GameSessionTest
    {
        [TestMethod]
        public void CRChange_CalculatesTheDifferenceBetweenTheInitialCR_AndTheFinalMatchCR()
        {
            // Arrange
            var initialCr = 1945;
            IEnumerable<MatchRecord> matches = Fakers.MatchRecordFaker.NMatchesBetweenDates(
                10,
                start: new DateTime(2018, 01, 01, 21, 00, 00),
                initialCR: initialCr
            );

            MatchRecord lastMatch = matches.Last();

            GameSession session = new GameSession(initialCr, matches);

            // Act
            int change = session.CRChange;

            // Assert
            Assert.AreEqual(lastMatch.CR - initialCr, change);
        }
    }
}
