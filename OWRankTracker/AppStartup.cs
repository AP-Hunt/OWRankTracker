using Autofac;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using OWRankTracker.Core.Services;
using OWRankTracker.Validation;
using OWRankTracker.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OWRankTracker
{
    class AppStartup
    {
        public static void Startup(Window parent)
        {
            IProfileManager profileManager = DependencyInjection.Container.Instance.Resolve<IProfileManager>();

            if (!profileManager.Profiles.Any())
            {
                RequestFirstProfileNameFromUser(profileManager);
            }
            else
            {
                profileManager.OpenDefaultProfile(emitMessage: false);
            }
        }

        private static void RequestFirstProfileNameFromUser(IProfileManager profileManager)
        {
            IProfileNameValidator validator = DependencyInjection.Container.Instance.Resolve<IProfileNameValidator>();
            Func<string, Tuple<bool, string>> ValidateProfileName = (name) =>
            {
                if (validator.Validate(name))
                {
                    return Tuple.Create(true, (string)null);
                }
                else
                {
                    return Tuple.Create(false, "Invalid profile name");
                }
            };
            PromptWindow prompt = new PromptWindow("Profile name", "Please provide the name of your first profile", ValidateProfileName);

            // Keep the user here until they fill it out
            while (prompt.DialogResult != true)
            {
                prompt.ShowDialog();
            }

            profileManager.Create(prompt.Input);
            profileManager.OpenProfile(prompt.Input, emitMessage: false);
        }
    }
}
