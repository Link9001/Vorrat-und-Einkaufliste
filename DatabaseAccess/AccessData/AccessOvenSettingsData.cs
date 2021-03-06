using Database_Models;
using Database_Models.DBModels.RecipeModels;
using HouseholdmanagementTool.DatabaseAccess.Interface;
using System;
using System.Collections.ObjectModel;

namespace HouseholdmanagementTool.DatabaseAccess.AccessData;

internal class AccessOvenSettingsData : IAccessData<ObservableCollection<OvenSettings>>
{
    private readonly ObservableCollection<OvenSettings> _settings;
    public AccessOvenSettingsData(Database db)
    {
        _settings = db.RecipeFolder.OvenSettings;
    }

    public ObservableCollection<OvenSettings> Data => Filter != null ? Filter.Invoke(_settings) : _settings;
    public Func<ObservableCollection<OvenSettings>, ObservableCollection<OvenSettings>>? Filter { get; set; }
}