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
            Task<bool> task = tcs.Task;

            MessageBoxWindow msgBoxWindow = new MessageBoxWindow(
                message,
                title,
                buttonConfirmText,
                buttonCancelText
            );

            try
            {
                bool? result = msgBoxWindow.ShowDialog();

                afterHideCallback?.Invoke();

                if (result.HasValue)
                {
                    afterHideCallbackWithResponse?.Invoke(result.Value);
                    afterHideInternal?.Invoke(result.Value);
                    tcs.TrySetResult(result.Value);
                }
                else
                {
                    tcs.TrySetResult(false);
                }
            }
            catch(Exception ex)
            {
                tcs.TrySetException(ex);
            }

            return task;
        }
    }
}
