using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OWRankTracker.Core.Services;

namespace OWRankTracker.ViewModel.Design
{
    class DesignTimeMatchRecordsTableViewModel : MatchRecordsTableViewModel
    {
        public DesignTimeMatchRecordsTableViewModel() : base(new DesignTime.DesignTimeProfileManager())
        {
        }
    }
}
