namespace Yugen.Toolkit.Standard.Helpers
{
    public static class ScreenUnitHelper
    {
        private const double PixelToCentimeterRatio = 37.79527559055;
        private const double PixelToInchRatio = 96;
        private const double CentimeterToInchRatio = 2.54;

        public static double ConvertPixelToCentimeter(double value) => value / PixelToCentimeterRatio;
        public static double ConvertPixelToInch(double value) => value / PixelToInchRatio;
        public static double ConvertCentimeterToPixel(double value) => value * PixelToCentimeterRatio;
        public static double ConvertCentimeterToInch(double value) => value / CentimeterToInchRatio;
        public static double ConvertInchToPixel(double value) => value * PixelToInchRatio;
        public static double ConvertInchToCentimeter(double value) => value * CentimeterToInchRatio;
    }
}