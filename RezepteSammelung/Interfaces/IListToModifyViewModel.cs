using Database_Models.Interfaces;
using System.Collections.ObjectModel;

namespace RezepteSammelung.Interfaces;

internal interface IListToModifyViewModel
{
    ObservableCollection<IName> ListToModify { get; set; }
}