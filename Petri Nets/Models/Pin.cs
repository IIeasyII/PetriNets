using Petri_Nets.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace Petri_Nets.Models
{
    /// <summary>
    /// Элемент соединяющий узлы
    /// </summary>
    [Serializable()]
    public class Pin : IDrawable, IDragable
    {
        public Node Parent { get; set; }
        public Point RelativeLocation { get; set; }

        private SerializableGraphicsPath path;

        public GraphicsPath Path
        {
            get { return path; }
            set { path = value; }
        }

        public Color BorderColor { get; set; } = Color.Navy;
        public Color FillColor { get; set; } = Color.DeepSkyBlue;

        private Point drag;

        public Point Location
        {
            get { return Parent.Location.Add(RelativeLocation); }
        }

        public Pin(Node parent)
        {
            Path = new GraphicsPath();
            Parent = parent;
            Path = GetFigure();

        }

        public void Drag(Point offset)
        {
            drag = drag.Add(offset);
        }

        public void EndPointDrag()
        {
            var p = Location.Add(drag);
            //Ищем целевой объект
            foreach (var node in Parent.Model.OfType<Node>())
            {
                if (node != Parent && node.AcceptPin)
                {
                    if (node.Hit(p))
                    {
                        node.Previous.Add(Parent);
                        Parent.Next.Add(node);

                    }
                }
            }

            drag = Point.Empty;
        }

        public GraphicsPath GetFigure()
        {
            var graphicsPath = new GraphicsPath();
            graphicsPath.AddEllipse(new Rectangle(-5, -5, 10, 10));

            return graphicsPath;
        }

        public bool Hit(Point point)
        {
            return Path.IsVisible(point.Sub(Location));
        }

        public void Paint(Graphics graphics)
        {
            if (drag != Point.Empty)
            {
                graphics.DrawLink(Location, Location.Add(drag));
            }

            var state = graphics.Save();
            graphics.TranslateTransform(Location.X, Location.Y);
            graphics.FillPath(FillColor.Brush(), Path);
            graphics.DrawPath(BorderColor.Pen(), Path);
            if (drag != Point.Empty)
            {
                graphics.DrawEllipse(BorderColor.Pen(), drag.X - 5, drag.Y - 5, 10, 10);
                graphics.FillEllipse(FillColor.Brush(), drag.X - 5, drag.Y - 5, 10, 10);
            }

            graphics.Restore(state);
        }

        public IDragable StartPointDrag(Point point)
        {
            return this;
        }
    }
}
