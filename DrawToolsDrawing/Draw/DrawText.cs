using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Runtime.Serialization;
using System.Windows.Forms;
using DrawToolsDrawing.GraphicsProperties;

namespace DrawToolsDrawing.Draw
{
    /// <summary>
    /// Rectangle graphic object
    /// </summary>
    //[Serializable]
    public class DrawText : DrawObject
    {
        #region Members

        private Rectangle rectangle;
        private string note;
        private Font _font;
        private Color textColor;
        public bool IsVerticalText = false;
        private Color textBackgroundColor;
        #region Properties
        /// <summary>
        /// 文本
        /// </summary>
        public string Note
        {
            get { return note; }
            set
            {
                note = value;
                TipText = value;
            }
        }
        /// <summary>
        /// 字体
        /// </summary>
        public Font TextFont
        {
            get { return _font; }
            set { _font = value; }
        }
        /// <summary>
        /// 矩形
        /// </summary>
        protected Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }
        /// <summary>
        /// 字体颜色
        /// </summary>
        public Color TextColor
        {
            get { return textColor; }
            set { textColor = value; }
        }
        /// <summary>
        /// 背景色
        /// </summary>
        public Color TextBackgroundColor
        {
            get { return textBackgroundColor; }
            set { textBackgroundColor = value; }
        }
        private const string entryRectangle = "Rect";
        private const string entryText = "Text";
        private const string entryFontName = "FontName";
        private const string entryFontBold = "FontBold";
        private const string entryFontItalic = "FontItalic";
        private const string entryFontSize = "FontSize";
        private const string entryFontStrikeout = "FontStrikeout";
        private const string entryFontUnderline = "FontUnderline";
        #endregion
        #endregion


        #region Constructor
        public DrawText()
        {
            note = "";
            Initialize();
        }
        public DrawText(Point point, Color textColor, Color fillColor)
        {
            rectangle.X = point.X;
            rectangle.Y = point.Y;
            _font = new Font("宋体", 13, FontStyle.Regular);
            this.textColor = textColor;
            FillColor = fillColor;
            note = "";
            Initialize();
        }
        public DrawText(int x, int y)
        {
            rectangle.X = x;
            rectangle.Y = y;
            note = "";
            Initialize();
        }
        public DrawText(int x, int y, string textToDraw, Font textFont, Color textColor, bool isver)
        {
            rectangle.X = x;
            rectangle.Y = y;
            note = textToDraw;
            _font = textFont;
            PenColor = textColor;
            IsVerticalText = isver;
            Initialize();
        }
        public DrawText(Point p, GraphicsPropertiesBase gp)
        {
            rectangle.X = p.X;
            rectangle.Y = p.Y;
            ApplyProperties(gp);
            Initialize();
        }
        protected override void Initialize()
        {
            drawingType = DrawingType.DrawText;
        }
        #endregion

        #region 克隆当前实例
        public override DrawObject Clone()
        {
            DrawText drawText = new DrawText();

            drawText._font = _font;
            drawText.note = note;
            drawText.rectangle = rectangle;

            FillDrawObjectFields(drawText);
            return drawText;
        }
        #endregion

        #region 粘贴图像的位置
        /// <summary>
        /// 设置粘贴图像的位置
        /// </summary>
        /// <param name="mousePoint">鼠标的坐标</param>
        public override void SetSpecialStartPoint(Point mousePoint, Point copyPoint)
        {
            rectangle.X = mousePoint.X - rectangle.Width / 2;
            rectangle.Y = mousePoint.Y - rectangle.Height / 2;
        }
        #endregion

        #region 绘图
        public override void Draw(Graphics g)
        {
            g.PageUnit = GraphicsUnit.Pixel;
            g.SmoothingMode = SmoothingMode.HighQuality;

            Brush brushText = new SolidBrush(textColor);
            GraphicsPath gpText = new GraphicsPath();
            StringFormat format = new StringFormat();
            format.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
            SizeF sizeF = g.MeasureString(note, _font, 5000, format);

            if (IsVerticalText)
            {
                float h = 0;
                for (int i = 0; i < Note.Length; i++)
                {
                    string strTemp = Note[i].ToString();
                    gpText.AddString(strTemp, _font.FontFamily, (int)_font.Style, _font.SizeInPoints,
                                           new PointF(rectangle.X + TextFont.Size / 2, rectangle.Y + h + 2), format);
                    h += _font.SizeInPoints;
                }
                rectangle.Size = new Size(Convert.ToInt32(sizeF.Height), Convert.ToInt32(_font.Size * Note.Length));
            }
            else
            {
                gpText.AddString(Note, _font.FontFamily, (int)_font.Style, _font.SizeInPoints,
                         new PointF(rectangle.X, rectangle.Y + TextFont.Size / 2), format);
                rectangle.Size = new Size(Convert.ToInt32(sizeF.Width) - Convert.ToInt32(sizeF.Width) / 4 - Convert.ToInt32(TextFont.Size) / 2, Convert.ToInt32(sizeF.Height));
            }

            //使绘图质量最高，即消除锯齿
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;

            g.FillPath(brushText, gpText);
            brushText.Dispose();
        }
        #endregion

