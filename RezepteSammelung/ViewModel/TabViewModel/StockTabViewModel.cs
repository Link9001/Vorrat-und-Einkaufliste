using Database_Models.DBModels.RecipeModels;
using Database_Models.DBModels.StockModels;
using HouseholdmanagementTool.DatabaseAccess.Interface;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace HouseholdmanagementTool.UI.ViewModel.TabViewModel;

internal class StockTabViewModel
{
    private readonly ObservableCollection<Recipe> recipes;

    public readonly ObservableCollection<Foodstuff> shoppingList;
    public readonly ObservableCollection<Foodstuff> stockList;
    public readonly ObservableCollection<Placement> Placements;
    public ObservableCollection<Foodstuff> ShoppingList { get; set; }
    public ObservableCollection<Foodstuff> StockList { get; set; }

    public StockTabViewModel(IAccessData<StockFolder> stockFolder, IAccessData<RecipeFolder> resipeFolder)
    {
        recipes = resipeFolder.Data.Recipes;
        shoppingList = stockFolder.Data.ShoppingList;
        ShoppingList = new(shoppingList);
        stockList = stockFolder.Data.StockList;
        StockList = new(stockList);
        Placements = stockFolder.Data.Placements;
        stockList.CollectionChanged += OnStockChange;
        CheckIfRecipeIsAvaiable();
    }

    private void OnStockChange(object? sender, NotifyCollectionChangedEventArgs e)
    {
        CheckIfRecipeIsAvaiable();
    }

    private void CheckIfRecipeIsAvaiable()
    {
        foreach (var recipe in recipes)
        {
            foreach (var ingredient in recipe.Ingredients)
            {
                ingredient.Status = stockList.Any(x => x.Name == ingredient.Name) ? ColorCollection.IsAvaiable : ColorCollection.IsNotAvaiable;
            }

            recipe.Status = recipe.Ingredients.Any(x => x.Status == ColorCollection.IsNotAvaiable) ? ColorCollection.IsNotAvaiable : ColorCollection.IsAvaiable;
        }
    }
}