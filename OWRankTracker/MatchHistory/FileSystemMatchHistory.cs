using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OWRankTracker.Model;

namespace OWRankTracker.MatchHistory
{
    class FileSystemMatchHistory : IMatchHistory
    {
        private string _csvPath;

        private List<MatchRecord> _records;
        private List<MatchRecord> Records
        {
            get
            {
                if(_records == null)
                {
                    using (var reader = Reader())
                    {
                        _records = reader.GetRecords<MatchRecord>().ToList();
                    }
                }

                return _records;
            }
        }

        public MatchRecord LastMatch => Records.LastOrDefault();

        public FileSystemMatchHistory(string filePath)
        {
            _csvPath = filePath;
        }

        public void Add(MatchRecord record)
        {
            using (var writer = Writer())
            {
                writer.WriteRecord(record);
                writer.NextRecord();
                Records.Add(record);
            }
        }

        public IEnumerator<MatchRecord> GetEnumerator()
        {
            return Records.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Records.GetEnumerator();
        }

        private CsvHelper.CsvReader Reader()
        {
            EnsureFileExists();
            var reader = new CsvHelper.CsvReader(new StreamReader(_csvPath), leaveOpen: false);
            reader.Configuration.HeaderValidated = null;
            reader.Configuration.RegisterClassMap(new CSV.MatchRecordConfiguration());

            return reader;
        }

        private CsvHelper.CsvWriter Writer()
        {
            EnsureFileExists();
            StreamWriter strmWriter = new StreamWriter(_csvPath, append: true);
            strmWriter.NewLine = Environment.NewLine;

            var writer = new CsvHelper.CsvWriter(strmWriter, leaveOpen: false);

            writer.Configuration.QuoteAllFields = true;
            writer.Configuration.RegisterClassMap(new CSV.MatchRecordConfiguration());

            return writer;
        }

        private void EnsureFileExists()
        {
            if(!File.Exists(_csvPath))
            {
                StreamWriter writer = new StreamWriter(_csvPath);
                using (CsvHelper.CsvWriter csvWriter = new CsvHelper.CsvWriter(writer, leaveOpen: false))
                {

                    csvWriter.Configuration.QuoteAllFields = true;
                    csvWriter.Configuration.RegisterClassMap(new CSV.MatchRecordConfiguration());

                    csvWriter.WriteHeader<MatchRecord>();
                    csvWriter.NextRecord();
                }
            }
        }
    }
}
