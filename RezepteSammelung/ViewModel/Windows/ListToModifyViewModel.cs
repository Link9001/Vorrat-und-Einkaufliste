using Database_Models.Interfaces;
using HouseholdmanagementTool.DatabaseAccess.Interface;
using HouseholdmanagementTool.UI.Interfaces;
using HouseholdmanagementTool.UtitlityFunctions.InterfaceExtention;
using Humanizer;
using System.Collections.ObjectModel;
using System.Linq;

namespace HouseholdmanagementTool.UI.ViewModel.Windows;

internal class ListToModifyVieModel<T> : IListToModifyViewModel
where T : class, IHaveName
{
    private readonly ObservableCollection<T> _items;
    public ListToModifyVieModel(IAccessData<ObservableCollection<T>> listToModifyData)
    {
        _items = listToModifyData.Data;
        ListToModify = new ObservableCollection<IHaveName>(listToModifyData.Data.Select(x => (IHaveName)x!)!);

        Capture = $"{listToModifyData.Data.First().GetName().Singularize()}: ";
        Title = $"Bitte trage deine {listToModifyData.Data.First().GetName()} ein.";
    }

    public ObservableCollection<IHaveName> ListToModify { get; set; }
    public string Title { get; init; }
    public string Capture { get; init; }

    public void ConvertBack()
    {
        _items.Clear();
        _items.AddCollectionToThis(ListToModify.Select(x => (T)x));
    }
}