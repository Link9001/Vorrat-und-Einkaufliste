using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Database_Models.DBModels.RecipeModels;
using Database_Models.DBModels.StockModels;
using DatabaseAccess.Interface;
using RezepteSammelung.ViewModel.TabViewModel;
using RezepteSammelung.ViewModel.Windows;
using Unity;
using UtitlityFunctions.InterfaceExtention;

namespace RezepteSammelung.Windows.RecipeWindow;

public partial class RecipeTab : UserControl
{
    private IUnityContainer container;
    internal RecipeTab(IUnityContainer container, RecipeTabViewModel viewModel)
    {
        this.container = container;
        DataContext = viewModel;
        InitializeComponent();
        Ingredients.ItemsSource = new ObservableCollection<Ingredient>();
    }

    private void UpdateRecipe(object sender, RoutedEventArgs e)
    {
        RecipeTabViewModel viewModel = (RecipeTabViewModel)DataContext;
        if (Recipes.SelectedItem is null)
        {
            return;
        }

        MessageBoxResult msResult = MessageBox.Show("Willst du das Rezept wirklich ändern?", "!", MessageBoxButton.YesNo, MessageBoxImage.Warning);

        if (msResult is MessageBoxResult.Yes)
        {
            Recipe currentRecipe = (Recipe)Recipes.SelectedItem;
            int indexFromOld = viewModel.Recipes.IndexOf(currentRecipe);
            viewModel.Recipes.Remove(currentRecipe);
            viewModel.Recipes.Insert(indexFromOld, new(
                (ObservableCollection<Ingredient>)Ingredients.ItemsSource,
                (OvenSettings)OvenSettings.SelectionBoxItem,
                RecipeTitle.Text,
                Duration.Text,
                Preparation.Text
                ));
        }
        Search();
    }

    private void Recipe_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        RecipeTabViewModel viewModel = (RecipeTabViewModel)DataContext;
        Recipe selectedRecipe = (Recipe)Recipes.SelectedItem;

