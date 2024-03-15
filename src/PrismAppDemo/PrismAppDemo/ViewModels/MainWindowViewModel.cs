using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using PrismAppDemo;
using System.Collections.Generic;
using System.Windows.Documents;

namespace PrismAppDemo.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        private IContainerExtension Container;
        private IDialogService Dialog;
        private ILog log;

        public bool IsEnabled
        { 
            get { return isEnabled; }
            set { SetProperty(ref isEnabled, value); }
        }
        private bool isEnabled = true;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public List<Dog> Dogs
        {
            get { return dogs; }
            set { SetProperty(ref dogs, value); }
        }
        private List<Dog> dogs = null; // new List<Dog>() { new Dog { Name = "Hapi" }, new Dog { Name = "Ted" } };  // A null list leads to a null control item list.

        //public MainWindowViewModel(IContainerExtension container)
        //{
        //    Container = container;
            
        //}

        public MainWindowViewModel(IDialogService dialogService)
        {
            Dialog = dialogService;
        }

        public DelegateCommand ExecuteCommand
        {
            get 
            {
                if (executeCommand == null)
                {
                    executeCommand = new DelegateCommand(Execute);
                }
                return executeCommand;
            }
        }
        private DelegateCommand executeCommand;

        public void Execute()
        {
            // Log("Info", "I'm clicked!");
            IDialogParameters dialogParameters = new DialogParameters();
            dialogParameters.Add("title", "消息框");
            dialogParameters.Add("message", "我是一个很长的测试信息");
            Dialog.Show("Message", dialogParameters, ret => {
                if (ret.Result == ButtonResult.Yes)
                {
                    var data = ret.Parameters.GetValue<string>("ret");
                }
            });
        }

        public DelegateCommand<Dog> ExecuteCommandWithParams
        {
            get
            {
                return executeCommandWithParam ?? (executeCommandWithParam = new DelegateCommand<Dog>(ExecuteWithParams).ObservesCanExecute(() => IsEnabled));
            }
        }
        private DelegateCommand<Dog> executeCommandWithParam;

        public void ExecuteWithParams(Dog dog)
        {
            if (dog == null)
            {
                return;
            }

            log = Container.Resolve<ILog>();
            log?.Write("Info", dog.Name);
        }

        #region private methods

        /// <summary>
        /// Log
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="content"></param>
        /// <param name="source"></param>
        private void Log(string tag, string content, string source = null)
        {
            log = Container.Resolve<ILog>();
            log?.Write(tag, content, source);
        }

        #endregion
    }

    public class Dog
    {
        public string Name { get; set; } = string.Empty;
    }
}
