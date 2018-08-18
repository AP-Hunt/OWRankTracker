using OWRankTracker.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Messages
{
    class NewMatchRecord
    {
        public NewMatchRecord(MatchRecord record)
        {
            Record = record;
        }

        public MatchRecord Record { get; }
    }
}
