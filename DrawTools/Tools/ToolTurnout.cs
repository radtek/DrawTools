using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DrawTools;
using DrawTools.Command;
using DrawToolsDrawing.Draw;

namespace DrawTools.Tools
{
    internal class ToolTurnout : ToolObject
    {
        private Point startPoint;
        public ToolTurnout()
        {
            Cursor = new Cursor("..\\..\\Resources\\Cursor\\Ellipse.cur");
        }
        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            startPoint = drawArea.BackTrackMouse(new Point(e.X, e.Y));

            // AddNewObject(drawArea, new DrawPie(p.X, p.Y, 1, 1, drawArea.LineColor, drawArea.FillColor, drawArea.DrawFilled, drawArea.LineWidth, 0, 45));
        }

        public override void OnMouseMove(DrawArea drawArea, MouseEventArgs e)
        {
            Point p = drawArea.BackTrackMouse(new Point(e.X, e.Y));
            drawArea.Cursor = Cursor;

            if (e.Button == MouseButtons.Left)
            {
                drawArea.TheLayers[drawArea.TheLayers.ActiveLayerIndex].Graphics.Dirty = true;
            }
        }

        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            int al = drawArea.TheLayers.ActiveLayerIndex;


            Point p = drawArea.BackTrackMouse(new Point(e.X, e.Y));


            if (drawArea.TheLayers[al].Graphics.Count > 0)
                drawArea.TheLayers[al].Graphics[0].Normalize();
            if (startPoint.X < p.X && startPoint.Y < p.Y)
                AddNewObject(drawArea, new DrawPie(startPoint.X, startPoint.Y, p.X - startPoint.X, p.Y - startPoint.Y, System.Drawing.Color.Lime, System.Drawing.Color.Lime, true, drawArea.LineWidth, 0, 45));
            else if (startPoint.X < p.X && startPoint.Y > p.Y)
                AddNewObject(drawArea, new DrawPie(startPoint.X, startPoint.Y, p.X - startPoint.X, startPoint.Y-p.Y, System.Drawing.Color.Lime, System.Drawing.Color.Lime, true, drawArea.LineWidth, 0, -45));
            else if (startPoint.X > p.X && startPoint.Y < p.Y)
                AddNewObject(drawArea, new DrawPie(startPoint.X, startPoint.Y, startPoint.X-p.X, p.Y - startPoint.Y, System.Drawing.Color.Lime, System.Drawing.Color.Lime, true, drawArea.LineWidth, 180, -45));
            else if (startPoint.X > p.X && startPoint.Y > p.Y)
                AddNewObject(drawArea, new DrawPie(startPoint.X, startPoint.Y, startPoint.X-p.X, startPoint.Y-p.Y, System.Drawing.Color.Lime, System.Drawing.Color.Lime, true, drawArea.LineWidth, 180, 45));
            drawArea.AddCommandToHistory(new CommandAdd(drawArea.TheLayers[drawArea.TheLayers.ActiveLayerIndex].Graphics[0]));
            drawArea.ActiveTool = DrawToolType.Pointer;

            drawArea.Capture = false;
            drawArea.Refresh();

        }
    }
}
