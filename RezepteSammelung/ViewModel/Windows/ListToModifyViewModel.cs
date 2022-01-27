using Database_Models.Interfaces;
using DatabaseAccess.Interface;
using Humanizer;
using RezepteSammelung.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;
using UtitlityFunctions.InterfaceExtention;

namespace RezepteSammelung.ViewModel.Windows;

internal class ListToModifyVieModel<T> : IListToModifyViewModel
where T : class, IName
{
    private readonly ObservableCollection<T> _items;
    public ListToModifyVieModel(IAccessData<ObservableCollection<T>> listToModifyData)
    {
        _items = listToModifyData.Data;
        ListToModify = new ObservableCollection<IName>(listToModifyData.Data.Select(x => (IName)x!)!);

        Capture = $"{listToModifyData.Data.First().GetName().Singularize()}: ";
        Title = $"Bitte trage deine {listToModifyData.Data.First().GetName()} ein.";
    }

    public ObservableCollection<IName> ListToModify { get; set; }
    public string Title { get; init; }
    public string Capture { get; init; }

    public void ConvertBack()
    {
        _items.Clear();
        _items.AddCollectionToThis(ListToModify.Select(x => (T)x));
    }
}