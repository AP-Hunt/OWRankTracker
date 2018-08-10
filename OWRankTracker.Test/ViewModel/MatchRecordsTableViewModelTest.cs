using Microsoft.VisualStudio.TestTools.UnitTesting;
using OWRankTracker.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Test.ViewModel
{
    [TestClass]
    public class MatchRecordsTableViewModelTest : ViewModelTestBase
    {
        [TestMethod]
        public void OnCreation_ExposesMatchHistoryAsACollection()
        {
            // Act
            MatchRecordsTableViewModel vm = new MatchRecordsTableViewModel(_profileManager);

            // Assert
            CollectionAssert.AreEquivalent(_defaultProfile.MatchHistory.ToList(), vm.Records);
        }

        [TestMethod]
        public void OnProfileChange_ExposesNewProfileMatchHistoryAsACollection()
        {
            // Arrange
            MatchRecordsTableViewModel vm = new MatchRecordsTableViewModel(_profileManager);

            // Act
            _profileManager.OpenProfile(_alternateProfile.Name);

            // Assert
            CollectionAssert.AreEquivalent(_alternateProfile.MatchHistory.ToList(), vm.Records);
        }
    }
}
