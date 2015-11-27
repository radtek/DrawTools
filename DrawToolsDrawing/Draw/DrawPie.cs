using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Globalization;
using System.Windows.Forms;
using DrawToolsDrawing.GraphicsProperties;
namespace DrawToolsDrawing.Draw
{
    [Serializable]
    public class DrawPie : DrawRectangle
    {
        private const string entryStart = "Start";
        private const string entryEnd = "End";
        string entryRectangle = "Pie";
        string entryangle = "angle";
        string entryAangle = "Aangle";

        public bool needcircle;
        public float StartAngle;
        public float Angel=0;
        public DrawPie(int x, int y, int width, int height, Color lineColor, Color fillColor, bool filled, int lineWidth,float startangle,float angle)
		{
			Rectangle = new Rectangle(x, y, width, height);
			Center = new Point(x + (width / 2), y + (height / 2));
			TipText = String.Format("Ellipse Center @ {0}, {1}", Center.X, Center.Y);
			PenColor = lineColor;
			FillColor = fillColor;
			Filled = filled;
			PenWidth = lineWidth;
            StartAngle = startangle;
            this.Angel = angle;
			Initialize();
		}
        public DrawPie()
		{
			SetRectangle(0, 0, 1, 1);
			Initialize();
		}
		public override void Draw(Graphics g)
		{

            Pen pen = new Pen(PenColor, PenWidth);
            Brush brush = new SolidBrush(FillColor);

            try
            {
                if (Rectangle.Width > 0 && Rectangle.Height > 0)
                {
                    g.DrawPie(pen, Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height, StartAngle, Angel);
                    g.FillPie(brush, DrawRectangle.GetNormalizedRectangle(Rectangle.X + (PenWidth / 2), Rectangle.Y + (PenWidth / 2), Rectangle.X + Rectangle.Width - PenWidth / 2, Rectangle.Y + Rectangle.Height - PenWidth / 2), StartAngle, Angel);
                    //if (ShowRedBox)
                    //{
                    //    Pen redRectangle = new Pen(Color.Red, 1);
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
                }
            }
            catch
            {
                throw new Exception();
            }


            pen.Dispose();
		}


        public override void SaveToStream(SerializationInfo info, int orderNumber, int objectIndex)
        {
            info.AddValue(
                 String.Format(CultureInfo.InvariantCulture,
                               "{0}{1}-{2}",
                               entryRectangle, orderNumber, objectIndex),
                 rectangle);
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryangle, orderNumber, objectIndex),
                StartAngle);
            info.AddValue(
               String.Format(CultureInfo.InvariantCulture,
                             "{0}{1}-{2}",
                             entryAangle, orderNumber, objectIndex),
               Angel);
            base.SaveToStream(info, orderNumber, objectIndex);
        }

        public override void LoadFromStream(SerializationInfo info, int orderNumber, int objectIndex)
        {
            StartAngle = (float)info.GetValue(
                                    String.Format(CultureInfo.InvariantCulture,
                                                  "{0}{1}-{2}",
                                                  entryangle, orderNumber, objectIndex),
                                    typeof(float));
            try
            {
                Angel = (float)info.GetValue(
                                        String.Format(CultureInfo.InvariantCulture,
                                                      "{0}{1}-{2}",
                                                      entryAangle, orderNumber, objectIndex),
                                        typeof(float));
            }
            catch
            {

            }
           base.LoadFromStream(info, orderNumber, objectIndex);
        }
        /// <summary>
        /// Get properties from selected objects and fill GraphicsProperties instance
        /// </summary>
        /// <returns></returns>
        public override void GetProperties()
        {
            //GraphicsPropertiesBase properties = new GraphicsPropertiesBase();
        }

        /// <summary>
        /// Apply properties for all selected objects
        /// </summary>
        public override void ApplyProperties( GraphicsPropertiesBase properties)
        {

        }
        public override DrawObject Clone()
        {
            DrawPie drawPie = new DrawPie();
            drawPie.Rectangle = this.Rectangle;
            drawPie.Angel = this.Angel;
            drawPie.StartAngle = this.StartAngle;
            drawPie.FillColor = this.FillColor;
            drawPie.Filled = this.Filled;

            //drawPie.rectangle = this.rectangle;
            FillDrawObjectFields(drawPie);
            return drawPie;
        }
    }
}
