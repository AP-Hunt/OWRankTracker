using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.ViewModel.Design
{
    class DesignTimeRankPlotViewModel : RankPlotViewModel
    {
        public DesignTimeRankPlotViewModel()
            :base(new DesignTime.DesignTimeProfileManager())
        {
        }
    }
}
