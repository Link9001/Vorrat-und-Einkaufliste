using System.Windows;
using RezepteSammelung.ViewModel.Windows;
using RezepteSammelung.Windows.RecipeWindow;
using RezepteSammelung.Windows.SettingsWindow;
using RezepteSammelung.Windows.StockWindow;
using Unity;

namespace RezepteSammelung.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IUnityContainer container;
        internal MainWindow(IUnityContainer container, MainWindowViewModel viewModel)
        {
            this.container = container;
            InitializeComponent();
            stock.Content = container.Resolve<StockTab>();
            recipe.Content = container.Resolve<RecipeTab>();
            settins.Content = container.Resolve<SettingsTab>();
        }
    }
}
