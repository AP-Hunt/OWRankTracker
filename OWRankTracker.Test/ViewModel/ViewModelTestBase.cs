using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OWRankTracker.Profile;
using OWRankTracker.Services;
using OWRankTracker.Test.Fakers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Test.ViewModel
{
    public class ViewModelTestBase
    {
        protected IProfileManager _profileManager;
        protected IMessenger _messenger;
        protected Fixtures.DefaultProfile _defaultProfile;
        protected Fixtures.OtherProfile _alternateProfile;

        [TestInitialize]
        public void Setup()
        {
            _defaultProfile = new Fixtures.DefaultProfile();
            _alternateProfile = new Fixtures.OtherProfile();
            _messenger = Messenger.Default;

            var profiles = new List<IProfile>() { _defaultProfile, _alternateProfile };
            _profileManager = ProfileManagerFaker.CreateSimpleManager(profiles, _messenger);
        }
    }
}
