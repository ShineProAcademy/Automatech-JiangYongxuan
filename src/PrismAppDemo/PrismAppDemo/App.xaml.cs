using Prism.Ioc;
using PrismAppDemo.Views;
using System.Windows;

namespace PrismAppDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register with creating a new instance each time whenever a log instance is required
            // containerRegistry.Register(typeof(ILog), typeof(Log));
            // Register with creating a global instance
            // containerRegistry.RegisterInstance(typeof(ILog), new Log());
            // Register with singleton mode, seems like a non-parameter contrustor is required
            containerRegistry.RegisterSingleton(typeof(ILog), typeof(Log));

            containerRegistry.RegisterDialog<Views.MessageBox, ViewModels.MessageBoxViewModel>("Message");
        }
    }
}
