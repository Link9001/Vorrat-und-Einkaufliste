using System.Windows;
using System.Windows.Controls;
using RezepteSammelung.ViewModel.TabViewModel;
using RezepteSammelung.ViewModel.Windows;
using Unity;

namespace RezepteSammelung.Windows.SettingsWindow
{
    /// <summary>
    /// Interaktionslogik für SettingsTab.xaml
    /// </summary>
    public partial class SettingsTab : UserControl
    {
        private IUnityContainer container;
        internal SettingsTab(IUnityContainer container, SettingsTabViewModel viewModel)
        {
            this.container = container;
            InitializeComponent();
            DataContext = viewModel;
        }

        private void EditOvenSettings(object sender, RoutedEventArgs e)
        {
            SettingsTabViewModel viewModel = (SettingsTabViewModel)DataContext;
            var ovenSettingsViewModel = container.Resolve<NewOvenSettingsViewModel>();
            viewModel.OvenSettingsList = NewOvenSettings.HandelNewOvensettings(ovenSettingsViewModel);
        }

        private void EditPlacement(object sender, RoutedEventArgs e)
        {
            SettingsTabViewModel viewModel = (SettingsTabViewModel) DataContext;
            NewPlacementViewModel placementViewModel = container.Resolve<NewPlacementViewModel>();
            viewModel.Placements = NewPlacement.HandelNewPlacments(placementViewModel);
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
