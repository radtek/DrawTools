using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using DrawToolsDrawing.GraphicsProperties;
using DrawToolsDrawing.PropertiesControl;
namespace DrawToolsDrawing.Draw
{
	/// <summary>
	/// Base class for all draw objects
	/// </summary>
	[Serializable]
	public abstract class DrawObject : IComparable
    {
        #region Members
        public DrawingType drawingType; // 当前类型
        private bool selected;          // 是否选中
        private bool filled;            // 是否填充
        //public string fileDirectory;  // 文件目录


        private Pen drawpen;            // 画笔
        private Brush drawBrush;        // 笔刷

        public Point StartPoint;       // 起始坐标
        private int penWidth;           // 线宽
        private Color penColor;         // 线颜色
        private Color fillColor;        // 填充色

        private GraphicsPropertiesBase newProperties;   //新属性类
        private GraphicsPropertiesBase nowProperties;   //当前属性类
        private GraphicsPropertiesBase oldProperties;   //旧属性类
        #region properties

        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }
        public bool Filled
        {
            get { return filled; }
            set { filled = value; }
        }

        public Pen DrawPen
        {
            get { return drawpen; }
            set { drawpen = value; }
        }
        public Brush DrawBrush
        {
            get { return drawBrush; }
            set { drawBrush = value; }
        }

        //public Point StartPoint
        //{
        //    get { return startPoint; }
        //    set { startPoint = value; }
        //}
        public int PenWidth
        {
            get { return penWidth; }
            set { penWidth = value; }
        }
        public Color PenColor
        {
            get { return penColor; }
            set { penColor = value; }
        }
        public Color FillColor
        {
            get { return fillColor; }
            set { fillColor = value; }
        }

        public GraphicsPropertiesBase NewProperties
        {
            get { return newProperties; }
            set { newProperties = value; }
        }
        public GraphicsPropertiesBase NowProperties
        {
            get { return nowProperties; }
            set { nowProperties = value; }
        }
        public GraphicsPropertiesBase OldProperties
        {
            get { return oldProperties; }
            set { oldProperties = value; }
        }
        #endregion
        #endregion



        #region
		private DrawingPens.PenType _penType;
		private FillBrushes.BrushType _brushType;
		private string tipText;
        public DrawObject nextobject = null;
		// Last used property values (may be kept in the Registry)
		private static Color lastUsedColor = Color.Black;
		private static int lastUsedPenWidth = 1;

		// Entry names for serialization
        private const string entryPenColor = "PenColor";
		private const string entryPenWidth = "PenWidth";
		private const string entryPen = "DrawPen";

        private const string entryTIEDASTRING = "TIEDASTRING";
		private const string entryBrush = "DrawBrush";
		private const string entryFillColor = "FillColor";
		private const string entryFilled = "Filled";
		private const string entryZOrder = "ZOrder";
		private const string entryRotation = "Rotation";
		private const string entryTipText = "TipText";
        private const string entryTIEDAID = "TIEDAID";

		private bool dirty;
		private int _id;
		private int _zOrder;
		private int _rotation = 0;
		private Point _center;

		#endregion Members
        public string TIEDASTRING;
        public int statusnum = 0;
        public int TwoFalse = 0;
        public int refreshflag = 0;
        public int isset = 0;


		#region Properties
		/// <summary>
		/// Center of the object being drawn.
		/// </summary>
		public Point Center
		{
			get { return _center; }
			set { _center = value; }
		}

        ///// <summary>
        ///// Rotation of the object in degrees. Negative is Left, Positive is Right.
        ///// </summary>
        ///// 
        //public void addnextobject(DrawObject drawobj)
        //{
        //    this.nextobject = drawobj;
        //}


        public ArrayList CriticalPointList;
		public int Rotation
		{
			get { return _rotation; }
			set
			{
				if (value > 360)
					_rotation = value - 360;
				else if (value < -360)
					_rotation = value + 360;
				else
					_rotation = value;
			}
		}

