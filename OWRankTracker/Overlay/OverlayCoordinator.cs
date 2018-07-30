using GalaSoft.MvvmLight.Threading;
using Overlay.NET.Wpf;
using Process.NET.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRankTracker.Overlay
{
    /// <remarks>
    /// In large part inspired or taken from
    ///     https://github.com/lolp1/Overlay.NET/blob/master/src/Overlay.NET.Demo/Wpf/WpfOverlayDemoExample.cs
    /// because I only wanted parts of what the library could do,
    /// and it was causing build errors
    /// </remarks>
    abstract class OverlayCoordinator : WpfOverlayPlugin
    {
        private readonly TickEngine _tickEngine = new TickEngine();
        private bool _isDisposed;

        private bool _isSetup;

        protected abstract void SetUp();

        public override void Initialize(IWindow targetWindow)
        {
            base.Initialize(targetWindow);
            _tickEngine.Interval = TimeSpan.FromMilliseconds(500);
            _tickEngine.PreTick += OnPreTick;
            _tickEngine.Tick += OnTick;
        }

        private void OnTick(object sender, EventArgs e)
        {
            // This will only be true if the target window is active
            // (or very recently has been, depends on your update rate)
            if (OverlayWindow.IsVisible)
            {
                OverlayWindow.Update();
            }
        }

        private void OnPreTick(object sender, EventArgs e)
        {
            // Only want to set them up once.
            if (!_isSetup)
            {
                SetUp();
                _isSetup = true;
            }

            var activated = TargetWindow.IsActivated;
            var visible = OverlayWindow.IsVisible;

            // Ensure window is shown or hidden correctly prior to updating
            if (!activated && visible)
            {
                OverlayWindow.Hide();
            }

            else if (activated && !visible)
            {
                OverlayWindow.Show();
            }
        }

        // Clear objects
        public override void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            if (IsEnabled)
            {
                Disable();
            }

            DispatcherHelper.CheckBeginInvokeOnUI(() => {
                //OverlayWindow?.Hide();
                //OverlayWindow?.Close();
                //OverlayWindow = null;
            });

            OverlayWindow = null;
            //_tickEngine.Stop();

            //base.Dispose();
            //_isDisposed = true;
        }

        ~OverlayCoordinator()
        {
            Dispose();
        }
    }
}
