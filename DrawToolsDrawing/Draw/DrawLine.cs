using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
namespace DrawToolsDrawing.Draw
{
	/// <summary>
	/// Line graphic object
	/// </summary>
	//[Serializable]
	public class DrawLine : DrawObject
	{
		public Point startPoint;
        public Point endPoint;

		private const string entryStart = "Start";
		private const string entryEnd = "End";
		/// <summary>
		///  Graphic objects for hit test
		/// </summary>
		private GraphicsPath areaPath = null;

		private Pen areaPen = null;
        public bool needcircle;
		private Region areaRegion = null;

        public override void SetSpecialStartPoint(Point pastePoint)
        {
            int halfWidth = (this.endPoint.X - this.startPoint.X) / 2;
            int halfHeight = (this.endPoint.Y - this.startPoint.Y) / 2;
            this.startPoint.X = pastePoint.X - halfWidth;
            this.startPoint.Y = pastePoint.Y - halfHeight;
            this.endPoint.X = pastePoint.X + halfWidth;
            this.endPoint.Y = pastePoint.Y + halfHeight;
        }

        #region Constructor
        public DrawLine()
        {
            startPoint.X = 0;
            startPoint.Y = 0;
            endPoint.X = 1;
            endPoint.Y = 1;
            ZOrder = 0;
            Initialize();
        }
        public DrawLine(int x1, int y1, int x2, int y2)
        {
            startPoint.X = x1;
            startPoint.Y = y1;
            endPoint.X = x2;
            endPoint.Y = y2;
            ZOrder = 0;
            TipText = String.Format("Line Start @ {0}-{1}, End @ {2}-{3}", x1, y1, x2, y2);
            Initialize();
        }
        public DrawLine(int x1, int y1, int x2, int y2, DrawingPens.PenType p)
        {
            startPoint.X = x1;
            startPoint.Y = y1;
            endPoint.X = x2;
            endPoint.Y = y2;
            DrawPen = DrawingPens.SetCurrentPen(p);
            _PenType = p;
            ZOrder = 0;
            TipText = String.Format("Line Start @ {0}-{1}, End @ {2}-{3}", x1, y1, x2, y2);
            //this.DrawProperties = new PropertieBase();
            Initialize();
        }
        public DrawLine(int x1, int y1, int x2, int y2, Color lineColor, int lineWidth)
        {
            startPoint.X = x1;
            startPoint.Y = y1;
            endPoint.X = x2;
            endPoint.Y = y2;
            PenColor = lineColor;
            PenWidth = lineWidth;
            ZOrder = 0;
            TipText = String.Format("Line Start @ {0}-{1}, End @ {2}-{3}", x1, y1, x2, y2);
            Initialize();
        }
        protected override void Initialize()
        {
            this.drawingType = DrawingType.DrawLine;
        } 
        #endregion

		public override void Draw(Graphics g)
		{
			g.SmoothingMode = SmoothingMode.AntiAlias;

			Pen pen;
			if (DrawPen == null)
                
				pen = new Pen(PenColor, PenWidth);
			else
				pen = (Pen)DrawPen.Clone();

            if (needcircle)
            {

                System.Drawing.Drawing2D.LineCap line  = LineCap.Round;
                pen.EndCap = line;
                pen.StartCap = line;
            }
			GraphicsPath gp = new GraphicsPath();
			gp.AddLine(startPoint, endPoint);
            if (ShowRedBox && PenWidth > 3 && needcircle )
            {
                Pen redRectangle = new Pen(Color.Red, 1);
                GraphicsPath gp2 = new GraphicsPath();
                gp2.AddRectangle(createRectangle());
                if (Rotation != 0)
                {
                    RectangleF pathBounds = gp2.GetBounds();
                    Matrix m = new Matrix();
                    m.RotateAt(Rotation, new PointF(pathBounds.Left + (pathBounds.Width / 2), pathBounds.Top + (pathBounds.Height / 2)), MatrixOrder.Append);
                    gp2.Transform(m);
                }
                g.DrawPath(redRectangle, gp2);
            }
			// Rotate the path about it's center if necessary
			if (Rotation != 0)
			{
				RectangleF pathBounds = gp.GetBounds();
				Matrix m = new Matrix();
				m.RotateAt(Rotation, new PointF(pathBounds.Left + (pathBounds.Width / 2), pathBounds.Top + (pathBounds.Height / 2)), MatrixOrder.Append);
				gp.Transform(m);
			}
           
			g.DrawPath(pen, gp);
			gp.Dispose();
			pen.Dispose();
		}


