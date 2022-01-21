namespace UtitlityFunctions.ClassExtention
{
    public static class StringExtention
    {
        public static string ReplaceWhitespaceWith_(this string @string)
        {
            return @string.Replace(' ', '_');
        }

        public static string Replace_WithWhitespace(this string @string)
        {
            return @string.Replace('_', ' ');
        }
    }
}
