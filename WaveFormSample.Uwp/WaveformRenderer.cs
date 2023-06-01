using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using Windows.UI;

namespace WaveFormSample.Uwp
{
    public class WaveformRenderer
    {
        private Color _topColor;
        private Color _bottomColor;

        public WaveformRenderer(Color color)
        {
            UpdateColor(color);
        }

        public void UpdateColor(Color color)
        {
            _topColor = color;
            _bottomColor = Color.FromArgb(150, _topColor.R, _topColor.G, _topColor.B);
        }

        public void DrawRealLine(CanvasControl sender, CanvasDrawingSession ds, List<(float min, float max)> peakList)
        {
            var width = (float)sender.ActualWidth;
            var height = (float)sender.ActualHeight;
            int midPoint = (int)(height / 2);
            int strokeWidth = 1;

            for (int x = 0; x < peakList.Count; x += 10)
            {
                var (min, max) = peakList[x];
                float topLineHeight = midPoint * max;
                float bottomLineHeight = midPoint * min;

                ds.DrawLine(x, midPoint, x, midPoint - topLineHeight, _topColor, strokeWidth);
                ds.DrawLine(x, midPoint, x, midPoint - bottomLineHeight, _bottomColor, strokeWidth);
            }
        }

        public void DrawFakeLine(CanvasControl sender, CanvasDrawingSession ds)
        {
            var width = (float)sender.ActualWidth;
            var height = (float)sender.ActualHeight;
            var steps = width / 10;

            for (var i = 0; i < steps; i++)
            {
                var mu = i / steps;
                var x = width * mu;
                var rnd = new Random();
                var y = rnd.Next(1, 100);
                var strokeWidth = 1;

                ds.DrawLine(x, 0, x, y, _topColor, strokeWidth);
                ds.DrawLine(x, height, x, y, _bottomColor, 10 - strokeWidth);
            }
        }
    }
}