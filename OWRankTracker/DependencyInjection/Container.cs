﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using OWRankTracker.Core.Profile;
using OWRankTracker.Core.Services.Storage;

namespace OWRankTracker.DependencyInjection
{
    public static class Container
    {
        private static Autofac.IContainer _instance;
        public static Autofac.IContainer Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = BuildContainer();
                }
                return _instance;
            }
        }

        internal static void Cleanup()
        {
            if(_instance != null)
            {
                _instance.Dispose();
            }
        }

        private static IContainer BuildContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.Register<IContainer>((ctx) => Container.Instance).SingleInstance();

            RegisterMVVMTypes(builder);
            RegisterApplicationTypes(builder);
            RegisterViewModels(builder);
            RegisterWindowTypes(builder);

            return builder.Build();
        }

        private static void RegisterMVVMTypes(ContainerBuilder builder)
        {
            builder.Register<IMessenger>((_) => GalaSoft.MvvmLight.Messaging.Messenger.Default).SingleInstance();
            builder.RegisterType<Services.Wpf.MessageBoxService>().AsImplementedInterfaces();
            builder.RegisterType<Services.Wpf.WindowService>().AsImplementedInterfaces();
        }

        private static void RegisterApplicationTypes(ContainerBuilder builder)
        {
            IProfileStorage profileStorage;
#if DEBUG_PROFILES
            // Debug configruation with existing profiles
            profileStorage = new Core.Services.Storage.InMemoryProfileStorage(new List<IProfile>()
                {
                    new DesignTime.Profiles.Empty("Default"),
                    new DesignTime.Profiles.Loser(),
                    new DesignTime.Profiles.Winner(),
                    new DesignTime.Profiles.FiveHundredMatches()
                });
#elif DEBUG_NEW_USER
            // Debug configuration with no profiles (new user)
            profileStorage = new Core.Services.Storage.InMemoryProfileStorage();
#else
            profileStorage = new FileSystemProfileStorage();
#endif
            builder.Register<IProfileStorage>((_) => profileStorage).SingleInstance();
            builder.RegisterType<Core.Services.ProfileManager>().AsSelf().AsImplementedInterfaces().SingleInstance();
        }

        private static void RegisterViewModels(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(MainWindow).Assembly)
                .Where(t => 
                       t.IsSubclassOf(typeof(GalaSoft.MvvmLight.ViewModelBase))
                    && !t.Name.Contains("Design")
                );
        }

        private static void RegisterWindowTypes(ContainerBuilder builder)
        {
            builder.RegisterType<Windows.ManageProfilesWindow>().AsSelf();
        }
    }
}
