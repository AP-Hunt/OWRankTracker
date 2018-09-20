using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Validation
{
    /// <summary>
    /// Provides profile name validation behaviour
    /// </summary>
    interface IProfileNameValidator
    {
        bool Validate(string profileName);
    }
}
