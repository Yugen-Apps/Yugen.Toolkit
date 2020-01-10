using System.Globalization;

namespace Yugen.Toolkit.Standard.Helpers
{
    public static class DecimalConverteSample
    {
        public static void Test()
        {
            var a = System.Convert.ToDecimal("1.2345"); //new CultureInfo("en-US") //where the number separator is "."
            var b = System.Convert.ToDecimal("1.2345", new CultureInfo("it-IT")); //where the number separator is ","
            var c = System.Convert.ToDecimal("1.2345", CultureInfo.CurrentUICulture); //The above line returns my culture
            var d = System.Convert.ToDecimal("1.2345", CultureInfo.InvariantCulture); // http://stackoverflow.com/questions/9760237/what-does-cultureinfo-invariantculture-mean

            var e = System.Convert.ToDecimal("1,2345"); //new CultureInfo("en-US") //where the number separator is "."
            var f = System.Convert.ToDecimal("1,2345", new CultureInfo("it-IT")); //where the number separator is ","
            var g = System.Convert.ToDecimal("1,2345", CultureInfo.CurrentUICulture); //The above line returns my culture
            var h = System.Convert.ToDecimal("1,2345", CultureInfo.InvariantCulture); // http://stackoverflow.com/questions/9760237/what-does-cultureinfo-invariantculture-mean
        }
    }
}