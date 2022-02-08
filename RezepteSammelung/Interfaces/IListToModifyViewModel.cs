using Database_Models.Interfaces;
using System.Collections.ObjectModel;

namespace HouseholdmanagementTool.UI.Interfaces;

internal interface IListToModifyViewModel
{
    ObservableCollection<IHaveName> ListToModify { get; set; }
}