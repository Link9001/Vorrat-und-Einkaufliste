using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Database_Models.DBModels.StockModels;
using RezepteSammelung.ViewModel.TabViewModel;
using RezepteSammelung.ViewModel.Windows;
using Unity;
using UtitlityFunctions.InterfaceExtention;

namespace RezepteSammelung.Windows.StockWindow
{
    public partial class StockTab : UserControl
    {
        private IUnityContainer container;
        private ListView? lastFocusedListView;
        internal StockTab(IUnityContainer container, StockTabViewModel viewModel)
        {
            this.container = container;
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

            viewModel.ShoppingList.AddCollectionToThis(viewModel.shoppingList.Where(x => x.Name.Contains(searchValue)));
            viewModel.StockList.AddCollectionToThis(viewModel.stockList.Where(x => x.Name.Contains(searchValue)));
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
            var viewModel = (StockTabViewModel) DataContext;
            var foodStuffViewModel = container.Resolve<NewFoodStuffViewModel>();
            Foodstuff newFoodstuff = Windows.NewFoodStuff.HandelNewFoodstuff(foodStuffViewModel);
            if (newFoodstuff == Foodstuff.EmptyFoodstuff)
            {
                return;
            }
            viewModel.shoppingList.Add(newFoodstuff);
            Search();
        }

        private void DeleteFoodStuffElement(object sender, RoutedEventArgs e)
        {
            StockTabViewModel viewModel = (StockTabViewModel) DataContext;
            if (lastFocusedListView is not null && lastFocusedListView.Name == "StockList")
            {
                Foodstuff toDeleteFoodstuff = (Foodstuff)StockList.SelectedItem;
                viewModel.stockList.Remove(toDeleteFoodstuff);
            }
            else
            {
                Foodstuff toDeleteFoodstuff = (Foodstuff) ShoppingList.SelectedItem;
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
            StockTabViewModel viewModel = (StockTabViewModel) DataContext;
            viewModel.stockList.AddCollectionToThis(viewModel.shoppingList);
            viewModel.shoppingList.Clear();
            Search();
        }


        private void ListViewLostFocus(object sender, RoutedEventArgs e)
        {
            lastFocusedListView = (ListView) sender;
        }
    }
}
