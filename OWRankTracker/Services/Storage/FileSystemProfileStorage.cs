using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OWRankTracker.Model;
using OWRankTracker.Repositories;

namespace OWRankTracker.Services.Storage
{
    class FileSystemProfileStorage : IProfileStorage
    {
        private static string PROFILE_FOLDER_PATH =
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "OverwatchRankTracker",
            "Profiles"
        );

        public string DefaultProfileName => "Default";

        public IEnumerable<string> AllProfileNames 
        {
            get
            {
                string[] files = Directory.GetFiles(PROFILE_FOLDER_PATH, "*.csv");
                return files.Select(f => Path.GetFileNameWithoutExtension(f));
            }
        }


        public FileSystemProfileStorage()
        {
            EnsureProfilesStroageExists();
            EnsureDefaultProfileExists();
        }

        public IMatchRepository Create(string profileName)
        {
            string profile = ProfilePath(profileName);
            StreamWriter writer = new StreamWriter(profile);
            using (CsvHelper.CsvWriter csvWriter = new CsvHelper.CsvWriter(writer, leaveOpen: false))
            {

                csvWriter.Configuration.QuoteAllFields = true;
                csvWriter.Configuration.RegisterClassMap(new CSV.MatchRecordConfiguration());

                csvWriter.WriteHeader<MatchRecord>();
                csvWriter.NextRecord();
            }

            return new MatchRepository(profile);
        }

        public bool Exists(string profileName)
        {
            return File.Exists(ProfilePath(profileName));
        }

        public IMatchRepository Get(string profileName)
        {
            return new Repositories.MatchRepository(ProfilePath(profileName));
        }

        private string ProfilePath(string name)
        {
            return Path.Combine(PROFILE_FOLDER_PATH, $"{name}.csv");
        }

        private void EnsureProfilesStroageExists()
        {
            if (!Directory.Exists(PROFILE_FOLDER_PATH))
            {
                Directory.CreateDirectory(PROFILE_FOLDER_PATH);
            }
        }

        private void EnsureDefaultProfileExists()
        {
            if (AllProfileNames.Count() == 0 && !Exists(DefaultProfileName))
            {
                Create(DefaultProfileName);
            }
        }
    }
}
