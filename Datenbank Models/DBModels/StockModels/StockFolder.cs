using System;
using System.Collections.ObjectModel;
using System.IO;
using Database_Models.Converters;
using UtitlityFunctions.InterfaceExtention;

namespace Database_Models.DBModels.StockModels;
internal class StockFolder : IDisposable
{
    private DirectoryInfo saveDirectoryInfo;
    private ObservableCollection<Foodstuff> stockList = new();
    private ObservableCollection<Foodstuff> shoppingList = new();
    private ObservableCollection<Placement> placements = new();

    public ObservableCollection<Foodstuff> StockList
    {
        get => stockList;
        set => shoppingList = value;
    }

    public ObservableCollection<Foodstuff> ShoppingList
    {
        get => shoppingList;
        set => shoppingList = value;
    }

    public ObservableCollection<Placement> Placements
    {
        get => placements;
        set => placements = value;
    }

    public StockFolder(string rootFolderPath)
    {
        saveDirectoryInfo = new(Path.Combine(rootFolderPath, "Stock"));
        if (!saveDirectoryInfo.Exists)
        {
            Directory.CreateDirectory(saveDirectoryInfo.FullName);
        }

        JsonConverter.Deserialize(ref stockList, nameof(stockList), saveDirectoryInfo.FullName);
        JsonConverter.Deserialize(ref shoppingList, nameof(shoppingList), saveDirectoryInfo.FullName);
        JsonConverter.Deserialize(ref placements, nameof(placements), saveDirectoryInfo.FullName);

        if (Placements.IsEmpty())
        {
            foreach (DefaultPlacement placement in Enum.GetValues<DefaultPlacement>())
            {
                Placements.Add(new(placement.ToString()));
            }
        }
    }

    public void Dispose()
    {
        JsonConverter.Serialize(stockList, nameof(stockList), saveDirectoryInfo.FullName);
        JsonConverter.Serialize(shoppingList, nameof(shoppingList), saveDirectoryInfo.FullName);
        JsonConverter.Serialize(placements, nameof(placements), saveDirectoryInfo.FullName);
    }
}

internal enum DefaultPlacement
{
    Keller = 0,
    Tiefkühler = 1,
    Reduit = 2,
    Kühlschrank = 3,
    Küche = 4
}