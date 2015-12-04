using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Serialization;
using System.Security;
using System.Windows.Forms;
using DocToolkit;
using System.IO;
using System.Linq;

using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Collections.Generic;
using DrawTools.Command;
using DrawToolsDrawing.Draw;
using DrawToolsDrawing;
using DrawTools.Common;
using System.Xml;
using DrawTools.Tools;
using System.Collections;
using DevExpress.XtraNavBar;

namespace DrawTools
{
    public partial class MainForm : Form
    {
        #region Members
        //private DrawArea drawArea;
        private DocManager docManager;

        private DragDropManager dragDropManager;

        private MruManager mruManager;


        private List<DictionaryEntry> TemplateGroupDictionary;
        private Dictionary<string, IList<DrawObject>> TemplateItemDictionary;
        private List<DictionaryEntry> GroupTypeList;
        public string menuFilesDirectory;   //模板文件目录
        public string menuImagesDirectory;  //图片目录
        public string menuFilePath;         //菜单配置文件路径（含文件名称）
        // private AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl axDrawingControl;
        //private string argumentFile = ""; // file name from command line

        private const string registryPath = "Software\\AlexF\\DrawTools";

        private bool _controlKey = false;
        private bool _panMode = false;
        // private SvgCreater svgCreater;
        #endregion

