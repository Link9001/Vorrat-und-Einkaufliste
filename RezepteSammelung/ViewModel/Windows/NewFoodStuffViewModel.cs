using Database_Models.DBModels.StockModels;
using HouseholdmanagementTool.DatabaseAccess.Interface;
using System.Collections.ObjectModel;

namespace HouseholdmanagementTool.UI.ViewModel.Windows;

internal class NewFoodStuffViewModel
{
    public NewFoodStuffViewModel(IAccessData<StockFolder> stockFolder)
    {
        Placements = stockFolder.Data.Placements;
    }

    public ObservableCollection<Placement> Placements { get; set; }
}