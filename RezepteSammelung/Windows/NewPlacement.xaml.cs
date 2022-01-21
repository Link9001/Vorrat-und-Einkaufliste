using RezepteSammelung.ViewModel.Windows;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Database_Models.DBModels.StockModels;

namespace RezepteSammelung.Windows
{
    /// <summary>
    /// Interaktionslogik für NewPlacement.xaml
    /// </summary>
    public partial class NewPlacement : Window
    {
        private NewPlacement(NewPlacementViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        internal static ObservableCollection<Placement> HandelNewPlacments(NewPlacementViewModel viewModel)
        {
            ObservableCollection<Placement> oldPlacement = viewModel.Placements;
            NewPlacement addSubjects = new(viewModel);
            addSubjects.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if ((bool)addSubjects.ShowDialog()!)
            {
                return viewModel.Placements;
            }
            return oldPlacement;
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            NewPlacementViewModel viewModel = (NewPlacementViewModel)DataContext;
            if (viewModel.Placements.All(x => x.Name != PlacmentTextBox.Text))
            {
                string sub = PlacmentTextBox.Text.Trim();
                if (sub[0] >= 'a' && sub[0] <= 'z')
                {
                    string toUpper = sub[0].ToString();
                    sub = sub.Substring(1, sub.Length - 1);
                    sub = sub.Insert(0, toUpper.ToUpper());
                }
                viewModel.Placements.Add(new(sub));
            }
            else
            {
                MessageBox.Show("Das hast du schon mal Eingetragen", "", MessageBoxButton.OK);
            }
            PlacmentTextBox.Text = "";
        }
        private void EnterAdd(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && PlacmentTextBox.Text != "")
            {
                Add(sender, e);
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            NewPlacementViewModel viewModel = (NewPlacementViewModel)DataContext;
            Placement subject = (Placement)subjects.SelectedItem;
            viewModel.Placements.Remove(subject);
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Wenn du Fächer gelöscht hast bei denen du schon Test eingetragen hast werden diese nicht mehr angezeit.\nWillst du fortfahren?", "", MessageBoxButton.YesNo);
            if (messageBoxResult is MessageBoxResult.Yes)
            {
                DialogResult = true;
            }
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
