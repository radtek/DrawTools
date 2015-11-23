using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using DocToolkit;
using System.Collections.Generic;
using DrawTools.Command;
using DrawTools.Tools;
using DrawToolsDrawing.Draw;
using DrawToolsDrawing;

namespace DrawTools
{
    /// <summary>
    /// Working area.
    /// Handles mouse input and draws graphics objects.
    /// </summary>
    public partial class DrawArea : UserControl
    {
        #region Constructor, Dispose
        public DrawArea()
        {
            // create list of Layers, with one default active visible layer

            _panning = false;
            _panX = 0;
            _panY = 0;
            switchMachineSize = 2;
            InitializeComponent();

        }


        #endregion Constructor, Dispose


        #region Members
        private int switchMachineSize;
        public float _zoom = 1.0f;
        private float _rotation = 0f;
        public int _panX = 0;
        public int _panY;
        private int _originalPanY;
        private bool _panning = false;
        private Point lastPoint;
        private Point copyPoint;
        private Color _lineColor = Color.Black;
        private Color _fillColor = Color.White;
        private bool _drawFilled = false;
        private int _lineWidth = -1;
        private Pen _currentPen;
        private DrawingPens.PenType _penType;
        private Brush _currentBrush;
        private FillBrushes.BrushType _brushType;

        // Define the Layers collection
        private Layers _layers;
        private MainForm owner;
        private DrawToolType activeTool; // active drawing tool
        private Tool[] tools; // array of tools

        // Information about owner form
        private DocManager docManager;
        private ContextMenuStrip m_ContextMenu;
        // group selection rectangle
        private Rectangle netRectangle;
        private bool drawNetRectangle = false;

        private Form myparent;

        private bool isPainting;

        public int SwitchMachineSize
        {
            get { return switchMachineSize; }
            set { switchMachineSize = value; }
        }

        public Form MyParent
        {
            get { return myparent; }
            set { myparent = value; }
        }
        public bool IsPainting
        {
            get { return isPainting; }
            set { isPainting = value; }
        }


        private UndoManager undoManager;
        #endregion Members
        public float lastzoom = 1.0F;
        public float smallestzoom;

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
        /// Original Y position - used when panning
        /// </summary>
        public int OriginalPanY
        {
            get { return _originalPanY; }
            set { _originalPanY = value; }
        }

        /// <summary>
        /// Flag is true if panning active
        /// </summary>
        public bool Panning
        {
            get { return _panning; }
            set { _panning = value; }
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
        /// Current Zoom factor
        /// </summary>
        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; }
        }

        /// <summary>
        /// Group selection rectangle. Used for drawing.
        /// </summary>
        public Rectangle NetRectangle
        {
            get { return netRectangle; }
            set { netRectangle = value; }
        }

        /// <summary>
        /// Flag is set to true if group selection rectangle should be drawn.
        /// </summary>
        public bool DrawNetRectangle
        {
            get { return drawNetRectangle; }
            set { drawNetRectangle = value; }
        }


        /// <summary>
        /// Reference to DocManager
        /// </summary>
        public DocManager DocManager
        {
            get { return docManager; }
            set { docManager = value; }
        }

        /// <summary>
        /// Active drawing tool.
        /// </summary>
        public DrawToolType ActiveTool
        {
            get { return activeTool; }
            set { activeTool = value; }
        }
        /// <summary>
        /// Reference to the owner form
        /// </summary>
        public MainForm Owner
        {
            get { return owner; }
            set { owner = value; }
        }
        /// <summary>
        /// List of Layers in the drawing
        /// </summary>
        public Layers TheLayers
        {
            get { return _layers; }
            set { _layers = value; }
        }
        public UndoManager UndoManager
        {
            get { return undoManager; }
            set { undoManager = value; }
        }
        /// <summary>
        /// Return True if Undo operation is possible
        /// </summary>
        public bool CanUndo
        {
            get
            {
                if (undoManager != null)
                {
                    return undoManager.CanUndo;
                }

                return false;
            }
        }

