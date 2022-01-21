using System.Collections.ObjectModel;
using Database_Models.DBModels.StockModels;
using DatabaseAccess.Interface;

namespace RezepteSammelung.ViewModel.Windows;

internal class NewPlacementViewModel
{
    public ObservableCollection<Placement> Placements { get; set; }

    public NewPlacementViewModel(IAccessData<StockFolder> recipeFolder)
    {
        Placements = recipeFolder.Data.Placements;
    }
}