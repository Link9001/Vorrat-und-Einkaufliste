using System.Collections.ObjectModel;
using Database_Models.DBModels.RecipeModels;
using Database_Models.DBModels.StockModels;
using DatabaseAccess.Interface;

namespace RezepteSammelung.ViewModel.TabViewModel;

internal class SettingsTabViewModel
{
    public ObservableCollection<OvenSettings> OvenSettingsList { get; set; }
    public ObservableCollection<Placement> Placements { get; set; }

    public SettingsTabViewModel(IAccessData<RecipeFolder> recipeFolder, IAccessData<StockFolder> stockFolder)
    {
        OvenSettingsList = recipeFolder.Data.OvenSettings;
        Placements = stockFolder.Data.Placements;
    }
}