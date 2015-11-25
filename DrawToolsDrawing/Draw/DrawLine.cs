using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using DrawToolsDrawing.GraphicsProperties;
namespace DrawToolsDrawing.Draw
{
    /// <summary>
    /// Line graphic object
    /// </summary>
    //[Serializable]
    public class DrawLine : DrawObject
    {
        #region Members
        private Point EndPoint;
        private const string entryStart = "Start";
        private const string entryEnd = "End";
        #endregion


        /// <summary>
        ///  Graphic objects for hit test
        /// </summary>
        private GraphicsPath areaPath = null;

        private Pen areaPen = null;
        public bool needcircle;
        private Region areaRegion = null;

        public override void SetSpecialStartPoint(Point mousePoint, Point copyPoint)
        {
            //int halfWidth = (this.EndPoint.X - this.StartPoint.X) / 2;
            //int halfHeight = (this.EndPoint.Y - this.StartPoint.Y) / 2;
            //this.StartPoint.X = mousePoint.X - halfWidth;
            //this.StartPoint.Y = mousePoint.Y - halfHeight;
            //this.EndPoint.X = mousePoint.X + halfWidth;
            //this.EndPoint.Y = mousePoint.Y + halfHeight;
        }

        #region Constructor
        public DrawLine()
        {
            StartPoint.X = 0;
            StartPoint.Y = 0;
            EndPoint.X = 1;
            EndPoint.Y = 1;
            ZOrder = 0;
            Initialize();
        }
        public DrawLine(int x1, int y1, int x2, int y2)
        {
            StartPoint.X = x1;
            StartPoint.Y = y1;
            EndPoint.X = x2;
            EndPoint.Y = y2;
            ZOrder = 0;
            TipText = String.Format("Line Start @ {0}-{1}, End @ {2}-{3}", x1, y1, x2, y2);
            Initialize();
        }
        public DrawLine(int x1, int y1, int x2, int y2, DrawingPens.PenType p)
        {
            StartPoint.X = x1;
            StartPoint.Y = y1;
            EndPoint.X = x2;
            EndPoint.Y = y2;
            DrawPen = DrawingPens.SetCurrentPen(p);
            _PenType = p;
            ZOrder = 0;
            TipText = String.Format("Line Start @ {0}-{1}, End @ {2}-{3}", x1, y1, x2, y2);
            //this.DrawProperties = new PropertieBase();
            Initialize();
        }
        public DrawLine(int x1, int y1, int x2, int y2, Color lineColor, int lineWidth)
        {
            StartPoint.X = x1;
            StartPoint.Y = y1;
            EndPoint.X = x2;
            EndPoint.Y = y2;
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

        #region 绘图
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
                System.Drawing.Drawing2D.LineCap line = LineCap.Round;
                pen.EndCap = line;
                pen.StartCap = line;
            }
            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(StartPoint, EndPoint);
            //if (ShowRedBox && PenWidth > 3 && needcircle )
            //{
            //    Pen redRectangle = new Pen(Color.Red, 1);
            //    GraphicsPath gp2 = new GraphicsPath();
            //    gp2.AddRectangle(createRectangle());
            //    if (Rotation != 0)
            //    {
            //        RectangleF pathBounds = gp2.GetBounds();
            //        Matrix m = new Matrix();
            //        m.RotateAt(Rotation, new PointF(pathBounds.Left + (pathBounds.Width / 2), pathBounds.Top + (pathBounds.Height / 2)), MatrixOrder.Append);
            //        gp2.Transform(m);
            //    }
            //    g.DrawPath(redRectangle, gp2);
            //}
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
        #endregion

        #region 获取当前属性
        /// <summary>
        /// 获取当前属性
        /// </summary>
        public override void GetProperties()
        {
            if (NowProperties == null)
                NowProperties = new GraphicsPropertiesLine();
            ((GraphicsPropertiesLine)NowProperties).EndPoint = EndPoint;
            base.GetProperties();
        }
        #endregion
        #region 应用属性
        public override void ApplyProperties(GraphicsPropertiesBase properties)
        {
            base.ApplyProperties(properties);
            this.EndPoint = ((GraphicsPropertiesLine)properties).EndPoint;
        } 
        #endregion
        private Rectangle createRectangle()
        {

            int width = Math.Abs(EndPoint.X - StartPoint.X);
            int height = Math.Abs(EndPoint.Y - StartPoint.Y);

            if (height <= 8)
            {
                height = 24;
            }
            if (width <= 8)
            {
                width = 24;
            }
            int X1 = StartPoint.X < EndPoint.X ? StartPoint.X : EndPoint.X;
            int Y1 = StartPoint.Y < EndPoint.Y ? StartPoint.Y : EndPoint.Y;
            if (width == 24)
            {
                X1 -= 12;
                Y1 -= 6;
                height += 12;

            }
            if (height == 24)
            {
                Y1 -= 12;
                X1 -= 6;
                width += 12;
            }


            return new Rectangle(X1, Y1, width, height);

        }

