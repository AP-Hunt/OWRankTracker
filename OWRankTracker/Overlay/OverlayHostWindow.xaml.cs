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
using System.Windows.Shapes;

namespace OWRankTracker.Overlay
{
    /// <summary>
    /// Interaction logic for OverlayHostWindow.xaml
    /// </summary>
    public partial class OverlayHostWindow : Window
    {
        private readonly Process.NET.Windows.IWindow _targetWindow;

        public event EventHandler<DrawingContext> Draw;

        public OverlayHostWindow()
        {
            InitializeComponent();
        }

        public OverlayHostWindow(Process.NET.Windows.IWindow targetWindow)
        {
            _targetWindow = targetWindow;
            InitializeComponent();
        }

        public void Update()
        {
            Width = _targetWindow.Width;
            Height = _targetWindow.Height;
            Left = _targetWindow.X;
            Top = _targetWindow.Y;
        }

        /// <summary>
        ///     Raises the <see cref="E:System.Windows.Window.SourceInitialized" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            // We need to do this in order to allow shapes
            // drawn on the canvas to be click-through. 
            // There is no other way to do this.
            // Source: https://social.msdn.microsoft.com/Forums/en-US/c32889d3-effa-4b82-b179-76489ffa9f7d/how-to-clicking-throughpassing-shapesellipserectangle?forum=wpf
            this.MakeWindowTransparent();
        }

        /// <summary>
        ///     When overridden in a derived class, participates in rendering operations that are directed by the layout system.
        ///     The rendering instructions for this element are not used directly when this method is invoked, and are instead
        ///     preserved for later asynchronous use by layout and drawing.
        /// </summary>
        /// <param name="drawingContext">
        ///     The drawing instructions for a specific element. This context is provided to the layout
        ///     system.
        /// </param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            OnDraw(drawingContext);
            base.OnRender(drawingContext);
        }

        /// <summary>
        /// Sets 
        /// </summary>
        /// <param name="ctrl"></param>
        public void SetHostedControl(Control ctrl) => Host.Content = ctrl;

        /// <summary>
        ///     Called when [draw].
        /// </summary>
        /// <param name="e">The e.</param>
        protected virtual void OnDraw(DrawingContext e) => Draw?.Invoke(this, e);
    }
}
