using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OWRankTracker.Core.Profile;
using OWRankTracker.Services.Wpf;
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
            ProfileSelectViewModel vm = new ProfileSelectViewModel(_profileManager, Mock.Of<IWindowService>());

            // Assert
            CollectionAssert.Contains(vm.AllProfiles, _defaultProfile.Name);
            CollectionAssert.Contains(vm.AllProfiles, _alternateProfile.Name);
        }

        [TestMethod]
        public void OnCreation_ExposesTheNameOfTheSelectedProfile()
        {
            // Act
            ProfileSelectViewModel vm = new ProfileSelectViewModel(_profileManager, Mock.Of<IWindowService>());

            // Assert
            Assert.AreEqual(_profileManager.ActiveProfile.Name, vm.SelectedProfile);
        }

        [TestMethod]
        public void OnNewProfile_AddsProfileNameToExposedProfiles()
        {
            // Arrange
            Mock<IProfile> profile = new Mock<IProfile>();
            string name = "Profile";
            profile.SetupGet(p => p.Name).Returns(name);

            Messages.NewProfile msg = new Messages.NewProfile(profile.Object);

            // Act
            ProfileSelectViewModel vm = new ProfileSelectViewModel(_profileManager, Mock.Of<IWindowService>());
            Messenger.Default.Send(msg);

            // Assert
            CollectionAssert.Contains(vm.AllProfiles, name);
        }
    }
}
