using Database_Models;
using Database_Models.DBModels.StockModels;
using HouseholdmanagementTool.DatabaseAccess.Interface;
using System;
using System.Collections.ObjectModel;

namespace HouseholdmanagementTool.DatabaseAccess.AccessData;

internal class AccessPlacementData : IAccessData<ObservableCollection<Placement>>
{
    private readonly ObservableCollection<Placement> _placements;
    public AccessPlacementData(Database db)
    {
        _placements = db.StockFolder.Placements;
    }

    public ObservableCollection<Placement> Data => Filter != null ? Filter.Invoke(_placements) : _placements;
    public Func<ObservableCollection<Placement>, ObservableCollection<Placement>>? Filter { get; set; }
}