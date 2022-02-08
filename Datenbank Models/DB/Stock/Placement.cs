using System.Collections.Generic;
using Database_Models.Interfaces;

namespace Database_Models.DB.Stock;

internal record Placement(string Name) : IDataBaseModel, IHaveName
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