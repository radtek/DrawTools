using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using DrawToolsDrawing.Draw;

namespace DrawToolsDrawing.PropertiesControl
{
    /// <summary>
    /// 打开属性框
    /// </summary>
    public partial class MainPropertie : Form
    {
        //添加using System.Runtime.InteropServices;
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        #region Properties
        private static MainPropertie frmMain = null; 
        #endregion

        #region Constructor
        private MainPropertie()
        {
            InitializeComponent();
        } 
        #endregion

        #region Public Function
        /// <summary>
        /// 打开此窗体
        /// </summary>
        /// <param name="drawObject">画图</param>
        public static DialogResult ShowPropertiesDialog(DrawObject drawObject)
        {                          
            if (frmMain == null)
                frmMain = new MainPropertie();
            frmMain.SelectionUcProperiesForm(drawObject);
            DialogResult dr = frmMain.ShowDialog();
            if (dr==DialogResult.OK)
            {
                drawObject.SetOldProperties();
                drawObject.NewProperties = ((ucProperiesBase)frmMain.plProperties.Controls[0]).GetProperties();
            }
            return dr;
        } 
        #endregion

        #region Private Function
        /// <summary>
        /// 选择窗体
        /// </summary>
        /// <param name="drawObject">图形</param>
        private Hashtable htUcCache = null; //缓存属性窗体
        private void SelectionUcProperiesForm(DrawObject drawObject)
        {
            //清除
            plProperties.Controls.Clear();

            if (htUcCache == null)
                htUcCache = new Hashtable();
            //缓存
            if (!htUcCache.ContainsKey(drawObject.drawingType))
                htUcCache.Add(drawObject.drawingType, GetNewUcProperies(drawObject.GetType()));
            //设置参数
            (htUcCache[drawObject.drawingType] as ucProperiesBase).SetGraphicsProperties(drawObject.NowProperties);
            //填充窗体
            (htUcCache[drawObject.drawingType] as ucProperiesBase).FillProperties();
            //设置属性窗体
            SetUcProperiesForm(htUcCache[drawObject.drawingType] as ucProperiesBase);
        }

        /// <summary>
        /// 获取新的属性窗体
        /// </summary>
        /// <param name="drawObject">图形</param>
        /// <returns></returns>
        private ucProperiesBase GetNewUcProperies(Type type)
        {
            ucProperiesBase ucBase = null;
            if (type.Name == new DrawLine().GetType().Name)
            {
                ucBase = new ucProperiesLine();
            }
            else if (type.Name == new DrawText().GetType().Name)
            {
                ucBase = new ucProperiesText();
            }
            else if (type.Name == new DrawRectangle().GetType().Name)
            {
                ucBase = new ucProperiesRectangle();
            }
            else if (type.Name == new DrawImage().GetType().Name)
            {
                ucBase = new ucProperiesImage();
            }
            else
            {
                if (type.Name != "DrawObject")
                {
                   ucBase= GetNewUcProperies(type.BaseType);
                }
                else
                {
                    ucBase = new ucProperiesBase();
                }
            }
            return ucBase;
        }
        /// <summary>
        /// 填充需要显示属性的用户窗体
        /// </summary>
        /// <param name="ucBase">属性用户窗体</param>
        private void SetUcProperiesForm(ucProperiesBase ucBase)
        {
            plProperties.Controls.Add(ucBase);
            this.Height = plTitle.Height + plCommon.Height + ucBase.Height;
            this.Width = ucBase.Width;
        } 
        #endregion

        #region EventHandler
        private void btnCheck_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void plTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture(); //释放鼠标捕捉
                //发送左键点击的消息至该窗体(标题栏)
                SendMessage(Handle, 0xA1, 0x02, 0);
            }
        }
        #endregion


    }
}
