using HouseholdmanagementTool.UI.Windows.RecipeWindow;
using HouseholdmanagementTool.UI.Windows.SettingsWindow;
using HouseholdmanagementTool.UI.Windows.StockWindow;
using System.Windows;
using System.Windows.Input;
using Unity;

namespace HouseholdmanagementTool.UI.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly IUnityContainer container;
    internal MainWindow(IUnityContainer container)
    {
        this.container = container;
        InitializeComponent();
        Title = "Haushalt";
        Stock.Content = container.Resolve<StockTab>();
        Recipe.Content = container.Resolve<RecipeTab>();
        Settings.Content = container.Resolve<SettingsTab>();
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        if (e.LeftButton is MouseButtonState.Pressed)
        {
            WindowState = WindowState.Normal;
            DragMove();
        }
    }

    private void OnCloseWindow(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void OnMaximiseWindow(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Maximized;
    }

    private void OnMinimiseWindow(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }
}