        private Rectangle createRectangle()
        {

            int width = Math.Abs(endPoint.X - startPoint.X);
            int height = Math.Abs(endPoint.Y - startPoint.Y);

            if (height <= 8)
            {
                height = 24;
            }
            if (width <= 8)
            {
                width = 24;
            }
            int X1 = startPoint.X < endPoint.X ? startPoint.X : endPoint.X;
            int Y1 = startPoint.Y < endPoint.Y ? startPoint.Y : endPoint.Y;
            if (width == 24)
            {
                X1 -= 12;
                Y1 -= 6;
                height += 12;

            }
            if (height == 24)
            {
                Y1 -=12;
                X1 -= 6;
                width += 12;
            }


            return new Rectangle(X1, Y1, width, height);

        }
        
		/// <summary>
		/// Clone this instance
		/// </summary>
		public override DrawObject Clone()
		{
			DrawLine drawLine = new DrawLine();
			drawLine.startPoint = startPoint;
			drawLine.endPoint = endPoint;
            drawLine.needcircle = needcircle;
			FillDrawObjectFields(drawLine);
			return drawLine;
		}

		public override int HandleCount
		{
			get { return 2; }
		}

		/// <summary>
		/// Get handle point by 1-based number
		/// </summary>
		/// <param name="handleNumber"></param>
		/// <returns></returns>
		public override Point GetHandle(int handleNumber)
		{
			GraphicsPath gp = new GraphicsPath();
			Matrix m = new Matrix();
			gp.AddLine(startPoint, endPoint);
			RectangleF pathBounds = gp.GetBounds();
			m.RotateAt(Rotation, new PointF(pathBounds.Left + (pathBounds.Width / 2), pathBounds.Top + (pathBounds.Height / 2)), MatrixOrder.Append);
			gp.Transform(m);
			Point start, end;
			start = Point.Truncate(gp.PathPoints[0]);
			end = Point.Truncate(gp.PathPoints[1]);
			gp.Dispose();
			m.Dispose();
			if (handleNumber == 1)
				return start;
			else
				return end;
		}

		/// <summary>
		/// Hit test.
		/// Return value: -1 - no hit
		///                0 - hit anywhere
		///                > 1 - handle number
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public override int HitTest(Point point)
		{
			if (Selected)
				for (int i = 1; i <= HandleCount; i++)
				{
					GraphicsPath gp = new GraphicsPath();
					gp.AddRectangle(GetHandleRectangle(i));
					bool vis = gp.IsVisible(point);
					gp.Dispose();
					if (vis)
						return i;
				}
			// OK, so the point is not on a selection handle, is it anywhere else on the line?
			if (PointInObject(point))
				return 0;
			return -1;
		}

		protected override bool PointInObject(Point point)
		{
			CreateObjects();
			//return AreaPath.IsVisible(point);
			return AreaRegion.IsVisible(point);
		}

		public override bool IntersectsWith(Rectangle rectangle)
		{
			CreateObjects();

			return AreaRegion.IsVisible(rectangle);
		}





		public override Cursor GetHandleCursor(int handleNumber)
		{
			switch (handleNumber)
			{
				case 1:
				case 2:
					return Cursors.SizeAll;
				default:
					return Cursors.Default;
			}
		}
        public override int GetMostRight()
        {
            if (startPoint.X > endPoint.X)
            {
                return startPoint.X;
            }
            else
            {

                return endPoint.X;
            }
            
        }

        public override int GetMostLeft()
        {
            if (startPoint.X > endPoint.X)
            {
                return endPoint.X;
            }
            else
            {

                return startPoint.X;
            }
        }

        public override int GetMostTop()
        {
            if (startPoint.Y > endPoint.Y)
            {
                return endPoint.Y;
            }
            else
            {

                return startPoint.Y;
            }
        }

        public override int GetMostButtom()
        {
            if (startPoint.Y < endPoint.Y)
            {
                return endPoint.Y;
            }
            else
            {

                return startPoint.Y;
            }
        }

		public override void MoveHandleTo(Point point, int handleNumber)
		{
			//GraphicsPath gp = new GraphicsPath();
			//Matrix m = new Matrix();
			//if (handleNumber == 1)
			//    gp.AddLine(point, endPoint);
			//else
			//    gp.AddLine(startPoint, point);

			//RectangleF pathBounds = gp.GetBounds();
			//m.RotateAt(Rotation, new PointF(pathBounds.Left + (pathBounds.Width / 2), pathBounds.Top + (pathBounds.Height / 2)), MatrixOrder.Append);
			//gp.Transform(m);
			//Point start, end;
			//start = Point.Truncate(gp.PathPoints[0]);
			//end = Point.Truncate(gp.PathPoints[1]);
			//gp.Dispose();
			//m.Dispose();
			//if (handleNumber == 1)
			//    startPoint = start;
			//else
			//    endPoint = end;

			if (handleNumber == 1)
				startPoint = point;
			else
				endPoint = point;

			Dirty = true;
			Invalidate();
		}