		/// <summary>
		/// ZOrder is the order the objects will be drawn in - lower the ZOrder, the closer the to top the object is.
		/// </summary>
		public int ZOrder
		{
			get { return _zOrder; }
			set { _zOrder = value; }
		}

		/// <summary>
		/// Object ID used for Undo Redo functions
		/// </summary>
		public int ID
		{
			get { return _id; }
			set { _id = value; }
		}
        public int TIEDAID = 0;
       
		/// <summary>
		/// Set to true whenever the object changes
		/// </summary>
		public bool Dirty
		{
			get { return dirty; }
			set { dirty = value; }
		}


		public FillBrushes.BrushType BrushType
		{
			get { return _brushType; }
			set { _brushType = value; }
		}

		public DrawingPens.PenType _PenType
		{
			get { return _penType; }
			set { _penType = value; }
		}
		/// <summary>
		/// Number of Connection Points
		/// </summary>
		public virtual int ConnectionCount
		{
			get { return 0; }
		}
		/// <summary>
		/// Last used color
		/// </summary>
		public static Color LastUsedColor
		{
			get { return lastUsedColor; }
			set { lastUsedColor = value; }
		}

		/// <summary>
		/// Last used pen width
		/// </summary>
		public static int LastUsedPenWidth
		{
			get { return lastUsedPenWidth; }
			set { lastUsedPenWidth = value; }
		}

		/// <summary>
		/// Text to display when mouse is over an object
		/// </summary>
		public string TipText
		{
			get { return tipText; }
			set { tipText = value; }
		}
        
		#endregion Properties
		#region Constructor

		protected DrawObject()
		{
			// ReSharper disable DoNotCallOverridableMethodsInConstructor
			ID = GetHashCode();

            //DrawProperties = new MainPropertie();

            CriticalPointList = new ArrayList();
			// ReSharper restore DoNotCallOverridableMethodsInConstructor
           
		}
        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Initialize()
        {
        }
		#endregion

        #region Virtual Functions
        #region 克隆当前实例
        /// <summary>
        /// 克隆当前实例
        /// </summary>
        /// <returns>当前对象的副本</returns>
        public abstract DrawObject Clone(); 
        #endregion
        #region 绘图
        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="g">画布</param>
        public virtual void Draw(Graphics g)
        {

        } 
        #endregion

        #region Selection handle methods
        /// <summary>
        /// 链接点数
        /// </summary>
        public virtual int HandleCount
        {
            get { return 0; }
        }
        /// <summary>
        /// 获取指定链接点的坐标
        /// </summary>
        /// <param name="handleNumber">指定连接点</param>
        /// <returns>返回连接点的坐标</returns>
        public virtual Point GetHandle(int handleNumber)
        {
            return new Point(0, 0);
        }


        #region 设置粘贴图像的位置
        /// <summary>
        /// 设置粘贴图像的位置
        /// </summary>
        /// <param name="mousePoint">鼠标的坐标</param>
        public virtual void SetSpecialStartPoint(Point mousePoint)
        {

        } 
        #endregion

        /// <summary>
        /// Get handle rectangle by 1-based number
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns>Rectangle structure to draw the handle</returns>
        public virtual Rectangle GetHandleRectangle(int handleNumber)
        {
            Point point = GetHandle(handleNumber);
            // Take into account width of pen
            return new Rectangle(point.X - (penWidth - 3), point.Y - (penWidth - 3), 2 + penWidth / 2, 2 + penWidth / 2);
        }

