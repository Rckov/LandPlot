using LandPlot.Foundations.Interfaces;
using LandPlot.Models;

using LandPlotCoordinate.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LandPlot.Foundations.Services;

internal class DrawingService : IDrawing
{
    private const int DefaultWidth = 450;
    private const int DefaultHeight = 450;

    public Image Draw(IEnumerable<Contour> contours)
    {
        var image = new Image()
        {
            Source = RenderContours(contours, DefaultWidth, DefaultHeight)
        };
        return image;
    }

    private WriteableBitmap RenderContours(IEnumerable<Contour> contours, int width, int height)
    {
        var writeableBitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Pbgra32, null);
        var drawingVisual = new DrawingVisual();
        var drawingContext = drawingVisual.RenderOpen();

        DrawBackground(drawingContext, width, height);

        if (contours == null || !contours.Any())
        {
            return writeableBitmap;
        }

        ApplyTransformations(drawingContext);

        var allCoordinates = contours.SelectMany(c => c.Coordinates);
        var minX = allCoordinates.Min(c => c.X);
        var minY = allCoordinates.Min(c => c.Y);
        var maxX = allCoordinates.Max(c => c.X);
        var maxY = allCoordinates.Max(c => c.Y);

        var pen = new Pen(Brushes.Black, 1);

        foreach (var contour in contours)
        {
            DrawContour(drawingContext, pen, contour, minX, minY, maxX, maxY, width, height);
        }

        drawingContext.Close();

        var renderTargetBitmap = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
        renderTargetBitmap.Render(drawingVisual);

        writeableBitmap.Lock();

        renderTargetBitmap.CopyPixels(Int32Rect.Empty, writeableBitmap.BackBuffer, writeableBitmap.BackBufferStride * writeableBitmap.PixelHeight, writeableBitmap.BackBufferStride);

        writeableBitmap.AddDirtyRect(new Int32Rect(0, 0, width, height));
        writeableBitmap.Unlock();

        return writeableBitmap;
    }

    private void DrawBackground(DrawingContext dc, double width, double height)
    {
        dc.DrawRectangle(Brushes.White, null, new Rect(0, 0, width, height));
    }

    private void ApplyTransformations(DrawingContext dc)
    {
        dc.PushTransform(new RotateTransform(-180, DefaultWidth / 2, DefaultHeight / 2));
        dc.PushTransform(new ScaleTransform(-1, 1, DefaultWidth / 2, DefaultHeight / 2));
    }

    private void DrawContour(DrawingContext dc, Pen pen, Contour contour, double minX, double minY, double maxX, double maxY, double width, double height)
    {
        var points = NormalizeCoordinates(contour.Coordinates, minX, minY, maxX, maxY, width, height);

        if (!points.Any())
        {
            return;
        }

        var geometry = CreateGeometry(points);

        DrawEllipsePoint(dc, points.First());
        DrawGeometry(dc, pen, geometry);

        foreach (var point in points)
        {
            DrawEllipsePoint(dc, point);
        }
    }

    private void DrawEllipsePoint(DrawingContext dc, Point point)
    {
        dc.DrawEllipse(Brushes.Red, null, point, 3, 3);
    }

    private void DrawGeometry(DrawingContext dc, Pen pen, StreamGeometry geometry)
    {
        dc.DrawGeometry(null, pen, geometry);
    }

    private List<Point> NormalizeCoordinates(IEnumerable<Coordinate> coordinates, double minX, double minY, double maxX, double maxY, double width, double height)
    {
        var xScale = (width - 20) / (maxX - minX);
        var yScale = (height - 20) / (maxY - minY);
        var scale = Math.Min(xScale, yScale);

        var xOffset = (width - (maxX - minX) * scale) / 2;
        var yOffset = (height - (maxY - minY) * scale) / 2;

        return coordinates.Select(c => new Point((c.X - minX) * scale + xOffset, (c.Y - minY) * scale + yOffset)).ToList();
    }

    private StreamGeometry CreateGeometry(IEnumerable<Point> points)
    {
        var geometry = new StreamGeometry();

        using (var context = geometry.Open())
        {
            if (!points.Any())
            {
                return geometry;
            }

            var startPoint = points.First();
            context.BeginFigure(startPoint, isFilled: false, isClosed: false);

            foreach (var point in points.Skip(1))
            {
                context.LineTo(point, isStroked: true, isSmoothJoin: false);
            }

            if (startPoint == points.Last())
            {
                context.Close();
            }
        }

        return geometry;
    }
}