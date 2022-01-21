using System;
using Database_Models;
using DatabaseAccess.Interface;
using Database_Models.DBModels.RecipeModels;

namespace DatabaseAccess.AccessData;

internal class AccessRecipeFolderData : IAccessData<RecipeFolder>
{
    private readonly RecipeFolder _recipeFolder;

    public AccessRecipeFolderData(Database db)
    {
        this._recipeFolder = db.RecipeFolder;
    }

    public RecipeFolder Data
    {
        get => this.Filter != null ? Filter.Invoke(this._recipeFolder) : this._recipeFolder;
    }
    public Func<RecipeFolder, RecipeFolder>? Filter { get; set; }
}