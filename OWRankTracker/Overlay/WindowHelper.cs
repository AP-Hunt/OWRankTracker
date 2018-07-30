using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace OWRankTracker.Overlay
{
    /// <summary>
    /// Source https://social.msdn.microsoft.com/Forums/en-US/c32889d3-effa-4b82-b179-76489ffa9f7d/how-to-clicking-throughpassing-shapesellipserectangle?forum=wpf
    /// </summary>
    public static class WindowHelper
    {
        public static void MakeWindowTransparent(this Window wnd)
        {
            IntPtr hwnd = new WindowInteropHelper(wnd).Handle;
            int extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
        }

        const int GWL_EXSTYLE = (-20);
        const int WS_EX_TRANSPARENT = 0x00000020;
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);
        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hwnd, int index);
    }
}
