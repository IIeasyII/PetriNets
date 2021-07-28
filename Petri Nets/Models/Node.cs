using Petri_Nets.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Petri_Nets.Models
{
    /// <summary>
    /// Элемент сети Петри
    /// </summary>
    [Serializable()]
    public class Node : IDrawable, IDragable, ISelectable
    {
        public Guid Id = Guid.NewGuid();

        public Model Model { get; set; }

        private SerializableGraphicsPath path;

        public GraphicsPath Path
        {
            get { return path; }
            set { path = value; }
        }

        public Point Location;

        public virtual Color BorderColor => Color.Black;
        public virtual Color FillColor => Color.FromArgb(30, Color.White);

        public object Tag { get; set; }

        public virtual bool AcceptPin => true;

        public Pin Pin { get; set; }
        public List<Node> Previous { get; set; } = new List<Node>();
        public List<Node> Next { get; set; } = new List<Node>();

        [NonSerialized]
        protected bool IsSelected = false;

        public Node(Model model)
        {
            this.Model = model;
        }

        public virtual GraphicsPath GetFigure()
        {
            return new GraphicsPath();
        }

        public void Drag(Point offset)
        {
            Location = Location.Add(offset);
        }

        const float GRID_STEP = 1;

        public void EndPointDrag()
        {
            Location = new Point((int)(GRID_STEP * Math.Round(Location.X / GRID_STEP)), (int)(GRID_STEP * Math.Round(Location.Y / GRID_STEP)));
        }

        public bool Hit(Point point)
        {
            if (Pin != null && Pin.Hit(point))
            {
                return true;
            }

            return Path.IsVisible(point.Sub(Location));
        }

        public virtual void Paint(Graphics graphics)
        {
            foreach (var linked in Previous)
            {
                graphics.DrawLink(linked.Pin.Location.Sub(new Point(-5, 0)), Location.Sub(new Point(11, -Pin.RelativeLocation.Y)));
            }

            

            var state = graphics.Save();
            graphics.TranslateTransform(Location.X, Location.Y);
            graphics.FillPath(FillColor.Brush(), Path);
            graphics.DrawPath(BorderColor.Pen(), Path);
            var rect = Path.GetBounds();
            //rect.Inflate(0, -5);
            rect.Offset(0, -(rect.Height / 2) - 10);
            if (Tag != null)
            {
                graphics.DrawString(Tag.ToString(), Helper.Font, Brushes.Black, rect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            }
            if (IsSelected)
            {
                Helper.DrawHalo(Path, graphics, Color.DarkOrange, Color.DarkOrange.Pen(), .25f);
            }

            graphics.Restore(state);

            if (Pin != null)
            {
                Pin.Paint(graphics);
            }
        }

        public IDragable StartPointDrag(Point point)
        {
            //Если кликнули на пин - тянем пин
            if (Pin != null && Pin.Hit(point))
            {
                return Pin;
            }

            //Тянем себя
            return this;
        }

        ISelectable ISelectable.Hit(Point point)
        {
            //var clickedLink = Previous.Find(link => link.Hit(point));
            //    //.Select(link => link.Hit(p)).FirstOrDefault(s => s != null);
            //if (clickedLink != null)
            //    return clickedLink;

            if (Path.GetBounds().Contains(point.Sub(Location)))
            {
                return this;
            }

            return null;
        }

        public void Select()
        {
            IsSelected = true;
        }

        public void Unselect()
        {
            IsSelected = false;
        }

        public void Remove()
        {
            Model.Remove(this);

            foreach (var node in Model.OfType<Node>())
            {
                node.Previous.RemoveAll(link => link == this);
                node.Next.RemoveAll(link => link == this);
            }
        }
    }
}
