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
        public bool IsAdd=false;

        //添加using System.Runtime.InteropServices;
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        #region Constructor
        public frmTemplatePropertie(List<DictionaryEntry> groupTypeList)
        {
            InitializeComponent();
            cmbTemplateMenu.Properties.DataSource = groupTypeList;
        }
        #endregion

        #region EventHandler
        private void btnCheck_Click(object sender, EventArgs e)
        {
            #region 非空验证
            if (btnAdd.Visible)
            {
                if (string.IsNullOrEmpty(cmbTemplateMenu.Text.Trim()))
                {
                    MessageBox.Show("请选择类别");
                    return;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtTemplateMenu.Text.Trim()))
                {
                    MessageBox.Show("请填写类别");
                    return;
                }
            }
            if (string.IsNullOrEmpty(txtTemplateText.Text.Trim()))
            {
                MessageBox.Show("请输入名称");
                return;
            } 
            #endregion

            IsAdd = !btnAdd.Visible;
            if (IsAdd)
            {
                GroupCaption = txtTemplateMenu.Text;
            }
            else
            {
                GroupCaption = cmbTemplateMenu.Text;
                GroupName = cmbTemplateMenu.EditValue.ToString();
            }
            ItemCaption = txtTemplateText.Text;
            
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

        private void frmTemplatePropertie_Load(object sender, EventArgs e)
        {
            txtTemplateMenu.Visible = false;
            btnBack.Visible = false;
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            txtTemplateMenu.Visible = false;
            btnBack.Visible = false;
            btnAdd.Visible = true;
            cmbTemplateMenu.Visible = true;
            cmbTemplateMenu.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtTemplateMenu.Visible = true;
            btnBack.Visible = true;
            btnAdd.Visible = false;
            cmbTemplateMenu.Visible = false;
            txtTemplateMenu.Focus();
        }
        #endregion
    }
    class Entity 
    {
        public int int1 { get; set; }
        public int int2 { get; set; }
        public int int3 { get; set; }
        public int int4 { get; set; }
        public int int5 { get; set; }
        public int int6 { get; set; }
        public int int7 { get; set; }
        public int int8 { get; set; }
        public int int9 { get; set; }
        public int int10 { get; set; }

        public string string1 { get; set; }
        public string string2 { get; set; }
        public string string3 { get; set; }
        public string string4 { get; set; }
        public string string5 { get; set; }
        public string string6 { get; set; }
        public string string7 { get; set; }
        public string string8 { get; set; }
        public string string9 { get; set; }
        public string string10 { get; set; }
    }
}
