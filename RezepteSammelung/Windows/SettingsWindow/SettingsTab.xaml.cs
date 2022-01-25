using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Database_Models.DBModels.RecipeModels;
using Database_Models.DBModels.StockModels;
using Database_Models.Interfaces;
using DatabaseAccess.Interface;
using RezepteSammelung.ViewModel.TabViewModel;
using RezepteSammelung.ViewModel.Windows;
using Unity;
using UtitlityFunctions.InterfaceExtention;

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
            var viewModel = (SettingsTabViewModel) DataContext;
            var ovenSettingsViewModel = container.Resolve<OvenSettingListViewModel>();
            //var modifyedListFromUser = ModifyListWindow.HandleModifyListFromUser(ovenSettingsViewModel);
            //if (modifyedListFromUser != null)
            //{
            //    viewModel.OvenSettingsList = modifyedListFromUser.CastCollection<IName, OvenSettings>();
            //}
        }

        private void EditPlacement(object sender, RoutedEventArgs e)
        {
            SettingsTabViewModel viewModel = (SettingsTabViewModel)DataContext;
            var placementViewModel = container.Resolve<PlacementListViewModel>();
            var modifyedListFromUser = ModifyListWindow.HandleModifyListFromUser(placementViewModel);
            if (modifyedListFromUser != null)
            {
                //viewModel.Placements = modifyedListFromUser.CastCollection<IName, Placement>();
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
