using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Database_Models.DBModels.RecipeModels;
using Database_Models.DBModels.StockModels;
using DatabaseAccess.Interface;

namespace RezepteSammelung.ViewModel.TabViewModel;

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
        this.recipes = resipeFolder.Data.Recipes;
        this.shoppingList = stockFolder.Data.ShoppingList;
        ShoppingList = new(shoppingList);
        this.stockList = stockFolder.Data.StockList;
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
                ingredient.Status = this.stockList.Any(x => x.Name == ingredient.Name) ? ColorCollection.IsAvaiable : ColorCollection.IsNotAvaiable;
            }

            recipe.Status = recipe.Ingredients.Any(x => x.Status == ColorCollection.IsNotAvaiable) ? ColorCollection.IsNotAvaiable : ColorCollection.IsAvaiable;
        }
    }
}