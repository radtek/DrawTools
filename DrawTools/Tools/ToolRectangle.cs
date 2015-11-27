using System.Drawing;
using System.Windows.Forms;
using DrawToolsDrawing.Draw;

namespace DrawTools.Tools
{
	/// <summary>
	/// Rectangle tool
	/// </summary>
	internal class ToolRectangle : ToolObject
	{
		public ToolRectangle()
		{
			//Cursor = new Cursor(GetType(), "Rectangle.cur");
            //Cursor = new Cursor("..\\..\\Cursor\\Rectangle.cur");
            Cursor = new Cursor("..\\..\\Resources\\Cursor\\Rectangle.cur");
		}

		public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
		{
			Point p = drawArea.BackTrackMouse(new Point(e.X, e.Y));
			if (drawArea.CurrentPen == null)
                if (drawArea.DrawFilled && drawArea.LineWidth==-1)
                    AddNewObject(drawArea, new DrawRectangle(p.X, p.Y, 1, 1, drawArea.LineColor, drawArea.FillColor, drawArea.DrawFilled, 1));
                else
                    AddNewObject(drawArea, new DrawRectangle(p.X, p.Y, 1, 1, drawArea.LineColor, drawArea.FillColor, drawArea.DrawFilled, drawArea.LineWidth));
			else
				AddNewObject(drawArea, new DrawRectangle(p.X, p.Y, 1, 1, drawArea.PenType, drawArea.FillColor, drawArea.DrawFilled));
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