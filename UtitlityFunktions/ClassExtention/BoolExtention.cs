namespace UtitlityFunctions.ClassExtention
{
    public static class BoolExtention
    {
        public static bool Not(this bool @bool)
        {
            if (@bool)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}