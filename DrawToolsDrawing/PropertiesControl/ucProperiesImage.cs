using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DrawToolsDrawing.GraphicsProperties;

namespace DrawToolsDrawing.PropertiesControl
{
    public partial class ucProperiesImage : ucProperiesBase
    {
        private Bitmap bitmap;
        #region Constructor
        public ucProperiesImage()
        {
            InitializeComponent();
        }
        #endregion

        #region 窗体事件
        private void buttonEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "图片文件(*.jpg,*.gif,*.bmp)|*.jpg;*.gif;*.bmp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                btnImage.Text = ofd.FileName;
            }
        }
        #endregion
        #region Virtual Functions
        /// <summary>
        /// 填充窗体
        /// </summary>
        public override void FillProperties()
        {
            spinOldWidth.Value = ((GraphicsPropertiesImage)graphicsPropertiesBase).Rectabgle.Width;
            spinNewWidth.Value = ((GraphicsPropertiesImage)graphicsPropertiesBase).Rectabgle.Width;

            spinOldHeight.Value = ((GraphicsPropertiesImage)graphicsPropertiesBase).Rectabgle.Height;
            spinNewHeight.Value = ((GraphicsPropertiesImage)graphicsPropertiesBase).Rectabgle.Height;
            btnImage.Text = "";
            bitmap = ((GraphicsPropertiesImage)graphicsPropertiesBase).Bitmap;
        }
        /// <summary>
        /// 获取属性
        /// </summary>
        public override GraphicsPropertiesBase GetProperties()
        {
            ((GraphicsPropertiesImage)graphicsPropertiesBase).Rectabgle.Height = Convert.ToInt32(spinNewHeight.Value);
            ((GraphicsPropertiesImage)graphicsPropertiesBase).Rectabgle.Width = Convert.ToInt32(spinNewWidth.Value);
            ((GraphicsPropertiesImage)graphicsPropertiesBase).Bitmap = btnImage.Text != "" ? new Bitmap(btnImage.Text) : null;
            return graphicsPropertiesBase;
        }
        #endregion

    }
}