		public override void Move(int deltaX, int deltaY)
		{
			startPoint.X += deltaX;
			startPoint.Y += deltaY;

			endPoint.X += deltaX;
			endPoint.Y += deltaY;
			Dirty = true;
			Invalidate();
		}


        public override void PretendToMoveStart(int deltaX, int deltaY)
        {
            startPoint.X += deltaX;
            startPoint.Y += deltaY;

            endPoint.X += deltaX;
            endPoint.Y += deltaY;
            Dirty = true;
        }
        public override ArrayList GetCriticalPointList()
        {
            this.CriticalPointList.Clear();
            Point a = new Point(startPoint.X, startPoint.Y);
            CriticalPointList.Add(a);
            a = new Point(endPoint.X , endPoint.Y);
            CriticalPointList.Add(a);
         
            return CriticalPointList;

        }
        public override void PretendToMoveOver(int deltaX, int deltaY)
        {
            startPoint.X -= deltaX;
            startPoint.Y -= deltaY;

            endPoint.X -= deltaX;
            endPoint.Y -= deltaY;
            Dirty = true;
        }






        string entryCIRCLE = "circle";
		public override void SaveToStream(SerializationInfo info, int orderNumber, int objectIndex)
		{
			info.AddValue(
				String.Format(CultureInfo.InvariantCulture,
							  "{0}{1}-{2}",
							  entryStart, orderNumber, objectIndex),
				startPoint);

			info.AddValue(
				String.Format(CultureInfo.InvariantCulture,
							  "{0}{1}-{2}",
							  entryEnd, orderNumber, objectIndex),
				endPoint);
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryCIRCLE, orderNumber, objectIndex),
                needcircle);
			base.SaveToStream(info, orderNumber, objectIndex);
		}

		public override void LoadFromStream(SerializationInfo info, int orderNumber, int objectIndex)
		{
			startPoint = (Point)info.GetValue(
									String.Format(CultureInfo.InvariantCulture,
												  "{0}{1}-{2}",
												  entryStart, orderNumber, objectIndex),
									typeof(Point));

			endPoint = (Point)info.GetValue(
								String.Format(CultureInfo.InvariantCulture,
											  "{0}{1}-{2}",
											  entryEnd, orderNumber, objectIndex),
								typeof(Point));



            needcircle = (bool)info.GetValue(
                                String.Format(CultureInfo.InvariantCulture,
                                              "{0}{1}-{2}",
                                              entryCIRCLE, orderNumber, objectIndex),
                                typeof(bool));

			base.LoadFromStream(info, orderNumber, objectIndex);
		}

		/// <summary>
		/// Invalidate object.
		/// When object is invalidated, path used for hit test
		/// is released and should be created again.
		/// </summary>
		protected void Invalidate()
		{
			if (AreaPath != null)
			{
				AreaPath.Dispose();
				AreaPath = null;
			}

			if (AreaPen != null)
			{
				AreaPen.Dispose();
				AreaPen = null;
			}

			if (AreaRegion != null)
			{
				AreaRegion.Dispose();
				AreaRegion = null;
			}
		}

		/// <summary>
		/// Create graphic objects used for hit test.
		/// </summary>
		protected virtual void CreateObjects()
		{
			if (AreaPath != null)
				return;

			// Create path which contains wide line
			// for easy mouse selection
			AreaPath = new GraphicsPath();
			// Take into account the width of the pen used to draw the actual object
			AreaPen = new Pen(Color.Black, PenWidth < 7 ? 7 : PenWidth);
			// Prevent Out of Memory crash when startPoint == endPoint
			if (startPoint.Equals((Point)endPoint))
			{
				endPoint.X++;
				endPoint.Y++;
			}
			AreaPath.AddLine(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
			AreaPath.Widen(AreaPen);
			// Rotate the path about it's center if necessary
			if (Rotation != 0)
			{
				RectangleF pathBounds = AreaPath.GetBounds();
				Matrix m = new Matrix();
				m.RotateAt(Rotation, new PointF(pathBounds.Left + (pathBounds.Width / 2), pathBounds.Top + (pathBounds.Height / 2)), MatrixOrder.Append);
				AreaPath.Transform(m);
				m.Dispose();
			}

			// Create region from the path
			AreaRegion = new Region(AreaPath);
		}

		protected GraphicsPath AreaPath
		{
			get { return areaPath; }
			set { areaPath = value; }
		}

		protected Pen AreaPen
		{
			get { return areaPen; }
			set { areaPen = value; }
		}

		protected Region AreaRegion
		{
			get { return areaRegion; }
			set { areaRegion = value; }
		}
	}
}