﻿using System;
using System.Windows;
using Autofac;
using GalaSoft.MvvmLight.Messaging;
using OWRankTracker.Services.Wpf;
using OWRankTracker.Validation;
using OWRankTracker.ViewModel;
using OWRankTracker.Windows;

namespace OWRankTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => DependencyInjection.Container.Cleanup();
        }

        public override void BeginInit()
        {
            base.BeginInit();
            AppStartup.Startup(this);
        }
    }
}