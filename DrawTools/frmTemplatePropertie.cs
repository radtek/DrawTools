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

namespace DrawTools
{
    /// <summary>
    /// 打开属性框
    /// </summary>
    public partial class frmTemplatePropertie : Form
    {
        public string ItemCaption;
        public string GroupName;
        public string GroupCaption;
         
        //添加using System.Runtime.InteropServices;
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        #region Constructor
        public frmTemplatePropertie()
        {
            InitializeComponent();
        } 
        #endregion
 
        #region EventHandler
        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbTemplateMenu.Text.Trim()))
            {
                MessageBox.Show("请选择或填写类别");
                return;
            }
            if (string.IsNullOrEmpty(txtTemplateText.Text.Trim()))
            {
                MessageBox.Show("请输入名称");
                return;
            }

            ItemCaption = txtTemplateText.Text;
            GroupCaption = cmbTemplateMenu.Text;
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