        if (selectedRecipe is not null)
        {
            RecipeTitle.Text = selectedRecipe.RecipeName;
            OvenSettings.SelectedIndex = viewModel.OvenSettingsList.IndexOf(selectedRecipe.OvenSettings);
            Ingredients.ItemsSource = selectedRecipe.Ingredients;
            Duration.Text = selectedRecipe.Duration;
            Preparation.Text = selectedRecipe.Preparation;
        }
        else
        {
            ResetRecipeUI(viewModel);
        }
    }

    private void AddNewIngredient(object sender, RoutedEventArgs e)
    {
        var stockList = container.Resolve<IAccessData<StockFolder>>();
        var ingredientViewModel = container.Resolve<NewIngredientViewModel>();
        while (true)
        {
            Ingredient? ingredientToAdd = NewItem.HandelNewItem<Ingredient>(ingredientViewModel);

            if (ingredientToAdd == null)
            {
                break;
            }

            ingredientToAdd.Status = stockList.Data.StockList.Any(x => x.Name == ingredientToAdd.Name)
                ? ColorCollection.IsAvaiable
                : ColorCollection.IsNotAvaiable;


            if (Recipes.SelectedItem is null)
            {
                ((ObservableCollection<Ingredient>) Ingredients.ItemsSource).Add(ingredientToAdd);
            }
            else
            {
                Recipe selectedRecipe = (Recipe) Recipes.SelectedItem;
                selectedRecipe.Ingredients.Add(ingredientToAdd);
            }
        }
    }

    private void DeleteNewIngredient(object sender, RoutedEventArgs e)
    {
        ObservableCollection<Ingredient> viewmodelIngredients;
        Ingredient todelete = (Ingredient)Ingredients.SelectedItem;
        if (Recipes.SelectedItem is null)
        {
            viewmodelIngredients = (ObservableCollection<Ingredient>)Ingredients.ItemsSource;
        }
        else
        {
            Recipe selectedRecipe = (Recipe)Recipes.SelectedItem;
            viewmodelIngredients = selectedRecipe.Ingredients;
        }

        viewmodelIngredients.Remove(todelete);
    }

    private void AddNewRecipe(object sender, RoutedEventArgs e)
    {
        RecipeTabViewModel viewModel = (RecipeTabViewModel)DataContext;
        List<string> errorList = ValidateRecipe();
        if (!errorList.IsEmpty())
        {
            MessageBox.Show(string.Join("\n", errorList), "Input Errors", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        Recipe newRecipe = new(
            (ObservableCollection<Ingredient>)Ingredients.ItemsSource,
            (OvenSettings)OvenSettings.SelectionBoxItem,
            RecipeTitle.Text.Trim(),
            Duration.Text.Trim(),
            Preparation.Text.Trim()
        );

        newRecipe.Status = newRecipe.Ingredients.Any(x => x.Status == ColorCollection.IsNotAvaiable)
            ? ColorCollection.IsNotAvaiable
            : ColorCollection.IsAvaiable;

        viewModel.allRecipes.Add(newRecipe);
        Search();
    }

    private void CopyNewRecipe(object sender, RoutedEventArgs e)
    {
        RecipeTabViewModel viewModel = (RecipeTabViewModel)DataContext;
        if (Recipes.SelectedItem is not null)
        {
            Recipe toCopyRecipe = (Recipe)Recipes.SelectedItem;
            viewModel.allRecipes.Add(toCopyRecipe.CreateCopy());
        }
        Search();
    }

    private void DeleteRecipe(object sender, RoutedEventArgs e)
    {
        RecipeTabViewModel viewModel = (RecipeTabViewModel)DataContext;
        Recipe toDeleteRecipe = (Recipe)Recipes.SelectedItem;

        if (MessageBox.Show($"Willst du das Rezept {toDeleteRecipe.RecipeName} wirklich löschen?", "?", MessageBoxButton.YesNo, MessageBoxImage.Warning) is MessageBoxResult.Yes)
        {
            viewModel.allRecipes.Remove(toDeleteRecipe);
            Search();
        }
    }

    private void KeyUpEventHandlerRecipe(object sender, KeyEventArgs e)
    {
        Search();
    }

    private void Search()
    {
        RecipeTabViewModel viewModel = (RecipeTabViewModel)DataContext;
        string searchValue = SearchValueRecipe.Text;

        viewModel.Recipes.Clear();

        if (string.IsNullOrEmpty(searchValue))
        {
            viewModel.Recipes.AddCollectionToThis(viewModel.allRecipes);
            return;
        }

        viewModel.Recipes.AddCollectionToThis(viewModel.allRecipes.Where(x => x.RecipeName.Contains(searchValue)));
    }

    private void ClearUI(object sender, RoutedEventArgs e)
    {
        RecipeTabViewModel viewModel = (RecipeTabViewModel)DataContext;
        ResetRecipeUI(viewModel);
    }

    private void ResetRecipeUI(RecipeTabViewModel viewModel)
    {
        Recipes.SelectedIndex = -1;
        RecipeTitle.Text = "";
        OvenSettings.SelectedIndex = viewModel.OvenSettingsList.IndexOf(viewModel.OvenSettingsList.First(x => x.Name == "Unbenutzt"));
        Ingredients.ItemsSource = new ObservableCollection<Ingredient>();
        Duration.Text = "";
        Preparation.Text = "";
    }

    private List<string> ValidateRecipe()
    {
        List<string> errorList = new();
        if (OvenSettings.SelectionBoxItem is null || OvenSettings.SelectionBoxItem as string == string.Empty)
        {
            errorList.Add("Du musst noch eine Ofeneinstellung auswählen. Falls du keinen Ofen brauchst dann wähle einfach unbenutzt.");
        }

        if (string.IsNullOrEmpty(RecipeTitle.Text.Trim()))
        {
            errorList.Add("Wie heisst dein Gericht?");
        }

        if (string.IsNullOrEmpty(Duration.Text.Trim()))
        {
            errorList.Add("Wie lange brauchst du für das Gericht?");
        }

        if (string.IsNullOrEmpty(Preparation.Text.Trim()))
        {
            errorList.Add("Wie machst du den dein Gericht?");
        }

        return errorList;
    }
}