        #region 获取当前属性
        public override void GetProperties()
        {
            if (NowProperties == null)
                NowProperties = new GraphicsPropertiesText();
            ((GraphicsPropertiesText)NowProperties).TextColor = this.TextColor;
            ((GraphicsPropertiesText)NowProperties).TextFont = this.TextFont;
            ((GraphicsPropertiesText)NowProperties).BackGroundColor = this.FillColor;
            ((GraphicsPropertiesText)NowProperties).Note = this.Note;
            base.GetProperties();
        }
        #endregion

        #region 应用属性
        public override void ApplyProperties(GraphicsPropertiesBase properties)
        {
            base.ApplyProperties(properties);
            this.textBackgroundColor = ((GraphicsPropertiesText)properties).BackGroundColor;
            this.Note = ((GraphicsPropertiesText)properties).Note;
            this.TextColor = ((GraphicsPropertiesText)properties).TextColor;
            this.TextFont = ((GraphicsPropertiesText)properties).TextFont;
            this.IsVerticalText = ((GraphicsPropertiesText)properties).IsVerticalText;
        }
        #endregion


        /// <summary>
        /// Get number of handles
        /// </summary>
        public override int HandleCount
        {
            get { return 8; }
        }
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
        public override int GetTextType()
        {
            return 0;
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

            return Cursors.Default;

        }

        /// <summary>
        /// Move handle to new point (resizing)
        /// </summary>
        /// <param name="point"></param>
        /// <param name="handleNumber"></param>
        public override void MoveHandleTo(Point point, int handleNumber)
        {

        }

        //protected void SetRectangle(int x, int y, int width, int height)
        //{
        //    rectangle.X = x;
        //    rectangle.Y = y;
        //    rectangle.Width = width;
        //    rectangle.Height = height;
        //}

        public override bool IntersectsWith(Rectangle rectangle)
        {
            return Rectangle.IntersectsWith(rectangle);
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
            if (rectangle.Y < 0)
            {
            }
            return rectangle.Y;

        }

        public override int GetMostButtom()
        {

            if (IsVerticalText)
            {

                StringFormat format2 = StringFormat.GenericTypographic;

                float h = 0;
                for (int i = 0; i < Note.Length; i++)
                {


                    h += _font.SizeInPoints;
                }



                return rectangle.Y + rectangle.Height + (int)h;


            }
            return rectangle.Y + rectangle.Height;

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

        }

        /// <summary>
        /// Normalize rectangle
        /// </summary>
        public override void Normalize()
        {

        }

        #region 保存/打开
        /// <summary>
        /// Save objevt to serialization stream
        /// </summary>
        /// <param name="info"></param>
        /// <param name="orderNumber">Index of the Layer being saved</param>
        /// <param name="objectIndex">Index of this object in the Layer</param>
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
                              entryText, orderNumber, objectIndex),
                Note);
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFontName, orderNumber, objectIndex),
                _font.Name);
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFontBold, orderNumber, objectIndex),
                _font.Bold);
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFontItalic, orderNumber, objectIndex),
                _font.Italic);
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFontSize, orderNumber, objectIndex),
                _font.Size);
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFontStrikeout, orderNumber, objectIndex),
                _font.Strikeout);
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFontUnderline, orderNumber, objectIndex),
                _font.Underline);

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              "entryver", orderNumber, objectIndex),
                this.IsVerticalText);

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
            Note = info.GetString(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryText, orderNumber, objectIndex));
            string name = info.GetString(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFontName, orderNumber, objectIndex));
            bool bold = info.GetBoolean(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFontBold, orderNumber, objectIndex));
            bool italic = info.GetBoolean(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFontItalic, orderNumber, objectIndex));
            float size = (float)info.GetValue(
                                    String.Format(CultureInfo.InvariantCulture,
                                                  "{0}{1}-{2}",
                                                  entryFontSize, orderNumber, objectIndex),
                                    typeof(float));
            bool strikeout = info.GetBoolean(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFontStrikeout, orderNumber, objectIndex));
            bool underline = info.GetBoolean(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFontUnderline, orderNumber, objectIndex));
            try
            {
                bool ver = info.GetBoolean(
                    String.Format(CultureInfo.InvariantCulture,
                                  "{0}{1}-{2}",
                                  "entryver", orderNumber, objectIndex));
                this.IsVerticalText = ver;
            }
            catch
            {

            }
            FontStyle fs = FontStyle.Regular;
            if (bold)
                fs |= FontStyle.Bold;
            if (italic)
                fs |= FontStyle.Italic;
            if (strikeout)
                fs |= FontStyle.Strikeout;
            if (underline)
                fs |= FontStyle.Underline;
            _font = new Font(name, size, fs);

            base.LoadFromStream(info, orderNumber, objectIndex);
        }
        #endregion

        #region Helper Functions
        //public static Rectangle GetNormalizedRectangle(int x1, int y1, int x2, int y2)
        //{
        //if ( x2 < x1 )
        //{
        //    int tmp = x2;
        //    x2 = x1;
        //    x1 = tmp;
        //}

        //if ( y2 < y1 )
        //{
        //    int tmp = y2;
        //    y2 = y1;
        //    y1 = tmp;
        //}

        //return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        //}

        //public static Rectangle GetNormalizedRectangle(Point p1, Point p2)
        //{
        //return GetNormalizedRectangle(p1.X, p1.Y, p2.X, p2.Y);
        //}

        //public static Rectangle GetNormalizedRectangle(Rectangle r)
        //{
        //return GetNormalizedRectangle(r.X, r.Y, r.X + r.Width, r.Y + r.Height);
        //}
        #endregion
    }
}