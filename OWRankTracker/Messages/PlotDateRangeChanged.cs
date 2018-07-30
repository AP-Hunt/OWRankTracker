using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Messages
{
    class PlotDateRangeChanged
    {
        public PlotDateRangeChanged(object sender, DateTime start, DateTime end)
        {
            Sender = sender;
            Start = start;
            End = end;
        }

        public object Sender { get; }
        public DateTime Start { get; }
        public DateTime End { get; }
    }
}
