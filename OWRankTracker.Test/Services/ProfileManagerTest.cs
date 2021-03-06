﻿using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OWRankTracker.Core.Profile;
using OWRankTracker.Core.Services;
using OWRankTracker.Core.Services.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OWRankTracker.Test.Services
{
    [TestClass]
    public class ProfileManagerTest
    {
        private InMemoryProfileStorage _profileStorage;
        private Fixtures.DefaultProfile _defaultProfile;
        private Fixtures.OtherProfile _otherProfile;
        private Mock<IMessenger> _messenger;

        [TestInitialize]
        public void Setup()
        {
            _defaultProfile = new Fixtures.DefaultProfile();
            _otherProfile = new Fixtures.OtherProfile();
            _messenger = new Mock<IMessenger>();
            _profileStorage = new InMemoryProfileStorage(new List<IProfile>() { _defaultProfile, _otherProfile });
        }

        [TestMethod]
        public void ProfileManagerBeginswithFirstProfileAlphabeticallySelected()
        {
            // Act
            ProfileManager manager = new ProfileManager(_profileStorage, _messenger.Object);
            manager.OpenDefaultProfile(emitMessage: false);

            // Assert
            Assert.AreEqual(_defaultProfile, manager.ActiveProfile);
        }

        [TestMethod]
        public void OpenProfile_SetsTheActiveProfileToTheOpenedOne()
        {
            // Arrange
            ProfileManager manager = new ProfileManager(_profileStorage, _messenger.Object);

            // Act
            manager.OpenProfile(_otherProfile.Name);

            // Assert
            Assert.AreEqual(_otherProfile, manager.ActiveProfile);
        }

        [TestMethod]
        public void OpenDefaultProfile_OpensFirstProfileAlphabetically()
        {
            // Arrange
            ProfileManager manager = new ProfileManager(_profileStorage, _messenger.Object);

            // Act
            manager.OpenDefaultProfile();

            // Assert
            Assert.AreEqual(_defaultProfile, manager.ActiveProfile);
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

            // Act
            manager.OpenProfile(_otherProfile.Name);

            // Assert
            _messenger.Verify(m => m.Send<Messages.ActiveProfileChanged>(It.IsAny<Messages.ActiveProfileChanged>()));
        }

        [TestMethod]
        public void MessageEmittedByOpenProfile_ContainsTheNewlyOpenedProfile()
        {
            // Arrange
            ProfileManager manager = new ProfileManager(_profileStorage, _messenger.Object);

            // Act
            manager.OpenProfile(_otherProfile.Name);

            // Assert
            _messenger.Verify(m =>
                m.Send<Messages.ActiveProfileChanged>(
                    It.Is<Messages.ActiveProfileChanged>(msg => msg.Profile == _otherProfile)
                )
            );
        }

        [TestMethod]
        [ExpectedException(typeof(ProfileNameConflictException))]
        public void Create_ThrowsException_IfProfileWithThatNameAlreadyExists()
        {
            // Arrange
            ProfileManager manager = new ProfileManager(_profileStorage, _messenger.Object);
            string name = "Default";

            // Act
            manager.Create(name);
        }

        [TestMethod]
        public void Create_ReturnsANewProfileInstance_ForTheNewProfile()
        {
            // Arrange
            ProfileManager manager = new ProfileManager(_profileStorage, _messenger.Object);
            string name = "Foo";

            // Act
            IProfile profile = manager.Create(name);

            // Assert
            Assert.IsNotNull(profile);
            CollectionAssert.Contains(manager.Profiles.ToList(), profile);
            Assert.AreEqual(name, profile.Name);
        }

        [TestMethod]
        public void Create_EmitsNewProfileMessage()
        {
            // Arrange
            ProfileManager manager = new ProfileManager(_profileStorage, _messenger.Object);

            // Act
            IProfile profile = manager.Create("foo");

            // Assert
            _messenger.Verify(m => 
                m.Send(
                    It.Is<Messages.NewProfile>(
                        msg => msg.Profile == profile
                    )
                )
            );
        }
    }
}