        /// <summary>
        /// Return True if Redo operation is possible
        /// </summary>
        public bool CanRedo
        {
            get
            {
                if (undoManager != null)
                {
                    return undoManager.CanRedo;
                }

                return false;
            }
        }
        /// <summary>
        /// Set dirty flag (file is changed after last save operation)
        /// </summary>
        public void SetDirty()
        {
            DocManager.Dirty = true;
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Draw graphic objects and group selection rectangle (optionally)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        public uint a = 0;
        private void DrawArea_Paint(object sender, PaintEventArgs e)
        {
            if (a == 0)
            {
                e.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
                e.Graphics.CompositingMode = CompositingMode.SourceOver;
                e.Graphics.InterpolationMode = InterpolationMode.Low;
                e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
                // e.Graphics.
            }
            // a= 0;
            a = 1;
            ;
            if ((a % 40) != 0)
            {
                Matrix mx = new Matrix();
                mx.Translate(-ClientSize.Width / 2f, -ClientSize.Height / 2f, MatrixOrder.Append);
                mx.Rotate(_rotation, MatrixOrder.Append);
                mx.Translate(ClientSize.Width / 2f + _panX, ClientSize.Height / 2f + _panY, MatrixOrder.Append);
                mx.Scale(_zoom, _zoom, MatrixOrder.Append);
                e.Graphics.Transform = mx;
                //// Determine center of ClientRectangle
                //Point centerRectangle = new Point();

                //centerRectangle.X = ClientRectangle.Left + ClientRectangle.Width / 2;
                //centerRectangle.Y = ClientRectangle.Top + ClientRectangle.Height / 2;
                //// Get true center point
                //centerRectangle = BackTrackMouse(centerRectangle);
                //// Determine offset from current mouse position
                SolidBrush brush = new SolidBrush(Color.Black);
                ////SolidBrush brush = new SolidBrush(Color.FromArgb(255, 255, 255));
                //e.Graphics.FillRectangle(brush,ClientRectangle);
                //// Draw objects on each layer, in succession so we get the correct layering. Only draw layers that are visible
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

                DrawNetSelection(e.Graphics);
                // this.BackColor = Color;
                brush.Dispose();
            }
        }

        public void DrawArea_Show(object sender, PaintEventArgs e)
        {
            Matrix mx = new Matrix();
            //mx.Translate(-ClientSize.Width / 2f, -ClientSize.Height / 2f, MatrixOrder.Prepend);
            // 
            if (a == 0)
            {
                //    mx.Translate(_panX, _panY, MatrixOrder.Append);
                e.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
                e.Graphics.CompositingMode = CompositingMode.SourceOver;
                e.Graphics.InterpolationMode = InterpolationMode.Low;
                e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
                // e.Graphics.
            }
            // a= 0;
            a = 1;
            ;
            if ((a % 40) != 0)
            {
                //  mx.Translate(-ClientSize.Width / 2f, -ClientSize.Height / 2f, MatrixOrder.Append);
                mx.Rotate(_rotation, MatrixOrder.Append);
                // mx.Translate(ClientSize.Width / 2f + _panX, ClientSize.Height / 2f + _panY, MatrixOrder.Append);
                //  mx.Rotate(_rotation, MatrixOrder.Append);

                if (_zoom != lastzoom)
                {
                    //float X = ((FT_Status)this.ParentForm).wheelX;
                    //float Y = ((FT_Status)this.ParentForm).wheelY;
                    ////this.textBox5.Text = X.ToString();
                    //// this.textBox6.Text = Y.ToString();
                    //if (_zoom > lastzoom)
                    //{
                    //    _panX -= (int)(((_zoom - lastzoom) * (X - _panX)) / lastzoom);
                    //    _panY -= (int)(((_zoom - lastzoom) * (Y - _panY)) / lastzoom);

                    //}
                    //else
                    //{

                    //    _panX += (int)(((lastzoom - _zoom) * (X - _panX)) / lastzoom);
                    //    _panY += (int)(((lastzoom - _zoom) * (Y - _panY)) / lastzoom);
                    //}


                    lastzoom = _zoom;

                    mx.Scale(_zoom, _zoom, MatrixOrder.Append);
                    mx.Translate(_panX, _panY, MatrixOrder.Append);
                }

                else
                {
                    mx.Scale(_zoom, _zoom, MatrixOrder.Append);
                    mx.Translate(_panX, _panY, MatrixOrder.Append);
                }

                e.Graphics.Transform = mx;
                Point centerRectangle = new Point();

                centerRectangle.X = ClientRectangle.Left + ClientRectangle.Width / 2;
                centerRectangle.Y = ClientRectangle.Top + ClientRectangle.Height / 2;
                // Get true center point
                centerRectangle = BackTrackMouse(centerRectangle);
                // Determine offset from current mouse position
                SolidBrush brush = new SolidBrush(Color.Black);
                //SolidBrush brush = new SolidBrush(Color.FromArgb(255, 255, 255));
                e.Graphics.FillRectangle(brush,
                             ClientRectangle);
                // Draw objects on each layer, in succession so we get the correct layering. Only draw layers that are visible
                if (_layers != null)
                {
                    int lc = _layers.Count;
                    for (int i = 0; i < lc; i++)
                    {
                        //Console.WriteLine(String.Format("Layer {0} is Visible: {1}", i.ToString(), _layers[i].IsVisible.ToString()));
                        if (_layers[i].IsVisible)
                        {
                            if (_layers[i].Graphics != null)
                                _layers[i].Graphics.Draw(e.Graphics);
                        }
                    }
                }

                DrawNetSelection(e.Graphics);
                brush.Dispose();
            }
        }

        /// <summary>
        /// Back Track the Mouse to return accurate coordinates regardless of zoom or pan effects.
        /// Courtesy of BobPowell.net <seealso cref="http://www.bobpowell.net/backtrack.htm"/>
        /// </summary>
        /// <param name="p">Point to backtrack</param>
        /// <returns>Backtracked point</returns>
        public Point BackTrackMouse(Point p)
        {

            if (isPainting)
            {
                //Backtrack the mouse...
                Point[] pts = new Point[] { p };
                Matrix mx = new Matrix();
                mx.Translate(-ClientSize.Width / 2f, -ClientSize.Height / 2f, MatrixOrder.Append);
                mx.Rotate(_rotation, MatrixOrder.Append);
                mx.Translate(ClientSize.Width / 2f + _panX, ClientSize.Height / 2f + _panY, MatrixOrder.Append);
                mx.Scale(_zoom, _zoom, MatrixOrder.Append);
                mx.Invert();
                mx.TransformPoints(pts);
                return pts[0];
            }
            else
            {

                // Backtrack the mouse...
                Point[] pts = new Point[] { p };
                Matrix mx = new Matrix();


                mx.Rotate(_rotation, MatrixOrder.Append);
                // mx.Translate(ClientSize.Width / 2f + _panX, ClientSize.Height / 2f + _panY, MatrixOrder.Append);
                //  mx.Rotate(_rotation, MatrixOrder.Append);

                if (_zoom != lastzoom)
                {
                    //float X = ((FT_Status)this.ParentForm).wheelX;
                    //float Y = ((FT_Status)this.ParentForm).wheelY;
                    ////this.textBox5.Text = X.ToString();
                    //// this.textBox6.Text = Y.ToString();
                    //if (_zoom > lastzoom)
                    //{
                    //    _panX -= (int)(((_zoom - lastzoom) * (X - _panX)) / lastzoom);
                    //    _panY -= (int)(((_zoom - lastzoom) * (Y - _panY)) / lastzoom);
                    //}
                    //else
                    //{
                    //    _panX += (int)(((lastzoom - _zoom) * (X - _panX)) / lastzoom);
                    //    _panY += (int)(((lastzoom - _zoom) * (Y - _panY)) / lastzoom);
                    //}


                    lastzoom = _zoom;

                    mx.Scale(_zoom, _zoom, MatrixOrder.Append);
                    mx.Translate(_panX, _panY, MatrixOrder.Append);

                }

                else
                {
                    mx.Scale(_zoom, _zoom, MatrixOrder.Append);
                    mx.Translate(_panX, _panY, MatrixOrder.Append);

                }

                mx.Invert();
                mx.TransformPoints(pts);

                return pts[0];
            }
        }

        /// <summary>
        /// Mouse down.
        /// Left button down event is passed to active tool.
        /// Right button down event is handled in this class.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawArea_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = BackTrackMouse(e.Location);
            if (e.Button ==
              MouseButtons.Left)
                tools[(int)activeTool].OnMouseDown(this, e);
            else if (e.Button ==
                 MouseButtons.Right && isPainting)
            {
                if (_panning)
                    _panning = false;
                if (activeTool == DrawToolType.PolyLine || activeTool == DrawToolType.Connector)
                    tools[(int)activeTool].OnMouseDown(this, e);
                ActiveTool = DrawToolType.Pointer;
                OnContextMenu(e);
            }
        }



