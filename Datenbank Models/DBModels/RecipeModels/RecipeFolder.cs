using System;
using System.Collections.ObjectModel;
using System.IO;
using Database_Models.Converters;
using UtitlityFunctions.InterfaceExtention;

namespace Database_Models.DBModels.RecipeModels;
internal class RecipeFolder : IDisposable
{
    private DirectoryInfo saveDirectoryInfo;

    private ObservableCollection<Recipe> recipes = new();
    private ObservableCollection<OvenSettings> ovenSettings = new();

    public ObservableCollection<Recipe> Recipes
    {
        get => recipes;
        set => recipes = value;
    }

    public ObservableCollection<OvenSettings> OvenSettings
    {
        get => ovenSettings;
        set => ovenSettings = value;
    }

    internal RecipeFolder(string rootFolderPath)
    {
        saveDirectoryInfo = new(Path.Combine(rootFolderPath, "Recipes"));

        if (!saveDirectoryInfo.Exists)
        {
            Directory.CreateDirectory(saveDirectoryInfo.FullName);
        }

        JsonConverter.Deserialize(ref recipes, nameof(recipes), saveDirectoryInfo.FullName);
        JsonConverter.Deserialize(ref ovenSettings, nameof(ovenSettings), saveDirectoryInfo.FullName);

        if (ovenSettings.IsEmpty())
        {
            foreach (DefaultOvenSettings ovenSettings in Enum.GetValues<DefaultOvenSettings>())
            {
                this.ovenSettings.Add(new(ovenSettings.ToString()));
            }
        }
    }

    public void Dispose()
    {
        JsonConverter.Serialize(recipes, nameof(recipes), saveDirectoryInfo.FullName);
        JsonConverter.Serialize(ovenSettings, nameof(ovenSettings), saveDirectoryInfo.FullName);
    }
}

internal enum DefaultOvenSettings
{
    Unbenutzt = 0,
    Heissluft_4D = 1,
    Ober_Unterhitze = 2,
    Heissluft_eco = 3,
    Ober_Unterhitze_eco = 4,
    Umluftgrillen = 5,
    Grill_gross = 6,
    Grill_klein = 7,
    Pizzastufe = 8,
    Sanftgaren = 9,
    Unterhitze = 10,
    Warmhalten = 11,
    Geschirr_vorwärmen = 12,
    coolStartFunktion = 13,
}