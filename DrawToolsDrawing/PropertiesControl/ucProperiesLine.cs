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
    public partial class ucProperiesLine : ucProperiesBase
    {
        #region Constructor
        public ucProperiesLine()
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
            spinOldLineLength.Value = ((GraphicsPropertiesLine)graphicsPropertiesBase).LineLength;
            spinNewLineLength.Value = ((GraphicsPropertiesLine)graphicsPropertiesBase).LineLength;

            cpOldLineColor.Color = ((GraphicsPropertiesLine)graphicsPropertiesBase).LineColor;
            cpNewLineColor.Color = ((GraphicsPropertiesLine)graphicsPropertiesBase).LineColor;

            cmbOldLineWdith.EditValue = ((GraphicsPropertiesLine)graphicsPropertiesBase).LineWidth;
            cmbNewLineWdith.EditValue = ((GraphicsPropertiesLine)graphicsPropertiesBase).LineWidth;
        }

        /// <summary>
        /// 获取属性
        /// </summary>
        public override GraphicsPropertiesBase GetProperties()
        {
            ((GraphicsPropertiesLine)graphicsPropertiesBase).LineLength = Convert.ToInt32(spinNewLineLength.Value);
            ((GraphicsPropertiesLine)graphicsPropertiesBase).LineColor = cpNewLineColor.Color;
            ((GraphicsPropertiesLine)graphicsPropertiesBase).LineWidth = Convert.ToInt32(cmbNewLineWdith.SelectedText);
            return graphicsPropertiesBase;
        }
        #endregion
    }
}