        /// <summary>
        /// Draw tracker for selected object
        /// </summary>
        /// <param name="g">Graphics to draw on</param>
        public virtual void DrawTracker(Graphics g)
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
        #endregion Selection handle methods
        #region Connection Point methods
        /// <summary>
        /// 获取连接点坐标
        /// </summary>
        /// <param name="connectionNumber">连接点</param>
        /// <returns>返回连接点坐标</returns>
        public virtual Point GetConnection(int connectionNumber)
        {
            return new Point(0, 0);
        }
        /// <summary>
        /// Get connectionPoint rectangle that defines the ellipse for the requested connection
        /// </summary>
        /// <param name="connectionNumber">0-based connection number</param>
        /// <returns>Rectangle structure to draw the connection</returns>
        public virtual Rectangle GetConnectionEllipse(int connectionNumber)
        {
            Point p = GetConnection(connectionNumber);
            // Take into account width of pen
            return new Rectangle(p.X - (penWidth + 3), p.Y - (penWidth + 3), 7 + penWidth, 7 + penWidth);
        }
        public virtual void DrawConnection(Graphics g, int connectionNumber)
        {
            SolidBrush b = new SolidBrush(System.Drawing.Color.Red);
            Pen p = new Pen(System.Drawing.Color.Red, -1.0f);
            g.DrawEllipse(p, GetConnectionEllipse(connectionNumber));
            g.FillEllipse(b, GetConnectionEllipse(connectionNumber));
            p.Dispose();
            b.Dispose();
        }
        /// <summary>
        /// Draws the ellipse for the connection handles on the object
        /// </summary>
        /// <param name="g">Graphics to draw on</param>
        public virtual void DrawConnections(Graphics g)
        {
            if (!Selected)
                return;
            SolidBrush b = new SolidBrush(System.Drawing.Color.White);
            Pen p = new Pen(System.Drawing.Color.Black, -1.0f);
            for (int i = 0; i < ConnectionCount; i++)
            {
                g.DrawEllipse(p, GetConnectionEllipse(i));
                g.FillEllipse(b, GetConnectionEllipse(i));
            }
            p.Dispose();
            b.Dispose();
        }
        #endregion Connection Point methods
        /// <summary>
        /// Hit test to determine if object is hit.
        /// </summary>
        /// <param name="point">Point to test</param>
        /// <returns>			(-1)		no hit
        ///						(0)		hit anywhere
        ///						(1 to n)	handle number</returns>
        public virtual int HitTest(Point point)
        {
            return -1;
        }

        public virtual int GetMostRight()
        {
            return -1;
        }
        public virtual int GetTextType()
        {
            return -1;
        }
        public virtual int GetMostLeft()
        {
            return -1;
        }

        public virtual int GetMostTop()
        {
            return -1;
        }

        public virtual int GetMostButtom()
        {
            return -1;
        }

        /// <summary>
        /// Test whether point is inside of the object
        /// </summary>
        /// <param name="point">Point to test</param>
        /// <returns>true if in object, false if not</returns>
        protected virtual bool PointInObject(Point point)
        {
            return false;
        }


        /// <summary>
        /// Get cursor for the handle
        /// </summary>
        /// <param name="handleNumber">handle number to return cursor for</param>
        /// <returns>Cursor object</returns>
        public virtual Cursor GetHandleCursor(int handleNumber)
        {
            return Cursors.Default;
        }

        /// <summary>
        /// Test whether object intersects with rectangle
        /// </summary>
        /// <param name="rectangle">Rectangle structure to test</param>
        /// <returns>true if intersect, false if not</returns>
        public virtual bool IntersectsWith(Rectangle rectangle)
        {
            return false;
        }

        /// <summary>
        /// Move object
        /// </summary>
        /// <param name="deltaX">Distance along X-axis: (+)=Right, (-)=Left</param>
        /// <param name="deltaY">Distance along Y axis: (+)=Down, (-)=Up</param>
        public virtual void Move(int deltaX, int deltaY)
        {
        }
        public virtual void PretendToMoveStart(int deltaX, int deltaY)
        {
        }
        public virtual void PretendToMoveOver(int deltaX, int deltaY)
        {
        }

        public virtual ArrayList GetCriticalPointList()
        {
            return null;
        }

        /// <summary>
        /// Move handle to the point
        /// </summary>
        /// <param name="point">Point to Move Handle to</param>
        /// <param name="handleNumber">Handle number to move</param>
        public virtual void MoveHandleTo(Point point, int handleNumber)
        {
        }

