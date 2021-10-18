using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using DataModel.Recipes;
using DataModel.Foodstuff;
using DataModel;

namespace Vorrat_und_Einkaufliste
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainDataModel mainDataModel = new();
        /// <summary>
        /// Constructor from the MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Placement.ItemsSource = Enum.GetValues(typeof(Placement));
            Oven_settings.ItemsSource = Enum.GetValues(typeof(Oven_Settings));
            Shopping_List.ItemsSource = mainDataModel.ShoppingList;
            Stock_List.ItemsSource = mainDataModel.StockList;
            Recipe.ItemsSource = mainDataModel.Recipes;
            Ingredients.ItemsSource = new ObservableCollection<Ingredient>();
        }

        /// <summary>
        /// When the Button "Übernahme" was pressed
        /// </summary>
        /// <param name="sender">The Object with has send the request to this Function / Method</param>
        /// <param name="e">Arguments that has been passed by the Object</param>
        private void MoveShoppingListToStock(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Willst du das wirklich machen?", "!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                mainDataModel.StockList.AddCollectionToThis(mainDataModel.ShoppingList);
            }
        }

        /// <summary>
        /// When the Button "Hinzufügen" was pressed
        /// </summary>
        /// <param name="sender">The Object with has send the request to this Function / Method</param>
        /// <param name="e">Arguments that has been passed by the Object</param>
        private void NewFoodStuff(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameOfFood.Text))
            {
                MessageBox.Show("Du hast keinen Namen eingegeben.");
                return;
            }
            if (Placement.SelectedItem == null)
            {
                MessageBox.Show("Du hast noch keinen Ort angegeben");
                return;
            }
            Foodstuff newItem = new(NameOfFood.Text, DateTime.MinValue, Placement.SelectedItem.ToString(), mainDataModel.ShoppingList);
            mainDataModel.ShoppingList.Add(newItem);
            NameOfFood.Text = "";
            string searchValue = NameOfFood.Text != "" ? NameOfFood.Text : SearchValueFoodStuff.Text;
            UpdateStockView(Stock_List, searchValue);
        }

        /// <summary>
        /// When the Button "Delete" was pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Foodstuff foodstuff = (Foodstuff)button.DataContext;
            ObservableCollection<Foodstuff> targetList = foodstuff.RelatedFoodList;
            targetList.Remove(foodstuff);
            string searchValue = NameOfFood.Text != "" ? NameOfFood.Text : SearchValueFoodStuff.Text;
            UpdateStockView(Stock_List, searchValue);
        }

        /// <summary>
        /// When the Close Button of the Window is pressed
        /// </summary>
        /// <param name="sender">The Object with has send the request to this Function / Method</param>
        /// <param name="e">Arguments that has been passed by the Object</param>
        private void OnWindowClose(object sender, CancelEventArgs e)
        {
            mainDataModel.Dispose();
        }

        /// <summary>
        /// When the Button "Gekauft" was pressed
        /// </summary>
        /// <param name="sender">The Object with has send the request to this Function / Method</param>
        /// <param name="e">Arguments that has been passed by the Object</param>
        private void Add(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Foodstuff foodstuff = (Foodstuff)button.DataContext;
            mainDataModel.StockList.Add(foodstuff);
            foodstuff.RelatedFoodList.Remove(foodstuff);
            foodstuff.RelatedFoodList = mainDataModel.StockList;
            string searchValue = NameOfFood.Text != "" ? NameOfFood.Text : SearchValueFoodStuff.Text;
            UpdateStockView(Stock_List, searchValue);
        }

        /// <summary>
        /// When any Key on the Keybord is pressed
        /// </summary>
        /// <param name="sender">The Object with has send the request to this Function / Method</param>
        /// <param name="e">Arguments that has been passed by the Object</param>
        private void KeyUpEventHandlerFoodStuff(object sender, KeyEventArgs e)
        {
            string searchValue = NameOfFood.Text != "" ? NameOfFood.Text : SearchValueFoodStuff.Text;
            UpdateStockView(Stock_List, searchValue);
        }

        private void KeyUpEventHandlerRecipe(object sender, KeyEventArgs e)
        {
            UpdateRecipe(Recipe, SearchValueRecipe.Text);
        }

        /// <summary>
        /// When the Button "Löschen" was pressed
        /// </summary>
        /// <param name="sender">The Object with has send the request to this Function / Method</param>
        /// <param name="e">Arguments that has been passed by the Object</param>
        private void DeleteShoppingList(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msResult = MessageBox.Show("Willst du wirklich die Einkaufliste löschen", "Löschen?", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

            if (msResult == MessageBoxResult.Yes)
            {
                mainDataModel.ShoppingList.Clear();
            }
        }

        private void UpdateRecipe(object sender, RoutedEventArgs e)
        {
            if (Recipe.SelectedItem is null)
            {
                MessageBox.Show("Du hast hein rezept ausgewählt");
                return;
            }

            MessageBoxResult msResult = MessageBox.Show("Willst du das Rezept wirklich ändern?", "!", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (msResult is MessageBoxResult.Yes)
            {
                Recipe currentRecipe = Recipe.SelectedItem as Recipe;
                mainDataModel.Recipes.Add(new(
                    Duration.Text,
                    Ingredients.ItemsSource as ObservableCollection<Ingredient>,
                    (Oven_Settings)Oven_settings.SelectedIndex,
                    Preparation.Text,
                    RecipeTitle.Text
                    ));
                mainDataModel.Recipes.Remove(currentRecipe);
            }
            UpdateRecipe(Recipe, SearchValueRecipe.Text);
        }

        private void AddNewRecipe(object sender, RoutedEventArgs e)
        {
            AddNewRecipe();
            RecipeTitle.Text = "";
            Oven_settings.SelectedIndex = (int)Oven_Settings.Unbenutzt;
            Ingredients.ItemsSource = new ObservableCollection<Ingredient>();
            Duration.Text = "";
            Preparation.Text = "";
            UpdateRecipe(Recipe, SearchValueRecipe.Text);
        }

        private void CopyNewRecipe(object sender, RoutedEventArgs e)
        {
            if (Recipe.SelectedItem != null)
            {
                mainDataModel.Recipes.Add(Recipe.SelectedItem as Recipe);
            }
            UpdateRecipe(Recipe, SearchValueRecipe.Text);
        }

        private void DeleteRecipe(object sender, RoutedEventArgs e)
        {
            if (Recipe.SelectedItem != null)
            {
                mainDataModel.Recipes.Remove(Recipe.SelectedItem as Recipe);
            }
            UpdateRecipe(Recipe, SearchValueRecipe.Text);
        }

        private void DeleteIngredient(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Ingredient> toUpdate;
            Button button = sender as Button;
            if (Recipe.SelectedItem != null)
            {
                toUpdate = (Recipe.SelectedItem as Recipe).Ingredients;
                (Recipe.SelectedItem as Recipe).Ingredients.Remove(button.DataContext as Ingredient);
            }
            else
            {
                toUpdate = Ingredients.ItemsSource as ObservableCollection<Ingredient>;
                toUpdate.Remove(button.DataContext as Ingredient);
            }
            UpdateIngredients(toUpdate);
        }

        private void AddNewIngredient(object sender, RoutedEventArgs e)
        {
            InputForNewIngredient newIngredient = new(Recipe.SelectedItem as Recipe, Ingredients.ItemsSource as ObservableCollection<Ingredient>);
            newIngredient.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            newIngredient.Show();
        }

        private void Recipe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Recipe selectedRecipe = Recipe.SelectedItem as Recipe;

            if (selectedRecipe is not null)
            {
                RecipeTitle.Text = selectedRecipe.RecipeName;
                Oven_settings.SelectedIndex = selectedRecipe.Oven_Settings;
                Ingredients.ItemsSource = selectedRecipe.Ingredients;
                Duration.Text = selectedRecipe.Duration;
                Preparation.Text = selectedRecipe.Preparation;
            }
            else
            {
                RecipeTitle.Text = "";
                Oven_settings.SelectedIndex = (int)Oven_Settings.Unbenutzt;
                Ingredients.ItemsSource = new ObservableCollection<Ingredient>();
                Duration.Text = "";
                Preparation.Text = "";
            }
            
        }

        private void UpdateStockView(ListView listView, string searchValue)
        {
            bool hasSuccessed = DateTime.TryParse(searchValue, out DateTime date);
            if (searchValue == "")
            {
                listView.ItemsSource = mainDataModel.StockList;
                return;
            }
            if (hasSuccessed)
            {
                string stringDate = $"{date.Day}.{date.Month}.{date.Year}";
                listView.ItemsSource = new ObservableCollection<Foodstuff>(mainDataModel.StockList.Where(x => x.Date.Contains(stringDate)).ToList());
            }
            else
            {
                listView.ItemsSource = new ObservableCollection<Foodstuff>(mainDataModel.StockList.Where(x => x.Name.Contains(searchValue)).ToList());
            }
            listView.Items.Refresh();
        }

        private void UpdateRecipe(ListView listView, string searchvalue)
        {
            bool tryparse = int.TryParse(searchvalue, out int _);
            if (searchvalue == "")
            {
                listView.ItemsSource = mainDataModel.Recipes;
            }
            else if (tryparse)
            {
                listView.ItemsSource = new ObservableCollection<Recipe>(mainDataModel.Recipes.Where(x => x.Duration.TryParse() <= searchvalue.TryParse()));
            }
            else
            {
                listView.ItemsSource = new ObservableCollection<Recipe>(mainDataModel.Recipes.Where(x => x.RecipeName.Contains(searchvalue)));
            }
            listView.Items.Refresh();
        }


        private void UpdateIngredients(ObservableCollection<Ingredient> ingredients)
        {
            if (Recipe.SelectedItem is not null)
            {
                Ingredients.ItemsSource = (Recipe.SelectedItem as Recipe).Ingredients;
            }
            else
            {
                Ingredients.ItemsSource = ingredients;
            }
        }

        private bool AddNewRecipe()
        {
            if (string.IsNullOrWhiteSpace(RecipeTitle.Text))
            {
                MessageBox.Show("Kein Titel?");
                return false;
            }
            if (Oven_settings.SelectedItem is null)
            {
                MessageBox.Show("Braucht es den Ofen?");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Duration.Text))
            {
                MessageBox.Show("Wie lange braucht du für das Gericht?");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Preparation.Text))
            {
                MessageBox.Show("Wie geht das?");
                return false;
            }

            mainDataModel.Recipes.Add(
                new(
                    RecipeTitle.Text,
                    Ingredients.ItemsSource as ObservableCollection<Ingredient> ?? new ObservableCollection<Ingredient>(),
                    (Oven_Settings)Enum.Parse(typeof(Oven_Settings), Oven_settings.SelectedItem.ToString()),
                    Duration.Text,
                    Preparation.Text
                    ));
            return true;
        }
    }
}