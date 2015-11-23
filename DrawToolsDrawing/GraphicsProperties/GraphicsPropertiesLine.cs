using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DrawToolsDrawing.GraphicsProperties
{
    public class GraphicsPropertiesLine : GraphicsPropertiesBase
    {
        #region Properties
        public Point EndPoint{get;set;}
        public bool? IsVertical { get; set; }
        #endregion
        #region Constructor
        public GraphicsPropertiesLine()
        {
            EndPoint = new Point(1, 1);
        } 
        #endregion
    }
}
