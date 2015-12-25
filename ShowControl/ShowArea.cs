using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using DocToolkit;
using System.Collections.Generic;
using DrawToolsDrawing.Draw;
using DrawToolsDrawing;
using System.IO;

namespace ShowControl
{
    /// <summary>
    ///用来显示的用户控件
    /// </summary>
    public partial class ShowArea : UserControl
    {
        #region Members
        private float _zoom = 1.0f;     //缩放比例
        private float _rotation = 0f;   //旋转比例
        private Point downPoint = new Point();
        public int _panX = 0;
        public int _panY;
        
        private Color _lineColor;
        private Color _fillColor;
        private bool _drawFilled = false;
        private int _lineWidth = -1;
        private Pen _currentPen;
        private DrawingPens.PenType _penType;
        private Brush _currentBrush;
        private FillBrushes.BrushType _brushType;

        private MruManager mruManager;
        private Layers _layers;
        private Form owner;
        private DocManager docManager;
        private Form myParent;
        private bool isPainting;
        public Form MyParent
        {
            get { return myParent; }
            set { myParent = value; }
        }
        public bool IsPainting
        {
            get { return isPainting; }
            set { isPainting = value; }
        }
        #endregion Members
        #region Properties
        /// <summary>
        /// Allow tools and objects to see the type of brush set
        /// </summary>
        public FillBrushes.BrushType BrushType
        {
            get { return _brushType; }
            set { _brushType = value; }
        }

        public Brush CurrentBrush
        {
            get { return _currentBrush; }
            set { _currentBrush = value; }
        }

        /// <summary>
        /// Allow tools and objects to see the type of pen set
        /// </summary>
        public DrawingPens.PenType PenType
        {
            get { return _penType; }
            set { _penType = value; }
        }

        /// <summary>
        /// Current Drawing Pen
        /// </summary>
        public Pen CurrentPen
        {
            get { return _currentPen; }
            set { _currentPen = value; }
        }

        /// <summary>
        /// Current Line Width
        /// </summary>
        public int LineWidth
        {
            get { return _lineWidth; }
            set { _lineWidth = value; }
        }

        /// <summary>
        /// Flag determines if objects will be drawn filled or not
        /// </summary>
        public bool DrawFilled
        {
            get { return _drawFilled; }
            set { _drawFilled = value; }
        }

        /// <summary>
        /// Color to draw filled objects with
        /// </summary>
        public Color FillColor
        {
            get { return _fillColor; }
            set { _fillColor = value; }
        }

        /// <summary>
        /// Color for drawing lines
        /// </summary>
        public Color LineColor
        {
            get { return _lineColor; }
            set { _lineColor = value; }
        }

        /// <summary>
        /// Current pan offset along X-axis
        /// </summary>
        public int PanX
        {
            get { return _panX; }
            set { _panX = value; }
        }

        /// <summary>
        /// Current pan offset along Y-axis
        /// </summary>
        public int PanY
        {
            get { return _panY; }
            set { _panY = value; }
        }

