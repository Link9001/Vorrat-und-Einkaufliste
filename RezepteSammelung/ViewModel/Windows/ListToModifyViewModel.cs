using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Database_Models.DBModels.RecipeModels;
using Database_Models.DBModels.StockModels;
using Database_Models.Interfaces;
using DatabaseAccess.Interface;
using Humanizer;
using RezepteSammelung.Interfaces;

namespace RezepteSammelung.ViewModel.Windows;

internal abstract class ListToModifyVieModelBase<T> : IListToModifyViewModelBase
    where T : IName
{
    public ListToModifyVieModelBase(ObservableCollection<T> listToModify)
    {
        this.ListToModify = listToModify;
    }
    public ObservableCollection<T> ListToModify { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Capture { get; set; } = string.Empty;
}

internal class PlacementListViewModel : ListToModifyVieModelBase<Placement>
{
    public PlacementListViewModel(IAccessData<ObservableCollection<Placement>> placementData)
     : base(placementData.Data)
    {
        this.Capture = $"{ListToModify.ToArray()[0].GetName().Singularize()}: ";
        this.Title = $"Bitte trage deine {ListToModify.ToArray()[0].GetName()} ein.";
    }
}

internal class OvenSettingListViewModel : ListToModifyVieModelBase<OvenSettings>
{
    public OvenSettingListViewModel(IAccessData<ObservableCollection<OvenSettings>> placementData)
     : base(placementData.Data)
    {
        this.Capture = $"{ListToModify.ToArray()[0].GetName().Singularize()}: ";
        this.Title = $"Bitte trage deine {ListToModify.ToArray()[0].GetName()} ein.";
    }
}