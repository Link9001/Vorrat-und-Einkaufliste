using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezepteSammelung.Interfaces
{
    internal interface IListToModifyViewModelBase
    {
        IList ListToModify { get; set; }

    }
}
