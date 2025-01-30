using Microsoft.Extensions.DependencyInjection;
using QM_ItemCreatorTool.Interfaces;
using QM_ItemCreatorTool.Managers;
using QM_ItemCreatorTool.ViewModel;
using System.Windows;

namespace QM_ItemCreatorTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;
        public App()
        {
            ServiceCollection service = new ServiceCollection();
            ConfigureService(service);
            _serviceProvider = service.BuildServiceProvider();
        }

        private void ConfigureService(ServiceCollection service)
        {
            service.AddTransient<MainViewModel>();
            service.AddTransient<MainWindow>();

            service.AddSingleton<IErrorHandler, MessageBoxErrorHandler>();
            service.AddSingleton<IMessageBoxHandler, ConfirmationManager>();

            service.AddTransient<GeneralTabViewModel>();
            service.AddTransient<RangedTabViewModel>();
            service.AddTransient<MeleeTabViewModel>();
            service.AddTransient<AmmoTabViewModel>();
            service.AddTransient<ItemReceiptTabViewModel>();
            service.AddTransient<LocalizationTabViewModel>();

            service.AddSingleton<DataProviderManager>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow?.Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            QM_ItemCreatorTool.Properties.Settings.Default.Save();
        }
    }
}
