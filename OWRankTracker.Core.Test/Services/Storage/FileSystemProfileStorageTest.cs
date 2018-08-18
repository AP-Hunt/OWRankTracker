using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OWRankTracker.Core.Profile;
using OWRankTracker.Core.Services;
using OWRankTracker.Core.Services.Storage;

namespace OWRankTracker.Test.Services.Storage
{
    [TestClass]
    public class FileSystemProfileStorageTest
    {
        private System.IO.Abstractions.TestingHelpers.MockFileSystem _fileSystem;

        [TestInitialize]
        public void SetUp()
        {
            _fileSystem = new System.IO.Abstractions.TestingHelpers.MockFileSystem();
            _fileSystem.AddDirectory(FileSystemProfileStorage.PROFILE_FOLDER_PATH);
            _fileSystem.AddFile(Path.Combine(FileSystemProfileStorage.PROFILE_FOLDER_PATH, "default.csv"), System.IO.Abstractions.TestingHelpers.MockFileData.NullObject);
            _fileSystem.AddFile(Path.Combine(FileSystemProfileStorage.PROFILE_FOLDER_PATH, "excel.xlsx"), System.IO.Abstractions.TestingHelpers.MockFileData.NullObject);
        }

        [TestMethod]
        [DataRow("default", true)]
        [DataRow("excel", false)]
        [DataRow("foo", false)]
        public void Exists_ReturnsTrue_OnlyWhenACSVWithTheSameNameExistsInTheFileSystem(string profileName, bool expected)
        {
            // Arrange
            FileSystemProfileStorage storage = new FileSystemProfileStorage(_fileSystem);

            // Act
            bool actual = storage.Exists(profileName);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Create_CreatesNewCSV_WithTheNameOfTheProfile()
        {
            // Arrange
            FileSystemProfileStorage storage = new FileSystemProfileStorage(_fileSystem);

            // Act
            storage.Create("new");

            // Assert
            Assert.IsTrue(_fileSystem.FileExists(Path.Combine(FileSystemProfileStorage.PROFILE_FOLDER_PATH, "new.csv")));
        }

        [TestMethod]
        [ExpectedException(typeof(ProfileNameConflictException))]
        public void Create_ThrowsException_WhenProfileWithSameNameAlreadyExists()
        {
            // Arrange
            FileSystemProfileStorage storage = new FileSystemProfileStorage(_fileSystem);

            // Act
            storage.Create("default");
        }

        [TestMethod]
        public void SubsequentProfileGets_ReturnTheSameProfileInstance()
        {
            // Arrange
            FileSystemProfileStorage storage = new FileSystemProfileStorage(_fileSystem);

            // Act
            IProfile resultOne = storage.Get("default");
            IProfile resultTwo = storage.Get("default");

            // Assert
            Assert.AreSame(resultOne, resultTwo);
        }

        [TestMethod]
        public void CreatingThenGettingAProfile_ReturnsTheSameInstance()
        {
            // Arrange
            FileSystemProfileStorage storage = new FileSystemProfileStorage(_fileSystem);

            // Act
            IProfile created = storage.Create("foo");
            IProfile gotten = storage.Get("foo");

            // Assert
            Assert.AreSame(created, gotten);
        }

        [TestMethod]
        public void GettingAndIteratingOverProfiles_ReturnsTheSameInstances()
        {
            // Arrange
            FileSystemProfileStorage storage = new FileSystemProfileStorage(_fileSystem);

            // Act
            IProfile iterated = storage.First(p => p.Name == "default");
            IProfile gotten = storage.Get("default");

            // Assert
            Assert.AreSame(iterated, gotten);
        }

        [TestMethod]
        public void CreatingAndIteratingOverProfiles_ReturnsTheSameInstances()
        {
                    // Arrange
            FileSystemProfileStorage storage = new FileSystemProfileStorage(_fileSystem);

            // Act
            IProfile created = storage.Create("foo");
            IProfile iterated = storage.First(p => p.Name == "foo");

            // Assert
            Assert.AreSame(iterated, created);
        }
    }
}
