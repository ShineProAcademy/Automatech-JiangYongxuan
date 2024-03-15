using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismAppDemo.ViewModels
{
    public class MessageBoxViewModel : BindableBase, IDialogAware
    {
        public MessageBoxViewModel()
        {

        }

        #region Properties

        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }
        private string message = string.Empty;

        #endregion

        #region Commands

        private DelegateCommand closeCommand;
        public DelegateCommand CloseCommand =>
            closeCommand ?? (closeCommand = new DelegateCommand(ExecuteCloseCommand));

        void ExecuteCloseCommand()
        {
            IDialogParameters dialogParameters = new DialogParameters();
            dialogParameters.Add("ret", ButtonResult.Yes);
            RequestClose?.Invoke(new DialogResult(ButtonResult.Yes, dialogParameters));
        }

        #endregion

        #region IDialogAware

        public string Title { get; set; } = "Information";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            Title = "Information";
            Message = string.Empty;
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            IDialogParameters dialogParameters = parameters as IDialogParameters;
            Title = dialogParameters.GetValue<string>("title");
            Message = dialogParameters.GetValue<string>("message");
        }

        #endregion
    }
}
