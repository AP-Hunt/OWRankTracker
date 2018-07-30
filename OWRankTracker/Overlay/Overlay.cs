using Process.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Overlay
{
    class Overlay
    {
        public static OverlayCoordinator Coordinator = null;

        public static void Startup()
        {
            System.Diagnostics.Process notepadProc = System.Diagnostics.Process.GetProcessesByName("notepad").FirstOrDefault();
            ProcessSharp ps = new ProcessSharp(notepadProc, Process.NET.Memory.MemoryType.Remote);

            RecordMatchOverlayCoordinator overlayCoordinator = new RecordMatchOverlayCoordinator();
            Coordinator = overlayCoordinator;
            Coordinator.Initialize(ps.WindowFactory.MainWindow);
            Coordinator.Enable();
        }
    }
}
