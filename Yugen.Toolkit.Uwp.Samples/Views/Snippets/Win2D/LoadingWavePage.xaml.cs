using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Numerics;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Yugen.Toolkit.Uwp.Samples.Views.Snippets.Win2D
{
    public sealed partial class LoadingWavePage : Page
    {
        private readonly int _radiusValue = 100;

        private int _offsetX = 0;
        private int _percent = 0;
        private int rate = 0;

        public LoadingWavePage()
        {
            this.InitializeComponent();
        }

        public void CreateLoadingWave(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, Vector2 position, Color color)
        {
            if (rate >= 10)
            {
                _percent++;
                rate = 0;
            }

            if (_percent > 100)
            {
                _percent = 0;
            }

            CanvasTextLayout textLayout = new CanvasTextLayout(sender, $"{_percent}", new CanvasTextFormat() { FontSize = _radiusValue }, _radiusValue * 2, _radiusValue * 2);

            CanvasGeometry orignalText = CanvasGeometry.CreateText(textLayout);
            _ = orignalText.ComputeBounds();
            var textOffsetX = (_radiusValue * 2 - textLayout.LayoutBoundsIncludingTrailingWhitespace.Width) / 2;
            var textOffsetY = (_radiusValue * 2 - textLayout.LayoutBoundsIncludingTrailingWhitespace.Height) / 2;

            orignalText = orignalText.Transform(Matrix3x2.CreateTranslation((float)textOffsetX + position.X, (float)textOffsetY + position.Y));

            CanvasPathBuilder builder = new CanvasPathBuilder(sender);

            var offsetY = 2 * rate / 10 + _percent * 2;
            builder.BeginFigure(0 + _offsetX + position.X, _radiusValue * 2 - offsetY + position.Y);

            builder.AddCubicBezier(new Vector2(_radiusValue * 1 + _offsetX + position.X, _radiusValue * 2 + _radiusValue / 3 - offsetY + position.Y), new Vector2(_radiusValue * 1 + _offsetX + position.X, _radiusValue * 2 - _radiusValue / 3 - offsetY + position.Y), new Vector2(_radiusValue * 2 + _offsetX + position.X, _radiusValue * 2 - offsetY + position.Y));
            builder.AddCubicBezier(new Vector2(_radiusValue * 3 + _offsetX + position.X, _radiusValue * 2 + _radiusValue / 3 - offsetY + position.Y), new Vector2(_radiusValue * 3 + _offsetX + position.X, _radiusValue * 2 - _radiusValue / 3 - offsetY + position.Y), new Vector2(_radiusValue * 4 + _offsetX + position.X, _radiusValue * 2 - offsetY + position.Y));

            builder.AddLine(_radiusValue * 4 + _offsetX + position.X, _radiusValue * 4 + position.Y);
            builder.AddLine(0 + _offsetX + position.X, _radiusValue * 4 + position.Y);

            builder.EndFigure(CanvasFigureLoop.Closed);

            var wavePath = CanvasGeometry.CreatePath(builder);
            var circlePath = CanvasGeometry.CreateCircle(sender, new Vector2(_radiusValue, _radiusValue), _radiusValue);

            var backgroundPath = circlePath.CombineWith(wavePath, Matrix3x2.Identity, CanvasGeometryCombine.Intersect);

            var topText = orignalText.CombineWith(backgroundPath, Matrix3x2.Identity, CanvasGeometryCombine.Exclude);
            var drawnText = orignalText.CombineWith(backgroundPath, Matrix3x2.Identity, CanvasGeometryCombine.Intersect);

            args.DrawingSession.FillGeometry(backgroundPath, position, color);
            args.DrawingSession.FillGeometry(topText, color);
            args.DrawingSession.FillGeometry(drawnText, Colors.White);

            var borderCircle = CanvasGeometry.CreateCircle(sender, new Vector2(_radiusValue, _radiusValue), _radiusValue - 1);
            args.DrawingSession.DrawGeometry(borderCircle, position, color);

            _offsetX--;
            if (_offsetX <= -_radiusValue * 2)
            {
                _offsetX = 0;
            }
            rate++;
        }

        public void CreateLoadingWaveText(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args, Vector2 position, Color color)
        {
            if (rate >= 10)
            {
                _percent++;
                rate = 0;
            }

            if (_percent > 100)
            {
                _percent = 0;
            }

            CanvasTextLayout textLayout = new CanvasTextLayout(sender, $"钴{Environment.NewLine}藍", new CanvasTextFormat() { FontSize = _radiusValue / 2 }, _radiusValue * 2, _radiusValue * 2);
            textLayout.SetFontFamily(0, 4, "华文楷体");

            CanvasGeometry orignalText = CanvasGeometry.CreateText(textLayout);
            _ = orignalText.ComputeBounds();
            var textOffsetX = (_radiusValue * 2 - textLayout.LayoutBoundsIncludingTrailingWhitespace.Width) / 2;
            var textOffsetY = (_radiusValue * 2 - textLayout.LayoutBoundsIncludingTrailingWhitespace.Height) / 2;

            orignalText = orignalText.Transform(Matrix3x2.CreateTranslation((float)textOffsetX + position.X, (float)textOffsetY + position.Y));

            CanvasPathBuilder builder = new CanvasPathBuilder(sender);

            var offsetY = 2 * rate / 10 + _percent * 2;
            builder.BeginFigure(0 + _offsetX + position.X, _radiusValue * 2 - offsetY + position.Y);

            builder.AddCubicBezier(new Vector2(_radiusValue * 1 + _offsetX + position.X, _radiusValue * 2 + _radiusValue / 3 - offsetY + position.Y), new Vector2(_radiusValue * 1 + _offsetX + position.X, _radiusValue * 2 - _radiusValue / 3 - offsetY + position.Y), new Vector2(_radiusValue * 2 + _offsetX + position.X, _radiusValue * 2 - offsetY + position.Y));
            builder.AddCubicBezier(new Vector2(_radiusValue * 3 + _offsetX + position.X, _radiusValue * 2 + _radiusValue / 3 - offsetY + position.Y), new Vector2(_radiusValue * 3 + _offsetX + position.X, _radiusValue * 2 - _radiusValue / 3 - offsetY + position.Y), new Vector2(_radiusValue * 4 + _offsetX + position.X, _radiusValue * 2 - offsetY + position.Y));

            builder.AddLine(_radiusValue * 4 + _offsetX + position.X, _radiusValue * 4 + position.Y);
            builder.AddLine(0 + _offsetX + position.X, _radiusValue * 4 + position.Y);

            builder.EndFigure(CanvasFigureLoop.Closed);

            var wavePath = CanvasGeometry.CreatePath(builder);
            var circlePath = CanvasGeometry.CreateCircle(sender, new Vector2(_radiusValue, _radiusValue), _radiusValue);
            circlePath = circlePath.Transform(Matrix3x2.CreateTranslation(position));
            var backgroundPath = circlePath.CombineWith(wavePath, Matrix3x2.Identity, CanvasGeometryCombine.Intersect);

            var topText = orignalText.CombineWith(backgroundPath, Matrix3x2.Identity, CanvasGeometryCombine.Exclude);
            var drawnText = orignalText.CombineWith(backgroundPath, Matrix3x2.Identity, CanvasGeometryCombine.Intersect);

            args.DrawingSession.FillGeometry(backgroundPath, Color.FromArgb(255, 99, 149, 176));
            args.DrawingSession.FillGeometry(topText, Color.FromArgb(255, 99, 149, 176));
            args.DrawingSession.FillGeometry(drawnText, Colors.White);

            var borderCircle = CanvasGeometry.CreateCircle(sender, new Vector2(_radiusValue, _radiusValue), _radiusValue - 1);
            args.DrawingSession.DrawGeometry(borderCircle, position, Color.FromArgb(255, 99, 149, 176), 2);

            _offsetX--;
            if (_offsetX <= -_radiusValue * 2)
            {
                _offsetX = 0;
            }
            rate++;
        }

        private void OnLoadingWavePageUnloaded(object sender, RoutedEventArgs e)
        {
            MyCanvas.RemoveFromVisualTree();
            MyCanvas = null;
        }

        private void OnMyCanvasDrawAnimated(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            CreateLoadingWave(sender, args, new Vector2(0, 0), Color.FromArgb(255, 195, 71, 59));
            //CreateLoadingWaveText(sender, args, new Vector2(300, 0), Color.FromArgb(255, 99, 149, 176));
        }
    }
}