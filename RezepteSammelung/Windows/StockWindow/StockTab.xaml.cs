using Database_Models.DBModels.StockModels;
using RezepteSammelung.ViewModel.TabViewModel;
using RezepteSammelung.ViewModel.Windows;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Unity;
using UtitlityFunctions.InterfaceExtention;

namespace RezepteSammelung.Windows.StockWindow
{
    public partial class StockTab : UserControl
    {
        private readonly IUnityContainer _container;
        private ListView? _lastFocusedListView;
        internal StockTab(IUnityContainer container, StockTabViewModel viewModel)
        {
            this._container = container;
            InitializeComponent();
            DataContext = viewModel;
        }

        private void SearchBar(object sender, KeyEventArgs e)
        {
            Search();
        }

        private void Search()
        {
            StockTabViewModel viewModel = (StockTabViewModel)DataContext;
            string searchValue = SearchValueFoodStuff.Text.Trim();

            viewModel.ShoppingList.Clear();
            viewModel.StockList.Clear();

            if (string.IsNullOrEmpty(searchValue))
            {
                viewModel.ShoppingList.AddCollectionToThis(viewModel.shoppingList);
                viewModel.StockList.AddCollectionToThis(viewModel.stockList);
                return;
            }

            if (viewModel.Placements.Any(x => x.Name.Contains(searchValue)))
            {
                viewModel.ShoppingList.AddCollectionToThis(viewModel.shoppingList.Where(x => x.Placement.Name.Contains(searchValue)));
                viewModel.StockList.AddCollectionToThis(viewModel.stockList.Where(x => x.Placement.Name.Contains(searchValue)));
            }

            if (DateTime.TryParse(searchValue, out DateTime result))
            {
                viewModel.ShoppingList.AddCollectionToThis(viewModel.shoppingList.Where(x => DateTime.Parse(x.Date) >= result));
                viewModel.StockList.AddCollectionToThis(viewModel.stockList.Where(x => DateTime.Parse(x.Date) >= result));
            }

            viewModel.ShoppingList.AddCollectionToThis(viewModel.shoppingList.Where(x => x.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)));
            viewModel.StockList.AddCollectionToThis(viewModel.stockList.Where(x => x.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)));
        }

        private void Byed(object sender, RoutedEventArgs e)
        {
            StockTabViewModel viewModel = (StockTabViewModel)DataContext;
            Button btn = (Button)sender;
            Foodstuff selectedFoodstuff = (Foodstuff)btn.DataContext;
            viewModel.shoppingList.Remove(selectedFoodstuff);
            viewModel.stockList.Add(selectedFoodstuff);
            Search();
        }

        private void NewFoodStuff(object sender, RoutedEventArgs e)
        {
            var viewModel = (StockTabViewModel)DataContext;
            var foodStuffViewModel = _container.Resolve<NewFoodStuffViewModel>();
            while (true)
            {
                Foodstuff? newFoodstuffs = NewItem.HandelNewItem<Foodstuff>(foodStuffViewModel);
                if (newFoodstuffs == null)
                {
                    break;
                }

                viewModel.shoppingList.Add(newFoodstuffs);
                Search();
            }
        }

        private void DeleteFoodStuffElement(object sender, RoutedEventArgs e)
        {
            StockTabViewModel viewModel = (StockTabViewModel)DataContext;
            if (_lastFocusedListView is not null && _lastFocusedListView.Name == "StockList")
            {
                Foodstuff toDeleteFoodstuff = (Foodstuff)StockList.SelectedItem;
                viewModel.stockList.Remove(toDeleteFoodstuff);
            }
            else
            {
                Foodstuff toDeleteFoodstuff = (Foodstuff)ShoppingList.SelectedItem;
                viewModel.shoppingList.Remove(toDeleteFoodstuff);
            }
            Search();
        }

        private void MoveShoppingListToStock(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.OK != MessageBox.Show("Hast du wirklich alls eingekauft?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning))
            {
                return;
            }
            StockTabViewModel viewModel = (StockTabViewModel)DataContext;
            viewModel.stockList.AddCollectionToThis(viewModel.shoppingList);
            viewModel.shoppingList.Clear();
            Search();
        }


        private void ListViewLostFocus(object sender, RoutedEventArgs e)
        {
            _lastFocusedListView = (ListView)sender;
        }
    }
}
