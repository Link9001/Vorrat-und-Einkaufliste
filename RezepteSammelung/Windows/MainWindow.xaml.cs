using RezepteSammelung.Windows.RecipeWindow;
using RezepteSammelung.Windows.SettingsWindow;
using RezepteSammelung.Windows.StockWindow;
using System.Windows;
using Unity;

namespace RezepteSammelung.Windows;

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
        stock.Content = container.Resolve<StockTab>();
        recipe.Content = container.Resolve<RecipeTab>();
        settins.Content = container.Resolve<SettingsTab>();
    }
}