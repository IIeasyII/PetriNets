using Petri_Nets.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace Petri_Nets.Models
{
    /// <summary>
    /// Элемент - Позиция
    /// </summary>
    [Serializable()]
    public class Place : Node
    {
        #region Data

        /// <summary>
        /// Количество фишек/маркеров
        /// </summary>
        public int Tokens { get; set; }
        #endregion

        public Place(Model model) : base(model)
        {
            Path = GetFigure();

            Pin = new Pin(this) { RelativeLocation = new Point(60, 30) };
        }

        #region Behavior

        /// <summary>
        /// Добавить к позиции фишку
        /// </summary>
        /// <param name="node"></param>
        public void Add(Node node)
        {
            var place = (Place)node;

            place.Tokens++;
        }

        /// <summary>
        /// Вычесть из позиции фишку
        /// </summary>
        /// <param name="node"></param>
        public void Sub(Node node)
        {
            var place = (Place)node;

            place.Tokens--;
        }
        #endregion

        #region Graphics
        /// <summary>
        /// Радиус окружности
        /// </summary>
        int r = 60;

        public override void Paint(Graphics graphics)
        {
            //graphics.DrawString("50", Helper.Font, Brushes.Black, 30, 30, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

            switch (Tokens)
            {
                case 0:
                    break;
                case 1:
                    graphics.FillEllipse(Brushes.Black, Location.X + 22, Location.Y + 22, 16, 16);
                    break;
                case 2:
                    graphics.FillEllipse(Brushes.Black, Location.X + 12, Location.Y + 22, 16, 16);
                    graphics.FillEllipse(Brushes.Black, Location.X + 34, Location.Y + 22, 16, 16);
                    break;
                case 3:
                    graphics.FillEllipse(Brushes.Black, Location.X + 12, Location.Y + 30, 16, 16);
                    graphics.FillEllipse(Brushes.Black, Location.X + 34, Location.Y + 30, 16, 16);
                    graphics.FillEllipse(Brushes.Black, Location.X + 22, Location.Y + 10, 16, 16);
                    break;
                default:
                    var rectangle = new RectangleF(Location.X, Location.Y, r, r);
                    graphics.DrawString(Convert.ToString(Tokens), Helper.Font, Brushes.Black, rectangle, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    break;
            }

            base.Paint(graphics);
        }


        public override GraphicsPath GetFigure()
        {
            var graphicsPath = new GraphicsPath();

            graphicsPath.AddEllipse(Location.X, Location.Y, r, r);
            graphicsPath.CloseFigure();

            return graphicsPath;
        }
        #endregion

    }
}
