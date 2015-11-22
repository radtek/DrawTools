using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DrawToolsDrawing.Draw;

namespace DrawTools.Tools
{
    internal  class ToolStationTrack:ToolLine
    {
        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            Point p = drawArea.BackTrackMouse(new Point(e.X, e.Y));
            DrawLine o =new DrawLine(p.X, p.Y, p.X + 1, p.Y + 1, System.Drawing.Color.Lime, 10);
            o.needcircle = true;
            AddNewObject(drawArea,o );
        }

    }
}
