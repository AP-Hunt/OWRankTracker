using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using OWRankTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OWRankTracker
{
    class AppStartup
    {
        public static void Startup(Window parent)
        {
            ViewModel.ViewModelLocator locator = new ViewModel.ViewModelLocator();
            
            IProfileManager profileManager = ServiceLocator.Current.GetInstance<IProfileManager>();
            profileManager.OpenDefaultProfile(emitMessage: false);
        }
    }
}
