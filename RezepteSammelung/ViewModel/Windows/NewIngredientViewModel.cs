﻿using System.Collections.ObjectModel;
using Database_Models.DBModels.StockModels;
using DatabaseAccess.Interface;

namespace RezepteSammelung.ViewModel.Windows;

internal class NewIngredientViewModel
{
    public readonly ObservableCollection<Foodstuff> StockCollection;
    public string? Name { get; set; }
    public string? Quantity { get; set; }
    public NewIngredientViewModel(IAccessData<StockFolder> stockFolder)
    {
        StockCollection = stockFolder.Data.StockList;
    }
}