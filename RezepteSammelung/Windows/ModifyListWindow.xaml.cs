using Database_Models.Interfaces;
using RezepteSammelung.Interfaces;
using RezepteSammelung.ViewModel.Windows;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace RezepteSammelung.Windows;

/// <summary>
/// Interaktionslogik für ModifyListWindow.xaml
/// </summary>
public partial class ModifyListWindow : Window
{
    private static Type _currentType = null!;
    private ModifyListWindow(object viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    internal static void HandleModifyListFromUser<TOfListObejcts>(ListToModifyVieModel<TOfListObejcts> viewModel)
        where TOfListObejcts : class, IName
    {
        var window = new ModifyListWindow(viewModel);
        _currentType = typeof(TOfListObejcts);
        window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        if ((bool)window.ShowDialog()!)
        {
            viewModel.ConvertBack();
        }
    }

    private void Add(object sender, RoutedEventArgs e)
    {
        var listToModify = (IListToModifyViewModel)DataContext;
        var newItem = InputNewItem.Text.Trim();

        if (listToModify.ListToModify.All(x => x.Name != newItem))
        {
            string sub = InputNewItem.Text.Trim();
            if (sub[0] >= 'a' && sub[0] <= 'z')
            {
                string toUpper = sub[0].ToString();
                sub = sub[1..];
                sub = sub.Insert(0, toUpper.ToUpper());
            }

            var newIName = Activator.CreateInstance(_currentType, new object[] { sub });

            if (newIName == null)
            {
                throw new NullReferenceException($"Could not create object from: '{_currentType.Name}'");
            }

            ((IListToModifyViewModel)DataContext).ListToModify.Add((IName)newIName);
        }
        else
        {
            MessageBox.Show("Das hast du schon mal Eingetragen", "", MessageBoxButton.OK);
        }
        InputNewItem.Text = "";
    }
    private void EnterAdd(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter && InputNewItem.Text != "")
        {
            Add(sender, e);
        }
    }
    private void Delete(object sender, RoutedEventArgs e)
    {
        var dataConext = (IListToModifyViewModel)DataContext;
        var subject = (IName)List.SelectedItem;
        dataConext.ListToModify.Remove(subject);
    }

    private void Save(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }
    private void Cancel(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}