        /// <summary>
        /// Mouse move.
        /// Moving without button pressed or with left button pressed
        /// is passed to active tool.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPainting)
            {
                Point curLoc = BackTrackMouse(e.Location);
                if (e.Button == MouseButtons.Left ||
                  e.Button == MouseButtons.None)
                    if (e.Button == MouseButtons.Left && _panning)
                    {
                        if (curLoc.X !=
                          lastPoint.X)
                            _panX += curLoc.X - lastPoint.X;
                        if (curLoc.Y !=
                          lastPoint.Y)
                            _panY += curLoc.Y - lastPoint.Y;
                        Invalidate();
                    }
                    else
                        tools[(int)activeTool].OnMouseMove(this, e);
                else
                    Cursor = Cursors.Default;
                lastPoint = BackTrackMouse(e.Location);
            }
            else
            {
                Point curLoc = BackTrackMouse(e.Location);
                //if ((this.Parent as FT_Status).hasParentSizeChange)
                //{
                //    (this.Parent as FT_Status).hasParentSizeChange = false;
                //    return;
                //}

                if (e.Button == MouseButtons.Left ||
                  e.Button == MouseButtons.None)
                    if (e.Button == MouseButtons.Left && _panning)
                    {
                        Point yuandian = new Point(0, 0);
                        Point zhongdian = new Point(0 + this.Width, 0 + this.Height);
                        if (Zoom != smallestzoom)
                        {
                            if (BackTrackMouse(yuandian).X <= (this._layers[0].TheMostLeft - 400 / _zoom))
                            {

                                if (curLoc.X > lastPoint.X)
                                {
                                    if (curLoc.Y !=
                              lastPoint.Y)
                                        _panY += (int)((curLoc.Y - lastPoint.Y) * _zoom);
                                    Invalidate();

                                    lastPoint = BackTrackMouse(e.Location);
                                    return;
                                }
                            }
                            else if (BackTrackMouse(zhongdian).X >= (this._layers[0].TheMostRight + 400 / _zoom))
                            {

                                if (curLoc.X < lastPoint.X)
                                {
                                    if (curLoc.Y !=
                              lastPoint.Y)
                                        _panY += (int)((curLoc.Y - lastPoint.Y) * _zoom);
                                    Invalidate();

                                    lastPoint = BackTrackMouse(e.Location);
                                    return;
                                }


                            }
                            else if (BackTrackMouse(yuandian).Y <= (this._layers[0].TheMostTop - 400 / _zoom))
                            {
                                if (curLoc.Y > lastPoint.Y)
                                {
                                    if (curLoc.X !=
                            lastPoint.X)
                                        _panX += (int)((curLoc.X - lastPoint.X) * _zoom);

                                    Invalidate();
                                    lastPoint = BackTrackMouse(e.Location);
                                    return;
                                }

                            }
                            else if (BackTrackMouse(zhongdian).Y >= (this._layers[0].TheMostButtom + 400 / _zoom))
                            {
                                if (curLoc.Y < lastPoint.Y)
                                {
                                    if (curLoc.X !=
                            lastPoint.X)
                                        _panX += (int)((curLoc.X - lastPoint.X) * _zoom);

                                    Invalidate();
                                    lastPoint = BackTrackMouse(e.Location);
                                    return;
                                }

                            }

                            if (curLoc.X !=
                              lastPoint.X)
                                _panX += (int)((curLoc.X - lastPoint.X) * _zoom);
                            if (curLoc.Y !=
                              lastPoint.Y)
                                _panY += (int)((curLoc.Y - lastPoint.Y) * _zoom);
                            Invalidate();
                        }
                    }
                    else
                        Cursor = Cursors.Default;

                lastPoint = BackTrackMouse(e.Location);

                if (e.Button != MouseButtons.Left)
                    tools[(int)activeTool].OnMouseMove(this, e);
            }
        }

        /// <summary>
        /// Mouse up event.
        /// Left button up event is passed to active tool.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawArea_MouseUp(object sender, MouseEventArgs e)
        {

            //lastPoint = BackTrackMouse(e.Location);
            if (e.Button ==
              MouseButtons.Left)
            {
                //this.AddCommandToHistory(new CommandAdd(this.TheLayers[al].Graphics[0]));
                tools[(int)activeTool].OnMouseUp(this, e);
            }
            //DrawArea_Paint();
            a = 1;
            Invalidate();

        }
        #endregion

        #region Other Functions
        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="owner">Reference to the owner form</param>
        /// <param name="docManager">Reference to Document manager</param>

        public void Initialize(DocManager docManager)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            Invalidate();
            // Keep reference to owner form
            //Owner = owner;
            DocManager = docManager;

            // set default tool
            activeTool = DrawToolType.Pointer;

            // Create undo manager
            undoManager = new UndoManager(_layers);

            // create array of drawing tools
            tools = new Tool[(int)DrawToolType.NumberOfDrawTools];
            tools[(int)DrawToolType.Pointer] = new ToolPointer();
            (tools[(int)DrawToolType.Pointer] as ToolPointer).parentContorl = this;
            //Owner.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FT_Status_Child_FormClosing);
            tools[(int)DrawToolType.Rectangle] = new ToolRectangle();
            tools[(int)DrawToolType.Ellipse] = new ToolEllipse();
            tools[(int)DrawToolType.Line] = new ToolLine();
            tools[(int)DrawToolType.PolyLine] = new ToolPolyLine();
            tools[(int)DrawToolType.Polygon] = new ToolPolygon();
            tools[(int)DrawToolType.Text] = new ToolText();
            tools[(int)DrawToolType.Image] = new ToolImage(false, null);
            tools[(int)DrawToolType.Connector] = new ToolConnector();
            tools[(int)DrawToolType.StationTrack] = new ToolStationTrack();
            tools[(int)DrawToolType.Turnout] = new ToolTurnout();
            // tools[(int)DrawToolType.SwitchMachine] = new ToolSwitchMachine();

            LineColor = Color.White;
            FillColor = Color.Black;
            LineWidth = -1;
        }

        public void SetImageName(bool isCustomImage, string imageName)
        {

            tools[(int)DrawToolType.Image] = new ToolImage(isCustomImage, imageName);
        }
        /// <summary>
        /// Add command to history.
        /// </summary>
        public void AddCommandToHistory(Command.Command command)
        {
            undoManager.AddCommandToHistory(command);
        }

        /// <summary>
        /// Clear Undo history.
        /// </summary>
        public void ClearHistory()
        {
            undoManager.ClearHistory();
        }

        /// <summary>
        /// Undo
        /// </summary>
        public void Undo()
        {
            undoManager.Undo();
            Refresh();
        }

        /// <summary>
        /// Redo
        /// </summary>
        public void Redo()
        {
            undoManager.Redo();
            Refresh();
        }
        public List<DrawObject> PrepareCopyObjectList = null;
        public List<DrawObject> FormalCopyObjectList = null;
        public DrawObject PrepareHitProject = null;
        /// <summary>
        ///  Draw group selection rectangle
        /// </summary>
        /// <param name="g"></param>
        public void DrawNetSelection(Graphics g)
        {
            if (!DrawNetRectangle)
                return;

            ControlPaint.DrawFocusRectangle(g, NetRectangle, Color.Black, Color.Transparent);
        }

        /// <summary>
        /// Right-click handler
        /// </summary>
        /// <param name="e"></param>
        private void OnContextMenu(MouseEventArgs e)
        {
            // Change current selection if necessary

            Point point = BackTrackMouse(new Point(e.X, e.Y));
            Point menuPoint = new Point(e.X, e.Y);
            int al = _layers.ActiveLayerIndex;
            int n = _layers[al].Graphics.Count;
            List<DrawObject> o = new List<DrawObject>();

            for (int i = n - 1; i >= 0; i--)
            {
                if (_layers[al].Graphics[i].HitTest(point) == 0)
                {
                    PrepareHitProject = _layers[al].Graphics[i];
                }
                if (_layers[al].Graphics[i].Selected)
                {
                    o.Add(_layers[al].Graphics[i].Clone());
                }
            }
            PrepareCopyObjectList = o;
            if (PrepareHitProject == null && PrepareCopyObjectList.Count == 0)
            {
                _layers[al].Graphics.UnselectAll();
            }

            Refresh();


            // Show context menu.
            // Context menu items are filled from owner form Edit menu items.
            m_ContextMenu = new ContextMenuStrip();

            int nItems = owner.ContextParent.DropDownItems.Count;

            // Read Edit items and move them to context menu.
            // Since every move reduces number of items, read them in reverse order.
            // To get items in direct order, insert each of them to beginning.
            for (int i = nItems - 1; i >= 0; i--)
            {
                m_ContextMenu.Items.Insert(0, owner.ContextParent.DropDownItems[i]);
            }

            // Show context menu for owner form, so that it handles items selection.
            // Convert pointscroll from this window coordinates to owner's coordinates.
            point.X += this.Left;
            point.Y += this.Top;

            Point org = new Point(e.X, e.Y);
            m_ContextMenu.Show(owner, org);

            Owner.SetStateOfControls();  // enable/disable menu items

            // Context menu is shown, but owner's Edit menu is now empty.
            // Subscribe to context menu Closed event and restore items there.
            m_ContextMenu.Closed += delegate(object sender, ToolStripDropDownClosedEventArgs args)
            {
                if (m_ContextMenu != null)      // precaution
                {
                    nItems = m_ContextMenu.Items.Count;

                    for (int k = nItems - 1; k >= 0; k--)
                    {
                        owner.ContextParent.DropDownItems.Insert(0, m_ContextMenu.Items[k]);
                    }
                }
            };

        }
        #endregion

        public void CutObject()
        {
            CopyObject();
            this.TheLayers[this.TheLayers.ActiveLayerIndex].Graphics.DeleteSelection();
            this.TheLayers[this.TheLayers.ActiveLayerIndex].Graphics.UnselectAll();
            this.Refresh();

        }
        public void CopyObject()
        {
            this.FormalCopyObjectList = this.PrepareCopyObjectList;
            this.copyPoint = this.lastPoint;
            this.PrepareCopyObjectList = null;
            this.Refresh();
        }
        public void PasteObject()
        {
            if (FormalCopyObjectList != null)
            {
                int al = this.TheLayers.ActiveLayerIndex;
                this.TheLayers[al].Graphics.UnselectAll();

                foreach (DrawObject FormalCopyObject in FormalCopyObjectList)
                {
                    DrawObject FormalCopyObjectClone = FormalCopyObject.Clone();
                    FormalCopyObjectClone.SetSpecialStartPoint(this.lastPoint);
                    FormalCopyObjectClone.Selected = true;
                    FormalCopyObjectClone.Dirty = true;
                    int objectID = 0;
                    // Set the object id now
                    for (int i = 0; i < this.TheLayers.Count; i++)
                    {
                        objectID = +this.TheLayers[i].Graphics.Count;
                    }
                    objectID++;
                    FormalCopyObjectClone.ID = objectID;
                    this.TheLayers[al].Graphics.Add(FormalCopyObjectClone);
                }

                this.Capture = true;
                this.Refresh();

            }
        }

        private void DrawArea_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            lastPoint = BackTrackMouse(e.Location);
            if (e.Button ==
              MouseButtons.Left)
                tools[(int)activeTool].OnMouseDoubleClick(this, e);
            /* else if (e.Button ==
                  MouseButtons.Right)
             {
                 if (_panning)
                     _panning = false;
                 if (activeTool == DrawToolType.PolyLine || activeTool == DrawToolType.Connector)
                     tools[(int)activeTool].OnMouseDown(this, e);
                 ActiveTool = DrawToolType.Pointer;
                 OnContextMenu(e);
             }*/
        }

        private void DrawArea_Load(object sender, EventArgs e)
        {

        }

        public event EventHandler LargeSmallEvent;
        private void btnLarge_Click(object sender, EventArgs e)
        {
            if (LargeSmallEvent != null)
                LargeSmallEvent(1, new EventArgs());
        }

        private void btnSmall_Click(object sender, EventArgs e)
        {
            if (LargeSmallEvent != null)
                LargeSmallEvent(2, new EventArgs());
        }

        //public void FT_Status_Child_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    (tools[(int)DrawToolType.Pointer] as ToolPointer).tpform.Dispose();
        //    (tools[(int)DrawToolType.Pointer] as ToolPointer).tpform = null;
        //}


        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem is ToolStripMenuItem)
            {
                if (e.ClickedItem.Tag is ZttMenuEventArgs)
                {
                    zttMenuEven(e.ClickedItem.Tag as ZttMenuEventArgs);
                }
            }
        }

        public void Item_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Tag is ZttMenuEventArgs)
            {
                zttMenuEven(e.ClickedItem.Tag as ZttMenuEventArgs);
            }
        }

        public event ZttMenuEventHandler zttMenuEven;

        public delegate void ZttMenuEventHandler(ZttMenuEventArgs args);

        public class ZttMenuEventArgs
        {
            public string devType;
            public string devName;
            public int menuID;
        }

        private void DrawArea_MouseLeave(object sender, EventArgs e)
        {
            tools[(int)activeTool].MouseLeave(this, e);
        }

        public List<DrawObject> GetSelectionDrawObject(Layer layer) 
        {
            List<DrawObject> listDrawObject=new List<DrawObject> ();
            for (int i = 0; i < layer.Graphics.graphicsList.Count; i++)
            {
                DrawObject drawObject=(DrawObject)layer.Graphics.graphicsList[i];
                if (drawObject.Selected)
                    listDrawObject.Add(drawObject);
            }
            return listDrawObject;
        }
    }
}