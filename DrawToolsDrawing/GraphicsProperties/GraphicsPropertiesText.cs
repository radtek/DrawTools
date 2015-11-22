using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DrawToolsDrawing.GraphicsProperties
{
    public class GraphicsPropertiesText : GraphicsPropertiesBase
    {
        #region Properties
        public Font TextFont { get; set; }
        public Color BackGroundColor { get; set; }
        public Color TextColor { get; set; }
        public string Note { get; set; }
        public bool IsVerticalText { get; set; }
        #endregion
        #region MyRegion
        public GraphicsPropertiesText() 
        {
            Note = "";
            TextFont = new Font("宋体", 13, FontStyle.Regular);
            BackGroundColor = System.Drawing.Color.Black;
            TextColor = System.Drawing.Color.White;
            IsVerticalText = false;
        }
        #endregion
    }
}
