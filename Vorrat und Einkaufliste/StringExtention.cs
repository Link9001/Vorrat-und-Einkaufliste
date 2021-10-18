namespace Vorrat_und_Einkaufliste
{
    public static class StringExtention
    {
        public static int TryParse(this string @string)
        {
            int.TryParse(@string, out int retrunValue);
            return retrunValue;
        }
    }
}