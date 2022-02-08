using Database_Models.Converters;
using HouseholdmanagementTool.UtitlityFunctions.InterfaceExtention;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace Database_Models.DBModels.StockModels;
internal class StockFolder : IDisposable
{
    private readonly DirectoryInfo _saveDirectoryInfo;
    private readonly ObservableCollection<Foodstuff> _stockList = new();
    private ObservableCollection<Foodstuff> _shoppingList = new();
    private ObservableCollection<Placement> _placements = new();

    public ObservableCollection<Foodstuff> StockList
    {
        get => _stockList;
        set => _shoppingList = value;
    }

    public ObservableCollection<Foodstuff> ShoppingList
    {
        get => _shoppingList;
        set => _shoppingList = value;
    }

    public ObservableCollection<Placement> Placements
    {
        get => _placements;
        set => _placements = value;
    }

    public StockFolder(string rootFolderPath)
    {
        _saveDirectoryInfo = new(Path.Combine(rootFolderPath, "Stock"));
        if (!_saveDirectoryInfo.Exists)
        {
            Directory.CreateDirectory(_saveDirectoryInfo.FullName);
        }

        JsonConverter.Deserialize(ref _stockList, nameof(_stockList), _saveDirectoryInfo.FullName);
        JsonConverter.Deserialize(ref _shoppingList, nameof(_shoppingList), _saveDirectoryInfo.FullName);
        JsonConverter.Deserialize(ref _placements, nameof(_placements), _saveDirectoryInfo.FullName);

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
        JsonConverter.Serialize(_stockList, nameof(_stockList), _saveDirectoryInfo.FullName);
        JsonConverter.Serialize(_shoppingList, nameof(_shoppingList), _saveDirectoryInfo.FullName);
        JsonConverter.Serialize(_placements, nameof(_placements), _saveDirectoryInfo.FullName);
    }
}

// ReSharper disable once UnusedMember.Global
internal enum DefaultPlacement
{
    Keller = 0,
    // ReSharper disable once UnusedMember.Global
    Tiefkühler = 1,
    // ReSharper disable once UnusedMember.Global
    Reduit = 2,
    // ReSharper disable once UnusedMember.Global
    Kühlschrank = 3,
    // ReSharper disable once UnusedMember.Global
    Küche = 4
}