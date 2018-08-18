using GalaSoft.MvvmLight.Messaging;
using Moq;
using OWRankTracker.Core.Profile;
using OWRankTracker.Core.Services;
using OWRankTracker.Core.Services.Storage;
using System.Collections.Generic;
using System.Linq;

namespace OWRankTracker.Test.Fakers
{
    public class ProfileManagerFaker
    {
        public static IProfileManager CreateSimpleManager(IEnumerable<IProfile> profiles, IMessenger messenger = null)
        {
            IProfileStorage storage = new InMemoryProfileStorage(profiles.ToList());

            IProfileManager mgr = new ProfileManager(
                storage,
                messenger ?? Mock.Of<IMessenger>()
            );

            mgr.OpenDefaultProfile();

            return mgr;
        }
    }
}
