using Database_Models.DBModels.StockModels;
using HouseholdmanagementTool.DatabaseAccess.Interface;
using System.Collections.ObjectModel;

namespace HouseholdmanagementTool.UI.ViewModel.Windows;

internal class NewIngredientViewModel
{
    public readonly ObservableCollection<Foodstuff> StockCollection;
    public NewIngredientViewModel(IAccessData<StockFolder> stockFolder)
    {
        StockCollection = stockFolder.Data.StockList;
    }
}