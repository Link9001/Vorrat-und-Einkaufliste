using Database_Models;
using Database_Models.DBModels.StockModels;
using DatabaseAccess.Interface;
using System;

namespace DatabaseAccess.AccessData;
internal class AccessStockFolderData : IAccessData<StockFolder>
{
    private readonly StockFolder _stockFolder;
    public AccessStockFolderData(Database db)
    {
        _stockFolder = db.StockFolder;
    }

    public StockFolder Data => Filter != null ? Filter.Invoke(_stockFolder) : _stockFolder;
    public Func<StockFolder, StockFolder>? Filter { get; set; }
}