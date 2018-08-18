using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OWRankTracker.Core.Model;

namespace OWRankTracker.Core.MatchHistory
{
    public class FileSystemMatchHistory : IMatchHistory
    {
        private readonly IFileSystem _fileSystem;
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

        public FileSystemMatchHistory(IFileSystem fileSystem, string filePath)
        {
            _fileSystem = fileSystem;
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
            StreamReader streamReader = new StreamReader(_fileSystem.File.OpenRead(_csvPath));
            var csvReader = new CsvHelper.CsvReader(streamReader, leaveOpen: false);
            csvReader.Configuration.HeaderValidated = null;
            csvReader.Configuration.RegisterClassMap(new CSV.MatchRecordConfiguration());

            return csvReader;
        }

        private CsvHelper.CsvWriter Writer()
        {
            EnsureFileExists();
            FileInfoBase fileInfo = _fileSystem.FileInfo.FromFileName(_csvPath);
            StreamWriter strmWriter = fileInfo.AppendText();
            strmWriter.NewLine = Environment.NewLine;

            var writer = new CsvHelper.CsvWriter(strmWriter, leaveOpen: false);

            writer.Configuration.QuoteAllFields = true;
            writer.Configuration.RegisterClassMap(new CSV.MatchRecordConfiguration());

            return writer;
        }

        private void EnsureFileExists()
        {
            if(!_fileSystem.File.Exists(_csvPath))
            {
                FileInfoBase fileInfo = _fileSystem.FileInfo.FromFileName(_csvPath);
                StreamWriter writer = fileInfo.AppendText();
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
