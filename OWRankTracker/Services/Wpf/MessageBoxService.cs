using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OWRankTracker.Services.Wpf
{
    class MessageBoxService : GalaSoft.MvvmLight.Views.IDialogService
    {
        public Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            return CreateDialog(message, title, buttonText, afterHideCallback: afterHideCallback);
        }

        public Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        {
            return CreateDialog(error.Message, title, buttonText, afterHideCallback: afterHideCallback);
        }

        public Task ShowMessage(string message, string title)
        {
            return CreateDialog(message, title);
        }

        public Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            return CreateDialog(message, title, buttonText, afterHideCallback: afterHideCallback);
        }

        public Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback)
        {
            return CreateDialog(message, title, buttonConfirmText, buttonCancelText, afterHideCallbackWithResponse: afterHideCallback);
        }

        public Task ShowMessageBox(string message, string title)
        {
            return CreateDialog(message, title);
        }

        private Task<bool> CreateDialog(
            string message, 
            string title, 
            string buttonConfirmText = "OK",
            string buttonCancelText = null,
            Action afterHideCallback = null,
            Action<bool> afterHideCallbackWithResponse = null,
            Action<bool> afterHideInternal = null)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            var dialog = new Xceed.Wpf.Toolkit.MessageBox();
            dialog.Caption = title;
            dialog.Text = message;
            dialog.OkButtonContent = buttonConfirmText;
            
            if(buttonCancelText != null)
            {
                dialog.CancelButtonContent = buttonCancelText;
            }

            dialog.Closed += (sender, args) =>
            {
                afterHideCallback?.Invoke();

                bool result;
                switch(dialog.MessageBoxResult)
                {
                    default:
                    case MessageBoxResult.Cancel:
                    case MessageBoxResult.No:
                    case MessageBoxResult.None:
                        result = false;
                        break;

                    case MessageBoxResult.OK:
                    case MessageBoxResult.Yes:
                        result = true;
                        break;
                }

                afterHideCallbackWithResponse?.Invoke(result);
                afterHideInternal?.Invoke(result);
            };

            try
            {
                bool? result = dialog.ShowDialog();
                tcs.TrySetResult(result.HasValue ? result.Value : false);
            }
            catch(Exception ex)
            {
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }
    }
}
