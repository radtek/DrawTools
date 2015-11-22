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
    public partial class ucProperiesRectangle : ucProperiesBase
    {
        #region Constructor
        public ucProperiesRectangle()
        {
            InitializeComponent();
        } 
        #endregion

        #region Virtual Functions
        /// <summary>
        /// 填充窗体
        /// </summary>
        public override void FillProperties()
        {
            spinOldWidth.Value = ((GraphicsPropertiesRectangle)graphicsPropertiesBase).Rectabgle.Width;
            spinNewWidth.Value = ((GraphicsPropertiesRectangle)graphicsPropertiesBase).Rectabgle.Width;

            spinOldHeight.Value = ((GraphicsPropertiesRectangle)graphicsPropertiesBase).Rectabgle.Height;
            spinNewHeight.Value = ((GraphicsPropertiesRectangle)graphicsPropertiesBase).Rectabgle.Height;

            cpOldLineColor.Color = ((GraphicsPropertiesRectangle)graphicsPropertiesBase).LineColor;
            cpNewLineColor.Color = ((GraphicsPropertiesRectangle)graphicsPropertiesBase).LineColor;

            cmbOldLineWdith.EditValue = ((GraphicsPropertiesRectangle)graphicsPropertiesBase).LineWidth;
            cmbNewLineWdith.EditValue = ((GraphicsPropertiesRectangle)graphicsPropertiesBase).LineWidth;

            cpOldFillColor.Color = ((GraphicsPropertiesRectangle)graphicsPropertiesBase).FillColor;
            cpNewFillColor.Color = ((GraphicsPropertiesRectangle)graphicsPropertiesBase).FillColor;

            chkFilled.Checked = ((GraphicsPropertiesRectangle)graphicsPropertiesBase).Filled;
        }
        /// <summary>
        /// 获取属性
        /// </summary>
        public override GraphicsPropertiesBase GetProperties()
        {
            ((GraphicsPropertiesRectangle)graphicsPropertiesBase).Rectabgle.Height = Convert.ToInt32(spinNewHeight.Value);
            ((GraphicsPropertiesRectangle)graphicsPropertiesBase).Rectabgle.Width = Convert.ToInt32(spinNewWidth.Value);
            ((GraphicsPropertiesRectangle)graphicsPropertiesBase).LineColor = cpNewLineColor.Color;
            ((GraphicsPropertiesRectangle)graphicsPropertiesBase).LineWidth = Convert.ToInt32(cmbNewLineWdith.EditValue);
            ((GraphicsPropertiesRectangle)graphicsPropertiesBase).FillColor = cpNewFillColor.Color;
            ((GraphicsPropertiesRectangle)graphicsPropertiesBase).Filled = chkFilled.Checked;
            return graphicsPropertiesBase;
        }
        #endregion
    }
}
