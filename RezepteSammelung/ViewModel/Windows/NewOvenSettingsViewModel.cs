using System.Collections.ObjectModel;
using Database_Models.DBModels.RecipeModels;
using DatabaseAccess.Interface;

namespace RezepteSammelung.ViewModel.Windows;

internal class NewOvenSettingsViewModel
{
    public ObservableCollection<OvenSettings> OvenSettingsList { get; set; }

    public NewOvenSettingsViewModel(IAccessData<RecipeFolder> recipeFolder)
    {
        OvenSettingsList = recipeFolder.Data.OvenSettings;
    }
}