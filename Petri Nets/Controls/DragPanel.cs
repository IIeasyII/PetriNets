using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Petri_Nets.Controls
{
    /// <summary>
    /// Панель для отображения IDrawable и перемещния IDragable
    /// </summary>
    public class DragPanel : UserControl
    {
        IEnumerable<object> model;
        Point offset;
        Point mouseDown;
        IDragable dragged;
        ISelectable selected;

        public DragPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.Selectable, true);
        }

        public void Build(IEnumerable<object> model)
        {
            this.model = model;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (model == null) return;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.TranslateTransform(offset.X, offset.Y);

            //отрисовываем объекты
            foreach (var obj in model.OfType<IDrawable>())
            {
                obj.Paint(e.Graphics);
            }
        }

        Point ToClient(Point point)
        {
            return point.Sub(offset);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDown = e.Location;
                var p = ToClient(e.Location);
                //ищем объект под мышкой
                var hitted = model.OfType<IDragable>().FirstOrDefault(n => n.Hit(p));
                if (hitted != null)
                {
                    dragged = hitted.StartPointDrag(p);//начинаем тащить
                }

                selected?.Unselect();
                selected = model.OfType<ISelectable>().Select(n => n.Hit(p)).FirstOrDefault(s => s != null);
                selected?.Select();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var shift = new Point(e.Location.X - mouseDown.X, e.Location.Y - mouseDown.Y);
                mouseDown = e.Location;
                //
                if (dragged != null)
                {
                    dragged.Drag(shift);//двигаем объект
                }
                else
                {
                    offset = new Point(offset.X + shift.X, offset.Y + shift.Y);//сдвигаем канвас
                }

                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (dragged != null)
            {
                dragged.EndPointDrag();
            }
            dragged = null;
            Invalidate();
        }

        /// <summary>
        /// Удаление выбранного элемента
        /// </summary>
        public void RemoveSelected()
        {
            selected?.Remove();
        }
    }
}
