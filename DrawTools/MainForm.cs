using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Serialization;
using System.Security;
using System.Windows.Forms;
using DocToolkit;
using System.IO;

using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Collections.Generic;
using DrawTools.Command;
using DrawToolsDrawing.Draw;
using DrawToolsDrawing;

namespace DrawTools
{
    public partial class MainForm : Form
    {
        #region Members
        private DrawArea drawArea;
        private DocManager docManager;
        private DragDropManager dragDropManager;
        private MruManager mruManager;
       // private AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl axDrawingControl;
        private string argumentFile = ""; // file name from command line

        private const string registryPath = "Software\\AlexF\\DrawTools";

        private bool _controlKey = false;
        private bool _panMode = false;
       // private SvgCreater svgCreater;
        #endregion

        #region Properties
        /// <summary>
        /// File name from the command line
        /// </summary>
        public string ArgumentFile
        {
            get { return argumentFile; }
            set { argumentFile = value; }
        }

        /// <summary>
        /// Get reference to Edit menu item.
        /// Used to show context menu in DrawArea class.
        /// </summary>
        /// <value></value>
        public ToolStripMenuItem ContextParent
        {
            get { return editToolStripMenuItem; }
        }
        #endregion

        #region Constructor
        public MainForm()
        {

            InitializeComponent();
            //this.svgCreater = new SvgCreater();

            //this.SwitchMachineSize.SelectedIndex = 1;

            //this.SwitchMachineSize.SelectedIndexChanged += SwitchMachineSize_SelectedIndexChanged;
            //if (MessageBox.Show("是否通过已有的Visio图转换?", "请选择", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            //    //this.axDrawingControl = new AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl();
            //    MouseWheel += new MouseEventHandler(MainForm_MouseWheel);
            //    OpenFileDialog add = new OpenFileDialog();
            //    add.ShowDialog();
            //    //   a.FileName
            //    this.axDrawingControl1.Src = add.FileName;
            //    doc = this.axDrawingControl1.Document;
            //    app = this.axDrawingControl1.Document.Application;
            //    page = this.axDrawingControl1.Document.Application.ActivePage;
                
            //}
        }

        void SwitchMachineSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.drawArea.SwitchMachineSize = SwitchMachineSize.SelectedIndex + 1;
        }
        #endregion

