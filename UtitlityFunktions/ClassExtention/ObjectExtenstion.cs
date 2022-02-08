using System.Collections;
using System.Reflection;

namespace UtitlityFunctions.ClassExtention;

public static class ObjectExtenstion
{
    public static bool IsNumber(this object obj, out int number)
    {
        number = 0;

        switch (obj)
        {
            case int intNumber:
                number = intNumber;
                return true;
            case string stringNumber:
                {
                    if (int.TryParse(stringNumber, out int result))
                    {
                        number = result;
                        return true;
                    }

                    break;
                }
            default:
                return false;
        }

        return false;
    }

    /// <summary>
    /// Compares the properties of two objects of the same type and returns if all properties are equal.
    /// </summary>
    /// <param name="objectA">The first object to compare.</param>
    /// <param name="objectB">The second object to compre.</param>
    /// <param name="ignoreList">A list of property names to ignore from the comparison.</param>
    /// <returns><c>true</c> if all property values are equal, otherwise <c>false</c>.</returns>
    public static bool AreObjectsEqual(this object objectA, object objectB, params string[] ignoreList)
    {
        var objectType = objectA.GetType();

        var result = false;

        foreach (var propertyInfo in objectType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanRead && !ignoreList.Contains(p.Name)))
        {

            var valueA = propertyInfo.GetValue(objectA, null);
            var valueB = propertyInfo.GetValue(objectB, null);

            if (valueA == null || valueB == null)
            {
                throw new NullReferenceException(
                    $"Could not get value from '{objectType.Name}' from Property '{propertyInfo.Name}'.");
            }

            if (CanDirectlyCompare(propertyInfo.PropertyType))
            {
                if (AreValuesEqual(valueA, valueB))
                {
                    continue;
                }

                result = false;
            }
            else if (typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType))
            {
                var collectionItems1 = ((IEnumerable)valueA).Cast<object>();
                var collectionItems2 = ((IEnumerable)valueB).Cast<object>();
                var collectionItemsCount1 = collectionItems1.Count();
                var collectionItemsCount2 = collectionItems2.Count();

                if (collectionItemsCount1 != collectionItemsCount2)
                {
                    result = false;
                }
                else
                {
                    for (var i = 0; i < collectionItemsCount1; i++)
                    {
                        var collectionItem1 = collectionItems1.ElementAt(i);
                        var collectionItem2 = collectionItems2.ElementAt(i);
                        var collectionItemType = collectionItem1.GetType();

                        if (CanDirectlyCompare(collectionItemType))
                        {
                            if (AreValuesEqual(collectionItem1, collectionItem2)) 
                                continue;
                            result = false;
                        }
                        else if (!AreObjectsEqual(collectionItem1, collectionItem2, ignoreList))
                        {
                            result = false;
                        }
                    }
                }

            }
            else if (propertyInfo.PropertyType.IsClass)
            {
                var valueFromObjectA = propertyInfo.GetValue(objectA, null);
                var valueFormObjectB = propertyInfo.GetValue(objectB, null);

                if (valueFromObjectA == null && valueFormObjectB == null)
                    continue;

                if (valueFromObjectA == null || valueFormObjectB == null)
                {
                    return false;
                }

                if (AreObjectsEqual(valueFromObjectA, valueFormObjectB, ignoreList)) 
                    continue;

                result = false;
            }
            else
            {
                result = false;
            }
        }

        return result;
    }

    /// <summary>
    /// Determines whether value instances of the specified type can be directly compared.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>
    ///   <c>true</c> if this value instances of the specified type can be directly compared; otherwise, <c>false</c>.
    /// </returns>
    private static bool CanDirectlyCompare(Type type)
    {
        return typeof(IComparable).IsAssignableFrom(type) || type.IsPrimitive || type.IsValueType;
    }

    /// <summary>
    /// Compares two values and returns if they are the same.
    /// </summary>
    /// <param name="valueA">The first value to compare.</param>
    /// <param name="valueB">The second value to compare.</param>
    /// <returns><c>true</c> if both values match, otherwise <c>false</c>.</returns>
    private static bool AreValuesEqual(object valueA, object valueB)
    {
        bool result;

        if (valueA == null && valueB != null || valueA != null && valueB == null)
        {
            result = false; // one of the values is null
        }
        else if (valueA is IComparable selfValueComparer && selfValueComparer.CompareTo(valueB) != 0)
        {
            result = false; // the comparison using IComparable failed
        }
        else if (!Equals(valueA, valueB))
        {
            result = false; // the comparison using Equals failed
        }
        else
        {
            result = true; // match
        }

        return result;
    }
}