using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OWRankTracker.Core.Model;
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
        Mock<GalaSoft.MvvmLight.Views.IDialogService> _mockDialogService;

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            _mockDialogService = new Mock<GalaSoft.MvvmLight.Views.IDialogService>();

            _mockDialogService
                .Setup(d => d.ShowMessage(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));
        }

        [TestMethod]
        public void OnSave_WhenAtLeastOneMatchExists_CreatesNewMatch_WithResultRelativeToLastResult()
        {
            // Arrange
            RecordMatchViewModel vm = new RecordMatchViewModel(_profileManager, _mockDialogService.Object);
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
            RecordMatchViewModel vm = new RecordMatchViewModel(_profileManager, _mockDialogService.Object);
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
