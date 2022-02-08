using Database_Models.DBModels.RecipeModels;
using Database_Models.DBModels.StockModels;
using HouseholdmanagementTool.DatabaseAccess.Interface;
using System.Collections.ObjectModel;

namespace HouseholdmanagementTool.UI.ViewModel.TabViewModel;

internal class SettingsTabViewModel
{
    public ObservableCollection<OvenSettings> OvenSettingsList { get; set; }
    public ObservableCollection<Placement> Placements { get; set; }

    public SettingsTabViewModel(IAccessData<ObservableCollection<OvenSettings>> ovenSettings, IAccessData<ObservableCollection<Placement>> placements)
    {
        OvenSettingsList = ovenSettings.Data;
        Placements = placements.Data;
    }
}