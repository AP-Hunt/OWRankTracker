using Autofac;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OWRankTracker.Services.Wpf
{
    internal class WindowService : IWindowService
    {
        private IContainer _container;

        public WindowService(IContainer container)
        {
            this._container = container;
        }

        public TWindow ShowWindow<TWindow>() where TWindow : Window
        {
            try
            {
                TWindow window = _container.Resolve<TWindow>();
                window.Show();
                return window;
            }
            catch(Autofac.Core.Registration.ComponentNotRegisteredException ex)
            {
                throw new ArgumentOutOfRangeException($"Cannot resolve window of type {typeof(TWindow).FullName}", ex);
            }
        }
    }
}
