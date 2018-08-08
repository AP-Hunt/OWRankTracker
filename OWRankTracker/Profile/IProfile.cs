using OWRankTracker.MatchHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Profile
{
    interface IProfile
    {
        string Name { get; }
        IMatchHistory MatchHistory { get; }
    }
}
