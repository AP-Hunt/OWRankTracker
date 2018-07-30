﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OWRankTracker.Model;

namespace OWRankTracker.CSV
{
    class MatchRecordConfiguration : CsvHelper.Configuration.ClassMap<Model.MatchRecord>
    {
        public MatchRecordConfiguration()
        {
            Map(m => m.CR)
                .Index(0);

            Map(m => m.Date)
                .Index(1);

            Map(m => m.Result)
                .Index(2)
                .TypeConverter(new MatchResultTypeConverter());

            Map(m => m.Diff)
                .Index(3);

            Map(m => m.Map)
                .Index(4);
        }
    }
}
