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

namespace OWRankTracker.Windows
{
    /// <summary>
    /// Interaction logic for ManageProfilesWindow.xaml
    /// </summary>
    public partial class ManageProfilesWindow : Window
    {
        public ManageProfilesWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PromptWindow prompt = new PromptWindow("Profile name", "Profile name", Validator);
            prompt.Owner = this;
            prompt.ShowDialog();
            
            if(prompt.DialogResult.HasValue && prompt.DialogResult.Value)
            {
                ViewModel.ManageProfilesViewModel dc = (ViewModel.ManageProfilesViewModel)DataContext;
                dc.AddProfileCommand.Execute(prompt.Input);
            }
        }

        private Tuple<bool, string> Validator(string input)
        {
            ViewModel.ManageProfilesViewModel dc = (ViewModel.ManageProfilesViewModel)DataContext;

            if(dc.Profiles.Contains(input))
            {
                return Tuple.Create(false, "A profile with that name already exists");
            }

            return Tuple.Create(true, (string)null);
        }
    }
}
