using System.Globalization;

namespace Yugen.Toolkit.Standard.Helpers
{
    public static class DecimalConverteSample
    {
        public static void Test()
        {
            _ = System.Convert.ToDecimal("1.2345"); //new CultureInfo("en-US") //where the number separator is "."
            _ = System.Convert.ToDecimal("1.2345", new CultureInfo("it-IT")); //where the number separator is ","
            _ = System.Convert.ToDecimal("1.2345", CultureInfo.CurrentUICulture); //The above line returns my culture
            _ = System.Convert.ToDecimal("1.2345", CultureInfo.InvariantCulture); // http://stackoverflow.com/questions/9760237/what-does-cultureinfo-invariantculture-mean
            _ = System.Convert.ToDecimal("1,2345"); //new CultureInfo("en-US") //where the number separator is "."
            _ = System.Convert.ToDecimal("1,2345", new CultureInfo("it-IT")); //where the number separator is ","
            _ = System.Convert.ToDecimal("1,2345", CultureInfo.CurrentUICulture); //The above line returns my culture
            _ = System.Convert.ToDecimal("1,2345", CultureInfo.InvariantCulture); // http://stackoverflow.com/questions/9760237/what-does-cultureinfo-invariantculture-mean
        }
    }
}