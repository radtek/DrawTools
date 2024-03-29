using System;
using System.Diagnostics;
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
    /// Rectangle graphic object
    /// </summary>
    [Serializable]
    public class DrawRectangle : DrawObject
    {
        #region Members
        public Rectangle rectangle;     // 巨型
        private PointF[] PointsConnection = new PointF[4];
        private Region areaRegion;


        private const string entryRectangle = "Rect";
        #region Properties
        protected Region AreaRegion
        {
            get { return areaRegion; }
            set { areaRegion = value; }
        }
        protected Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }
        #endregion
        #endregion
        #region Constructor
        public DrawRectangle()
        {
            SetRectangle(0, 0, 1, 1);
            Initialize();
        }

        public DrawRectangle(int x, int y, int width, int height)
        {
            Center = new Point(x + (width / 2), y + (height / 2));
            rectangle.X = x;
            rectangle.Y = y;
            rectangle.Width = width;
            rectangle.Height = height;
            TipText = String.Format("Rectangle Center @ {0}, {1}", Center.X, Center.Y);
            Initialize();
        }

        public DrawRectangle(int x, int y, int width, int height, Color lineColor, Color fillColor)
        {
            Center = new Point(x + (width / 2), y + (height / 2));
            rectangle.X = x;
            rectangle.Y = y;
            rectangle.Width = width;
            rectangle.Height = height;
            PenColor = lineColor;
            FillColor = fillColor;
            PenWidth = -1;
            TipText = String.Format("Rectangle Center @ {0}, {1}", Center.X, Center.Y);
            Initialize();
        }

        public DrawRectangle(int x, int y, int width, int height, Color lineColor, Color fillColor, bool filled)
        {
            Center = new Point(x + (width / 2), y + (height / 2));
            rectangle.X = x;
            rectangle.Y = y;
            rectangle.Width = width;
            rectangle.Height = height;
            PenColor = lineColor;
            FillColor = fillColor;
            Filled = filled;
            PenWidth = -1;
            TipText = String.Format("Rectangle Center @ {0}, {1}", Center.X, Center.Y);
            Initialize();
        }

        public DrawRectangle(int x, int y, int width, int height, DrawingPens.PenType pType, Color fillColor, bool filled)
        {
            Center = new Point(x + (width / 2), y + (height / 2));
            rectangle.X = x;
            rectangle.Y = y;
            rectangle.Width = width;
            rectangle.Height = height;
            DrawPen = DrawingPens.SetCurrentPen(pType);
            _PenType = pType;
            FillColor = fillColor;
            Filled = filled;
            TipText = String.Format("Rectangle Center @ {0}, {1}", Center.X, Center.Y);
            Initialize();
        }

        public DrawRectangle(int x, int y, int width, int height, Color lineColor, Color fillColor, bool filled, int lineWidth)
        {
            Center = new Point(x + (width / 2), y + (height / 2));
            rectangle.X = x;
            rectangle.Y = y;
            rectangle.Width = width;
            rectangle.Height = height;
            PenColor = lineColor;
            FillColor = fillColor;
            Filled = filled;
            PenWidth = lineWidth;
            TipText = String.Format("Rectangle Center @ {0}, {1}", Center.X, Center.Y);
            Initialize();
        }

        protected override void Initialize()
        {
            drawingType = DrawingType.DrawRectangle;
        }
        #endregion

        #region 绘图
        public override void Draw(Graphics g)
        {
            Pen pen;
            Brush b = new SolidBrush(FillColor);

            if (DrawPen == null)
                pen = new Pen(PenColor, PenWidth);
            else
                pen = (Pen)DrawPen.Clone();
            GraphicsPath gp = new GraphicsPath();

            gp.AddRectangle(GetNormalizedRectangle(Rectangle));
            //旋转
            if (Rotation != 0)
            {
                RectangleF pathBounds = gp.GetBounds();
                Matrix m = new Matrix();
                m.RotateAt(Rotation, new PointF(pathBounds.Left + (pathBounds.Width / 2), pathBounds.Top + (pathBounds.Height / 2)), MatrixOrder.Append);
                gp.Transform(m);
                
                PointsConnection = new PointF[] {  // 将原来四边形的4个顶点坐标放入数组
                 rectangle.Location,
                 new PointF(rectangle.Right, rectangle.Top),
                 new PointF(rectangle.Right, rectangle.Bottom),
                 new PointF(rectangle.Left, rectangle.Bottom)
                };
                m.TransformPoints(PointsConnection);
                areaRegion = new Region(gp);
            }
            if (Filled)
                g.FillPath(b, gp);

            g.DrawPath(pen, gp);
            gp.Dispose();
            pen.Dispose();
            b.Dispose();
        }
        #endregion

        #region MyRegion
        public override void DrawTracker(Graphics g)
        {
            if (!Selected)
                return;
            SolidBrush brush = new SolidBrush(Color.White);

            for (int i = 1; i <= HandleCount; i++)
            {
                g.FillRectangle(brush, GetHandleRectangle(i));
            }
            brush.Dispose();
        }
        #endregion

        protected void SetRectangle(int x, int y, int width, int height)
        {
            rectangle.X = x;
            rectangle.Y = y;
            rectangle.Width = width;
            rectangle.Height = height;
        }

        #region 连接点
        public override int HandleCount
        {
            get { return 8; }
        }
        public override int ConnectionCount
        {
            get { return HandleCount; }
        }
        public override Point GetConnection(int connectionNumber)
        {
            return GetHandle(connectionNumber);
        }
        public override Point GetHandle(int handleNumber)
        {
            Point[] points = new Point[8];
            if (Rotation == 0)
            {
                points[0] = new Point(rectangle.X, rectangle.Y);
                points[1] = new Point(rectangle.X + rectangle.Width / 2, rectangle.Y);
                points[2] = new Point(rectangle.Right, rectangle.Y);
                points[3] = new Point(rectangle.Right, rectangle.Y + rectangle.Height / 2);
                points[4] = new Point(rectangle.Right, rectangle.Bottom);
                points[5] = new Point(rectangle.X + rectangle.Width / 2, rectangle.Bottom);
                points[6] = new Point(rectangle.X, rectangle.Bottom);
                points[7] = new Point(rectangle.X, rectangle.Y + rectangle.Height / 2);
            }
            else
            {
                points[0] = new Point(Convert.ToInt32(PointsConnection[0].X), Convert.ToInt32(PointsConnection[0].Y));
                points[1] = new Point(Convert.ToInt32(PointsConnection[0].X + PointsConnection[1].X) / 2, Convert.ToInt32(PointsConnection[0].Y + PointsConnection[1].Y)/2);
                points[2] = new Point(Convert.ToInt32(PointsConnection[1].X), Convert.ToInt32(PointsConnection[1].Y));
                points[3] = new Point(Convert.ToInt32(PointsConnection[1].X + PointsConnection[2].X) / 2, Convert.ToInt32(PointsConnection[1].Y + PointsConnection[2].Y) / 2);
                points[4] = new Point(Convert.ToInt32(PointsConnection[2].X), Convert.ToInt32(PointsConnection[2].Y));
                points[5] = new Point(Convert.ToInt32(PointsConnection[2].X + PointsConnection[3].X) / 2, Convert.ToInt32(PointsConnection[2].Y + PointsConnection[3].Y) / 2);
                points[6] = new Point(Convert.ToInt32(PointsConnection[3].X), Convert.ToInt32(PointsConnection[3].Y));
                points[7] = new Point(Convert.ToInt32(PointsConnection[3].X + PointsConnection[0].X) / 2, Convert.ToInt32(PointsConnection[3].Y + PointsConnection[0].Y) / 2);
            }
            return points[handleNumber - 1];
        }
        #endregion

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
            {
                for (int i = 1; i <= HandleCount; i++)
                {
                    if (GetHandleRectangle(i).Contains(point))
                        return i;
                }
            }

            if (PointInObject(point))
                return 0;
            return -1;
        }

        protected override bool PointInObject(Point point)
        {
            if (Rotation == 0 || areaRegion==null)
            {
                return rectangle.Contains(point);
            }
            else
            {
                return areaRegion.IsVisible(point);
            }
        }

        /// <summary>
        /// Get cursor for the handle
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public override Cursor GetHandleCursor(int handleNumber)
        {
            switch (handleNumber)
            {
                case 1:
                    return Cursors.SizeNWSE;
                case 2:
                    return Cursors.SizeNS;
                case 3:
                    return Cursors.SizeNESW;
                case 4:
                    return Cursors.SizeWE;
                case 5:
                    return Cursors.SizeNWSE;
                case 6:
                    return Cursors.SizeNS;
                case 7:
                    return Cursors.SizeNESW;
                case 8:
                    return Cursors.SizeWE;
                default:
                    return Cursors.Default;
            }
        }

        /// <summary>
        /// Move handle to new point (resizing)
        /// </summary>
        /// <param name="point"></param>
        /// <param name="handleNumber"></param>
        public override void MoveHandleTo(Point point, int handleNumber)
        {
            int left = Rectangle.Left;
            int top = Rectangle.Top;
            int right = Rectangle.Right;
            int bottom = Rectangle.Bottom;

            switch (handleNumber)
            {
                case 1:
                    left = point.X;
                    top = point.Y;
                    break;
                case 2:
                    top = point.Y;
                    break;
                case 3:
                    right = point.X;
                    top = point.Y;
                    break;
                case 4:
                    right = point.X;
                    break;
                case 5:
                    right = point.X;
                    bottom = point.Y;
                    break;
                case 6:
                    bottom = point.Y;
                    break;
                case 7:
                    left = point.X;
                    bottom = point.Y;
                    break;
                case 8:
                    left = point.X;
                    break;
            }
            Dirty = true;

            if ((this.rectangle.Width < 0) || (this.rectangle.Height < 0))
            {
                int i = 1;
                i++;
            }
            SetRectangle(left, top, right - left, bottom - top);
        }


        public override bool IntersectsWith(Rectangle rectangle)
        {
            return Rectangle.IntersectsWith(rectangle);
        }

        /// <summary>
        /// Move object
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public override void Move(int deltaX, int deltaY)
        {
            rectangle.X += deltaX;
            rectangle.Y += deltaY;
            Dirty = true;
        }
        public override void PretendToMoveStart(int deltaX, int deltaY)
        {
            rectangle.X += deltaX;
            rectangle.Y += deltaY;
        }
        public override ArrayList GetCriticalPointList()
        {
            this.CriticalPointList.Clear();
            Point a = new Point(rectangle.X, rectangle.Y);
            CriticalPointList.Add(a);
            a = new Point(rectangle.X + rectangle.Width, rectangle.Y);
            CriticalPointList.Add(a);
            a = new Point(rectangle.X, rectangle.Y + rectangle.Height);
            CriticalPointList.Add(a);
            a = new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);
            CriticalPointList.Add(a);
            return CriticalPointList;

        }
        public override void PretendToMoveOver(int deltaX, int deltaY)
        {
            rectangle.X -= deltaX;
            rectangle.Y -= deltaY;
        }
        public override void Dump()
        {
            base.Dump();

            Trace.WriteLine("rectangle.X = " + rectangle.X.ToString(CultureInfo.InvariantCulture));
            Trace.WriteLine("rectangle.Y = " + rectangle.Y.ToString(CultureInfo.InvariantCulture));
            Trace.WriteLine("rectangle.Width = " + rectangle.Width.ToString(CultureInfo.InvariantCulture));
            Trace.WriteLine("rectangle.Height = " + rectangle.Height.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Normalize rectangle
        /// </summary>
        public override void Normalize()
        {
            rectangle = GetNormalizedRectangle(rectangle);
        }

        #region 保存/加载
        /// <summary>
        /// Save objevt to serialization stream
        /// </summary>
        /// <param name="info">Contains all data being written to disk</param>
        /// <param name="orderNumber">Index of the Layer being saved</param>
        /// <param name="objectIndex">Index of the drawing object in the Layer</param>
        public override void SaveToStream(SerializationInfo info, int orderNumber, int objectIndex)
        {
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryRectangle, orderNumber, objectIndex),
                rectangle);

            base.SaveToStream(info, orderNumber, objectIndex);
        }

        /// <summary>
        /// LOad object from serialization stream
        /// </summary>
        /// <param name="info"></param>
        /// <param name="orderNumber"></param>
        /// <param name="objectIndex"></param>
        public override void LoadFromStream(SerializationInfo info, int orderNumber, int objectIndex)
        {
            rectangle = (Rectangle)info.GetValue(
                                    String.Format(CultureInfo.InvariantCulture,
                                                  "{0}{1}-{2}",
                                                  entryRectangle, orderNumber, objectIndex),
                                    typeof(Rectangle));

            base.LoadFromStream(info, orderNumber, objectIndex);
        }
        #endregion

        #region Helper Functions
        #region 获取正规矩形
        /// <summary>
        /// 获取正规的矩形
        /// 解决反方向画矩形出现的问题，获取矩形左上角的坐标
        /// </summary>
        public static Rectangle GetNormalizedRectangle(int x1, int y1, int x2, int y2)
        {
            if (x2 < x1)
            {
                int tmp = x2;
                x2 = x1;
                x1 = tmp;
            }

            if (y2 < y1)
            {
                int tmp = y2;
                y2 = y1;
                y1 = tmp;
            }
            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }

        public static Rectangle GetNormalizedRectangle(Point p1, Point p2)
        {
            return GetNormalizedRectangle(p1.X, p1.Y, p2.X, p2.Y);
        }

        public static Rectangle GetNormalizedRectangle(Rectangle r)
        {
            return GetNormalizedRectangle(r.X, r.Y, r.X + r.Width, r.Y + r.Height);
        }
        #endregion

        #region 获取矩形四边的左/上距离
        /// <summary>
        /// 获取最右边的距离
        /// </summary>
        public override int GetMostRight()
        {
            Rectangle rec = GetNormalizedRectangle(Rectangle);
            return rec.X + rec.Width;

        }
        /// <summary>
        /// 获取最左边的距离
        /// </summary>
        public override int GetMostLeft()
        {
            Rectangle rec = GetNormalizedRectangle(Rectangle);
            return rec.X;
        }
        /// <summary>
        /// 获取最上边的距离
        /// </summary>
        public override int GetMostTop()
        {
            Rectangle rec = GetNormalizedRectangle(Rectangle);
            return rec.Y;
        }
        /// <summary>
        /// 获取最下边的距离
        /// </summary>
        public override int GetMostButtom()
        {
            Rectangle rec = GetNormalizedRectangle(Rectangle);
            return rec.Y + rec.Height;
        }
        #endregion

        #region 粘贴图像的位置
        /// <summary>
        /// 设置粘贴图像的位置（单个图像）
        /// </summary>
        /// <param name="mousePoint">鼠标的坐标</param>
        public override void SetSpecialStartPoint(Point mousePoint)
        {
            rectangle.X = mousePoint.X - rectangle.Width / 2;
            rectangle.Y = mousePoint.Y - rectangle.Height / 2;
        }
        /// <summary>
        /// 设置粘贴图像的位置（多个图像）
        /// </summary>
        /// <param name="mousePoint">鼠标的坐标</param>
        /// <param name="mousePoint">文件鼠标坐标</param>
        public override void SetSpecialStartPoint(Point mousePoint, Point copyPoint)
        {
            rectangle.X = rectangle.X + mousePoint.X - copyPoint.X;
            rectangle.Y = rectangle.Y + mousePoint.Y - copyPoint.Y;
        }
        #endregion

        #region 克隆
        public override DrawObject Clone()
        {
            DrawRectangle drawRectangle = new DrawRectangle();
            drawRectangle.rectangle = rectangle;

            FillDrawObjectFields(drawRectangle);
            return drawRectangle;
        }
        #endregion



        #endregion Helper Functions
        #region 设置属性
        protected override void FillDrawObjectFields(DrawObject drawObject)
        {
            base.FillDrawObjectFields(drawObject);
        }

        public override void ApplyProperties(GraphicsPropertiesBase properties)
        {
            base.ApplyProperties(properties);
            this.rectangle.Height = ((GraphicsPropertiesRectangle)properties).Rectabgle.Height;
            this.rectangle.Width = ((GraphicsPropertiesRectangle)properties).Rectabgle.Width;
            this.FillColor = ((GraphicsPropertiesRectangle)properties).FillColor;

        }

        public override void GetProperties()
        {
            if (NowProperties == null)
                NowProperties = new GraphicsPropertiesRectangle();
            ((GraphicsPropertiesRectangle)NowProperties).Rectabgle.Width = this.Rectangle.Width;
            ((GraphicsPropertiesRectangle)NowProperties).Rectabgle.Height = this.Rectangle.Height;
            ((GraphicsPropertiesRectangle)NowProperties).FillColor = this.FillColor;
            ((GraphicsPropertiesRectangle)NowProperties).Filled = this.Filled;
            base.GetProperties();
        }
        #endregion
    }
}