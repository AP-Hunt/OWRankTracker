/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:OWRankTracker.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using OWRankTracker.Model;
using OWRankTracker.Profile;
using System.Collections.Generic;

namespace OWRankTracker.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    class ViewModelLocator
    {
        static ViewModelLocator()
        {
            Services.Storage.IProfileStorage profileStorage;
#if DEBUG
            profileStorage =  new Services.Storage.InMemoryProfileStorage(new List<IProfile>()
                {   
                    new DesignTime.Profiles.Empty("Default"),
                    new DesignTime.Profiles.Loser(),
                    new DesignTime.Profiles.Winner(),
                    new DesignTime.Profiles.FiveHundredMatches()
                });       
#else
            profileStorage = new Services.Storage.FileSystemProfileStorage();
#endif

            Services.ProfileManager profMgr = new Services.ProfileManager(profileStorage,GalaSoft.MvvmLight.Messaging.Messenger.Default);
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<GalaSoft.MvvmLight.Messaging.IMessenger>(() => GalaSoft.MvvmLight.Messaging.Messenger.Default);
            SimpleIoc.Default.Register<GalaSoft.MvvmLight.Views.IDialogService, Services.Wpf.MessageBoxService>();
            SimpleIoc.Default.Register<Services.IProfileManager>(() => profMgr);
            SimpleIoc.Default.Register<Services.ProfileManager>(() => profMgr);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<RecordMatchViewModel>();
            SimpleIoc.Default.Register<MatchRecordsTableViewModel>();
            SimpleIoc.Default.Register<RankPlotViewModel>();
            SimpleIoc.Default.Register<MapWinRatesViewModel>();
            SimpleIoc.Default.Register<ProfileSelectViewModel>();
        }


        #if DEBUG
        public static void ReplaceProfileManagerWithDebugProfileManager()
        {
            SimpleIoc.Default.Unregister<Services.IProfileManager>();
            SimpleIoc.Default.Unregister<Services.ProfileManager>();

            var designTimeProfile = new DesignTime.DesignTimeProfileManager();
            SimpleIoc.Default.Register<Services.IProfileManager>(() => designTimeProfile);
            //SimpleIoc.Default.Register<Services.ProfileManager>(() => designTimeProfile);
        }
        #endif

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}