using System;

namespace Yugen.Toolkit.Standard.Helpers
{
    public static class MathHelper
    {
        /// <summary>
        /// Convert from one range to another
        /// oldMin : oldValue : oldMax = newMin : newValue : newMax
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="oldMin"></param>
        /// <param name="oldMax"></param>
        /// <param name="newMin"></param>
        /// <param name="newMax"></param>
        /// <returns>newValue</returns>
        public static double RangeConvert(double oldValue, double oldMin, double oldMax, double newMin, double newMax) =>
            (oldValue - oldMin) * (newMax - newMin) /
                (oldMax - oldMin) + newMin;

        public static Tuple<int, int> RatioConvert(int width, int height, int newHeight, int newWidth)
        {
            //calculate the ratio
            var ratio = width / (double)height;

            //set height of image to boxHeight and check if resulting width is less than boxWidth, 
            //else set width of image to boxWidth and calculate new height
            return (int)(newHeight * ratio) <= newWidth
                ? new Tuple<int, int>((int)(newHeight * ratio), newHeight)
                : new Tuple<int, int>(newWidth, (int)(newWidth / ratio));
        }

        public static double ConvertAngleToRadians(double angle) =>
            Math.PI / 180 * angle;

        public static double ConvertRadiansToAngle(double radians) =>
            180 / Math.PI * radians;

        /// <summary>
        /// Return a value multiple of
        /// </summary>
        /// <param name="value"></param>
        /// <param name="multipleOf"></param>
        /// <returns></returns>
        public static double RoundToNearestMultiple(double value, double multipleOf) =>
            Math.Round(value / multipleOf) * multipleOf;
    }
}