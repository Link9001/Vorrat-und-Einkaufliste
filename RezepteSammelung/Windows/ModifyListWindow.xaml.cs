using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Database_Models.Interfaces;
using HouseholdmanagementTool.UI.Interfaces;
using HouseholdmanagementTool.UI.ViewModel.Windows;

namespace HouseholdmanagementTool.UI.Windows;

/// <summary>
/// Interaktionslogik für ModifyListWindow.xaml
/// </summary>
public partial class ModifyListWindow : Window
{
    private static Type _currentType = null!;
    private ModifyListWindow(IListToModifyViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        Title = viewModel.ListToModify[0].GetName();
    }

    internal static void HandleModifyListFromUser<TOfListObejcts>(ListToModifyVieModel<TOfListObejcts> viewModel)
        where TOfListObejcts : class, IHaveName
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

            ((IListToModifyViewModel)DataContext).ListToModify.Add((IHaveName)newIName);
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
        var subject = (IHaveName)List.SelectedItem;
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