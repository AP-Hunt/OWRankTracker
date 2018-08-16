using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace OWRankTracker.Services.Wpf
{
    /// <summary>
    /// Interaction logic for MessageBox.xaml
    /// </summary>
    public partial class MessageBoxWindow : Window
    {
        public string MessageText { get; }
        public string Caption { get; }
        public string ConfirmText { get; }
        public string CancelText { get; }
        public Visibility CancelIsVisible => CancelText != null ? Visibility.Visible : Visibility.Collapsed;

        public MessageBoxWindow(
            string message, 
            string title, 
            string buttonConfirmText = "OK",
            string buttonCancelText = null)
        {

            MessageText = message;
            Caption = title;
            ConfirmText = buttonConfirmText;
            CancelText = buttonCancelText;

            DataContext = this;
            InitializeComponent();
        }

        public MessageBoxWindow()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                MessageText = "A sufficiently long message that will cause the text to wrap in order to keep within a reasonable width";
                Caption = "A reasonably long title";
                ConfirmText = "OK";
                CancelText = "Cancel";
                DataContext = this;
            }
            else
            {
                throw new InvalidOperationException("Don't use this constructor outside of design time");
            }
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
