using OWRankTracker.Core.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Messages
{
    class NewProfile
    {
        public IProfile Profile { get; }

        public NewProfile(IProfile profile)
        {
            Profile = profile;
        }
    }
}
