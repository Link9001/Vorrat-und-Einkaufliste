using System.Collections.Generic;
using Database_Models.Interfaces;

namespace Database_Models.DB.Recipe;

internal record Ingredient(string Name, string Quantity) : ListViewItem, IDataBaseModel
{
    public static readonly Ingredient EmptyIngredient = new(string.Empty, string.Empty);
    public string Name { get; set; } = Name;
    public string Quantity { get; set; } = Quantity;

    public List<string> Validate()
    {
        var errorList = new List<string>();
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