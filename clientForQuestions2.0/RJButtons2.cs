using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace clientForQuestions2._0
{
    internal class RJButtons2 : Button
    {
        //https://www.youtube.com/watch?v=u8SL5g9QGcI
        //the code taken from here
        private int borderSize = 0;
        private int borderRadiuos = 40;
        private Color borderColor = Color.Black;
        public RJButtons2()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(150, 40);
            this.BackColor = Color.MediumSlateBlue;
            this.ForeColor = Color.White;
        }
        public int BorderSize
        {
            get { return borderSize; }
            set{borderSize = value;}
        }
        public int BorderRadiuos
        {
            get { return borderRadiuos; }
            set { borderRadiuos = value; }
        }
        public GraphicsPath GetFigureGraph(RectangleF rect, float radious)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, radious, radious, 180, 90);
            path.AddArc(rect.Width - radious, rect.Y, radious, radious, 270, 90);
            path.AddArc(rect.Width - radious, rect.Height - radious, radious, radious, 0, 90);
            path.AddArc(rect.X, rect.Height - radious, radious, radious, 90, 90);
            path.CloseFigure();
            return path;
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF rectSurface = new RectangleF(0, 0, this.Width, this.Height);
            RectangleF rectBorder = new RectangleF(1, 1, this.Width - 0.8F, this.Height - 1);
            if (borderRadiuos > 2)//rounded button
            {
                using (GraphicsPath pathSurface = GetFigureGraph(rectSurface, borderRadiuos))
                using (GraphicsPath pathBorder = GetFigureGraph(rectBorder, borderRadiuos - 1F))
                using (Pen penSurface = new Pen(this.Parent.BackColor, 2))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    penBorder.Alignment = PenAlignment.Inset;

                    this.Region = new Region(pathSurface);

                    pevent.Graphics.DrawPath(penSurface, pathSurface);

                    if (borderSize >= 1)
                    {
                        pevent.Graphics.DrawPath(penBorder, pathBorder);
                    }
                }
            }
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);
        }
        private void Container_BackColorChanged(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                this.Invalidate();
            }
        }
    }
}

