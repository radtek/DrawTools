using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DrawToolsDrawing.Draw;

namespace DrawTools.Tools
{
    internal class ToolTemplate : ToolObject
    {
        public ToolTemplate()
        {
            Cursor = new Cursor("..\\..\\Resources\\Cursor\\Rectangle.cur");
        }

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            Point p = drawArea.BackTrackMouse(new Point(e.X, e.Y));

        }

        public override void OnMouseMove(DrawArea drawArea, MouseEventArgs e)
        {
            drawArea.Cursor = Cursor;
            int al = drawArea.TheLayers.ActiveLayerIndex;
            if (e.Button ==
                MouseButtons.Left)
            {
                Point point = drawArea.BackTrackMouse(new Point(e.X, e.Y));
                drawArea.TheLayers[al].Graphics[0].MoveHandleTo(point, 5);
                drawArea.Refresh();
            }
        }
    }
}
