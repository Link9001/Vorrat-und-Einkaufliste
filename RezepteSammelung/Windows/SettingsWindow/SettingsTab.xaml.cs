using Database_Models.DBModels.RecipeModels;
using Database_Models.DBModels.StockModels;
using HouseholdmanagementTool.UI.ViewModel.TabViewModel;
using HouseholdmanagementTool.UI.ViewModel.Windows;
using System.Windows;
using System.Windows.Controls;
using Unity;

namespace RezepteSammelung.Windows.SettingsWindow
{
    /// <summary>
    /// Interaktionslogik für SettingsTab.xaml
    /// </summary>
    public partial class SettingsTab : UserControl
    {
        private readonly IUnityContainer container;
        internal SettingsTab(IUnityContainer container, SettingsTabViewModel viewModel)
        {
            this.container = container;
            InitializeComponent();
            DataContext = viewModel;
        }

        private void EditOvenSettings(object sender, RoutedEventArgs e)
        {
            var ovenSettingsViewModel = container.Resolve<ListToModifyVieModel<OvenSettings>>();
            ModifyListWindow.HandleModifyListFromUser(ovenSettingsViewModel);
        }

        private void EditPlacement(object sender, RoutedEventArgs e)
        {
            var placementViewModel = container.Resolve<ListToModifyVieModel<Placement>>();
            ModifyListWindow.HandleModifyListFromUser(placementViewModel);
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
