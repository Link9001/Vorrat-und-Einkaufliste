using Database_Models;
using HouseholdmanagementTool.DatabaseAccess;
using HouseholdmanagementTool.UI;
using RezepteSammelung.Windows;
using System.Windows;
using Unity;
using Configuration = HouseholdmanagementTool.UI.Configuration;

namespace RezepteSammelung;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private Database? _db;
    private void AppStartup(object sender, StartupEventArgs e)
    {
        var container = Configuration.CreateUnityContainer();
        container.ConfigureDatabase()
            .ConfigureDataAccess()
            .ConfigureApp();
        _db = container.Resolve<Database>();
        var mainWindow = container.Resolve<MainWindow>();
        mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        mainWindow.Show();
    }

    private void AppExit(object sender, ExitEventArgs e)
    {
        _db?.Dispose();
    }
}