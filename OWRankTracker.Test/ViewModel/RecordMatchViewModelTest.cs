using Microsoft.VisualStudio.TestTools.UnitTesting;
using OWRankTracker.Model;
using OWRankTracker.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Test.ViewModel
{
    [TestClass]
    public class RecordMatchViewModelTest : ViewModelTestBase
    {
        [TestMethod]
        public void OnSave_WhenAtLeastOneMatchExists_CreatesNewMatch_WithResultRelativeToLastResult()
        {
            // Arrange
            RecordMatchViewModel vm = new RecordMatchViewModel(_profileManager);
            string map = vm.Maps[1];
            vm.SelectedMap = map;

            int newCR = 2000;
            vm.CR = newCR;
            
            int previousCR = _defaultProfile.MatchHistory.LastMatch.CR;
            int currentCount = _defaultProfile.MatchHistory.Count();

            // Act
            vm.SaveCommand.Execute(null);

            // Assert
            Assert.AreEqual(currentCount + 1, _defaultProfile.MatchHistory.Count());
            
            MatchRecord newRecord = _defaultProfile.MatchHistory.LastMatch;
            Assert.AreEqual(newCR, newRecord.CR);
            Assert.AreEqual(newCR - previousCR, newRecord.Diff);
            Assert.AreEqual(MatchResult.WIN, newRecord.Result);
            Assert.AreEqual(map, newRecord.Map);
        }

        [TestMethod]
        public void OnSave_WhenNoMatchesExist_CreatesNewMatch_AsAWin_AndDifferenceAsCR()
        {
            // Arrange
            RecordMatchViewModel vm = new RecordMatchViewModel(_profileManager);
            _profileManager.OpenProfile(_emptyProfile.Name);

            string map = vm.Maps[1];
            vm.SelectedMap = map;

            int newCR = 2000;
            vm.CR = newCR;

            // Act
            vm.SaveCommand.Execute(null);

            // Assert
            Assert.AreEqual(1, _emptyProfile.MatchHistory.Count());

            MatchRecord newRecord = _emptyProfile.MatchHistory.LastMatch;
            Assert.AreEqual(newCR, newRecord.CR);
            Assert.AreEqual(newCR, newRecord.Diff);
            Assert.AreEqual(MatchResult.WIN, newRecord.Result);
            Assert.AreEqual(map, newRecord.Map);
        }
    }
}