        /// <summary>
        /// Degrees of rotation of the drawing
        /// </summary>
        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        /// <summary>
        /// 获取或设置页面缩放比例
        /// </summary>
        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; }
        }

        /// <summary>
        /// 对文件操作
        /// </summary>
        private DocManager DocManager
        {
            get { return docManager; }
            set { docManager = value; }
        }

        /// <summary>
        ///窗体的所有者
        /// </summary>
        public Form Owner
        {
            get { return owner; }
            set { owner = value; }
        }
        /// <summary>
        /// 图层集合
        /// </summary>
        public Layers TheLayers
        {
            get { return _layers; }
            set { _layers = value; }
        }

        /// <summary>
        /// Set dirty flag (file is changed after last save operation)
        /// </summary>
        public void SetDirty()
        {
            DocManager.Dirty = true;
        }
        #endregion
        #region Constructor
        public ShowArea()
        {
            _panX = 0;
            _panY = 0;
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            DocManagerData data = new DocManagerData();
            data.FormOwner = owner;
            data.UpdateTitle = true;
            data.FileDialogFilter = "DrawTools files (*.dtl)|*.dtl|All Files (*.*)|*.*";


            docManager = new DocManager(data);
            //docManager.RegisterFileType("dtl", "dtlfile", "DrawTools File");
            docManager.LoadEvent += docManager_LoadEvent;
            docManager.OpenEvent += delegate(object sender1, OpenFileEventArgs e1)
            {
                if (e1.Succeeded)
                    mruManager.Add(e1.FileName);
                else
                    mruManager.Remove(e1.FileName);
            };

            docManager.DocChangedEvent += delegate
            {
                this.Refresh();
            };

            docManager.ClearEvent += delegate
            {
                bool haveObjects = false;
                for (int i = 0; i < this.TheLayers.Count; i++)
                {
                    if (this.TheLayers[i].Graphics.Count > 1)
                    {
                        haveObjects = true;
                        break;
                    }
                }
                if (haveObjects)
                {
                    this.TheLayers.Clear();
                    this.Refresh();
                }
            };

            mruManager = new MruManager();
        }
        #endregion 

        #region Event Handlers
        /// <summary>
        /// Draw graphic objects and group selection rectangle (optionally)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowArea_Paint(object sender, PaintEventArgs e)
        {
            //if (a == 0)
            //{
            //    e.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
            //    e.Graphics.CompositingMode = CompositingMode.SourceOver;
            //    e.Graphics.InterpolationMode = InterpolationMode.Low;
            //    e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
            //}
            Matrix mx = new Matrix();
            mx.Translate(-ClientSize.Width / 2f, -ClientSize.Height / 2f, MatrixOrder.Append);
            mx.Rotate(_rotation, MatrixOrder.Append);

            mx.Translate(ClientSize.Width / 2f + _panX, ClientSize.Height / 2f + _panY, MatrixOrder.Append);
            mx.Scale(_zoom, _zoom, MatrixOrder.Append);

            e.Graphics.Transform = mx;

            if (_layers != null)
            {
                int lc = _layers.Count;
                for (int i = 0; i < lc; i++)
                {
                    Console.WriteLine(String.Format("Layer {0} is Visible: {1}", i.ToString(), _layers[i].IsVisible.ToString()));
                    if (_layers[i].IsVisible)
                    {
                        if (_layers[i].Graphics != null)
                            _layers[i].Graphics.Draw(e.Graphics);
                    }
                }
            }
        }
        /// <summary>
        /// Back Track the Mouse to return accurate coordinates regardless of zoom or pan effects.
        /// </summary>
        /// <param name="p">Point to backtrack</param>
        /// <returns>Backtracked point</returns>
        public Point BackTrackMouse(Point p)
        {
            //if (isPainting)
            //{
            Point[] pts = new Point[] { p };
            Matrix mx = new Matrix();
            mx.Translate(-ClientSize.Width / 2f, -ClientSize.Height / 2f, MatrixOrder.Append);
            mx.Rotate(_rotation, MatrixOrder.Append);
            mx.Translate(ClientSize.Width / 2f + _panX, ClientSize.Height / 2f + _panY, MatrixOrder.Append);
            mx.Scale(_zoom, _zoom, MatrixOrder.Append);
            mx.Invert();
            mx.TransformPoints(pts);
            return pts[0];
            //}
            //else
            //{
            //    Point[] pts = new Point[] { p };
            //    Matrix mx = new Matrix();
            //    mx.Rotate(_rotation, MatrixOrder.Append);

            //    if (_zoom != lastzoom)
            //    {
            //        lastzoom = _zoom;
            //        mx.Scale(_zoom, _zoom, MatrixOrder.Append);
            //        mx.Translate(_panX, _panY, MatrixOrder.Append);
            //    }
            //    else
            //    {
            //        mx.Scale(_zoom, _zoom, MatrixOrder.Append);
            //        mx.Translate(_panX, _panY, MatrixOrder.Append);
            //    }
            //    mx.Invert();
            //    mx.TransformPoints(pts);
            //    return pts[0];
            //}
            //return new Point();
        }

        /// <summary>
        /// Mouse down.
        /// Left button down event is passed to active tool.
        /// Right button down event is handled in this class.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void ShowArea_MouseDown(object sender, MouseEventArgs e)
        //{
        //    downPoint = BackTrackMouse(e.Location);
        //}

        /// <summary>
        /// Mouse move.
        /// Moving without button pressed or with left button pressed
        /// is passed to active tool.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowArea_MouseMove(object sender, MouseEventArgs e)
        {
            Point curLoc = BackTrackMouse(e.Location);
            if (e.Button == MouseButtons.Left)
            {
                if (curLoc.X !=
                  downPoint.X)
                    _panX += curLoc.X - downPoint.X;
                if (curLoc.Y !=
                  downPoint.Y)
                    _panY += curLoc.Y - downPoint.Y;
                Invalidate();
            }
            downPoint = BackTrackMouse(e.Location);
        }
        #endregion

        private void ShowArea_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //lastPoint = BackTrackMouse(e.Location);
            if (downPoint!=e.Location)
            {
                
            }
        }

        /// <summary>
        /// 设置图像的数据源文件
        /// </summary>
        /// <param name="fileName">文件全路径（.dtl）</param>
        public void SetDataSource(string fileName) 
        {
            if (!File.Exists(fileName) && Path.GetExtension(fileName) != "dtl")
                return;
            docManager.OpenDocument(fileName);
        }

        #region 保存和加载
        /// <summary>
        /// Load document from the stream supplied by DocManager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void docManager_LoadEvent(object sender, SerializationEventArgs e)
        {
            try
            {
                string fileDlgInitDir = new FileInfo(e.FileName).DirectoryName;
                TheLayers = (Layers)e.Formatter.Deserialize(e.SerializationStream);
            }

            catch (Exception ex)
            {
                
            }
        }

        /// <summary>
        /// Save document to stream supplied by DocManager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void docManager_SaveEvent(object sender, SerializationEventArgs e)
        {
            // DocManager asks to save document to supplied stream
            try
            {
                e.Formatter.Serialize(e.SerializationStream, TheLayers);
            }
            catch (Exception ex)
            {
               
            }
        }
        #endregion

        #region 放大和缩小
        private void ztbcZoom_ValueChanged(object sender, EventArgs e)
        {
            lbZoom.Text = ztbcZoom.Value.ToString() + "%";
            _zoom = float.Parse(Convert.ToString(ztbcZoom.Value * 0.01));
            this.Refresh();
        }

        private void btnRestoreZoom_Click(object sender, EventArgs e)
        {
            ztbcZoom.Value = 100;
            ztbcZoom.Focus();
            ztbcZoom.Refresh();
        } 
        #endregion

        private void btnDrag_Click(object sender, EventArgs e)
        {
            _panX = 0;
            _panY = 0;
            Invalidate();
            ztbcZoom.Focus();
            ztbcZoom.Refresh();
        }
    }
}