using System.Collections.ObjectModel;
using Database_Models.DBModels.StockModels;
using DatabaseAccess.Interface;

namespace RezepteSammelung.ViewModel.Windows;

internal class NewFoodStuffViewModel
{
    public NewFoodStuffViewModel(IAccessData<StockFolder> stockFolder)
    {
        Placements = stockFolder.Data.Placements;
    }

    public ObservableCollection<Placement> Placements { get; set; }
}