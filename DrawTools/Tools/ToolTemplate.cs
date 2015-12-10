using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DrawTools.Command;
using DrawToolsDrawing.Draw;

namespace DrawTools.Tools
{
    internal class ToolTemplate : ToolObject
    {
        IList<DrawObject> template;
        public ToolTemplate() 
        {
            //Cursor = new Cursor(GetType(), "Rectangle.cur");
            Cursor = new Cursor(Application.StartupPath + "\\Resources\\Cursor\\Rectangle.cur");
        }
        public ToolTemplate(IList<DrawObject> template)
        {
            //Cursor = new Cursor(GetType(), "Rectangle.cur");
            Cursor = new Cursor(Application.StartupPath + "\\Resources\\Cursor\\Rectangle.cur");
            this.template = template;
        }

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            Point p = drawArea.BackTrackMouse(new Point(e.X, e.Y));
            drawArea.PasteTemplateObject(template);
            CommandPaste command = new CommandPaste(drawArea.TheLayers);
            drawArea.AddCommandToHistory(command);
        }

        public override void OnMouseMove(DrawArea drawArea, MouseEventArgs e)
        {
            drawArea.Cursor = Cursor;
        }

        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            drawArea.ActiveTool = DrawToolType.Pointer;
            drawArea.Capture = false;
            drawArea.Refresh();
        }
    }
}
