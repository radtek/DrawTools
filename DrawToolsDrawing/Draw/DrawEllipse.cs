using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DrawToolsDrawing.Draw
{
	/// <summary>
	/// Ellipse graphic object
	/// </summary>
	[Serializable]
	public class DrawEllipse : DrawRectangle
	{
        #region Constructor
        public DrawEllipse()
        {
            SetRectangle(0, 0, 1, 1);
            Initialize();
        }

        public DrawEllipse(int x, int y, int width, int height)
        {
            Rectangle = new Rectangle(x, y, width, height);
            Center = new Point(x + (width / 2), y + (height / 2));
            TipText = String.Format("Ellipse Center @ {0}, {1}", Center.X, Center.Y);
            Initialize();
        }

        public DrawEllipse(int x, int y, int width, int height, Color lineColor, Color fillColor, bool filled)
        {
            Rectangle = new Rectangle(x, y, width, height);
            Center = new Point(x + (width / 2), y + (height / 2));
            TipText = String.Format("Ellipse Center @ {0}, {1}", Center.X, Center.Y);
            PenColor = lineColor;
            FillColor = fillColor;
            Filled = filled;
            Initialize();
        }

        public DrawEllipse(int x, int y, int width, int height, DrawingPens.PenType pType, Color fillColor, bool filled)
        {
            Rectangle = new Rectangle(x, y, width, height);
            Center = new Point(x + (width / 2), y + (height / 2));
            TipText = String.Format("Ellipse Center @ {0}, {1}", Center.X, Center.Y);
            DrawPen = DrawingPens.SetCurrentPen(pType);
            _PenType = pType;
            FillColor = fillColor;
            Filled = filled;
            Initialize();
        }

        public DrawEllipse(int x, int y, int width, int height, Color lineColor, Color fillColor, bool filled, int lineWidth)
        {
            Rectangle = new Rectangle(x, y, width, height);
            Center = new Point(x + (width / 2), y + (height / 2));
            TipText = String.Format("Ellipse Center @ {0}, {1}", Center.X, Center.Y);
            PenColor = lineColor;
            FillColor = fillColor;
            Filled = filled;
            PenWidth = lineWidth;
            Initialize();
        }

        protected override void Initialize()
        {
            drawingType = DrawingType.DrawEllipse;
        } 
        #endregion

		public override void Draw(Graphics g)
		{
			Pen pen;
			Brush b = new SolidBrush(FillColor);

			if (DrawPen == null)
				pen = new Pen(PenColor, PenWidth);
			else
				pen = (Pen)DrawPen.Clone();
			GraphicsPath gp = new GraphicsPath();
			gp.AddEllipse(GetNormalizedRectangle(Rectangle));
            //if (ShowRedBox)
            //{
            //    Pen redRectangle = new Pen(Color.Red, PenWidth);
            //    GraphicsPath gp4 = new GraphicsPath();
            //    gp4.AddRectangle(GetNormalizedRectangle(Rectangle));
            //    if (Rotation != 0)
            //    {
            //        RectangleF pathBounds = gp4.GetBounds();
            //        Matrix m = new Matrix();
            //        m.RotateAt(Rotation, new PointF(pathBounds.Left + (pathBounds.Width / 2), pathBounds.Top + (pathBounds.Height / 2)), MatrixOrder.Append);
            //        gp4.Transform(m);
            //    }
            //    g.DrawPath(redRectangle, gp4);
            //}


			// Rotate the path about it's center if necessary
			if (Rotation != 0)
			{
				RectangleF pathBounds = gp.GetBounds();
				Matrix m = new Matrix();
				m.RotateAt(Rotation, new PointF(pathBounds.Left + (pathBounds.Width / 2), pathBounds.Top + (pathBounds.Height / 2)), MatrixOrder.Append);
				gp.Transform(m);
			}
            if (Filled)
                g.FillPath(b, gp);
            g.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
			g.DrawPath(pen, gp);


			gp.Dispose();
			pen.Dispose();
			b.Dispose();

            //if (this.nextobject != null)
            //{
            //    nextobject.Draw(g,showRedBox);

            //}
		}

        /// <summary>
        /// Clone this instance
        /// </summary>
        public override DrawObject Clone()
        {
            DrawEllipse drawEllipse = new DrawEllipse();
            drawEllipse.Rectangle = Rectangle;

            FillDrawObjectFields(drawEllipse);
            return drawEllipse;
        }
	}
}