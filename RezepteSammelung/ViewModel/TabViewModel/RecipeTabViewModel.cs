using System.Collections.ObjectModel;
using Database_Models.DBModels.RecipeModels;
using Database_Models.DBModels.StockModels;
using DatabaseAccess.Interface;

namespace RezepteSammelung.ViewModel.TabViewModel;

internal class RecipeTabViewModel
{
    
    public readonly ObservableCollection<Foodstuff> StockCollection;
    public readonly ObservableCollection<Recipe> allRecipes;

    public ObservableCollection<OvenSettings> OvenSettingsList { get; set; }
    public ObservableCollection<Recipe> Recipes { get; set; }
    public RecipeTabViewModel(IAccessData<RecipeFolder> recipeFolder, IAccessData<StockFolder> stockFolder)
    {
        StockCollection = stockFolder.Data.StockList;
        allRecipes = recipeFolder.Data.Recipes;
        Recipes = new(recipeFolder.Data.Recipes);
        OvenSettingsList = recipeFolder.Data.OvenSettings;
    }
}