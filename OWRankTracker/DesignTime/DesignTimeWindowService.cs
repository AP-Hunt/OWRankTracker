using OWRankTracker.Services.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OWRankTracker.DesignTime
{
    class DesignTimeWindowService : IWindowService
    {
        public TWindow ShowWindow<TWindow>() where TWindow : Window
        {
            // No op
            return null;
        }

        public TWindow ShowWindow<TWindow>(Window owner) where TWindow : Window
        {
            // No op
            return null;
        }
    }
}
