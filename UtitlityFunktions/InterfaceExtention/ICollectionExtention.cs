namespace HouseholdmanagementTool.UtitlityFunctions.InterfaceExtention;

public static class ICollectionExtention
{

    public static bool IsEmpty<T>(this ICollection<T> collection)
    {
        return collection.Count == 0;
    }

    public static void AddCollectionToThis<TFrom, TTo>(this ICollection<TFrom> collection, IEnumerable<TTo> collectionToAdd)
        where TTo : TFrom
    {
        foreach (var item in collectionToAdd)
        {
            collection.Add(item);
        }
    }
}