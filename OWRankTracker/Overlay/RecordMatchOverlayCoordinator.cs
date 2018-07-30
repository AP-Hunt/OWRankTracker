using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Overlay
{
    class RecordMatchOverlayCoordinator : OverlayCoordinator
    {
        protected override void SetUp()
        {
            Views.RecordMatchView view = new Views.RecordMatchView();
            ViewModel.RecordMatchViewModel viewModel = ServiceLocator.Current.GetInstance<ViewModel.RecordMatchViewModel>();

            view.DataContext = viewModel;
            
            OverlayWindow.Add(view);
        }
    }
}
