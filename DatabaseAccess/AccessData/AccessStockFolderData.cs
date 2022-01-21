using System;
using Database_Models.DBModels.StockModels;
using DatabaseAccess.Interface;
using Database_Models;

namespace DatabaseAccess.AccessData;
internal class AccessStockFolderData : IAccessData<StockFolder>
{
    private readonly StockFolder _stockFolder;
    public AccessStockFolderData(Database db)
    {
        this._stockFolder = db.StockFolder;
    }

    public StockFolder Data => Filter != null ? Filter.Invoke(this._stockFolder) : this._stockFolder;
    public Func<StockFolder, StockFolder>? Filter { get; set; }
}