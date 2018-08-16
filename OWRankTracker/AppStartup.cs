using Autofac;
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
            IProfileManager profileManager = DependencyInjection.Container.Instance.Resolve<IProfileManager>();
            profileManager.OpenDefaultProfile(emitMessage: false);
        }
    }
}
