using System;
using System.Collections.ObjectModel;

using DataModel.Recipes;

namespace DataModel
{
    public class MainDataModel : IDisposable
    {
        public MainDataModel()
        {
            StockList.LoadList(nameof(StockList));
            ShoppingList.LoadList(nameof(ShoppingList));
            Recipes.LoadList(nameof(Recipes));
        }

        /// <summary>
        /// List with every StockItem
        /// ObservableCollection for notify the Ui when the number of Entrys changes
        /// </summary>
        public ObservableCollection<Foodstuff.Foodstuff> StockList { get; set; } = new();


        /// <summary>
        /// List with every ShoppinglistItem
        /// ObservableCollection for notify the Ui when the number of Entrys changes
        /// </summary>
        public ObservableCollection<Foodstuff.Foodstuff> ShoppingList { get; set; } = new();

        public ObservableCollection<Recipe> Recipes { get; set; } = new();

        /// <inheritdoc/>
        public void Dispose()
        {
            StockList.Dispose(nameof(StockList));
            ShoppingList.Dispose(nameof(ShoppingList));
            Recipes.Dispose(nameof(Recipes));
            GC.SuppressFinalize(this);
        }
    }
}