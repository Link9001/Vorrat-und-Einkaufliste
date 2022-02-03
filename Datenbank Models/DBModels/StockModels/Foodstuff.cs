using Database_Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using UtitlityFunctions.Atributte;

namespace Database_Models.DBModels.StockModels;
internal class Foodstuff : ListViewItem, IDataBaseModel
{
    public static readonly Foodstuff EmptyFoodstuff = new(new Placement(string.Empty), string.Empty, string.Empty, 0, string.Empty);

    private double _quantity;

    [IgnoreForCreationOfObject(true)]
    public string Date { get; set; }
    
    public string Name { get; set; }
    public Placement Placement { get; set; }

    public double Quantity
    {
        get => _quantity;
        set
        {
            _quantity = value;
            OnPropertyChanged(nameof(_quantity));
        }
    }

    public string Quantitiespesification { get; set; }

    [JsonConstructor]
    public Foodstuff(Placement placement, string name = "", string dateTime = "", double quantity = 0, string quantitiespesification = "")
    {
        Date = dateTime;
        Name = name;
        Placement = placement;
        Quantity = quantity;
        Quantitiespesification = quantitiespesification;

        Status = new SolidColorBrush(Colors.Black);
    }

    public Foodstuff(Placement placement, string name, double quantity, string quantitiespesification)
    {
        var dateTime = DateTime.Now;
        Date = $"{dateTime.Day}.{dateTime.Month}.{dateTime.Year}";
        Name = name;
        Placement = placement;
        Quantity = quantity;
        Quantitiespesification = quantitiespesification;

        Status = new SolidColorBrush(Colors.Black);
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