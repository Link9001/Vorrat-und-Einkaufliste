using Database_Models.Interfaces;
using System.Collections.Generic;

namespace Database_Models.DBModels.RecipeModels;

internal class Ingredient : ListViewItem, IDataBaseModel
{
    public static readonly Ingredient EmptyIngredient = new("", "");
    public string Name { get; set; }
    public string Quantity { get; set; }

    public Ingredient(string name, string quantity)
    {
        Name = name;
        Quantity = quantity;
    }

    public List<string> Validate()
    {
        List<string> errorList = new();
        if (string.IsNullOrEmpty(Name))
        {
            errorList.Add("Was willst du zum kochen benutzten? Nichts. Was kann man den damit machen.");
        }
        if (string.IsNullOrEmpty(Quantity))
        {
            errorList.Add($"Wie viel braucht man von {Quantity} für dieses Gericht?");
        }

        return errorList;
    }
}