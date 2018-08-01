using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OWRankTracker.Repositories;
using OWRankTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Test.Services
{
    [TestClass]
    public class ProfileManagerTest
    {
        private FakeProfileStorage _profileStorage;
        private IMatchRepository _defaultProfile;
        private IMatchRepository _otherProfile;
        private Mock<IMessenger> _messenger;

        [TestInitialize]
        public void Setup()
        {
            _defaultProfile = Mock.Of<IMatchRepository>();
            _messenger = new Mock<IMessenger>();
            _otherProfile = Mock.Of<IMatchRepository>();
            _profileStorage = new FakeProfileStorage(new Dictionary<string, IMatchRepository>()
            {
                { "Other", _otherProfile },
                { "Default",  _defaultProfile }
            });
        }

        [TestMethod]
        public void ProfileManagerBeginswithNoProfileSelected()
        {
            // Act
            ProfileManager manager = new ProfileManager(_profileStorage, _messenger.Object);

            // Assert
            Assert.IsNull(manager.ActiveProfile);
            Assert.IsNull(manager.ActiveProfileName);
        }

        [TestMethod]
        public void AllProfiles_ReturnsNamesOfAllDefinedProfiles()
        {
            // Arrange
            ProfileManager manager = new ProfileManager(_profileStorage, _messenger.Object);

            // Act
            List<string> profiles = manager.AllProfiles().ToList();

            // Assert
            Assert.AreEqual(2, profiles.Count());
            CollectionAssert.Contains(profiles, "Default");
            CollectionAssert.Contains(profiles, "Other");
        }

        [TestMethod]
        public void OpenProfile_SetsTheActiveProfileToTheOpenedOne()
        {
            // Arrange
            ProfileManager manager = new ProfileManager(_profileStorage, _messenger.Object);

            // Act
            manager.OpenProfile("Other");

            // Assert
            Assert.AreEqual(_otherProfile, manager.ActiveProfile);
            Assert.AreEqual("Other", manager.ActiveProfileName);
        }

        [TestMethod]
        public void OpenDefaultProfile_OpensTheProfileWithTheSameNameAsTheStoragesDefaultProfileName()
        {
            // Arrange
            ProfileManager manager = new ProfileManager(_profileStorage, _messenger.Object);
            _profileStorage.DefaultProfileName = "Other";

            // Act
            manager.OpenDefaultProfile();

            // Assert
            Assert.AreEqual(_otherProfile, manager.ActiveProfile);
            Assert.AreEqual("Other", manager.ActiveProfileName);
        }

        [TestMethod]
        public void OpenDefaultProfile_OpensTheFirstProfileAlphabetically_IfTheDefaultProfileDoesNotExist()
        {
            // Arrange
            ProfileManager manager = new ProfileManager(_profileStorage, _messenger.Object);
            _profileStorage.DefaultProfileName = "Foo";

            // Act
            manager.OpenDefaultProfile();

            // Assert
            Assert.AreEqual(_defaultProfile, manager.ActiveProfile);
            Assert.AreEqual("Default", manager.ActiveProfileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OpenProfile_ThrowsAnException_IfProfileDoesNotExist()
        {
            // Arrange
            ProfileManager manager = new ProfileManager(_profileStorage, _messenger.Object);

            // Act
            manager.OpenProfile("Non-Existent");
        }

        [TestMethod]
        public void OpenProfile_EmitsAnActiveProfileChangedMessage()
        {
            // Arrange
            ProfileManager manager = new ProfileManager(_profileStorage, _messenger.Object);
            _profileStorage.DefaultProfileName = "Foo";

            // Act
            manager.OpenDefaultProfile();

            // Assert
            _messenger.Verify(m => m.Send<Messages.ActiveProfileChanged>(It.IsAny<Messages.ActiveProfileChanged>()));
        }

        [TestMethod]
        public void MessageEmittedByOpenProfile_ContainsTheNewlyOpenedProfile()
        {
            // Arrange
            ProfileManager manager = new ProfileManager(_profileStorage, _messenger.Object);

            // Act
            manager.OpenDefaultProfile();

            // Assert
            _messenger.Verify(m =>
                m.Send<Messages.ActiveProfileChanged>(
                    It.Is<Messages.ActiveProfileChanged>(msg =>
                        msg.MatchRepository == _defaultProfile &&
                        msg.ProfileName == "Default"
                    )
                )
            );
        }
    }
}
