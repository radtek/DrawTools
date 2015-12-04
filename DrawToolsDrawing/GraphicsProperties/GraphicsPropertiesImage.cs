using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DrawToolsDrawing.GraphicsProperties
{
    public class GraphicsPropertiesImage : GraphicsPropertiesBase
    {
        public Rectangle Rectabgle;
        public Bitmap Bitmap; 
        public GraphicsPropertiesImage() 
        {
            Rectabgle = new Rectangle(1, 1, 1, 1);
        }
    }
}
