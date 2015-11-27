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
    public partial class ucProperiesText : ucProperiesBase
    {

        #region Construcotr
        public ucProperiesText()
        {
            InitializeComponent();
        } 
        #endregion

        private void btnSetFont_Click(object sender, EventArgs e)
        {
            fontDialog.AllowSimulations = true;
            fontDialog.AllowVectorFonts = true;
            fontDialog.AllowVerticalFonts = true;
            fontDialog.MaxSize = 200;
            fontDialog.MinSize = 4;
            fontDialog.ShowApply = true;
            fontDialog.Apply += new EventHandler(SetProperties);
            fontDialog.ShowColor = true;
            fontDialog.ShowEffects = true;
            if (DialogResult.OK == fontDialog.ShowDialog())
            {
                SetProperties(null, null);
            }
        }

        /// <summary>
        /// 应用时填充属性
        /// </summary>
        public void SetProperties(object o, EventArgs e)
        {
            ((GraphicsPropertiesText)graphicsPropertiesBase).TextColor = fontDialog.Color;
            ((GraphicsPropertiesText)graphicsPropertiesBase).TextFont = fontDialog.Font;
            FillProperties();
        }

        #region  Virtual Functions
        /// <summary>
        /// 绑定属性
        /// </summary>
        public override void FillProperties()
        {
            fontDialog.Font = ((GraphicsPropertiesText)graphicsPropertiesBase).TextFont;
            fontDialog.Color = ((GraphicsPropertiesText)graphicsPropertiesBase).TextColor;
            cpFillBackGroundColor.Color = ((GraphicsPropertiesText)graphicsPropertiesBase).BackGroundColor;
            txtNote.Font = ((GraphicsPropertiesText)graphicsPropertiesBase).TextFont;
            txtNote.ForeColor = ((GraphicsPropertiesText)graphicsPropertiesBase).TextColor;
            if (((GraphicsPropertiesText)graphicsPropertiesBase).Filled)
            {
                txtNote.BackColor = ((GraphicsPropertiesText)graphicsPropertiesBase).BackGroundColor; 
            }
            txtNote.Text = ((GraphicsPropertiesText)graphicsPropertiesBase).Note;
            cmbFont.SelectedItem = ((GraphicsPropertiesText)graphicsPropertiesBase).TextFont.Name;
            txtSize.Text = ((GraphicsPropertiesText)graphicsPropertiesBase).TextFont.Size.ToString();
            txtStyle.Text = ((GraphicsPropertiesText)graphicsPropertiesBase).TextFont.Style.ToString();
            cpFontColor.Color = ((GraphicsPropertiesText)graphicsPropertiesBase).TextColor;
            chkUnderline.Checked = ((GraphicsPropertiesText)graphicsPropertiesBase).TextFont.Underline;
            chkDeleteLine.Checked = ((GraphicsPropertiesText)graphicsPropertiesBase).TextFont.Strikeout;
        }
        /// <summary>
        /// 获取属性
        /// </summary>
        public override GraphicsPropertiesBase GetProperties()
        {
            ((GraphicsPropertiesText)graphicsPropertiesBase).TextColor = cpFontColor.Color;
            ((GraphicsPropertiesText)graphicsPropertiesBase).TextFont = txtNote.Font;
            ((GraphicsPropertiesText)graphicsPropertiesBase).Note = txtNote.Text;
            ((GraphicsPropertiesText)graphicsPropertiesBase).BackGroundColor = cpFillBackGroundColor.Color;
            ((GraphicsPropertiesText)graphicsPropertiesBase).Filled = chkFilledColor.Checked;
            ((GraphicsPropertiesText)graphicsPropertiesBase).IsVerticalText = chkVerticalText.Checked;
            return graphicsPropertiesBase;
        }
        #endregion

        private void chkFilledColor_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFilledColor.Checked)
            {
                txtNote.BackColor = cpFillBackGroundColor.Color;
            }
        }

        private void cpFillBackGroundColor_EditValueChanged(object sender, EventArgs e)
        {
            if (chkFilledColor.Checked)
            {
                txtNote.BackColor = cpFillBackGroundColor.Color;
            }
        }
    }
}
