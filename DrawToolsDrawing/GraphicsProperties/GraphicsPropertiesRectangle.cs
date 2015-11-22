using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DrawToolsDrawing.GraphicsProperties
{
    public class GraphicsPropertiesRectangle : GraphicsPropertiesBase
    {
        public Rectangle Rectabgle;

        public GraphicsPropertiesRectangle() 
        {
            Rectabgle = new Rectangle(1, 1, 1, 1);
        }
    }
}
