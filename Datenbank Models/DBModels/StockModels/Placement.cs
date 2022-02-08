using Database_Models.Interfaces;
using System.Collections.Generic;

namespace Database_Models.DBModels.StockModels;

internal record Placement(string Name) : IDataBaseModel, IName
{
    public static readonly Placement EmptyPlacement = new(string.Empty);
    public string Name { get; set; } = Name;

    public List<string> Validate()
    {
        var errorList = new List<string>();
        if (string.IsNullOrWhiteSpace(Name))
        {
            errorList.Add("Wo willst du es hinlegen?");
        }

        return errorList;
    }

    public string GetName()
    {
        return "Lagerorte";
    }
}