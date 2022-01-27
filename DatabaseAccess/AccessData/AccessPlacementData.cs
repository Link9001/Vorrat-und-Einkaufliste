using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Database_Models;
using Database_Models.DBModels.StockModels;
using DatabaseAccess.Interface;

namespace DatabaseAccess.AccessData
{
    internal class AccessPlacementData : IAccessData<ObservableCollection<Placement>>
    {
        private readonly ObservableCollection<Placement> _placements;
        public AccessPlacementData(Database db)
        {
            _placements = db.StockFolder.Placements;
        }

        public ObservableCollection<Placement> Data => Filter != null ? Filter.Invoke(this._placements) : this._placements;
        public Func<ObservableCollection<Placement>, ObservableCollection<Placement>>? Filter { get; set; }
    }
}
