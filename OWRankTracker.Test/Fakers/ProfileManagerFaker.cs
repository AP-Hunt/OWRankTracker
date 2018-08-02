using GalaSoft.MvvmLight.Messaging;
using Moq;
using OWRankTracker.Model;
using OWRankTracker.Repositories;
using OWRankTracker.Services;
using OWRankTracker.Services.Storage;
using System.Collections.Generic;

namespace OWRankTracker.Test.Fakers
{
    class ProfileManagerFaker
    {
        public static IProfileManager CreateSimpleManager(IEnumerable<MatchRecord> records, IMessenger messenger = null)
        {
            IMatchRepository matchRepo = new InMemoryMatchRepository(records);
            IProfileStorage storage = new InMemoryProfileStorage(new Dictionary<string, IMatchRepository>()
            {
                { "Default", matchRepo }
            });

            IProfileManager mgr = new ProfileManager(
                storage,
                messenger ?? Mock.Of<IMessenger>()
            );

            mgr.OpenDefaultProfile();

            return mgr;
        }
    }
}
