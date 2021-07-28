using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Petri_Nets.Models
{
    /// <summary>
    /// Элемент - Переход
    /// </summary>
    [Serializable()]
    public class Transition : Node
    {
        /// <summary>
        /// Разрешен ли переход
        /// </summary>
        public bool AllowTransition { get; set; }

        /// <summary>
        /// Количество сработанных переходов
        /// </summary>
        public int CountTransited { get; set; }

        /// <summary>
        /// Количество фишек/маркеров
        /// </summary>
        public int Tokens { get; set; }

        /// <summary>
        /// Приоритет запуска перехода
        /// </summary>
        public int Priority { get; set; }

        public Transition(Model model) : base(model)
        {
            Path = GetFigure();

            Pin = new Pin(this) { RelativeLocation = new Point(35, 50) };
        }

        public override void Paint(Graphics graphics)
        {
            if (AllowTransition == true)
            {
                graphics.FillRectangle(Brushes.Green, new Rectangle(Location.X, Location.Y, 35, 100));
            }
            else
            {
                graphics.FillRectangle(Brushes.Red, new Rectangle(Location.X, Location.Y, 35, 100));
            }

            base.Paint(graphics);
        }

        public override GraphicsPath GetFigure()
        {
            var graphicsPath = new GraphicsPath();
            var rectangle = new Rectangle(0, 0, 35, 100);

            graphicsPath.AddRectangle(rectangle);
            graphicsPath.CloseFigure();

            return graphicsPath;
        }
    }

    public static class ObjectExtensions
    {
        #region Methods

        public static T Copy<T>(this T source)
        {
            var isNotSerializable = !typeof(T).IsSerializable;
            if (isNotSerializable)
                throw new ArgumentException("The type must be serializable.", "source");

            var sourceIsNull = ReferenceEquals(source, null);
            if (sourceIsNull)
                return default(T);

            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        #endregion
    }
}
