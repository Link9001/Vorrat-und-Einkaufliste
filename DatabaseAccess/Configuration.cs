using Database_Models.DBModels.RecipeModels;
using Database_Models.DBModels.StockModels;
using HouseholdmanagementTool.DatabaseAccess.AccessData;
using HouseholdmanagementTool.DatabaseAccess.Interface;
using System.Collections.ObjectModel;
using Unity;

namespace HouseholdmanagementTool.DatabaseAccess;

public static class Configuration
{
    public static IUnityContainer ConfigureDataAccess(this IUnityContainer container)
    {
        return container
            .RegisterType<IAccessData<StockFolder>, AccessStockFolderData>()
            .RegisterType<IAccessData<RecipeFolder>, AccessRecipeFolderData>()
            .RegisterType<IAccessData<ObservableCollection<Placement>>, AccessPlacementData>()
            .RegisterType<IAccessData<ObservableCollection<OvenSettings>>, AccessOvenSettingsData>();
    }
}