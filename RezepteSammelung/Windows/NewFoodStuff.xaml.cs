using RezepteSammelung.ViewModel.Windows;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Database_Models.DBModels.StockModels;
using UtitlityFunctions.InterfaceExtention;

namespace RezepteSammelung.Windows
{
    /// <summary>
    /// Interaktionslogik für NewFoodStuff.xaml
    /// </summary>
    public partial class NewFoodStuff : Window
    {
        private static Foodstuff toReturnFoodstuff  = Foodstuff.EmptyFoodstuff;
        private NewFoodStuff(NewFoodStuffViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            Placement.Items.Clear();
            toReturnFoodstuff = Foodstuff.EmptyFoodstuff;
        }

        internal static Foodstuff HandelNewFoodstuff(NewFoodStuffViewModel viewModel)
        {
            NewFoodStuff newFoodStuff = new(viewModel);
            newFoodStuff.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _ = newFoodStuff.ShowDialog();
            return toReturnFoodstuff;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            Foodstuff newFoodstuff = new(
                Foodname.Text.Trim(),
                DateTime.Now,
                (Placement) Placement.SelectionBoxItem);

            List<string> errorMessages = newFoodstuff.Validate();
            if (!errorMessages.IsEmpty())
            {
                MessageBox.Show(string.Join("\n", errorMessages), "Input Errors", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            toReturnFoodstuff = newFoodstuff;
            Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            toReturnFoodstuff = Foodstuff.EmptyFoodstuff;
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
