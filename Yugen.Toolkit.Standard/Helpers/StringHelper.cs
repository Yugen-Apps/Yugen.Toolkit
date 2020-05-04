namespace Yugen.Toolkit.Standard.Helpers
{
    public static class StringHelper
    {
        public static string Center(string s, int width)
        {
            if (s.Length >= width)
            {
                return s;
            }

            var leftPadding = (width - s.Length) / 2;
            var rightPadding = width - s.Length - leftPadding;

            return new string(' ', leftPadding) + s + new string(' ', rightPadding);
        }

        public static string Cut(string s, int width)
        {
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }

            if (s.Length <= width)
            {
                return s;
            }

            return s.Substring(0, width);
        }
    }
}
