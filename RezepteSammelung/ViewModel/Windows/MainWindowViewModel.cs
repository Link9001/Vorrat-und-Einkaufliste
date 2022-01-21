using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Database_Models.DBModels.RecipeModels;
using RezepteSammelung.ViewModel.TabViewModel;
using Unity;

namespace RezepteSammelung.ViewModel.Windows;

internal class MainWindowViewModel
{
    private IUnityContainer container;
    public StockTabViewModel StockTabViewModel { get; set; }
    public RecipeTabViewModel RecipeTabViewModel { get; set; }
    public SettingsTabViewModel SettingsTabViewModel { get; set; }
    public MainWindowViewModel(IUnityContainer container, StockTabViewModel stockTabViewModel, RecipeTabViewModel recipeTabViewModel, SettingsTabViewModel settingsTabViewModel)
    {
        this.container = container;
        StockTabViewModel = stockTabViewModel;
        RecipeTabViewModel = recipeTabViewModel;
        SettingsTabViewModel = settingsTabViewModel;
        StockTabViewModel.stockList.CollectionChanged += OnStockChange;
        CheckIfRecipeIsAvaiable();
    }

    private void OnStockChange(object? sender, NotifyCollectionChangedEventArgs e)
    {
        CheckIfRecipeIsAvaiable();
    }

    private void CheckIfRecipeIsAvaiable()
    {
        foreach (Recipe recipe in RecipeTabViewModel.Recipes)
        {
            foreach (Ingredient ingredient in recipe.Ingredients)
            {
                if (StockTabViewModel.stockList.Any(x => x.Name == ingredient.Name))
                {
                    ingredient.Status = ColorCollection.IsAvaiable;
                }
                else
                {
                    ingredient.Status = ColorCollection.IsNotAvaiable;
                }
            }

            if (recipe.Ingredients.Any(x => x.Status == ColorCollection.IsNotAvaiable))
            {
                recipe.Status = ColorCollection.IsNotAvaiable;
            }
            else
            {
                recipe.Status = ColorCollection.IsAvaiable;
            }
        }
    }

}