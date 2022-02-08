using Database_Models.Interfaces;
using HouseholdmanagementTool.Attributes.UserObjectCreation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Database_Models.DBModels.StockModels;
internal record Foodstuff : ListViewItem, IDataBaseModel
{
    public static readonly Foodstuff EmptyFoodstuff = new(new Placement(string.Empty), string.Empty, string.Empty, 0, string.Empty);

    private string _date = string.Empty;
    private string _name;
    private Placement _placement;
    private double _quantity;
    private string _quantitiespesification;

    [IgnoreForUserCreation(true)]
    public string Date
    {
        get => _date;
        set
        {
            _date = value;
            OnPropertyChanged(nameof(Date));
        }
    }

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    public Placement Placement
    {
        get => _placement;
        set
        {
            _placement = value;
            OnPropertyChanged(nameof(Placement));
        }
    }
    public double Quantity
    {
        get => _quantity;
        set
        {
            _quantity = value;
            OnPropertyChanged(nameof(Quantity));
        }
    }

    public string Quantitiespesification
    {
        get => _quantitiespesification;
        set
        {
            _quantitiespesification = value;
            OnPropertyChanged(nameof(Quantitiespesification));
        }
    }

    [JsonConstructor]
    public Foodstuff(Placement placement, string name = "", string dateTime = "", double quantity = 0, string quantitiespesification = "")
    {
        _date = dateTime;
        _name = name;
        _placement = placement;
        _quantity = quantity;
        _quantitiespesification = quantitiespesification;

        Status = new SolidColorBrush(Colors.Black);
    }

    public Foodstuff(string name, Placement placement, double quantity, string quantitiespesification)
    {
        _name = name;
        _placement = placement;
        _quantity = quantity;
        _quantitiespesification = quantitiespesification;

        Status = new SolidColorBrush(Colors.Black);
        SetDate();
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
            errorMessages.Add("Wo willst du es lagern? Keine Sorge, ich lade das auf keine Cloud wo du das lagerns willst. Habe keine Cookies. :-)");
        }

        if (Quantity <= 0)
        {
            errorMessages.Add($"Wie viel hast du  vor zu kaufen von {Name}? Keine.. dann musst du es auch nicht eintragen.");
        }

        if (string.IsNullOrWhiteSpace(Quantitiespesification))
        {
            errorMessages.Add($"Wie viel hast du den von {Name}? {Quantity} Äpfel oder Birnen?");
        }

        return errorMessages;
    }

    public Foodstuff SetDate()
    {
        var dateTime = DateTime.Now;
        _date = $"{dateTime.Day}.{dateTime.Month}.{dateTime.Year}";
        return this;
    }
}