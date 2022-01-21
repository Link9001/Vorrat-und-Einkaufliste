namespace UtitlityFunctions.InterfaceExtention
{
    public static class ICollectionExtention
    {

        public static bool IsEmpty<T>(this ICollection<T> collection)
        {
            return collection.Count == 0;
        }

        public static void AddCollectionToThis<T>(this ICollection<T> collection, IEnumerable<T> collectionToAdd)
        {
            foreach (T item in collectionToAdd)
            {
                collection.Add(item);
            }
        }
    }
}
