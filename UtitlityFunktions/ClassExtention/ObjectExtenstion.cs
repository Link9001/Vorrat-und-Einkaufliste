using System.Runtime.CompilerServices;

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
}