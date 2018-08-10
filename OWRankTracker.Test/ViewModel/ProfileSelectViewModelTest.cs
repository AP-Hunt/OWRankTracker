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
    public class ProfileSelectViewModelTest : ViewModelTestBase
    {
        [TestMethod]
        public void OnCreation_ExposesTheNamesOfAllProfiles()
        {
            // Act
            ProfileSelectViewModel vm = new ProfileSelectViewModel(_profileManager);

            // Assert
            CollectionAssert.Contains(vm.AllProfiles, _defaultProfile.Name);
            CollectionAssert.Contains(vm.AllProfiles, _alternateProfile.Name);
        }

        [TestMethod]
        public void OnCreation_ExposesTheNameOfTheSelectedProfile()
        {
            // Act
            ProfileSelectViewModel vm = new ProfileSelectViewModel(_profileManager);

            // Assert
            Assert.AreEqual(_profileManager.ActiveProfile.Name, vm.SelectedProfile);
        }
    }
}
