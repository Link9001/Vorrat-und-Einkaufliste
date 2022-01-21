using System.Collections.ObjectModel;
using Database_Models.DBModels.StockModels;
using DatabaseAccess.Interface;

namespace RezepteSammelung.ViewModel.TabViewModel;

internal class StockTabViewModel
{
    public readonly ObservableCollection<Foodstuff> shoppingList;
    public readonly ObservableCollection<Foodstuff> stockList;
    public readonly ObservableCollection<Placement> Placements;
    public ObservableCollection<Foodstuff> ShoppingList { get; set; }
    public ObservableCollection<Foodstuff> StockList { get; set; }

    public StockTabViewModel(IAccessData<StockFolder> stockFolder)
    {
        this.shoppingList = stockFolder.Data.ShoppingList;
        ShoppingList = new(shoppingList);
        this.stockList = stockFolder.Data.StockList;
        StockList = new(stockList);
        Placements = stockFolder.Data.Placements;
    }
}