using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Windows.Input;

namespace Vorrat_und_Einkaufliste
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Filenames to save the Data
        /// </summary>
        private static readonly string[] nameOfFile = { "ShoppingList", "StockList" };

        /// <summary>
        /// List with every StockItem
        /// ObservableCollection for notify the Ui when the number of Entrys changes
        /// </summary>
        private ObservableCollection<Foodstuff> StockList { get; set; } = new();

        /// <summary>
        /// List with every ShoppinglistItem
        /// ObservableCollection for notify the Ui when the number of Entrys changes
        /// </summary>
        private static ObservableCollection<Foodstuff> ShoppingList { get; set; } = new();

        /// <summary>
        /// Constructor from the MainWindow
        /// </summary>
        public MainWindow()
        {
            string path = Path.Combine(Environment.CurrentDirectory, AppDomain.CurrentDomain.FriendlyName, nameOfFile[0] + ".json");
            if (File.Exists(path))
            {
                using StreamReader file = File.OpenText(path);
                IsoDateTimeConverter converter = new() { DateTimeFormat = "dd/MM/yyyy" };
                ShoppingList = JsonConvert.DeserializeObject<ObservableCollection<Foodstuff>>(file.ReadToEnd(), converter);
            }
            path = Path.Combine(Environment.CurrentDirectory, AppDomain.CurrentDomain.FriendlyName, nameOfFile[1] + ".json");
            if (File.Exists(path))
            {
                using StreamReader file = File.OpenText(path);
                StockList = JsonConvert.DeserializeObject<ObservableCollection<Foodstuff>>(file.ReadToEnd());
            }
            InitializeComponent();
            Ort.ItemsSource = Enum.GetValues(typeof(Placement));
            Shopping_List.ItemsSource = ShoppingList;
            Stock_List.ItemsSource = StockList;
        }

        /// <summary>
        /// When the Button "Übernahme" was pressed
        /// </summary>
        /// <param name="sender">The Object with has send the request to this Function / Method</param>
        /// <param name="e">Arguments that has been passed by the Object</param>
        private void MoveShoppingListToStock(object sender, RoutedEventArgs e)
        {
            foreach (Foodstuff foodstuff in ShoppingList)
            {
                foodstuff.RelatedFoodList = StockList;
                StockList.Add(foodstuff);
            }
            ShoppingList.Clear();
            Search(sender, e);
        }

        /// <summary>
        /// When the Button "Hinzufügen" was pressed
        /// </summary>
        /// <param name="sender">The Object with has send the request to this Function / Method</param>
        /// <param name="e">Arguments that has been passed by the Object</param>
        private void NewFoodStuff(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameOfFood.Text))
            {
                MessageBox.Show("Du hast keinen Namen eingegeben.");
                return;
            }
            if (Ort.SelectedItem == null)
            {
                MessageBox.Show("Du hast noch keinen Ort angegeben");
                return;
            }

            Foodstuff foodstuff = new(NameOfFood.Text, DateTime.Now, Ort.SelectedItem.ToString(), ShoppingList);
            ShoppingList.Add(foodstuff);
            NameOfFood.Text = "";
            Search(sender, e);
        }

        /// <summary>
        /// When the Button "Suche" was pressed
        /// </summary>
        /// <param name="sender">The Object with has send the request to this Function / Method</param>
        /// <param name="e">Arguments that has been passed by the Object</param>
        private void Search(object sender, RoutedEventArgs e)
        {
            string searchValue = NameOfFood.Text != "" ? NameOfFood.Text : SearchValue.Text;

            bool hasSuccessed = DateTime.TryParse(searchValue, out DateTime date);
            if (searchValue == "")
            {
                Stock_List.ItemsSource = StockList;
                return;
            }
            if (hasSuccessed)
            {
                string stringDate = $"{date.Day}.{date.Month}.{date.Year}";
                Stock_List.ItemsSource = new ObservableCollection<Foodstuff>(StockList.Where(x => x.Date.Contains(stringDate)).ToList());
            }
            else
            {
                Stock_List.ItemsSource = new ObservableCollection<Foodstuff>(StockList.Where(x => x.Name.Contains(searchValue)).ToList());
            }
            Stock_List.Items.Refresh();
        }

        /// <summary>
        /// When the Button "Delete" was pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Foodstuff foodstuff = (Foodstuff)button.DataContext;
            ObservableCollection<Foodstuff> targetList = foodstuff.RelatedFoodList;
            targetList.Remove(foodstuff);
            Search(sender, e);
        }

        /// <summary>
        /// When the Close Button of the Window is pressed
        /// </summary>
        /// <param name="sender">The Object with has send the request to this Function / Method</param>
        /// <param name="e">Arguments that has been passed by the Object</param>
        private void OnWindowClose(object sender, CancelEventArgs e)
        {
            WriteDataToFile(nameOfFile[0], ShoppingList);
            WriteDataToFile(nameOfFile[1], StockList);
        }

        /// <summary>
        /// When the Button "Gekauft" was pressed
        /// </summary>
        /// <param name="sender">The Object with has send the request to this Function / Method</param>
        /// <param name="e">Arguments that has been passed by the Object</param>
        private void Add(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Foodstuff foodstuff = (Foodstuff)button.DataContext;
            if (foodstuff.RelatedFoodList != StockList)
            {
                StockList.Add(foodstuff);
                foodstuff.RelatedFoodList.Remove(foodstuff);
                foodstuff.RelatedFoodList = StockList;
            }
            Search(sender, e);
        }

        /// <summary>
        /// When any Key on the Keybord is pressed
        /// </summary>
        /// <param name="sender">The Object with has send the request to this Function / Method</param>
        /// <param name="e">Arguments that has been passed by the Object</param>
        private void KeyUpEventHandler(object sender, KeyEventArgs e)
        {
            Search(sender, e);
        }

        /// <summary>
        /// This Method will write the content to a JSON-File
        /// </summary>
        /// <param name="nameOfNewFile">The name of the File</param>
        /// <param name="content">The Observablecollection that should be saved</param>
        private static void WriteDataToFile(string nameOfNewFile, ObservableCollection<Foodstuff> content)
        {
            DirectoryInfo directoryInfo = Directory.CreateDirectory(AppDomain.CurrentDomain.FriendlyName);
            string finalPath = Path.Combine(directoryInfo.FullName, nameOfNewFile + ".json");
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
            using FileStream file = File.Create(finalPath);
            {
                file.Write(System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(content, options));
            }
        }

        /// <summary>
        /// When the Button "Löschen" was pressed
        /// </summary>
        /// <param name="sender">The Object with has send the request to this Function / Method</param>
        /// <param name="e">Arguments that has been passed by the Object</param>
        private void DeleteShoppingList(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msResult = MessageBox.Show("Willst du wirklich die Einkaufliste löschen", "Löschen?", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

            if (msResult == MessageBoxResult.Yes)
            {
                ShoppingList.Clear();
            }
        }
    }
}