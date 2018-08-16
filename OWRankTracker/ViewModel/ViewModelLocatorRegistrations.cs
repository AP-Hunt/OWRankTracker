using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using OWRankTracker.Model;
using OWRankTracker.Profile;
using System.Collections.Generic;
using Autofac;

namespace OWRankTracker.ViewModel
{
    /// <summary>
    /// This partial class contains automatically 
    /// generated locator attributes for viewmodel types
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("T4", "x")]
    partial class ViewModelLocator
    {
        public OWRankTracker.ViewModel.MainViewModel MainViewModel => OWRankTracker.DependencyInjection.Container.Instance.Resolve<OWRankTracker.ViewModel.MainViewModel>();
        public OWRankTracker.ViewModel.MapWinRatesViewModel MapWinRatesViewModel => OWRankTracker.DependencyInjection.Container.Instance.Resolve<OWRankTracker.ViewModel.MapWinRatesViewModel>();
        public OWRankTracker.ViewModel.MatchDataViewModelBase MatchDataViewModelBase => OWRankTracker.DependencyInjection.Container.Instance.Resolve<OWRankTracker.ViewModel.MatchDataViewModelBase>();
        public OWRankTracker.ViewModel.MatchRecordsTableViewModel MatchRecordsTableViewModel => OWRankTracker.DependencyInjection.Container.Instance.Resolve<OWRankTracker.ViewModel.MatchRecordsTableViewModel>();
        public OWRankTracker.ViewModel.ProfileSelectViewModel ProfileSelectViewModel => OWRankTracker.DependencyInjection.Container.Instance.Resolve<OWRankTracker.ViewModel.ProfileSelectViewModel>();
        public OWRankTracker.ViewModel.RankPlotSettingViewModel RankPlotSettingViewModel => OWRankTracker.DependencyInjection.Container.Instance.Resolve<OWRankTracker.ViewModel.RankPlotSettingViewModel>();
        public OWRankTracker.ViewModel.RankPlotViewModel RankPlotViewModel => OWRankTracker.DependencyInjection.Container.Instance.Resolve<OWRankTracker.ViewModel.RankPlotViewModel>();
        public OWRankTracker.ViewModel.RecordMatchViewModel RecordMatchViewModel => OWRankTracker.DependencyInjection.Container.Instance.Resolve<OWRankTracker.ViewModel.RecordMatchViewModel>();
    }
}