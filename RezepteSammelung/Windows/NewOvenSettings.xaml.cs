using System.Windows;

namespace RezepteSammelung.Windows
{
    public partial class NewOvenSettings : Window
    {
        //        private NewOvenSettings(Viewmodel viewModel)
        //        {
        //            InitializeComponent();
        //            DataContext = viewModel;
        //        }

        //        internal static ObservableCollection<OvenSettings> HandelNewOvensettings(ListToModifyViewModel<> viewModel)
        //        {
        //            ObservableCollection<OvenSettings> oldOvenSettings = viewModel.OvenSettingsList;
        //            NewOvenSettings addSubjects = new(viewModel);
        //            addSubjects.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        //            if ((bool)addSubjects.ShowDialog()!)
        //            {
        //                return viewModel.OvenSettingsList;
        //            }
        //            return oldOvenSettings;
        //        }

        //        private void Add(object sender, RoutedEventArgs e)
        //        {
        //            ListToModifyViewModel<> viewModel = (ListToModifyViewModel<>)DataContext;
        //            if (!viewModel.OvenSettingsList.Any(x => x.Name == OvenSettingsTextBox.Text))
        //            {
        //                string sub = OvenSettingsTextBox.Text.Trim();
        //                if (sub[0] >= 'a' && sub[0] <= 'z')
        //                {
        //                    string toUpper = sub[0].ToString();
        //                    sub = sub.Substring(1, sub.Length - 1);
        //                    sub = sub.Insert(0, toUpper.ToUpper());
        //                }
        //                viewModel.OvenSettingsList.Add(new(sub));
        //            }
        //            else
        //            {
        //                MessageBox.Show("Das hast du schon mal Eingetragen", "", MessageBoxButton.OK);
        //            }
        //            OvenSettingsTextBox.Text = "";
        //        }
        //        private void EnterAdd(object sender, KeyEventArgs e)
        //        {
        //            if (e.Key == Key.Enter && OvenSettingsTextBox.Text != "")
        //            {
        //                Add(sender, e);
        //            }
        //        }
        //        private void Delete(object sender, RoutedEventArgs e)
        //        {
        //            ListToModifyViewModel<> viewModel = (ListToModifyViewModel<>)DataContext;
        //            OvenSettings subject = (OvenSettings)subjects.SelectedItem;
        //            viewModel.OvenSettingsList.Remove(subject);
        //        }

        //        private void Save(object sender, RoutedEventArgs e)
        //        {
        //            MessageBoxResult messageBoxResult = MessageBox.Show("Wenn du Fächer gelöscht hast bei denen du schon Test eingetragen hast werden diese nicht mehr angezeit.\nWillst du fortfahren?", "", MessageBoxButton.YesNo);
        //            if (messageBoxResult is MessageBoxResult.Yes)
        //            {
        //                DialogResult = true;
        //            }
        //        }
        //        private void Cancel(object sender, RoutedEventArgs e)
        //        {
        //            DialogResult = false;
        //        }
    }
}