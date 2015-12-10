using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using DrawToolsDrawing.GraphicsProperties;
namespace DrawToolsDrawing.Draw
{
    /// <summary>
    /// Image graphic object
    /// </summary>
    //[Serializable]
    public class DrawImage : DrawObject
    {
        #region Members
        public Rectangle rectangle;
        public Bitmap image;
        public Bitmap originalImage;

        #region Porperties
        protected Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }
        public Bitmap TheImage
        {
            get { return image; }
            set
            {
                originalImage = value;
                ResizeImage(rectangle.Width, rectangle.Height);
            }
        }
        #endregion
        #endregion


        #region 克隆
        public override DrawObject Clone()
        {
            DrawImage drawImage = new DrawImage();
            drawImage.image = image;
            drawImage.originalImage = originalImage;
            drawImage.rectangle = rectangle;
            FillDrawObjectFields(drawImage);
            return drawImage;
        }
        #endregion

        public int subpictrue = 0;



        #region Constructor
        public DrawImage()
        {
            SetRectangle(0, 0, 1, 1);
            Initialize();
        }

        public DrawImage(int x, int y)
        {
            rectangle.X = x;
            rectangle.Y = y;
            rectangle.Width = 1;
            rectangle.Height = 1;
            Initialize();
        }
        public DrawImage(int x, int y, int width, int height)
        {
            rectangle.X = x;
            rectangle.Y = y;
            rectangle.Width = width;
            rectangle.Height = height;
            Initialize();
        }
        public DrawImage(int x, int y, Bitmap image)
        {
            rectangle.X = x;
            rectangle.Y = y;
            //  _image = (Bitmap)image.Clone();  frank

            image = (Bitmap)image.Clone();
            SetRectangle(rectangle.X, rectangle.Y, image.Width, image.Height);
            Center = new Point(x + (image.Width / 2), y + (image.Height / 2));
            TipText = String.Format("Image Center @ {0}, {1}", Center.X, Center.Y);
            Initialize();
        }
        #endregion

        //Rectangle outsiderec;
        public Color outsidecolor = System.Drawing.Color.White;

        public string StringProperties = "";
        public override int GetKind()
        {
            return 2;

        }

        #region 绘图
        public override void Draw(Graphics g)
        {
            Matrix mSave = g.Transform;
            if (image == null)
            {
                Pen p = new Pen(Color.Black, -1f);
                g.DrawRectangle(p, rectangle);
            }
            else
            {
                g.DrawImage(image, rectangle);
            }

            if (Rotation != 0)
            {
                Matrix m = mSave.Clone();
                m.RotateAt(Rotation, new PointF(rectangle.Left + (rectangle.Width / 2), rectangle.Top + (rectangle.Height / 2)), MatrixOrder.Append);
                g.Transform = m;
            }
            g.Transform = mSave;
        }
        #endregion

        protected void SetRectangle(int x, int y, int width, int height)
        {
            rectangle.X = x;
            rectangle.Y = y;
            rectangle.Width = width;
            rectangle.Height = height;
        }
        public override int GetMostRight()
        {
            return rectangle.X + rectangle.Width;

        }

        public override int GetMostLeft()
        {
            return rectangle.X;
        }

        public override int GetMostTop()
        {
            return rectangle.Y;
        }

        public override int GetMostButtom()
        {
            return rectangle.Y + rectangle.Height;
        }
        /// <summary>
        /// Get number of handles
        /// </summary>
        public override int HandleCount
        {
            get { return 8; }
        }