        /// <summary>
        /// Dump (for debugging)
        /// </summary>
        public virtual void Dump()
        {
            Trace.WriteLine("");
            Trace.WriteLine(GetType().Name);
            Trace.WriteLine("Selected = " + selected.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Normalize object.
        /// Call this function in the end of object resizing.
        /// </summary>
        public virtual void Normalize()
        {
        }
        public Form DrawProperties;

        #region Save / Load methods
        /// <summary>
        /// Save object to serialization stream
        /// </summary>
        /// <param name="info">The data being written to disk</param>
        /// <param name="orderNumber">Index of the Layer being saved</param>
        /// <param name="objectIndex">Index of the object on the Layer</param>
        public virtual void SaveToStream(SerializationInfo info, int orderNumber, int objectIndex)
        {
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryPenColor, orderNumber, objectIndex),
                PenColor.ToArgb());

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryPenWidth, orderNumber, objectIndex),
                PenWidth);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryPen, orderNumber, objectIndex),
                _PenType);

            info.AddValue(
                string.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryBrush, orderNumber, objectIndex),
                BrushType);

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFillColor, orderNumber, objectIndex),
                FillColor.ToArgb());

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFilled, orderNumber, objectIndex),
                Filled);

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryZOrder, orderNumber, objectIndex),
                ZOrder);

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryRotation, orderNumber, objectIndex),
                Rotation);

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryTipText, orderNumber, objectIndex),
                tipText);

            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryTIEDAID, orderNumber, objectIndex),
                TIEDAID);

            info.AddValue(
               String.Format(CultureInfo.InvariantCulture,
                             "{0}{1}-{2}",
                             entryTIEDASTRING, orderNumber, objectIndex),
               TIEDASTRING);



        }

        /// <summary>
        /// Load object from serialization stream
        /// </summary>
        /// <param name="info">Data from disk to parse into an object</param>
        /// <param name="orderNumber">Index of the layer object resides on</param>
        /// <param name="objectData">Index of the object on the layer</param>
        public virtual void LoadFromStream(SerializationInfo info, int orderNumber, int objectData)
        {
            int n = info.GetInt32(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryPenColor, orderNumber, objectData));

            PenColor = Color.FromArgb(n);

            PenWidth = info.GetInt32(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryPenWidth, orderNumber, objectData));
            // PenWidth -=2;
            _PenType = (DrawingPens.PenType)info.GetValue(
                                            String.Format(CultureInfo.InvariantCulture,
                                                          "{0}{1}-{2}",
                                                          entryPen, orderNumber, objectData),
                                            typeof(DrawingPens.PenType));

            BrushType = (FillBrushes.BrushType)info.GetValue(
                                                string.Format(CultureInfo.InvariantCulture,
                                                              "{0}{1}-{2}",
                                                              entryBrush, orderNumber, objectData),
                                                typeof(FillBrushes.BrushType));

            n = info.GetInt32(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFillColor, orderNumber, objectData));

            FillColor = Color.FromArgb(n);
            //FillColor.Color.FromArgb(255, 128, 128, 128);
            if ((FillColor.A == 255) && (FillColor.R == 128) && (FillColor.G == 128) && (FillColor.B == 128))
            {
                FillColor = Color.FromArgb(73, 85, 97);
                PenColor = Color.FromArgb(73, 85, 97);
            }
            if (FillColor == Color.Green)
            {

            }
            Filled = info.GetBoolean(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryFilled, orderNumber, objectData));

            ZOrder = info.GetInt32(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryZOrder, orderNumber, objectData));

            TIEDAID = info.GetInt32(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryTIEDAID, orderNumber, objectData));

            TIEDASTRING = info.GetString(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryTIEDASTRING, orderNumber, objectData));
            if (TIEDASTRING != null && TIEDASTRING != "")
            {
                FillColor = Color.Lime;
                //Color = Color.Lime;
            }


            Rotation = info.GetInt32(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryRotation, orderNumber, objectData));

            tipText = info.GetString(String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}-{2}",
                              entryTipText, orderNumber, objectData));

            // Set the Pen and the Brush, if defined
            if (_PenType != DrawingPens.PenType.Generic)
                DrawPen = DrawingPens.SetCurrentPen(_PenType);
            if (BrushType != FillBrushes.BrushType.NoBrush)
                DrawBrush = FillBrushes.SetCurrentBrush(BrushType);
        }
        #endregion Save/Load methods
        #endregion Virtual Functions


        public bool IsDianBaoMa = false;
        public string DianBaoMa = "";
        public virtual int GetKind()
        {
            return -1;

        }

		

		#region Other functions

		//		private 
		/// <summary>
		/// Copy fields from this instance to cloned instance drawObject.
		/// Called from Clone functions of derived classes.
		/// </summary>
		/// <param name="drawObject">Object being cloned</param>
		protected virtual void FillDrawObjectFields(DrawObject drawObject)
		{
			drawObject.selected = selected;
			drawObject.penColor = penColor;
			drawObject.penWidth = penWidth;
			drawObject.ID = ID;
			drawObject._brushType = _brushType;
			drawObject._penType = _penType;
			drawObject.drawBrush = drawBrush;
			drawObject.drawpen = drawpen;
            drawObject.filled = filled;
			drawObject.fillColor = fillColor;
			drawObject._rotation = _rotation;
			drawObject._center = _center;
		}
		#endregion Other functions

		#region IComparable Members
		/// <summary>
		/// Returns (-1), (0), (+1) to represent the relative Z-order of the object being compared with this object
		/// </summary>
		/// <param name="obj">DrawObject that is compared to this object</param>
		/// <returns>	(-1)	if the object is less (further back) than this object.
		///				(0)	if the object is equal to this object (same level graphically).
		///				(1)	if the object is greater (closer to the front) than this object.</returns>
		public int CompareTo(object obj)
		{
			DrawObject d = obj as DrawObject;
			int x = 0;
			if (d != null)
				if (d.ZOrder == ZOrder)
					x = 0;
				else if (d.ZOrder > ZOrder)
					x = -1;
				else
					x = 1;

			return x;
		}
		#endregion IComparable Members

        #region 深克隆对象
        private object CloneObject(object o)
        {
            Type t = o.GetType();
            PropertyInfo[] properties = t.GetProperties();
            Object p = t.InvokeMember("", System.Reflection.BindingFlags.CreateInstance, null, o, null);
            foreach (PropertyInfo pi in properties)
            {
                if (pi.CanWrite)
                {
                    object value = pi.GetValue(o, null);
                    pi.SetValue(p, value, null);
                }
            }
            return p;
        }
        /// <summary>
        /// 将当前属性保存在旧属性对象中
        /// </summary>
        public void SetOldProperties() 
        {
            this.OldProperties = CloneObject(NowProperties) as GraphicsPropertiesBase;
        }
        #endregion

        #region 打开属性框
        /// <summary>
        /// 打开属性框
        /// </summary>
        /// <returns></returns>
        public bool ShowPropertiesDialog()
        {
            GetProperties();
            if (MainPropertie.ShowPropertiesDialog(this) != DialogResult.OK)
                return false;

            ApplyProperties(NewProperties);

            return true;
        } 
        #endregion

        #region 获取当前属性
        /// <summary>
        /// 获取当前属性
        /// </summary>
        public virtual void GetProperties()
        {
            NowProperties.LineWidth = this.PenWidth;
            NowProperties.LineColor = this.PenColor;
            NowProperties.StartPoint = this.StartPoint;
        }
        #endregion

        #region 应用属性
        /// <summary>
        /// 应用属性
        /// </summary>
        /// <param name="properties">需要应用的属性</param>
        public virtual void ApplyProperties(GraphicsPropertiesBase properties)
        {
            this.StartPoint = properties.StartPoint;
            this.PenColor = properties.LineColor;
            this.PenWidth = properties.LineWidth;
            this.Filled = properties.Filled;
            this.FillColor = properties.FillColor;
        } 
        #endregion
	}
}