using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OWRankTracker.Core.Services;

namespace OWRankTracker.ViewModel.Design
{
    class DesignTimeManageProfilesViewModel : ManageProfilesViewModel
    {
        public DesignTimeManageProfilesViewModel() : base(new DesignTime.DesignTimeProfileManager(), new Services.Wpf.MessageBoxService())
        {
        }
    }
}
