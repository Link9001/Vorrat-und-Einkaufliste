using RezepteSammelung.ViewModel.TabViewModel;
using RezepteSammelung.ViewModel.Windows;
using System.Windows;
using Database_Models;
using DatabaseAccess;
using RezepteSammelung.Windows;
using Unity;

namespace RezepteSammelung
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Database? db;
        private void AppStartup(object sender, StartupEventArgs e)
        {
            var container = Configure.CreateUnityContainer();
            container.ConfigureDatabase()
                .ConfigureDataAccess()
                .ConfigureApp();
            db = container.Resolve<Database>();
            MainWindow mainWindow = container.Resolve<MainWindow>();
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mainWindow.Show();
        }

        private void AppExit(object sender, ExitEventArgs e)
        {
            db?.Dispose();
        }
    }
}
