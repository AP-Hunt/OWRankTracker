using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Services.Wpf
{
    public interface IWindowService
    {
        TWindow ShowWindow<TWindow>() where TWindow : System.Windows.Window;
    }
}
