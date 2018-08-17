using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Views;
using OWRankTracker.Services;

namespace OWRankTracker.ViewModel.Design
{
    class DesignTimeRecordMatchViewModel : RecordMatchViewModel
    {
        public DesignTimeRecordMatchViewModel() : base(new DesignTime.DesignTimeProfileManager(), new Services.Wpf.MessageBoxService())
        {
        }
    }
}
