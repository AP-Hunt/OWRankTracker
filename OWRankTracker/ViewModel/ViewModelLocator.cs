/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:OWRankTracker.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using Autofac;
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
    partial class ViewModelLocator
    {
        public MainViewModel MainViewModel                              => DependencyInjection.Container.Instance.Resolve<MainViewModel>();
        public MapWinRatesViewModel MapWinRatesViewModel                => DependencyInjection.Container.Instance.Resolve<MapWinRatesViewModel>();
        public MatchDataViewModelBase MatchDataViewModelBase            => DependencyInjection.Container.Instance.Resolve<MatchDataViewModelBase>();
        public MatchRecordsTableViewModel MatchRecordsTableViewModel    => DependencyInjection.Container.Instance.Resolve<MatchRecordsTableViewModel>();
        public ProfileSelectViewModel ProfileSelectViewModel            => DependencyInjection.Container.Instance.Resolve<ProfileSelectViewModel>();
        public RankPlotSettingViewModel RankPlotSettingViewModel        => DependencyInjection.Container.Instance.Resolve<RankPlotSettingViewModel>();
        public RankPlotViewModel RankPlotViewModel                      => DependencyInjection.Container.Instance.Resolve<RankPlotViewModel>();
        public RecordMatchViewModel RecordMatchViewModel                => DependencyInjection.Container.Instance.Resolve<RecordMatchViewModel>();
    }
}