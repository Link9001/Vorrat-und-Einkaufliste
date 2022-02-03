using Database_Models.Converters;
using System;
using System.Collections.ObjectModel;
using System.IO;
using UtitlityFunctions.InterfaceExtention;

namespace Database_Models.DBModels.RecipeModels;
internal class RecipeFolder : IDisposable
{
    private readonly DirectoryInfo _saveDirectoryInfo;

    private ObservableCollection<Recipe> _recipes = new();
    private ObservableCollection<OvenSettings> _ovenSettings = new();

    public ObservableCollection<Recipe> Recipes
    {
        get => _recipes;
        set => _recipes = value;
    }

    public ObservableCollection<OvenSettings> OvenSettings
    {
        get => _ovenSettings;
        set => _ovenSettings = value;
    }

    internal RecipeFolder(string rootFolderPath)
    {
        _saveDirectoryInfo = new(Path.Combine(rootFolderPath, "Recipes"));

        if (!_saveDirectoryInfo.Exists)
        {
            Directory.CreateDirectory(_saveDirectoryInfo.FullName);
        }

        JsonConverter.Deserialize(ref _recipes, nameof(_recipes), _saveDirectoryInfo.FullName);
        JsonConverter.Deserialize(ref _ovenSettings, nameof(_ovenSettings), _saveDirectoryInfo.FullName);

        if (_ovenSettings.IsEmpty())
        {
            foreach (DefaultOvenSettings ovenSettings in Enum.GetValues<DefaultOvenSettings>())
            {
                this._ovenSettings.Add(new(ovenSettings.ToString()));
            }
        }
    }

    public void Dispose()
    {
        JsonConverter.Serialize(_recipes, nameof(_recipes), _saveDirectoryInfo.FullName);
        JsonConverter.Serialize(_ovenSettings, nameof(_ovenSettings), _saveDirectoryInfo.FullName);
    }
}

// ReSharper disable once UnusedMember.Global
internal enum DefaultOvenSettings
{
    Unbenutzt = 0,
    // ReSharper disable once UnusedMember.Global
    Heissluft_4D = 1,
    // ReSharper disable once UnusedMember.Global
    Ober_Unterhitze = 2,
    // ReSharper disable once UnusedMember.Global
    Heissluft_eco = 3,
    // ReSharper disable once UnusedMember.Global
    Ober_Unterhitze_eco = 4,
    // ReSharper disable once UnusedMember.Global
    Umluftgrillen = 5,
    // ReSharper disable once UnusedMember.Global
    Grill_gross = 6,
    // ReSharper disable once UnusedMember.Global
    Grill_klein = 7,
    // ReSharper disable once UnusedMember.Global
    Pizzastufe = 8,
    // ReSharper disable once UnusedMember.Global
    Sanftgaren = 9,
    // ReSharper disable once UnusedMember.Global
    Unterhitze = 10,// ReSharper disable once UnusedMember.Global
    // ReSharper disable once UnusedMember.Global
    Warmhalten = 11,
    // ReSharper disable once UnusedMember.Global
    Geschirr_vorwärmen = 12,
    // ReSharper disable once UnusedMember.Global
    coolStartFunktion = 13,
}