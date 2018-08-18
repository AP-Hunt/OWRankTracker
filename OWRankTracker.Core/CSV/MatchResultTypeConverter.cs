using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using OWRankTracker.Core.Model;

namespace OWRankTracker.Core.CSV
{
    public class MatchResultTypeConverter : CsvHelper.TypeConversion.ITypeConverter
    {
        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            switch (text.ToUpper())
            {
                case "L": return MatchResult.LOSE;
                case "W": return MatchResult.WIN;
                case "D": default: return MatchResult.DRAW;
            }
        }

        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if(!(value is MatchResult))
            {
                return null;
            }

            MatchResult r = (MatchResult)value;
            switch(r)
            {
                default: case MatchResult.DRAW: return "D";
                case MatchResult.LOSE: return "L";
                case MatchResult.WIN: return "W";
            }
        }
    }
}
