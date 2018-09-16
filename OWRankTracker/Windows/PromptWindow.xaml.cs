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

namespace OWRankTracker.Windows
{
    /// <summary>
    /// Interaction logic for PromptWindow.xaml
    /// </summary>
    public partial class PromptWindow : Window, IDataErrorInfo
    {
        private Func<string, Tuple<bool, string>> _validator;

        public string Prompt { get; set; }
        public string Input { get; set; }

        public string Error => null;
        public string this[string columnName]
        {
           get
           {
                switch(columnName)
                {
                    case nameof(Input):
                        (bool valid, string error) = _validator(Input);
                        if(!valid)
                        {
                            return error;
                        }

                        return null;

                    default:
                        return string.Empty;
                }
           }
        }

        public PromptWindow()
        {
            InitializeComponent();
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                Title = "A prompt";
                Prompt = "Give me some text";
                Input = "A bit of text";
                DataContext = this;
            }
        }

        public PromptWindow(string title, string prompt, Func<string, Tuple<bool, string>> validator)
        {
            Title = title;
            Prompt = prompt;
            Input = String.Empty;
            _validator = validator;

            DataContext = this;
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
