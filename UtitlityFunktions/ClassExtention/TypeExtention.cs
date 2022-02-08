namespace HouseholdmanagementTool.UtitlityFunctions.ClassExtention;

public static class TypeExtention
{
    private static readonly HashSet<Type> NumericTypes = new HashSet<Type>
    {
        typeof(int),  typeof(double),  typeof(decimal),
        typeof(long), typeof(short),
        typeof(ulong), typeof(ushort),
        typeof(uint), typeof(float)
    };

    public static bool IsNumber(this Type type)
    {
        return NumericTypes.Contains(type);
    }
}