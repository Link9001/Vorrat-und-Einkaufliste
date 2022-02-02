using Database_Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UtitlityFunctions.Atributte;

namespace Database_Models.DBModels.StockModels;
internal class Foodstuff : IDataBaseModel
{
    public static readonly Foodstuff EmptyFoodstuff = new(string.Empty, string.Empty, new(string.Empty));

    [IgnoreForCreationOfObject(true)]
    public string Date { get; set; }
    
    public string Name { get; set; }
    public Placement Placement { get; set; }

    [JsonConstructor]
    public Foodstuff(string name, string dateTime, Placement placement)
    {
        Date = dateTime;
        Name = name;
        Placement = placement;
    }

    public Foodstuff(string name, Placement placement)
    {
        var dateTime = DateTime.Now;
        Date = $"{dateTime.Day}.{dateTime.Month}.{dateTime.Year}";
        Name = name;
        Placement = placement;
    }

    public List<string> Validate()
    {
        List<string> errorMessages = new();
        if (string.IsNullOrEmpty(Name))
        {
            errorMessages.Add("Was willst du hinzufügen? Nichts. Das geht nicht.");
        }

        if (string.IsNullOrEmpty(Placement.Name))
        {
            errorMessages.Add("Wo willst du es lagern? Keine Sorge, ich lade das auf keine Cloud wo du das lagerns willst. Habe keine Cookies.");
        }
        return errorMessages;
    }
}