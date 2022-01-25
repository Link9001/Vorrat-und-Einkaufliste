using System.Collections.ObjectModel;

namespace UtitlityFunctions.InterfaceExtention
{
    public static class ICollectionExtention
    {

        public static bool IsEmpty<T>(this ICollection<T> collection)
        {
            return collection.Count == 0;
        }

        public static void AddCollectionToThis<TFrom, TTo>(this ICollection<TFrom> collection, IEnumerable<TTo> collectionToAdd)
        where TTo : TFrom
        {
            foreach (TTo item in collectionToAdd)
            {
                collection.Add(item);
            }
        }

        public static ICollection<TTo> CastCollection<TFrom, TTo>(this ICollection<TFrom> collection)
        where TTo : TFrom
        {
            var tempCollection = new ObservableCollection<TTo>();
            foreach (TFrom item in tempCollection)
            {
                if (item == null)
                {
                    throw new NullReferenceException($"Item in collection is null but is not allowed.");
                }
                tempCollection.Add((TTo)item);
            }

            return tempCollection;
        }
    }
}
