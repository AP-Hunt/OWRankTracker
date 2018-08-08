using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OWRankTracker.Model;
using OWRankTracker.MatchHistory;
using OWRankTracker.Profile;
using System.Collections;
using System.IO.Abstractions;

namespace OWRankTracker.Services.Storage
{
    class FileSystemProfileStorage : IProfileStorage
    {
        internal static string PROFILE_FOLDER_PATH =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "OverwatchRankTracker",
                "Profiles"
            );

        private System.IO.Abstractions.IFileSystem _fileSystem;

        private Dictionary<string, FileSystemProfile> _profileCache = new Dictionary<string, FileSystemProfile>();

        public FileSystemProfileStorage(System.IO.Abstractions.IFileSystem fileSystem = null)
        {
            _fileSystem = fileSystem ?? new System.IO.Abstractions.FileSystem();

            EnsureProfilesStorageExists();
            PopulateProfileCache();
        }

        public IProfile Create(string profileName)
        {
            if(Exists(profileName))
            {
                throw new Services.ProfileNameConflictException(profileName);
            }

            string profilePath = ProfilePath(profileName);
            FileInfoBase profileFileInfo = _fileSystem.FileInfo.FromFileName(profilePath);
            StreamWriter writer = profileFileInfo.CreateText();

            using (CsvHelper.CsvWriter csvWriter = new CsvHelper.CsvWriter(writer, leaveOpen: false))
            {

                csvWriter.Configuration.QuoteAllFields = true;
                csvWriter.Configuration.RegisterClassMap(new CSV.MatchRecordConfiguration());

                csvWriter.WriteHeader<MatchRecord>();
                csvWriter.NextRecord();
            }

            FileSystemProfile profile = new FileSystemProfile(_fileSystem, profileName, profilePath);
            AddToCache(profileName, profile);
            return profile;
        }

        public bool Exists(string profileName)
        {
            if (!IsInCache(profileName))
            { 
                return _fileSystem.File.Exists(ProfilePath(profileName));
            }

            return true;
        }

        public IProfile Get(string profileName)
        {
            if(!Exists(profileName))
            {
                return null;
            }

            if(IsInCache(profileName))
            {
                return CacheEntry(profileName);
            }

            FileSystemProfile profile = new FileSystemProfile(_fileSystem, profileName, ProfilePath(profileName));
            AddToCache(profileName, profile);
            return profile;
        }

        private void AddToCache(string profileName, FileSystemProfile profile)
        {
            _profileCache.Add(profileName, profile);
        }

        private IProfile CacheEntry(string profileName)
        {
            return _profileCache[profileName];
        }

        private bool IsInCache(string profileName)
        {
            return _profileCache.ContainsKey(profileName);
        }

        private string ProfilePath(string name)
        {
            return Path.Combine(PROFILE_FOLDER_PATH, $"{name}.csv");
        }

        private void EnsureProfilesStorageExists()
        {
            if (!Directory.Exists(PROFILE_FOLDER_PATH))
            {
                Directory.CreateDirectory(PROFILE_FOLDER_PATH);
            }
        }

        private void PopulateProfileCache()
        {
            foreach(string path in _fileSystem.Directory.EnumerateFiles(PROFILE_FOLDER_PATH, "*.csv"))
            {
                string name = _fileSystem.Path.GetFileNameWithoutExtension(path);
                AddToCache(name, new FileSystemProfile(_fileSystem, name, path));
            }
        }

        #region IEnumerable<IProfile>
        public IEnumerator<IProfile> GetEnumerator()
        {
            return _profileCache.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _profileCache.Values.GetEnumerator();
        }
        #endregion

        /// <summary>
        /// Implementation of IProfile for use with FileSystemProfileStorage
        /// </summary>
        class FileSystemProfile : IProfile
        {
            public string Name { get; private set; }

            public IMatchHistory MatchHistory { get; private set; }

            public FileSystemProfile(IFileSystem fileSystem, string name, string filePath)
            {
                Name = name;
                MatchHistory = new FileSystemMatchHistory(fileSystem, filePath);
            }
        }
    }
}