        public override DrawObject Clone()
        {
            DrawLine drawLine = new DrawLine();
            drawLine.StartPoint = StartPoint;
            drawLine.EndPoint = EndPoint;
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
            gp.AddLine(StartPoint, EndPoint);
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
            if (StartPoint.X > EndPoint.X)
            {
                return StartPoint.X;
            }
            else
            {

                return EndPoint.X;
            }

        }

        public override int GetMostLeft()
        {
            if (StartPoint.X > EndPoint.X)
            {
                return EndPoint.X;
            }
            else
            {

                return StartPoint.X;
            }
        }

        public override int GetMostTop()
        {
            if (StartPoint.Y > EndPoint.Y)
            {
                return EndPoint.Y;
            }
            else
            {

                return StartPoint.Y;
            }
        }

        public override int GetMostButtom()
        {
            if (StartPoint.Y < EndPoint.Y)
            {
                return EndPoint.Y;
            }
            else
            {

                return StartPoint.Y;
            }
        }

        public override void MoveHandleTo(Point point, int handleNumber)
        {
            //GraphicsPath gp = new GraphicsPath();
            //Matrix m = new Matrix();
            //if (handleNumber == 1)
            //    gp.AddLine(point, endPoint);
            //else
            //    gp.AddLine(StartPoint, point);

            //RectangleF pathBounds = gp.GetBounds();
            //m.RotateAt(Rotation, new PointF(pathBounds.Left + (pathBounds.Width / 2), pathBounds.Top + (pathBounds.Height / 2)), MatrixOrder.Append);
            //gp.Transform(m);
            //Point start, end;
            //start = Point.Truncate(gp.PathPoints[0]);
            //end = Point.Truncate(gp.PathPoints[1]);
            //gp.Dispose();
            //m.Dispose();
            //if (handleNumber == 1)
            //    StartPoint = start;
            //else
            //    endPoint = end;

            if (handleNumber == 1)
                StartPoint = point;
            else
                EndPoint = point;

            Dirty = true;
            Invalidate();
        }

        public override void Move(int deltaX, int deltaY)
        {
            StartPoint.X += deltaX;
            StartPoint.Y += deltaY;

            EndPoint.X += deltaX;
            EndPoint.Y += deltaY;
            Dirty = true;
            Invalidate();
        }


        public override void PretendToMoveStart(int deltaX, int deltaY)
        {
            StartPoint.X += deltaX;
            StartPoint.Y += deltaY;

            EndPoint.X += deltaX;
            EndPoint.Y += deltaY;
            Dirty = true;
        }
        public override ArrayList GetCriticalPointList()
        {
            this.CriticalPointList.Clear();
            Point a = new Point(StartPoint.X, StartPoint.Y);
            CriticalPointList.Add(a);
            a = new Point(EndPoint.X, EndPoint.Y);
            CriticalPointList.Add(a);

            return CriticalPointList;

        }
        public override void PretendToMoveOver(int deltaX, int deltaY)
        {
            StartPoint.X -= deltaX;
            StartPoint.Y -= deltaY;

            EndPoint.X -= deltaX;
            EndPoint.Y -= deltaY;
            Dirty = true;
        }






        string entryCIRCLE = "circle";
        #region 保存/打开
        public override void SaveToStream(SerializationInfo info, int orderNumber, int objectIndex)
        {
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryStart, orderNumber, objectIndex),
                StartPoint);

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryEnd, orderNumber, objectIndex),
                EndPoint);
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryCIRCLE, orderNumber, objectIndex),
                needcircle);
            base.SaveToStream(info, orderNumber, objectIndex);
        }

        public override void LoadFromStream(SerializationInfo info, int orderNumber, int objectIndex)
        {
            StartPoint = (Point)info.GetValue(
                                    String.Format(CultureInfo.InvariantCulture,
                                                  "{0}{1}-{2}",
                                                  entryStart, orderNumber, objectIndex),
                                    typeof(Point));

            EndPoint = (Point)info.GetValue(
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
        #endregion

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
            // Prevent Out of Memory crash when StartPoint == endPoint
            if (StartPoint.Equals((Point)EndPoint))
            {
                EndPoint.X++;
                EndPoint.Y++;
            }
            AreaPath.AddLine(StartPoint.X, StartPoint.Y, EndPoint.X, EndPoint.Y);
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