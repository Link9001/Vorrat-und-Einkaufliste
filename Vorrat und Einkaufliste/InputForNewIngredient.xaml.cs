 using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using DataModel.Recipes;

namespace Vorrat_und_Einkaufliste
{
    /// <summary>
    /// Interaktionslogik für InputForNewIngredient.xaml
    /// </summary>
    public partial class InputForNewIngredient : Window
    {
        private readonly Recipe selectedRecipe;

        private ObservableCollection<Ingredient> ingreedients;

        public InputForNewIngredient(Recipe selectedRecipe, ObservableCollection<Ingredient> ingredients)
        {
            this.selectedRecipe = selectedRecipe;
            this.ingreedients = ingredients;
            InitializeComponent();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            if (selectedRecipe is not null)
            {
                selectedRecipe.Ingredients.Add(new(ingredentName.Text, Quantity.Text));
            }
            else
            {
                ingreedients.Add(new(ingredentName.Text, Quantity.Text));
            }
            Close();
        }
    }
}
