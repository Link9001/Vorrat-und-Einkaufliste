using Database_Models;
using Database_Models.DBModels.RecipeModels;
using DatabaseAccess.Interface;
using System;

namespace DatabaseAccess.AccessData;

internal class AccessRecipeFolderData : IAccessData<RecipeFolder>
{
    private readonly RecipeFolder _recipeFolder;

    public AccessRecipeFolderData(Database db)
    {
        _recipeFolder = db.RecipeFolder;
    }

    public RecipeFolder Data => Filter != null ? Filter.Invoke(_recipeFolder) : _recipeFolder;
    public Func<RecipeFolder, RecipeFolder>? Filter { get; set; }
}