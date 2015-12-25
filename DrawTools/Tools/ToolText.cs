using System.Drawing;
using System.Windows.Forms;
using DrawToolsDrawing.Draw;

namespace DrawTools.Tools
{
	/// <summary>
	/// Rectangle tool
	/// </summary>
	internal class ToolText : ToolObject
	{
		public ToolText()
		{
            //Cursor = new Cursor(GetType(), "Rectangle.cur");
            Cursor = new Cursor(Application.StartupPath + "\\Resources\\Cursor\\Rectangle.cur");
		}

		public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
		{
			Point p = drawArea.BackTrackMouse(new Point(e.X, e.Y));
            DrawText drawText = new DrawText(p,drawArea.LineColor,drawArea.BackColor);
            if (drawText.ShowPropertiesDialog())
			{
                AddNewObject(drawArea, new DrawText(p, drawText.NowProperties));
                drawArea.ActiveTool = DrawToolType.Pointer;
			}
		}

		public override void OnMouseMove(DrawArea drawArea, MouseEventArgs e)
		{
			drawArea.Cursor = Cursor;
			int al = drawArea.TheLayers.ActiveLayerIndex;
			if (e.Button == MouseButtons.Left)
			{
				Point point = drawArea.BackTrackMouse(new Point(e.X, e.Y));
				drawArea.TheLayers[al].Graphics[0].MoveHandleTo(point, 5);
				drawArea.Refresh();
			}
		}
	}
}