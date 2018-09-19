using Autofac;
using OWRankTracker.Services.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OWRankTracker.Views
{
    /// <summary>
    /// Interaction logic for ProfileSelect.xaml
    /// </summary>
    public partial class ProfileSelect : UserControl
    {
        private Windows.ManageProfilesWindow _manageProfilesWindow;
        private IWindowService _windowService;

        public ProfileSelect()
        {
            InitializeComponent();
            _windowService = DependencyInjection.Container.Instance.Resolve<IWindowService>();
        }

        private void OnProfileManageClick(object sender, RoutedEventArgs evt)
        {
            if (_manageProfilesWindow == null)
            {
                _manageProfilesWindow = _windowService.ShowWindow<Windows.ManageProfilesWindow>(Window.GetWindow(this));
                _manageProfilesWindow.Closed += (_, __) =>
                {
                    _manageProfilesWindow = null;
                };
            }
        }
    }
}
