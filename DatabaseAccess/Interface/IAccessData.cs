using System;

namespace HouseholdmanagementTool.DatabaseAccess.Interface;
internal interface IAccessData<T>
{
    T Data { get; }

    Func<T, T>? Filter { get; set; }
}