        #region Properties
        /// <summary>
        /// File name from the command line
        /// </summary>
        //public string ArgumentFile
        //{
        //    get { return argumentFile; }
        //    set { argumentFile = value; }
        //}

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
            dlgColor.Color = drawArea.BackColor;
            if (dlgColor.ShowDialog() ==
                DialogResult.OK)
            {
                panelShow.BackColor = drawArea.BackColor = System.Drawing.Color.FromArgb(255, dlgColor.Color);
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
                if (drawArea.TheLayers.ActiveLayer.Graphics.SelectionCount == 1)
                {
                    CommandChangeProperty();
                }
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


        private object docManager_LoadTemplateEvent(object sender, SerializationEventArgs e)
        {

            try
            {
                GraphicsList graphicsList = (GraphicsList)e.Formatter.Deserialize(e.SerializationStream);
                return graphicsList;
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
            catch (Exception ex)
            {
                HandleLoadException(ex, e);
            }
            return null;
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
        /// <summary>
        /// 保存模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void docManager_SaveTemplateEvent(object sender, SerializationEventArgs e)
        {
            try
            {
                e.Formatter.Serialize(e.SerializationStream, drawArea.TheLayers.ActiveLayer.Graphics);
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
            catch (Exception ex)
            {
                HandleSaveException(ex, e);
            }
        }
        #endregion
        private void tsbSaveTemp_Click(object sender, EventArgs e)
        {
            if (docManager.Dirty && drawArea.TheLayers.ActiveLayer.Graphics.SelectionCount > 0)
            {
                frmTemplatePropertie frm = new frmTemplatePropertie(GroupTypeList);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    #region 配置菜单文件
                    string itemName = Guid.NewGuid().ToString().Replace("-", "");
                    string tempImagePath = menuImagesDirectory + itemName + ".png";
                    string groupName;
                    if (frm.IsAdd)
                        groupName = Guid.NewGuid().ToString().Replace("-", "");
                    else
                        groupName = frm.GroupName;
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    
                    dic.Add("name", groupName);
                    dic.Add("caption", frm.GroupCaption);
                    dic.Add("visible", "true");
                    XmlHelper.CreateOrUpdateAttributesByXPath(menuFilePath, "//Menu", "Group", "name", groupName, dic);
                    dic["name"] = itemName;
                    dic["caption"] = frm.ItemCaption;
                    dic["visible"]= "true";
                    XmlHelper.CreateOrUpdateAttributesByXPath(menuFilePath, "//Menu/Group[@name='" + groupName + "']", "Item", "name", itemName, dic);
                    #endregion
                    #region 保存图片
                    Image image = drawArea.GetWinformImage();
                    Image saveImg = ReduceImage(image, 30, 30);
                    saveImg.Save(tempImagePath);
                    #endregion
                    #region 保存模板
                    docManager.SaveDocumentTemplate(menuFilesDirectory + itemName + ".dtl");
                    #endregion
                    #region 生成菜单
                    /*
                    #region 保存菜单
                    TemplateGroupDictionary.Add(new DictionaryEntry(groupName, itemName));
                    List<DrawObject> list = Clone(drawArea.TheLayers.ActiveLayer.Graphics.Selection);
                    TemplateItemDictionary.Add(itemName, list);
                    #endregion
                    #region 生成Group
                    DevExpress.XtraNavBar.NavBarGroup nbgTemp;
                    if (frm.IsAdd)
                    {
                        nbgTemp = new DevExpress.XtraNavBar.NavBarGroup();
                        nbgTemp.Name = groupName;
                        nbgTemp.Caption = frm.GroupCaption;
                        nbgTemp.DragDropFlags = DevExpress.XtraNavBar.NavBarDragDrop.AllowDrop;
                        nbgTemp.ItemChanged += new EventHandler(delegate
                        {
                            foreach (DevExpress.XtraNavBar.NavBarGroup group in navMenu.Groups)
                            {
                                if (group.Name != nbgTemp.Name)
                                {
                                    foreach (DevExpress.XtraNavBar.NavBarItemLink item in group.ItemLinks)
                                    {
                                        item.Item.Appearance.ForeColor = Color.FromArgb(235, 235, 235);
                                    }
                                }
                            }
                            navMenu.Refresh();
                        });
                    }
                    else
                    {
                        nbgTemp = navMenu.Groups[frm.GroupName];
                    }
                    #endregion
                    #region 生成模板选项
                    DevExpress.XtraNavBar.NavBarItem nviTemp = new DevExpress.XtraNavBar.NavBarItem();
                    nviTemp.Name = itemName;
                    nviTemp.Caption = frm.ItemCaption;
                    nviTemp.Appearance.ForeColor = Color.FromArgb(235, 235, 235);
                    nviTemp.SmallImage = Image.FromFile(menuImagesDirectory + itemName + ".png");
                    nviTemp.LinkClicked +=
                        new DevExpress.XtraNavBar.NavBarLinkEventHandler(
                            delegate
                            {
                                foreach (DevExpress.XtraNavBar.NavBarItemLink item in nbgTemp.ItemLinks)
                                {
                                    item.Item.Appearance.ForeColor = Color.FromArgb(235, 235, 235);
                                }
                                nviTemp.Appearance.ForeColor = Color.LightSkyBlue;
                                navMenu.Refresh();
                                CommandTemplate(list);
                            }
                            );
                    nbgTemp.ItemLinks.Add(nviTemp);
                    #endregion
                    navMenu.Groups.Add(nbgTemp);
                    navMenu.Groups[groupName].Expanded = true;
                    MessageBox.Show("保存成功！");
                    navMenu.Refresh();
                    */
                    #endregion
                   
                } 
            }     
        }
        public List<DrawObject> Clone(List<DrawObject> list)
        {
            List<DrawObject> result = new List<DrawObject>();
            foreach (var item in list)
            {
                result.Add(item.Clone());
            }
            return result;
        }
        #region Event Handlers
        #region 窗体加载
        private void MainForm_Load(object sender, EventArgs e)
        {
            drawArea.IsPainting = true;
            drawArea.TheLayers = new Layers();
            drawArea.TheLayers.CreateNewLayer("Default");
            drawArea.Location = new Point(200, 0);
            //drawArea.Size = new Size(10, 10);
            drawArea.Owner = this;
            //drawArea.Dock = DockStyle.Fill;
            drawArea.BringToFront();
            InitializeHelperObjects();
            drawArea.Initialize(docManager);
            ResizeDrawArea();
            System.Windows.Forms.Application.Idle += delegate { SetStateOfControls(); };
            foreach (ToolStripItem item in menuStrip1.Items)
            {
                if (item.GetType() ==
                    typeof(ToolStripMenuItem))
                {
                    ((ToolStripMenuItem)item).DropDownOpened += MainForm_DropDownOpened;
                }
            }
            string menuTemplateDirectory = Application.StartupPath + "\\Template\\";
            menuFilesDirectory = menuTemplateDirectory + "Files\\";
            menuImagesDirectory = menuTemplateDirectory + "Images\\";
            menuFilePath = menuTemplateDirectory + "NavMenu.xml";
            try
            {
                FillNavBarMenu(menuFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.Close();
            }
        }
        /// <summary>
        /// 填充模板菜单
        /// </summary>
        /// <param name="menuFile"></param>
        public void FillNavBarMenu(string menuFile)
        {
            TemplateGroupDictionary = new List<DictionaryEntry>();
            TemplateItemDictionary = new Dictionary<string, IList<DrawObject>>();
            GroupTypeList = new List<DictionaryEntry>();
            XmlNodeList xmlNodeList = XmlHelper.GetXmlNodeListByXpath(menuFile, "Menu/Group");
            for (int i = 0; i < xmlNodeList.Count; i++)
            {
                XmlNode xn = xmlNodeList[i];
                if (xn.Attributes["visible"].Value == "false")
                    continue;
                DevExpress.XtraNavBar.NavBarGroup nbgTemp = new DevExpress.XtraNavBar.NavBarGroup();
                nbgTemp.Name = xn.Attributes["name"].Value;
                nbgTemp.Caption = xn.Attributes["caption"].Value;
                nbgTemp.DragDropFlags = DevExpress.XtraNavBar.NavBarDragDrop.AllowDrop;
                nbgTemp.ItemChanged += new EventHandler(delegate
                {
                    foreach (DevExpress.XtraNavBar.NavBarGroup group in navMenu.Groups)
                    {
                        if (group.Name != nbgTemp.Name)
                        {
                            foreach (DevExpress.XtraNavBar.NavBarItemLink item in group.ItemLinks)
                            {
                                item.Item.Appearance.ForeColor = Color.FromArgb(235, 235, 235);
                            }
                        }
                    }
                    //navMenu.Refresh();
                });
                //GroupTypeList.Add(new Entity() { string1 = nbgTemp.Name, string2 = nbgTemp.Caption });
                //List<DrawObject> d=new List<DrawObject> ();
                GroupTypeList.Add(new DictionaryEntry(nbgTemp.Name, nbgTemp.Caption));
                for (int j = 0; j < xn.ChildNodes.Count; j++)
                {
                    if (xn.ChildNodes[j].Attributes["visible"].Value == "false")
                        continue;
                    DevExpress.XtraNavBar.NavBarItem nviTemp = new DevExpress.XtraNavBar.NavBarItem();
                    nviTemp.Name = xn.ChildNodes[j].Attributes["name"].Value;
                    nviTemp.Caption = xn.ChildNodes[j].Attributes["caption"].Value;
                    nviTemp.SmallImage = Image.FromFile(menuImagesDirectory + xn.ChildNodes[j].Attributes["name"].Value + ".png");
                    nviTemp.Appearance.ForeColor = Color.FromArgb(235, 235, 235);
                    ArrayList al = ((GraphicsList)docManager.OpenDocumentTemplate(menuFilesDirectory + nviTemp.Name + ".dtl")).graphicsList;
                    IList<DrawObject> temp = new List<DrawObject>();
                    for (int k = 0; k < al.Count; k++)
                    {
                        temp.Add(al[k] as DrawObject);
                    }
                    if (temp.Count > 0)
                    {
                        TemplateItemDictionary.Add(nviTemp.Name, temp);
                    }
                    else
                    {
                        throw new Exception("模板配置文件加载出错");
                    }
                    nviTemp.LinkClicked +=
                        new DevExpress.XtraNavBar.NavBarLinkEventHandler(
                            delegate
                            {
                                foreach (DevExpress.XtraNavBar.NavBarItemLink item in nbgTemp.ItemLinks)
                                {
                                    item.Item.Appearance.ForeColor = Color.FromArgb(235, 235, 235);
                                }
                                nviTemp.Appearance.ForeColor = Color.LightSkyBlue;
                                navMenu.Refresh();
                                CommandTemplate(temp);
                            }
                            );
                    nbgTemp.ItemLinks.Add(nviTemp);
                    TemplateGroupDictionary.Add(new DictionaryEntry(nbgTemp.Name, nviTemp.Name));

                }
                navMenu.Groups.Add(nbgTemp);
            }
            navMenu.Groups[0].Expanded = true;
        }

        #endregion


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
                if (tsbSaveTemp.Visible)
                {
                    e.Cancel = false;
                    return; 
                }
                DialogResult dr = docManager.ClearDrawArea();
                if (dr == DialogResult.No)
                {
                    e.Cancel = false;
                }
                else if (dr == DialogResult.OK)
                {
                    //SaveSettingsToRegistry();
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
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
            tsbPointer.Checked = (drawArea.ActiveTool == DrawToolType.Pointer);
            tsbRectangle.Checked = (drawArea.ActiveTool == DrawToolType.Rectangle);
            tsbEllipse.Checked = (drawArea.ActiveTool == DrawToolType.Ellipse);
            tsbLine.Checked = (drawArea.ActiveTool == DrawToolType.Line);
            tsbPencil.Checked = (drawArea.ActiveTool == DrawToolType.Polygon);

            //pointerToolStripMenuItem.Checked = (drawArea.ActiveTool == DrawToolType.Pointer);
            //rectangleToolStripMenuItem.Checked = (drawArea.ActiveTool == DrawToolType.Rectangle);
            //ellipseToolStripMenuItem.Checked = (drawArea.ActiveTool == DrawToolType.Ellipse);
            //lineToolStripMenuItem.Checked = (drawArea.ActiveTool == DrawToolType.Line);
            //pencilToolStripMenuItem.Checked = (drawArea.ActiveTool == DrawToolType.Polygon);

            bool enabled = drawArea.TheLayers.ActiveLayer.Graphics.Count > 0;
            bool selectedEnabled = drawArea.TheLayers.ActiveLayer.Graphics.SelectionCount > 0;
            // File operations
            tsmiSave.Enabled = enabled;
            tsbSave.Enabled = enabled;
            tsmiSaveAs.Enabled = enabled;

            // 右键菜单
            tsmiCut.Enabled = selectedEnabled;
            tsmiCopy.Enabled = selectedEnabled;
            tsmiDelete.Enabled = selectedEnabled;
            tsmiDeleteAll.Enabled = enabled;
            tsmiSelectAll.Enabled = enabled;
            tsmiUnselectAll.Enabled = selectedEnabled;

            tsmiMoveToFront.Enabled = selectedEnabled;
            tsmiMoveToBack.Enabled = selectedEnabled;
            tsmiProperties.Enabled = drawArea.TheLayers.ActiveLayer.Graphics.SelectionCount == 1;

            tsbUndo.Enabled = drawArea.CanUndo;

            tsbRedo.Enabled = drawArea.CanRedo;

            tslCurrentLayer.Text = drawArea.TheLayers.ActiveLayer.LayerName;
            tslNumberOfObjects.Text = drawArea.TheLayers.ActiveLayer.Graphics.Count.ToString();
            tslPanPosition.Text = drawArea.PanX + ", " + drawArea.PanY;
            tslRotation.Text = drawArea.Rotation + " deg";
            tslZoomLevel.Text = (Math.Round(drawArea.Zoom * 100)) + " %";

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
            docManager.SaveTemplateEvent += docManager_SaveTemplateEvent;
            docManager.LoadTemplateEvent += docManager_LoadTemplateEvent;
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
                tsmiRecentFiles, // Recent Files menu item
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
        #region 打开文件
        public void OpenDocument(string file)
        {
            docManager.OpenDocument(file);
        }
        #endregion


        #region 保存注册表配置
        #region 加载注册表配置
        private void LoadSettingsFromRegistry()
        {
            //try
            //{
            //    RegistryKey key = Registry.CurrentUser.CreateSubKey(registryPath);

            //    DrawObject = System.Drawing.Color.FromArgb((int)key.GetValue(
            //                                                    "Color",
            //                                                    System.Drawing.Color.Black.ToArgb()));

            //    DrawObject.LastUsedPenWidth = (int)key.GetValue(
            //                                        "Width",
            //                                        1);
            //}
            //catch (ArgumentNullException ex)
            //{
            //    HandleRegistryException(ex);
            //}
            //catch (SecurityException ex)
            //{
            //    HandleRegistryException(ex);
            //}
            //catch (ArgumentException ex)
            //{
            //    HandleRegistryException(ex);
            //}
            //catch (ObjectDisposedException ex)
            //{
            //    HandleRegistryException(ex);
            //}
            //catch (UnauthorizedAccessException ex)
            //{
            //    HandleRegistryException(ex);
            //}
        }

        #endregion
        #region 保存配置到注册表
        private void SaveSettingsToRegistry()
        {
            //try
            //{
            //    RegistryKey key = Registry.CurrentUser.CreateSubKey(registryPath);

            //    key.SetValue("Color", DrawObject.LastUsedColor.ToArgb());
            //    key.SetValue("Width", DrawObject.LastUsedPenWidth);
            //}
            //catch (SecurityException ex)
            //{
            //    HandleRegistryException(ex);
            //}
            //catch (ArgumentException ex)
            //{
            //    HandleRegistryException(ex);
            //}
            //catch (ObjectDisposedException ex)
            //{
            //    HandleRegistryException(ex);
            //}
            //catch (UnauthorizedAccessException ex)
            //{
            //    HandleRegistryException(ex);
            //}
        }
        #endregion
        private void HandleRegistryException(Exception ex)
        {
            Trace.WriteLine("Registry operation failed: " + ex.Message);
        }
        #endregion


        #region 改变选择的工具
        #region 选择鼠标
        private void CommandPointer()
        {
            drawArea.ActiveTool = DrawToolType.Pointer;
        }
        #endregion

        #region 选择矩形
        private void CommandRectangle()
        {
            drawArea.ActiveTool = DrawToolType.Rectangle;
            drawArea.DrawFilled = false;
        }
        #endregion

        #region 选择圆
        private void CommandEllipse()
        {
            drawArea.ActiveTool = DrawToolType.Ellipse;
            drawArea.DrawFilled = false;
        }
        #endregion

        #region 选择直线
        private void CommandLine()
        {
            drawArea.ActiveTool = DrawToolType.Line;
        }
        #endregion

        #region 选择多边形
        private void CommandPolygon()
        {
            drawArea.ActiveTool = DrawToolType.Polygon;
        }
        #endregion

        #region MyRegion
        public void CommandTemplate(IList<DrawObject> list)
        {
            drawArea.ActiveTool = DrawToolType.Template;
            drawArea.SetTemplateName(list);
        }
        #endregion
        #endregion

        #region 文件操作
        #region 新建
        private void CommandNew()
        {
            docManager.NewDocument();
        }
        #endregion

        #region 打开
        private void CommandOpen()
        {
            docManager.OpenDocument("");
        }
        #endregion

        #region 保存文件
        private void CommandSave()
        {
            docManager.SaveDocument(DocManager.SaveType.Save);
        }
        #endregion

        #region 另存为
        private void CommandSaveAs()
        {
            docManager.SaveDocument(DocManager.SaveType.SaveAs);
        }
        #endregion

        #region 另存为MVG
        private void tsmiSaveToMVG_Click(object sender, EventArgs e)
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
        #endregion

        #region 另存为BMP
        private void tsmiSaveToBMP_Click(object sender, EventArgs e)
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
        #endregion
        #endregion

        #region 右键菜单
        #region 撤销
        private void CommandUndo()
        {
            drawArea.Undo();
        }
        #endregion

        #region 重做
        private void CommandRedo()
        {
            drawArea.Redo();
        }
        #endregion

        #region 复制
        private void CommandCopy()
        {
            drawArea.CopyObject();
        }
        #endregion

        #region 粘贴
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
        #endregion

        #region 修改属性
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
        #endregion

        #region 剪贴
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
        #endregion

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
            int activeLayerIndex = drawArea.TheLayers.ActiveLayerIndex;

            if (e.Control && e.KeyCode == Keys.C)
            {
                //List<DrawObject> o = new List<DrawObject>();

                //for (int i = drawArea.TheLayers[activeLayerIndex].Graphics.Count - 1; i >= 0; i--)
                //{
                //    if (drawArea.TheLayers[activeLayerIndex].Graphics[i].Selected)
                //    {
                //        o.Add(drawArea.TheLayers[activeLayerIndex].Graphics[i].Clone());
                //    }
                //}
                //drawArea.PrepareCopyObjectList = o;
                //if (drawArea.PrepareHitProject == null && drawArea.PrepareCopyObjectList.Count == 0)
                //{
                //    drawArea.TheLayers[activeLayerIndex].Graphics.UnselectAll();
                //}
                drawArea.PrepareCopyObjectList = drawArea.TheLayers.ActiveLayer.Graphics.Selection;
                drawArea.CopyObject();
                Refresh();
            }
            else if (e.Control && e.KeyCode == Keys.A)
            {
                drawArea.TheLayers[activeLayerIndex].Graphics.SelectAll();
                drawArea.Refresh();
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                drawArea.PasteObject();
                CommandPaste command = new CommandPaste(drawArea.TheLayers);
                drawArea.AddCommandToHistory(command);
                drawArea.TheLayers[activeLayerIndex].Graphics.UnselectAll();
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        CommandDelete command = new CommandDelete(drawArea.TheLayers);

                        if (drawArea.TheLayers[activeLayerIndex].Graphics.DeleteSelection())
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
            dlgColor.Color = tsbLineColor.BackColor;
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
            dlgColor.Color = tsbFillColor.BackColor;
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


        public Image ReduceImage(Image originalImage, int toWidth, int toHeight)
        {
            if (toWidth <= 0 && toHeight <= 0)
            {
                return originalImage;
            }
            else if (toWidth > 0 && toHeight > 0)
            {
                if (originalImage.Width < toWidth && originalImage.Height < toHeight)
                    return originalImage;
            }
            else if (toWidth <= 0 && toHeight > 0)
            {
                if (originalImage.Height < toHeight)
                    return originalImage;
                toWidth = originalImage.Width * toHeight / originalImage.Height;
            }
            else if (toHeight <= 0 && toWidth > 0)
            {
                if (originalImage.Width < toWidth)
                    return originalImage;
                toHeight = originalImage.Height * toWidth / originalImage.Width;
            }
            Image toBitmap = new Bitmap(toWidth, toHeight);
            using (Graphics g = Graphics.FromImage(toBitmap))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.Clear(Color.Transparent);
                g.DrawImage(originalImage,
                            new Rectangle(0, 0, toWidth, toHeight),
                            new Rectangle(0, 0, originalImage.Width, originalImage.Height),
                            GraphicsUnit.Pixel);
                originalImage.Dispose();
                return toBitmap;
            }
        }

        private void navMenu_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DevExpress.XtraNavBar.NavBarHitInfo nbhi = navMenu.CalcHitInfo(new Point(e.X, e.Y));
                if (!nbhi.InGroup)
                    return;
                cmsNavMenu = new ContextMenuStrip();
                if (nbhi.InGroupCaption)
                {
                    navMenu.Groups[nbhi.Group.Name].Expanded = true;
                    if (nbhi.Group.Name == "group1")
                        return;
                    cmsNavMenu.Items.Add("删除分组", null, new EventHandler(delegate
                    {
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("visible", "false");
                        XmlHelper.CreateOrUpdateAttributesByXPath(menuFilePath, "//Menu", "Group", "name", nbhi.Group.Name, dic);
                        navMenu.Groups.Remove(navMenu.Groups[nbhi.Group.Name]);
                    })
                        );
                }
                else if (nbhi.InLink)
                {
                    navMenu.Groups[nbhi.Group.Name].Expanded = true;
                    cmsNavMenu.Items.Add("删除模板", null, new EventHandler(delegate
                    {
                        //if (MessageBox.Show("是否删除'" + nbhi.Link.Caption + "'模板？", "操作提示", MessageBoxButtons.YesNo) == DialogResult.OK)
                        //{
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("visible", "false");
                        XmlHelper.CreateOrUpdateAttributesByXPath(menuFilePath, "//Menu/Group[@name='" + nbhi.Link.Group.Name + "']", "Item", "name", nbhi.Link.ItemName, dic);
                        navMenu.Groups[nbhi.Link.Group.Name].ItemLinks.Remove(nbhi.Link.Item);

                        //navMenu.Refresh();
                        //}
                    })
                        );
                }
                cmsNavMenu.Show(this, new Point(e.X + 2, e.Y + 57));
            }
        }

        private void tsmiNewTemplate_Click(object sender, EventArgs e)
        {
            MainForm frmTemplate = new MainForm();
            frmTemplate.menuStrip1.Visible = false;
            frmTemplate.navMenu.Visible = false;
            frmTemplate.tsbNew.Visible = false;
            frmTemplate.tsbSave.Visible = false;
            frmTemplate.tsbSaveTemp.Visible = true;
            frmTemplate.toolStripStatus.Visible = false;
            frmTemplate.Width = 500;
            frmTemplate.Height = 500;
            frmTemplate.Text = "新增模板";
            //frmTemplate.toolStrip1.LayoutStyle = ToolStripLayoutStyle.Flow;
            frmTemplate.tsbBackColor.DisplayStyle = ToolStripItemDisplayStyle.None;
            frmTemplate.ShowDialog();
            string activeGroupName = navMenu.ActiveGroup.Name;
            navMenu.Groups.Clear();
            navMenu.Items.Clear();
            FillNavBarMenu(menuFilePath);
            navMenu.Groups[activeGroupName].Expanded = true;
        }
    }
}