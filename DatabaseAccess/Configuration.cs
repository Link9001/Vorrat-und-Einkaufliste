using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            container.RegisterType<IAccessData<StockFolder>, AccessStockFolderData>();
            container.RegisterType<IAccessData<RecipeFolder>, AccessRecipeFolderData>();
            return container;
        }
    }
}
