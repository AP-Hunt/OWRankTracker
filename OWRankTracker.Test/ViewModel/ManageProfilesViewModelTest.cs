using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OWRankTracker.Core.Profile;
using OWRankTracker.Core.Services;
using OWRankTracker.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Test.ViewModel
{
    [TestClass]
    public class ManageProfilesViewModelTest : ViewModelTestBase
    {
        private Mock<IDialogService> _dialogSvcMock;

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            _dialogSvcMock = new Mock<IDialogService>();
        }

        [TestMethod]
        public void OnCreation_ExposesAllProfileNamesAsStrings()
        {
            // Act
            ManageProfilesViewModel vm = new ManageProfilesViewModel(_profileManager, _dialogSvcMock.Object);

            // Assert
            Assert.AreEqual(3, vm.Profiles.Count());
            CollectionAssert.Contains(vm.Profiles.ToList(), _defaultProfile.Name);
            CollectionAssert.Contains(vm.Profiles.ToList(), _alternateProfile.Name);
            CollectionAssert.Contains(vm.Profiles.ToList(), _emptyProfile.Name);
        }

        [TestMethod]
        public void AddProfileCommand_ShowsErrorDialog_WhenProfileNameConflictOccurrs()
        {
            // Arrange
            Mock<IProfileManager> profileMgrMock = new Mock<IProfileManager>();
            profileMgrMock.SetupGet(p => p.Profiles).Returns(Enumerable.Empty<IProfile>());
            profileMgrMock.SetupGet(p => p.ActiveProfile).Returns((IProfile)null);

            profileMgrMock.Setup(p => p.Create(It.IsAny<string>())).Throws(new ProfileNameConflictException("foo"));

            ManageProfilesViewModel vm = new ManageProfilesViewModel(profileMgrMock.Object, _dialogSvcMock.Object);

            // Act
            vm.AddProfileCommand.Execute("foo");

            // Assert
            _dialogSvcMock.Verify(d => 
                d.ShowError(
                    It.IsAny<ProfileNameConflictException>(), 
                    It.IsAny<string>(), 
                    It.IsAny<string>(), 
                    It.Is<Action>(a => a == null)
                )
            );
        }

        [TestMethod]
        public void OnNewProfile_AddsTheProfileNameToTheExposedList()
        {
            // Arrange
            Mock<IProfile> mockProfile = new Mock<IProfile>();
            string name = "profile";
            mockProfile.SetupGet(p => p.Name).Returns(name);

            // Act
            ManageProfilesViewModel vm = new ManageProfilesViewModel(_profileManager, _dialogSvcMock.Object);
            Messenger.Default.Send(new Messages.NewProfile(mockProfile.Object));

            // Assert
            CollectionAssert.Contains(vm.Profiles, name);
        }
    }
}
