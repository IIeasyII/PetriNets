using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace Petri_Nets.Controls
{
    /// <summary>
    /// Интерфейс для рисования
    /// </summary>
    public interface IDrawable
    {
        /// <summary>
        /// Рисуем
        /// </summary>
        /// <param name="graphics"></param>
        void Paint(Graphics graphics);

        /// <summary>
        /// Получить фигуру для рисования
        /// </summary>
        /// <returns></returns>
        GraphicsPath GetFigure();
    }

    /// <summary>
    /// Интерфейс для перемешения
    /// </summary>
    public interface IDragable
    {
        bool Hit(Point point);
        void Drag(Point offset);
        IDragable StartPointDrag(Point point);
        void EndPointDrag();
    }

    /// <summary>
    /// Объект можно выделить
    /// </summary>
    public interface ISelectable
    {
        ISelectable Hit(Point point);
        void Select();
        void Unselect();
        void Remove();
    }
}
