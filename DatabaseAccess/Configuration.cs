using System.Collections.Generic;
using System.Collections.ObjectModel;
using Database_Models.DBModels.RecipeModels;
using Database_Models.DBModels.StockModels;
using DatabaseAccess.AccessData;
using DatabaseAccess.Interface;
using Unity;

namespace DatabaseAccess
{
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
}
