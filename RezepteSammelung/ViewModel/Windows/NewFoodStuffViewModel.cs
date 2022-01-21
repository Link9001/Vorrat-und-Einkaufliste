using System.Collections.ObjectModel;
using Database_Models.DBModels.StockModels;
using DatabaseAccess.Interface;

namespace RezepteSammelung.ViewModel.Windows;

internal class NewFoodStuffViewModel
{
    public NewFoodStuffViewModel(IAccessData<StockFolder> stockFolder)
    {
        AllPlacements = stockFolder.Data.Placements;
    }

    public ObservableCollection<Placement> AllPlacements { get; set; }
    public string? Name { get; set; }
    public int? Placement { get; set; }
}