        #region Toolbar Event Handlers
        private void toolStripButtonNew_Click(object sender, EventArgs e)
        {
            CommandNew();
        }

        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            CommandOpen();
        }


        private void toolStripButtonOpen_ClickText(object sender, EventArgs e)
        {
            CommandOpen();
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            CommandSave();
        }

        private void toolStripButtonPointer_Click(object sender, EventArgs e)
        {
            CommandPointer();
        }

        private void toolStripButtonRectangle_Click(object sender, EventArgs e)
        {
            CommandRectangle();
        }

        private void toolStripButtonEllipse_Click(object sender, EventArgs e)
        {
            CommandEllipse();
        }

        private void toolStripButtonLine_Click(object sender, EventArgs e)
        {
            CommandLine();
        }

        private void toolStripButtonPencil_Click(object sender, EventArgs e)
        {
            CommandPolygon();
        }

        private void toolStripButtonUndo_Click(object sender, EventArgs e)
        {
            CommandUndo();
        }

        private void toolStripButtonRedo_Click(object sender, EventArgs e)
        {
            CommandRedo();
        }

        private void toolStripButton1_Click_2(object sender, EventArgs e)
        {
            dlgColor.AllowFullOpen = true;
            dlgColor.AnyColor = true;
            if (dlgColor.ShowDialog() ==
                DialogResult.OK)
            {
                drawArea.BackColor = System.Drawing.Color.FromArgb(255, dlgColor.Color);
                tsbBackColor.BackColor = System.Drawing.Color.FromArgb(255, dlgColor.Color);
            }
        }
        #endregion Toolbar Event Handlers

        #region Menu Event Handlers
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandNew();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandOpen();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandSave();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandSaveAs();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int x = drawArea.TheLayers.ActiveLayerIndex;
            drawArea.TheLayers[x].Graphics.SelectAll();
            drawArea.Refresh();
        }

        private void unselectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int x = drawArea.TheLayers.ActiveLayerIndex;
            drawArea.TheLayers[x].Graphics.UnselectAll();
            drawArea.Refresh();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int x = drawArea.TheLayers.ActiveLayerIndex;
            CommandDelete command = new CommandDelete(drawArea.TheLayers);

            if (drawArea.TheLayers[x].Graphics.DeleteSelection())
            {
                drawArea.SetDirty();
                drawArea.Refresh();
                drawArea.AddCommandToHistory(command);
            }
        }

        private void deleteAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int x = drawArea.TheLayers.ActiveLayerIndex;
            CommandDeleteAll command = new CommandDeleteAll(drawArea.TheLayers);

            if (drawArea.TheLayers[x].Graphics.Clear())
            {
                drawArea.SetDirty();
                drawArea.Refresh();
                drawArea.AddCommandToHistory(command);
            }
        }

        private void moveToFrontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int x = drawArea.TheLayers.ActiveLayerIndex;
            if (drawArea.TheLayers[x].Graphics.MoveSelectionToFront())
            {
                drawArea.SetDirty();
                drawArea.Refresh();
            }
        }

        private void moveToBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int x = drawArea.TheLayers.ActiveLayerIndex;
            if (drawArea.TheLayers[x].Graphics.MoveSelectionToBack())
            {
                drawArea.SetDirty();
                drawArea.Refresh();
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (drawArea.PrepareHitProject != null)
            {
                CommandChangeProperty();
            }
        }

        private void pointerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandPointer();
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandRectangle();
        }

        private void ellipseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandEllipse();
        }

        private void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandLine();
        }

        private void pencilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandPolygon();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandRedo();
        }


        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CommandCopy();
        }



        private void pasteToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CommandPaste();
        }
        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CommandCut();

        }

        #endregion Menu Event Handlers

        #region DocManager Event Handlers
        /// <summary>
        /// Load document from the stream supplied by DocManager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void docManager_LoadEvent(object sender, SerializationEventArgs e)
        {
            // DocManager asks to load document from supplied stream
            try
            {
                string fileDlgInitDir = new FileInfo(e.FileName).DirectoryName;
                //StaticHelper a = StaticHelper.getinstance();
                //a.filedir = fileDlgInitDir;
                drawArea.TheLayers = (Layers)e.Formatter.Deserialize(e.SerializationStream);

                int al = this.drawArea.TheLayers.ActiveLayerIndex;

                //int height = this.drawArea.TheLayers[al].TheMostButtom - this.drawArea.TheLayers[al].TheMostTop;
                //int width = this.drawArea.TheLayers[al].TheMostRight - this.drawArea.TheLayers[al].TheMostLeft;
                //float zo1 = (float)(this.drawArea.Height) / (float)height;
                //float zo2 = (float)(this.drawArea.Width) / (float)width;
                drawArea.UndoManager.Layers = drawArea.TheLayers;



                //if (zo1 > zo2)
                //{
                //    drawArea._zoom = zo2 / (1.05F);
                //}
                //else
                //{
                //    drawArea._zoom = zo1 / (1.05F);
                //}
            }

            catch (ArgumentNullException ex)
            {
                HandleLoadException(ex, e);
            }
            catch (SerializationException ex)
            {
                HandleLoadException(ex, e);
            }
            catch (SecurityException ex)
            {
                HandleLoadException(ex, e);
            }
        }

        private void docManager_LoadEventText(object sender, SerializationEventArgs e)
        {
            // DocManager asks to load document from supplied stream
            try
            {
                string fileDlgInitDir = new FileInfo(e.FileName).DirectoryName;
                //StaticHelper a = StaticHelper.getinstance();
                //a.filedir = fileDlgInitDir;
                Layers temp = (Layers)e.Formatter.Deserialize(e.SerializationStream);
                for (int i = 0; i < temp[0].Graphics.Count; i++)
                {
                    DrawObject o =
                       temp[0].Graphics[i];
                    drawArea.TheLayers[0].Graphics.Add(o);

                }
                drawArea.UndoManager.Layers = drawArea.TheLayers;

            }
            catch (ArgumentNullException ex)
            {
                HandleLoadException(ex, e);
            }
            catch (SerializationException ex)
            {
                HandleLoadException(ex, e);
            }
            catch (SecurityException ex)
            {
                HandleLoadException(ex, e);
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
                e.Formatter.Serialize(e.SerializationStream, drawArea.TheLayers);
            }
            catch (ArgumentNullException ex)
            {
                HandleSaveException(ex, e);
            }
            catch (SerializationException ex)
            {
                HandleSaveException(ex, e);
            }
            catch (SecurityException ex)
            {
                HandleSaveException(ex, e);
            }
        }
        #endregion

        #region Event Handlers
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Create draw area
            drawArea = new DrawArea();
            //drawArea.BackgroundImage = (Bitmap)Bitmap.FromFile("PICS\\" + "background.gif");
            drawArea.IsPainting = true;
            drawArea.TheLayers = new Layers();
            drawArea.TheLayers.CreateNewLayer("Default");
            drawArea.Location = new Point(200, 0);
            drawArea.Size = new Size(10, 10);
            drawArea.Owner = this; 
            Controls.Add(drawArea);

            // Helper objects (DocManager and others)
            InitializeHelperObjects();
            drawArea.Initialize(docManager);

            ResizeDrawArea();

            LoadSettingsFromRegistry();

            // Submit to Idle event to set controls state at idle time
            System.Windows.Forms.Application.Idle += delegate { SetStateOfControls(); };

            // Open file passed in the command line
            if (ArgumentFile.Length > 0)
                OpenDocument(ArgumentFile);

            // Subscribe to DropDownOpened event for each popup menu
            // (see details in MainForm_DropDownOpened)
            foreach (ToolStripItem item in menuStrip1.Items)
            {
                if (item.GetType() ==
                    typeof(ToolStripMenuItem))
                {
                    ((ToolStripMenuItem)item).DropDownOpened += MainForm_DropDownOpened;
                }
            }

        }


        /// <summary>
        /// Resize draw area when form is resized
        /// </summary>
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized &&
                drawArea != null)
            {
                ResizeDrawArea();
            }
        }

        /// <summary>
        /// Form is closing
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason ==
                CloseReason.UserClosing)
            {
                if (!docManager.CloseDocument())
                    e.Cancel = true;
            }

            SaveSettingsToRegistry();
        }

        /// <summary>
        /// Popup menu item (File, Edit ...) is opened.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_DropDownOpened(object sender, EventArgs e)
        {
            // Reset active tool to pointer.
            // This prevents bug in rare case when non-pointer tool is active, user opens
            // main main menu and after this clicks in the drawArea. MouseDown event is not
            // raised in this case (why ??), and MouseMove event works incorrectly.
            drawArea.ActiveTool = DrawToolType.Pointer;
        }
        #endregion Event Handlers

        #region Other Functions
        /// <summary>
        /// Set state of controls.
        /// Function is called at idle time.
        /// </summary>
        public void SetStateOfControls()
        {
            // Select active tool
            toolStripButtonPointer.Checked = (drawArea.ActiveTool == DrawToolType.Pointer);
            toolStripButtonRectangle.Checked = (drawArea.ActiveTool == DrawToolType.Rectangle);
            toolStripButtonEllipse.Checked = (drawArea.ActiveTool == DrawToolType.Ellipse);
            toolStripButtonLine.Checked = (drawArea.ActiveTool == DrawToolType.Line);
            toolStripButtonPencil.Checked = (drawArea.ActiveTool == DrawToolType.Polygon);

            pointerToolStripMenuItem.Checked = (drawArea.ActiveTool == DrawToolType.Pointer);
            rectangleToolStripMenuItem.Checked = (drawArea.ActiveTool == DrawToolType.Rectangle);
            ellipseToolStripMenuItem.Checked = (drawArea.ActiveTool == DrawToolType.Ellipse);
            lineToolStripMenuItem.Checked = (drawArea.ActiveTool == DrawToolType.Line);
            pencilToolStripMenuItem.Checked = (drawArea.ActiveTool == DrawToolType.Polygon);

            int x = drawArea.TheLayers.ActiveLayerIndex;
            bool objects = (drawArea.TheLayers[x].Graphics.Count > 0);
            bool selectedObjects = (drawArea.TheLayers[x].Graphics.SelectionCount > 0);
            // File operations
            saveToolStripMenuItem.Enabled = objects;
            toolStripButtonSave.Enabled = objects;
            saveAsToolStripMenuItem.Enabled = objects;

            // Edit operations
            deleteToolStripMenuItem.Enabled = selectedObjects;
            deleteAllToolStripMenuItem.Enabled = objects;
            selectAllToolStripMenuItem.Enabled = objects;
            unselectAllToolStripMenuItem.Enabled = objects;
            moveToFrontToolStripMenuItem.Enabled = selectedObjects;
            moveToBackToolStripMenuItem.Enabled = selectedObjects;
            propertiesToolStripMenuItem.Enabled = selectedObjects;

            // Undo, Redo
            undoToolStripMenuItem.Enabled = drawArea.CanUndo;
            toolStripButtonUndo.Enabled = drawArea.CanUndo;

            redoToolStripMenuItem.Enabled = drawArea.CanRedo;
            toolStripButtonRedo.Enabled = drawArea.CanRedo;

            // Status Strip
            tslCurrentLayer.Text = drawArea.TheLayers[x].LayerName;
            tslNumberOfObjects.Text = drawArea.TheLayers[x].Graphics.Count.ToString();
            tslPanPosition.Text = drawArea.PanX + ", " + drawArea.PanY;
            tslRotation.Text = drawArea.Rotation + " deg";
            tslZoomLevel.Text = (Math.Round(drawArea.Zoom * 100)) + " %";

            // Pan button
            tsbPanMode.Checked = drawArea.Panning;
        }

        /// <summary>
        /// Set draw area to all form client space except toolbar
        /// </summary>
        private void ResizeDrawArea()
        {
            Rectangle rect = ClientRectangle;

            drawArea.Left = rect.Left;
            drawArea.Top = rect.Top + menuStrip1.Height + toolStrip1.Height;
            drawArea.Width = rect.Width;
            drawArea.Height = rect.Height - menuStrip1.Height - toolStrip1.Height;
        }

        /// <summary>
        /// Initialize helper objects from the DocToolkit Library.
        /// 
        /// Called from Form1_Load. Initialized all objects except
        /// PersistWindowState wich must be initialized in the
        /// form constructor.
        /// </summary>
        private void InitializeHelperObjects()
        {
            // DocManager
            DocManagerData data = new DocManagerData();
            data.FormOwner = this;
            data.UpdateTitle = true;
            data.FileDialogFilter = "DrawTools files (*.dtl)|*.dtl|All Files (*.*)|*.*";
            data.NewDocName = "Untitled.dtl";
            data.RegistryPath = registryPath;

            docManager = new DocManager(data);
            docManager.RegisterFileType("dtl", "dtlfile", "DrawTools File");

            // Subscribe to DocManager events.
            docManager.SaveEvent += docManager_SaveEvent;
            docManager.LoadEvent += docManager_LoadEvent;

            // Make "inline subscription" using anonymous methods.
            docManager.OpenEvent += delegate(object sender, OpenFileEventArgs e)
                                        {
                                            // Update MRU List
                                            if (e.Succeeded)
                                                mruManager.Add(e.FileName);
                                            else
                                                mruManager.Remove(e.FileName);
                                        };

            docManager.DocChangedEvent += delegate
                                            {
                                                drawArea.Refresh();
                                                drawArea.ClearHistory();
                                            };

            docManager.ClearEvent += delegate
                                        {
                                            bool haveObjects = false;
                                            for (int i = 0; i < drawArea.TheLayers.Count; i++)
                                            {
                                                if (drawArea.TheLayers[i].Graphics.Count > 1)
                                                {
                                                    haveObjects = true;
                                                    break;
                                                }
                                            }
                                            if (haveObjects)
                                            {
                                                drawArea.TheLayers.Clear();
                                                drawArea.ClearHistory();
                                                drawArea.Refresh();
                                            }
                                        };

            docManager.NewDocument();

            // DragDropManager
            dragDropManager = new DragDropManager(this);
            dragDropManager.FileDroppedEvent += delegate(object sender, FileDroppedEventArgs e) { OpenDocument(e.FileArray.GetValue(0).ToString()); };

            // MruManager
            mruManager = new MruManager();
            mruManager.Initialize(
                this, // owner form
                recentFilesToolStripMenuItem, // Recent Files menu item
                fileToolStripMenuItem, // parent
                registryPath); // Registry path to keep MRU list

            mruManager.MruOpenEvent += delegate(object sender, MruFileOpenEventArgs e) { OpenDocument(e.FileName); };
        }

        /// <summary>
        /// Handle exception from docManager_LoadEvent function
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="e"></param>
        private void HandleLoadException(Exception ex, SerializationEventArgs e)
        {
            MessageBox.Show(this,
                            "Open File operation failed. File name: " + e.FileName + "\n" +
                            "Reason: " + ex.Message,
                            System.Windows.Forms.Application.ProductName);

            e.Error = true;
        }

        /// <summary>
        /// Handle exception from docManager_SaveEvent function
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="e"></param>
        private void HandleSaveException(Exception ex, SerializationEventArgs e)
        {
            MessageBox.Show(this,
                            "Save File operation failed. File name: " + e.FileName + "\n" +
                            "Reason: " + ex.Message,
                            System.Windows.Forms.Application.ProductName);

            e.Error = true;
        }

        /// <summary>
        /// Open document.
        /// Used to open file passed in command line or dropped into the window
        /// </summary>
        /// <param name="file"></param>
        public void OpenDocument(string file)
        {
            docManager.OpenDocument(file);
        }

        /// <summary>
        /// Load application settings from the Registry
        /// </summary>
        private void LoadSettingsFromRegistry()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.CreateSubKey(registryPath);

                DrawObject.LastUsedColor = System.Drawing.Color.FromArgb((int)key.GetValue(
                                                                "Color",
                                                                System.Drawing.Color.Black.ToArgb()));

                DrawObject.LastUsedPenWidth = (int)key.GetValue(
                                                    "Width",
                                                    1);
            }
            catch (ArgumentNullException ex)
            {
                HandleRegistryException(ex);
            }
            catch (SecurityException ex)
            {
                HandleRegistryException(ex);
            }
            catch (ArgumentException ex)
            {
                HandleRegistryException(ex);
            }
            catch (ObjectDisposedException ex)
            {
                HandleRegistryException(ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                HandleRegistryException(ex);
            }
        }

        /// <summary>
        /// Save application settings to the Registry
        /// </summary>
        private void SaveSettingsToRegistry()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.CreateSubKey(registryPath);

                key.SetValue("Color", DrawObject.LastUsedColor.ToArgb());
                key.SetValue("Width", DrawObject.LastUsedPenWidth);
            }
            catch (SecurityException ex)
            {
                HandleRegistryException(ex);
            }
            catch (ArgumentException ex)
            {
                HandleRegistryException(ex);
            }
            catch (ObjectDisposedException ex)
            {
                HandleRegistryException(ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                HandleRegistryException(ex);
            }
        }

        private void HandleRegistryException(Exception ex)
        {
            Trace.WriteLine("Registry operation failed: " + ex.Message);
        }

        /// <summary>
        /// Set Pointer draw tool
        /// </summary>
        private void CommandPointer()
        {
            drawArea.ActiveTool = DrawToolType.Pointer;
        }

        /// <summary>
        /// Set Rectangle draw tool
        /// </summary>
        private void CommandRectangle()
        {
            drawArea.ActiveTool = DrawToolType.Rectangle;
            drawArea.DrawFilled = false;
        }

        /// <summary>
        /// Set Ellipse draw tool
        /// </summary>
        private void CommandEllipse()
        {
            drawArea.ActiveTool = DrawToolType.Ellipse;
            drawArea.DrawFilled = false;
        }

        /// <summary>
        /// Set Line draw tool
        /// </summary>
        private void CommandLine()
        {
            drawArea.ActiveTool = DrawToolType.Line;
        }

        /// <summary>
        /// Set Polygon draw tool
        /// </summary>
        private void CommandPolygon()
        {
            drawArea.ActiveTool = DrawToolType.Polygon;
        }

        /// <summary>
        /// Open new file
        /// </summary>
        private void CommandNew()
        {
            docManager.NewDocument();
        }

        /// <summary>
        /// Open file
        /// </summary>
        private void CommandOpen()
        {
            docManager.OpenDocument("");
        }

        /// <summary>
        /// Save file
        /// </summary>
        private void CommandSave()
        {
            docManager.SaveDocument(DocManager.SaveType.Save);
        }

        /// <summary>
        /// Save As
        /// </summary>
        private void CommandSaveAs()
        {
            docManager.SaveDocument(DocManager.SaveType.SaveAs);
        }

        /// <summary>
        /// Undo
        /// </summary>
        private void CommandUndo()
        {
            drawArea.Undo();
        }

        /// <summary>
        /// Redo
        /// </summary>
        private void CommandRedo()
        {
            drawArea.Redo();
        }

        private void CommandCopy()
        {
            drawArea.CopyObject();
        }

        private void CommandPaste()
        {
            if (drawArea.FormalCopyObjectList != null && drawArea.FormalCopyObjectList.Count != 0)
            {
                drawArea.PasteObject();
                CommandPaste command = new CommandPaste(drawArea.TheLayers);
                drawArea.AddCommandToHistory(command);
                drawArea.TheLayers[drawArea.TheLayers.ActiveLayerIndex].Graphics.UnselectAll();
            }
        }
        private void CommandChangeProperty()
        {
            if (drawArea.PrepareHitProject != null)
            {
                if (drawArea.PrepareHitProject.ShowPropertiesDialog())
                {
                    drawArea.SetDirty();
                    drawArea.Refresh();
                }
                CommandChangeProperty command = new CommandChangeProperty(drawArea.PrepareHitProject);
                drawArea.AddCommandToHistory(command);
            }
        }
        private void CommandCut()
        {
            if (drawArea.PrepareCopyObjectList != null && drawArea.PrepareCopyObjectList.Count != 0)
            {
                CommandCut command = new CommandCut(drawArea.PrepareCopyObjectList);
                drawArea.AddCommandToHistory(command);
                drawArea.CutObject();
            }
        }

        #endregion

        #region Mouse Functions
        private void MainForm_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta != 0)
            {
                if (_controlKey)
                {
                    // We are panning up or down using the wheel
                    if (e.Delta > 0)
                        drawArea.PanY += 10;
                    else
                        drawArea.PanY -= 10;
                    Invalidate();
                }
                else
                {
                    // We are zooming in or out using the wheel
                    if (e.Delta > 0)
                        AdjustZoom(.1f);
                    else
                        AdjustZoom(-.1f);
                }
                SetStateOfControls();
                return;
            }
        }
        #endregion Mouse Functions

        #region Keyboard Functions
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            int al = drawArea.TheLayers.ActiveLayerIndex;

            if (e.Control && e.KeyCode == Keys.C)
            {
                List<DrawObject> o = new List<DrawObject>();

                for (int i = drawArea.TheLayers[al].Graphics.Count-1; i >=0; i--)
                {
                    if (drawArea.TheLayers[al].Graphics[i].Selected)
                    {
                        o.Add(drawArea.TheLayers[al].Graphics[i].Clone());
                    }
                }
                drawArea.PrepareCopyObjectList = o;
                if (drawArea.PrepareHitProject == null && drawArea.PrepareCopyObjectList.Count == 0)
                {
                    drawArea.TheLayers[al].Graphics.UnselectAll();
                }
                drawArea.CopyObject();
                Refresh();
            }
            else if (e.Control && e.KeyCode == Keys.A)
            {
                drawArea.TheLayers[al].Graphics.SelectAll();
                drawArea.Refresh() ;
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                drawArea.PasteObject();
                CommandPaste command = new CommandPaste(drawArea.TheLayers);
                drawArea.AddCommandToHistory(command);
                drawArea.TheLayers[drawArea.TheLayers.ActiveLayerIndex].Graphics.UnselectAll();
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        CommandDelete command = new CommandDelete(drawArea.TheLayers);

                        if (drawArea.TheLayers[drawArea.TheLayers.ActiveLayerIndex].Graphics.DeleteSelection())
                        {
                            drawArea.SetDirty();
                            drawArea.Refresh();
                            drawArea.AddCommandToHistory(command);
                        }
                        break;
                    case Keys.Right:
                        drawArea.PanX -= 10;
                        drawArea.Invalidate();
                        break;
                    case Keys.Left:
                        drawArea.PanX += 10;
                        drawArea.Invalidate();
                        break;
                    case Keys.Up:
                        if (e.KeyCode == Keys.Up &&
                            e.Shift)
                            AdjustZoom(.1f);
                        else
                            drawArea.PanY += 10;
                        drawArea.Invalidate();
                        break;
                    case Keys.Down:
                        if (e.KeyCode == Keys.Down &&
                            e.Shift)
                            AdjustZoom(-.1f);
                        else
                            drawArea.PanY -= 10;
                        drawArea.Invalidate();
                        break;
                    case Keys.ControlKey:
                        _controlKey = true;
                        break;
                    default:
                        break;
                }
            }
            drawArea.Invalidate();
            SetStateOfControls();
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            _controlKey = false;
        }
        #endregion Keyboard Functions

        #region Zoom, Pan, Rotation Functions
        /// <summary>
        /// Adjust the zoom by the amount given, within reason
        /// </summary>
        /// <param name="_amount">float value to adjust zoom by - may be positive or negative</param>
        private void AdjustZoom(float _amount)
        {
            drawArea.Zoom += _amount;
            if (drawArea.Zoom < .1f)
                drawArea.Zoom = .1f;
            if (drawArea.Zoom > 10)
                drawArea.Zoom = 10f;
            drawArea.Invalidate();
            SetStateOfControls();
        }

        private void tsbZoomIn_Click(object sender, EventArgs e)
        {
            AdjustZoom(.1f);
        }

        private void tsbZoomOut_Click(object sender, EventArgs e)
        {
            AdjustZoom(-.1f);
        }

        private void tsbZoomReset_Click(object sender, EventArgs e)
        {
            drawArea.Zoom = 1.0f;
            drawArea.Invalidate();
        }

        private void tsbRotateRight_Click(object sender, EventArgs e)
        {
            int al = drawArea.TheLayers.ActiveLayerIndex;
            if (drawArea.TheLayers[al].Graphics.SelectionCount > 0)
                RotateObject(10);
            else
                RotateDrawing(10);
        }

        private void tsbRotateLeft_Click(object sender, EventArgs e)
        {
            int al = drawArea.TheLayers.ActiveLayerIndex;
            if (drawArea.TheLayers[al].Graphics.SelectionCount > 0)
                RotateObject(-10);
            else
                RotateDrawing(-10);
        }

        private void tsbRotateReset_Click(object sender, EventArgs e)
        {
            int al = drawArea.TheLayers.ActiveLayerIndex;
            if (drawArea.TheLayers[al].Graphics.SelectionCount > 0)
                RotateObject(0);
            else
                RotateDrawing(0);
        }

        /// <summary>
        /// Rotate the selected Object(s)
        /// </summary>
        /// <param name="p">Amount to rotate. Negative is Left, Positive is Right, Zero indicates Reset to zero</param>
        private void RotateObject(int p)
        {
            int al = drawArea.TheLayers.ActiveLayerIndex;
            for (int i = 0; i < drawArea.TheLayers[al].Graphics.Count; i++)
            {
                if (drawArea.TheLayers[al].Graphics[i].Selected)
                    if (p == 0)
                        drawArea.TheLayers[al].Graphics[i].Rotation = 0;
                    else
                        drawArea.TheLayers[al].Graphics[i].Rotation += p;
            }
            drawArea.Invalidate();
            SetStateOfControls();
        }

        /// <summary>
        /// Rotate the entire drawing
        /// </summary>
        /// <param name="p">Amount to rotate. Negative is Left, Positive is Right, Zero indicates Reset to zero</param>
        private void RotateDrawing(int p)
        {
            if (p == 0)
                drawArea.Rotation = 0;
            else
            {
                drawArea.Rotation += p;
                if (p < 0) // Left Rotation
                {
                    if (drawArea.Rotation <
                        -360)
                        drawArea.Rotation = 0;
                }
                else
                {
                    if (drawArea.Rotation > 360)
                        drawArea.Rotation = 0;
                }
            }
            drawArea.Invalidate();
            SetStateOfControls();
        }

        private void tsbPanMode_Click(object sender, EventArgs e)
        {
            _panMode = !_panMode;
            if (_panMode)
                tsbPanMode.Checked = true;
            else
                tsbPanMode.Checked = false;
            drawArea.ActiveTool = DrawToolType.Pointer;
            drawArea.Panning = _panMode;
        }

        private void tsbPanReset_Click(object sender, EventArgs e)
        {
            _panMode = false;
            if (tsbPanMode.Checked)
                tsbPanMode.Checked = false;
            drawArea.Panning = false;
            drawArea.PanX = 0;
            drawArea.PanY = drawArea.OriginalPanY;
            drawArea.Invalidate();
        }
        #endregion  Zoom, Pan, Rotation Functions




        private void tslCurrentLayer_Click(object sender, EventArgs e)
        {
            LayerDialog ld = new LayerDialog(drawArea.TheLayers);
            ld.ShowDialog();
            // First add any new layers
            for (int i = 0; i < ld.layerList.Count; i++)
            {
                if (ld.layerList[i].LayerNew)
                {
                    Layer layer = new Layer();
                    layer.LayerName = ld.layerList[i].LayerName;
                    layer.Graphics = new GraphicsList();
                    drawArea.TheLayers.Add(layer);
                }
            }
            drawArea.TheLayers.InactivateAllLayers();
            for (int i = 0; i < ld.layerList.Count; i++)
            {
                if (ld.layerList[i].LayerActive)
                    drawArea.TheLayers.SetActiveLayer(i);

                if (ld.layerList[i].LayerVisible)
                    drawArea.TheLayers.MakeLayerVisible(i);
                else
                    drawArea.TheLayers.MakeLayerInvisible(i);

                drawArea.TheLayers[i].LayerName = ld.layerList[i].LayerName;
            }
            // Lastly, remove any deleted layers
            for (int i = 0; i < ld.layerList.Count; i++)
            {
                if (ld.layerList[i].LayerDeleted)
                    drawArea.TheLayers.RemoveLayer(i);
            }
            drawArea.Invalidate();
        }

        #region Additional Drawing Tools
        /// <summary>
        /// Draw PolyLine objects - a polyline is a series of straight lines of various lengths connected at their end points.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbPolyLine_Click(object sender, EventArgs e)
        {
            drawArea.ActiveTool = DrawToolType.PolyLine;
            drawArea.DrawFilled = false;
        }

        private void tsbConnector_Click(object sender, EventArgs e)
        {
            drawArea.ActiveTool = DrawToolType.Connector;
            drawArea.DrawFilled = false;
        }
        /// <summary>
        /// Draw Text objects
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbDrawText_Click(object sender, EventArgs e)
        {
            drawArea.ActiveTool = DrawToolType.Text;
        }

        private void tsbFilledRectangle_Click(object sender, EventArgs e)
        {
            drawArea.ActiveTool = DrawToolType.Rectangle;
            drawArea.DrawFilled = true;
        }

        private void tsbFilledEllipse_Click(object sender, EventArgs e)
        {
            drawArea.ActiveTool = DrawToolType.Ellipse;
            drawArea.DrawFilled = true;
        }

        private void tsbImage_Click(object sender, EventArgs e)
        {
            drawArea.SetImageName(false, null);
            drawArea.ActiveTool = DrawToolType.Image;
        }

        private void tsbSelectLineColor_Click(object sender, EventArgs e)
        {
            dlgColor.AllowFullOpen = true;
            dlgColor.AnyColor = true;
            if (dlgColor.ShowDialog() ==
                DialogResult.OK)
            {
                drawArea.LineColor = System.Drawing.Color.FromArgb(255, dlgColor.Color);
                tsbLineColor.BackColor = System.Drawing.Color.FromArgb(255, dlgColor.Color);
            }
        }

        private void tsbSelectFillColor_Click(object sender, EventArgs e)
        {
            dlgColor.AllowFullOpen = true;
            dlgColor.AnyColor = true;
            if (dlgColor.ShowDialog() ==
                DialogResult.OK)
            {
                drawArea.FillColor = System.Drawing.Color.FromArgb(255, dlgColor.Color);
                tsbFillColor.BackColor = System.Drawing.Color.FromArgb(255, dlgColor.Color);
            }
        }

        private void tsbLineThinnest_Click(object sender, EventArgs e)
        {
            drawArea.LineWidth = -1;
        }

        private void tsbLineThin_Click(object sender, EventArgs e)
        {
            drawArea.LineWidth = 2;
        }

        private void tsbThickLine_Click(object sender, EventArgs e)
        {
            drawArea.LineWidth = 5;
        }

        private void tsbThickerLine_Click(object sender, EventArgs e)
        {
            drawArea.LineWidth = 10;
        }

        private void tsbThickestLine_Click(object sender, EventArgs e)
        {
            drawArea.LineWidth = 15;
        }
        #endregion Additional Drawing Tools

        private void toolStripMenuItemGenericPen_Click(object sender, EventArgs e)
        {
            drawArea.PenType = DrawingPens.PenType.Generic;
            drawArea.CurrentPen = DrawingPens.SetCurrentPen(DrawingPens.PenType.Generic);
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawArea.PenType = DrawingPens.PenType.RedPen;
            drawArea.CurrentPen = DrawingPens.SetCurrentPen(DrawingPens.PenType.RedPen);
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawArea.PenType = DrawingPens.PenType.BluePen;
            drawArea.CurrentPen = DrawingPens.SetCurrentPen(DrawingPens.PenType.BluePen);
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawArea.PenType = DrawingPens.PenType.GreenPen;
            drawArea.CurrentPen = DrawingPens.SetCurrentPen(DrawingPens.PenType.GreenPen);
        }

        private void redDottedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawArea.PenType = DrawingPens.PenType.RedDottedPen;
            drawArea.CurrentPen = DrawingPens.SetCurrentPen(DrawingPens.PenType.RedDottedPen);
        }

        private void redDotDashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawArea.PenType = DrawingPens.PenType.RedDotDashPen;
            drawArea.CurrentPen = DrawingPens.SetCurrentPen(DrawingPens.PenType.RedDotDashPen);
        }

        private void doubleLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawArea.PenType = DrawingPens.PenType.DoubleLinePen;
            drawArea.CurrentPen = DrawingPens.SetCurrentPen(DrawingPens.PenType.DoubleLinePen);
        }

        private void dashedArrowLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawArea.PenType = DrawingPens.PenType.DashedArrowPen;
            drawArea.CurrentPen = DrawingPens.SetCurrentPen(DrawingPens.PenType.DashedArrowPen);
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap b = new Bitmap(drawArea.Width, drawArea.Height);
            Graphics g = Graphics.FromImage(b);
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "BMP files (*.bmp)|*.bmp";
            saveFile.FileName = "无标题.bmp";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {

               
                g.Clear(
                    System.Drawing.Color.Black);
                drawArea.TheLayers.Draw(g);
                b.Save(saveFile.FileName, ImageFormat.Bmp);

                g.Dispose();
                b.Dispose();
            }
        }



        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = "DrawTool.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }




        private void toolStripButton3_Click(object sender, EventArgs e)
        {

            int al2 = drawArea.TheLayers.ActiveLayerIndex;
            /*  int  m = this.drawArea.TheLayers[al2].drawlist[8, 5].Count;

              for (int i = 0; i < m; i++)
              {
                  this.drawArea.TheLayers[al2].drawlist[8, 5][i].FillColor = System.Drawing.Color.Red;

                  this.drawArea.TheLayers[al2].drawlist[8, 5][i].Color = System.Drawing.Color.Red;
            
              }
              */


            int mn = this.drawArea.TheLayers[al2].DigitGraphicsList[0, 4098].Count;

            for (int i = 0; i < mn; i++)
            {
                //this.drawArea.TheLayers[al2].drawlist[8, 5][i].FillColor = System.Drawing.Color.Red;

                //this.drawArea.TheLayers[al2].drawlist[8, 5][i].Color = System.Drawing.Color.Red;

                System.Drawing.Color dd = this.drawArea.TheLayers[al2].DigitGraphicsList[0, 4098].Listcolor;
                this.drawArea.TheLayers[al2].DigitGraphicsList[0, 4098][i].PenColor = dd;
            }

            this.Refresh();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            //// _panMode = !_panMode;
            // if (_panMode)
            //     tsbPanMode.Checked = true;
            // else
            //     tsbPanMode.Checked = false;
            drawArea.ActiveTool = DrawToolType.TextMove;
            // drawArea.Panning = _panMode;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            docManager.LoadEvent -= docManager_LoadEvent;
            docManager.LoadEvent += docManager_LoadEventText;
            CommandOpen();
            docManager.LoadEvent += docManager_LoadEvent;
            docManager.LoadEvent -= docManager_LoadEventText;
        }


        private void DrawImages(string p)
        {
            
            drawArea.SetImageName(true, p);

            drawArea.ActiveTool = DrawToolType.Image;
        }
        
        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {

        }

        //信号机
        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            drawArea.ActiveTool = DrawToolType.OneLightTeleseme;
        }

        //双灯信号机
        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawArea.ActiveTool = DrawToolType.TowLightTeleseme;
        }

        //股道区段
        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            drawArea.ActiveTool = DrawToolType.StationTrack;
        }

        //道岔
        private void turnoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawArea.ActiveTool = DrawToolType.Turnout;
        }

        //LED信号机
        private void lEDDisplayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawImages("LED显示器");
        }

        //iMac
        private void iMacToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawImages("iMac");
        }

        //PC
        private void pCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawImages("PC");
        }


        //便携电脑
        private void LaptopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawImages("便携电脑");
        }

        //大型机
        private void MainframeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawImages("大型机");
        }

        //服务器1U
        private void Server1UToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawImages("服务器1U");
        }

        //服务器2U
        private void Server2UToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawImages("服务器2U");
        }

        //服务器8U
        private void RackServer8UToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawImages("服务器8U");
        }

        //环形网络
        private void RingNetworkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawImages("环形网络");
        }


        //机柜
        private void EquipmentCabinetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawImages("机柜");
        }


        //机柜2
        private void Cabinet2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawImages("机柜2");
        }


        //机柜板
        private void FrameInterfacePanelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawImages("机柜板");
        }


        //基础方形
        private void SquareFoundationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawImages("基础方形");
        }

        //立式服务器
        private void VerticalServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawImages("立式服务器");
        }


        //路由器
        private void RouterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawImages("路由器");
        }

        //信号机住上
        private void SignalColumnUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawImages("信号机柱上");
        }


        //信号机住下
        private void SignalColumnDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawImages("信号机柱下");
        }

        //以太网
        private void NetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawImages("以太网");
        }

        //private void toolStripConvertINI_Click(object sender, EventArgs e)
        //{

        //    OpenFileDialog openFileDialog1 = new OpenFileDialog();
        //    openFileDialog1.Filter = "condll ini files (*.ini)|*.*";
        //    DialogResult res = openFileDialog1.ShowDialog(this);
        //    if (res == DialogResult.OK)
        //    {
        //        string iniFileName = openFileDialog1.FileName;

        //        if (iniFileName.Substring(iniFileName.Length - 10, 10) != "condll.ini")
        //        {
        //            MessageBox.Show("请输入正确的condll.ini");
        //        }
        //        else
        //        {
        //            INIConvertor convertor = new INIConvertor(iniFileName, this.drawArea);
        //            convertor.LoadMessageGetherData();
        //            convertor.ConvertIniToDtl();
        //            this.drawArea.Refresh();
        //        }
        //    }
        //}

        private void SaveToMVGMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "BMP files (*.bmp)|*.bmp";
            saveFile.FileName = "无标题.bmp";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string m_sXmlDeclaration = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>";
                    string m_sXmlDocType = "<!DOCTYPE svg PUBLIC \"-//W3C//DTD SVG 1.1//EN\" \"http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd\">";
                    string sXML;

                    sXML = m_sXmlDeclaration + "\r\n";
                    sXML += m_sXmlDocType + "\r\n";
                    sXML += "<svg width=\"" + this.drawArea.Width.ToString(CultureInfo.InvariantCulture) +
                        "\" height=\"" + this.drawArea.Height.ToString(CultureInfo.InvariantCulture) + "\" version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\">" + "\r\n";

                    //sXML += this.svgCreater.GetXmlString(drawArea.TheLayers[drawArea.TheLayers.ActiveLayerIndex].Graphics);
                    sXML += "</svg>" + "\r\n";
                    StreamWriter streamWriter = new StreamWriter(sXML);
                    streamWriter.Write(sXML);
                    streamWriter.Close();

                }
                catch
                {
                    MessageBox.Show("保存失败");
                }
            }
        
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("help.chm");
        }



        private void SwitchMachineButton_Click(object sender, EventArgs e)
        {
            drawArea.ActiveTool = DrawToolType.SwitchMachine;

        }

    }
}