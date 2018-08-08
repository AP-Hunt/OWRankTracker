using GalaSoft.MvvmLight.Messaging;
using Moq;
using OWRankTracker.Profile;
using OWRankTracker.Services;
using OWRankTracker.Services.Storage;
using System.Collections.Generic;
using System.Linq;

namespace OWRankTracker.Test.Fakers
{
    class ProfileManagerFaker
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
