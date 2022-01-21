using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Database_Models.DBModels.RecipeModels;
using RezepteSammelung.ViewModel.Windows;
using UtitlityFunctions.InterfaceExtention;

namespace RezepteSammelung.Windows
{
    public partial class NewIngredients : Window
    {
        private static Ingredient toReturnIngredient = Ingredient.EmptyIngredient;

        private NewIngredients(NewIngredientViewModel viewmodel)
        {
            InitializeComponent();
            DataContext = viewmodel;
            toReturnIngredient = Ingredient.EmptyIngredient;
        }

        internal static Ingredient HandelNewIngredient(NewIngredientViewModel viewModel)
        {
            NewIngredients newIngredients = new(viewModel);
            newIngredients.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _ = newIngredients.ShowDialog();
            return toReturnIngredient;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            NewIngredientViewModel viewModel = (NewIngredientViewModel) DataContext;
            Ingredient newIngredient = new(
                IngredientName.Text.Trim(),
                Quantity.Text.Trim()
                );
            List<string> errorMessages = newIngredient.Validate();
            if (!errorMessages.IsEmpty())
            {
                MessageBox.Show(string.Join("\n", errorMessages), "Input Errors", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            newIngredient.Status = viewModel.StockCollection.Any(x => x.Name == newIngredient.Name)
                ? ColorCollection.IsAvaiable
                : ColorCollection.IsNotAvaiable;

            toReturnIngredient = newIngredient;
            Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RemovePlaceholder_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox target = (TextBox)sender;
            target.Text = "";
            target.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void AddPlaceholder_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox target = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(target.Text))
            {
                target.Text = target.Tag.ToString();
                target.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }
    }
}
