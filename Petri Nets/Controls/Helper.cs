using Petri_Nets.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Text;

namespace Petri_Nets.Controls
{
    static class Helper
    {
        public static Font Font = new Font(FontFamily.GenericSansSerif, 14);

        public static Point Add(this Point point, Point other)
        {
            return new Point(point.X + other.X, point.Y + other.Y);
        }

        public static Point Sub(this Point point, Point other)
        {
            return new Point(point.X - other.X, point.Y - other.Y);
        }

        public static void Priority(Model model)
        {

        }

        public static void Shuffle<T>(this IList<T> list)
        {
            Random random = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        static SolidBrush brush = new SolidBrush(Color.Bisque);

        public static SolidBrush Brush(this Color color)
        {
            brush.Color = color;
            return brush;
        }

        static Pen pen = new Pen(Color.Bisque);

        public static Pen Pen(this Color color, float width = 1f)
        {
            pen.Color = color;
            pen.Width = width;
            return pen;
        }

        public static void DrawLink(this Graphics graphics, Point p1, Point p2)
        {
            using (var pen = new Pen(Color.Lime, 2))
            {
                using (GraphicsPath capPath = new GraphicsPath())
                {
                    // A triangle
                    capPath.AddLine(-3, 0, 3, 0);
                    capPath.AddLine(-3, 0, 0, 5);
                    capPath.AddLine(0, 5, 3, 0);

                    pen.CustomEndCap = new CustomLineCap(null, capPath);
                }

                graphics.DrawBezier(pen, p1, new Point(p1.X + 50, p1.Y), new Point(p2.X - 50, p2.Y), p2);
            }

            //old
            //using (var pen = new Pen(Color.Lime, 2))
            //    gr.DrawBezier(pen, p1, new Point(p1.X + 50, p1.Y), new Point(p2.X - 50, p2.Y), p2);

        }

        public static void DrawHalo(this GraphicsPath path, Graphics gr, Color color, Pen pen, float lineWidth = 1f, int size = 3, int step = 3, int opaque = 200, PointF? offset = null)
        {
            if (size <= 0) return;

            var state = gr.Save();

            if (offset != null)
                gr.TranslateTransform(offset.Value.X, offset.Value.Y);

            for (int i = size; i > 0; i--)
            {
                pen.Color = color.Opaque(1f * opaque / size);
                pen.Width = lineWidth + i * step;
                gr.DrawPath(pen, path);
            }

            gr.Restore(state);
        }

        public static Color Opaque(this Color color, float opaque)
        {
            return Color.FromArgb((opaque * color.A / 255).To255(), color);
        }

        public static byte To255(this float v)
        {
            if (v > 255) return 255;
            if (v < 0) return 0;
            return (byte)v;
        }
    }

    /// <summary>
    /// Сериализуемая обертка над GraphicsPath
    /// </summary>
    [Serializable]
    public sealed class SerializableGraphicsPath : ISerializable, IDisposable
    {
        public GraphicsPath Path = new GraphicsPath();

        public SerializableGraphicsPath() { }

        private SerializableGraphicsPath(SerializationInfo info, StreamingContext context)
        {
            if (info.MemberCount > 0)
            {
                var points = (PointF[])info.GetValue("p", typeof(PointF[]));
                var types = (byte[])info.GetValue("t", typeof(byte[]));
                Path = new GraphicsPath(points, types);
            }
            else
                Path = new GraphicsPath();
        }

        public void Dispose()
        {
            Path?.Dispose();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (Path.PointCount <= 0) return;
            info.AddValue("p", Path.PathPoints);
            info.AddValue("t", Path.PathTypes);
        }

        public static implicit operator GraphicsPath(SerializableGraphicsPath path)
        {
            return path.Path;
        }

        public static implicit operator SerializableGraphicsPath(GraphicsPath path)
        {
            return new SerializableGraphicsPath { Path = path };
        }
    }
}
