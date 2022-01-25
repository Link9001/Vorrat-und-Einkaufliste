using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Database_Models.Interfaces;
using RezepteSammelung.ViewModel.Windows;
using UtitlityFunctions.InterfaceExtention;

namespace RezepteSammelung.Windows
{
    /// <summary>
    /// Interaktionslogik für ModifyListWindow.xaml
    /// </summary>
    public partial class ModifyListWindow : Window
    {
        private static ObservableCollection<IName>? _toReturn;
        private static Type _currentType;
        private ModifyListWindow(object viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        internal static ObservableCollection<TOFListObejcts> HandleModifyListFromUser<TOFListObejcts>(ListToModifyVieModelBase<TOFListObejcts> viewModel) 
            where TOFListObejcts: IName
        {
            var window = new ModifyListWindow(viewModel);
            _toReturn = new(viewModel.ListToModify.Select(x => (IName)x));
            _currentType = typeof(TOFListObejcts);
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _ = window.ShowDialog();
            viewModel.ListToModify.Clear();
            viewModel.ListToModify.AddCollectionToThis(_toReturn.Select(x => (TOFListObejcts)x));
            return viewModel.ListToModify;
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            var listToModify = DataContext.GetType().GetProperty("ListToModify");
            
            if ((()listToModify.GetValue()).All(x => x.Name != InputNewItem.Text))
            {
                string sub = InputNewItem.Text.Trim();
                if (sub[0] >= 'a' && sub[0] <= 'z')
                {
                    string toUpper = sub[0].ToString();
                    sub = sub[1..];
                    sub = sub.Insert(0, toUpper.ToUpper());
                }

                var typOfIName = dataContext.ListToModify.ToArray()[0].GetType();
                var newIname = Activator.CreateInstance(typOfIName, new object[] { sub });

                if (newIname == null)
                {
                    throw new NullReferenceException($"Could not create object from: '{typOfIName.Name}'");
                }

                dataContext.ListToModify.Add((IName)newIname);
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
            var dataConext = (ListToModifyVieModelBase)DataContext;
            var subject = (IName)List.SelectedItem;
            dataConext.ListToModify.Remove(subject);
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            _toReturn = (ObservableCollection<IName>)List.ItemsSource;
            DialogResult = true;
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

    }
}
