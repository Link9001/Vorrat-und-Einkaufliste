using Database_Models.Interfaces;
using System.Collections.Generic;

namespace Database_Models.DBModels.RecipeModels;

public record OvenSettings(string Name) : IDataBaseModel, IName
{
    public static readonly OvenSettings EmptyOvenSettings = new(string.Empty);
    public string Name { get; set; } = Name;

    public List<string> Validate()
    {
        var errorList = new List<string>();
        if (string.IsNullOrWhiteSpace(Name))
        {
            errorList.Add("Wie heisst die Einstellung im Ofen?");
        }

        return errorList;
    }

    public string GetName()
    {
        return "Ofeneinstellungen";
    }
}