        /// <summary>
        /// Get handle point by 1-based number
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public override Point GetHandle(int handleNumber)
        {
            int x, y, xCenter, yCenter;

            xCenter = rectangle.X + rectangle.Width / 2;
            yCenter = rectangle.Y + rectangle.Height / 2;
            x = rectangle.X;
            y = rectangle.Y;

            switch (handleNumber)
            {
                case 1:
                    x = rectangle.X;
                    y = rectangle.Y;
                    break;
                case 2:
                    x = xCenter;
                    y = rectangle.Y;
                    break;
                case 3:
                    x = rectangle.Right;
                    y = rectangle.Y;
                    break;
                case 4:
                    x = rectangle.Right;
                    y = yCenter;
                    break;
                case 5:
                    x = rectangle.Right;
                    y = rectangle.Bottom;
                    break;
                case 6:
                    x = xCenter;
                    y = rectangle.Bottom;
                    break;
                case 7:
                    x = rectangle.X;
                    y = rectangle.Bottom;
                    break;
                case 8:
                    x = rectangle.X;
                    y = yCenter;
                    break;
            }
            return new Point(x, y);
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
            return rectangle.Contains(point);
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
            if (((right - left) > 10) && ((bottom - top) > 10))
            {
                SetRectangle(left, top, right - left, bottom - top);
            }
            else
            {
                SetRectangle(left, top, 10, 10);

            }
            ResizeImage(rectangle.Width, rectangle.Height);
        }

        public void ResizeImage(int width, int height)
        {
            if (width > 0 && height > 0)
            {
                if (originalImage != null)
                {
                    Bitmap b = new Bitmap(originalImage, new Size(width, height));
                    image = (Bitmap)b.Clone();
                    b.Dispose();
                }
            }
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
            rectangle = DrawRectangle.GetNormalizedRectangle(rectangle);
        }

        #region 保存/加载
        private const string entryRectangle = "Rect";
        private const string entryImage = "Image";
        private const string entryImageOriginal = "OriginalImage";
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
                              entryImage, orderNumber, objectIndex),
                image);
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryImageOriginal, orderNumber, objectIndex),
                originalImage);

            base.SaveToStream(info, orderNumber, objectIndex);
        }
        public override void LoadFromStream(SerializationInfo info, int orderNumber, int objectIndex)
        {
            rectangle = (Rectangle)info.GetValue(
                        String.Format(CultureInfo.InvariantCulture,
                                                  "{0}{1}-{2}",
                                                  entryRectangle, orderNumber, objectIndex),
                        typeof(Rectangle));
            image = (Bitmap)info.GetValue(
                        String.Format(CultureInfo.InvariantCulture,
                                      "{0}{1}-{2}",
                                      entryImage, orderNumber, objectIndex),
                        typeof(Bitmap));

            originalImage = (Bitmap)info.GetValue(
                        String.Format(CultureInfo.InvariantCulture,
                          "{0}{1}-{2}",
                          entryImageOriginal, orderNumber, objectIndex),
                        typeof(Bitmap));
            base.LoadFromStream(info, orderNumber, objectIndex);
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

        #region Helper Functions
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
        #endregion Helper Functions

        #region 设置属性
        protected override void FillDrawObjectFields(DrawObject drawObject)
        {
            base.FillDrawObjectFields(drawObject);
        }

        public override void ApplyProperties(GraphicsPropertiesBase properties)
        {
            base.ApplyProperties(properties);
            this.rectangle.Height = ((GraphicsPropertiesImage)properties).Rectabgle.Height;
            this.rectangle.Width = ((GraphicsPropertiesImage)properties).Rectabgle.Width;
            this.FillColor = ((GraphicsPropertiesImage)properties).FillColor;
            if (((GraphicsPropertiesImage)properties).Bitmap != null)
            {
               this.image= this.TheImage = ((GraphicsPropertiesImage)properties).Bitmap;
            }
        }

        public override void GetProperties()
        {
            if (NowProperties == null)
                NowProperties = new GraphicsPropertiesImage();
            ((GraphicsPropertiesImage)NowProperties).Rectabgle.Width = this.Rectangle.Width;
            ((GraphicsPropertiesImage)NowProperties).Rectabgle.Height = this.Rectangle.Height;
            ((GraphicsPropertiesImage)NowProperties).FillColor = this.FillColor;
            ((GraphicsPropertiesImage)NowProperties).Filled = this.Filled;
            ((GraphicsPropertiesImage)NowProperties).Bitmap = this.image;
            base.GetProperties();
        }
        #endregion
    }
}