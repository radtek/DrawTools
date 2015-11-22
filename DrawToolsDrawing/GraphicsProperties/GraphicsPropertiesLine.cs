using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrawToolsDrawing.GraphicsProperties
{
    public class GraphicsPropertiesLine : GraphicsPropertiesBase
    {
        #region Properties
        public int LineLength { get; set; }
        #endregion
        #region Constructor
        public GraphicsPropertiesLine()
        {
            LineLength = 1;
        } 
        #endregion
    }
}
