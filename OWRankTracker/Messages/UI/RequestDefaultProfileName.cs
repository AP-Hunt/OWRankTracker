using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Messages.UI
{
    /// <summary>
    /// Message sent on application startup
    /// to instruct the UI to request the name 
    /// of the first profile
    /// </summary>
    class RequestDefaultProfileName
    {
        public Action<string> NameProvidedCallback { get; }

        public RequestDefaultProfileName(Action<string> nameProvidedCallback)
        {
            NameProvidedCallback = nameProvidedCallback;
        }
    }
}
