using Database_Models.DBModels.RecipeModels;
using System;
using System.IO;
using Database_Models.DBModels.StockModels;

namespace Database_Models;

internal class Database : IDisposable
{
    public readonly DirectoryInfo SaveDirectoryInfo =
        new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
            "Rezepte Sammelung", "DataRepository"));

    public StockFolder StockFolder { get; set; }
    public RecipeFolder RecipeFolder { get; set; }

    public Database()
    {
        if (!SaveDirectoryInfo.Exists)
        {
            Directory.CreateDirectory(SaveDirectoryInfo.FullName);
        }

        StockFolder = new(SaveDirectoryInfo.FullName);
        RecipeFolder = new(SaveDirectoryInfo.FullName);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        StockFolder.Dispose();
        RecipeFolder.Dispose();
        GC.SuppressFinalize(this);
    }
}