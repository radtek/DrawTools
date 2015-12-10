using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using DrawToolsDrawing.Draw;
namespace DrawTools.Tools
{
    /// <summary>
    /// Image tool
    /// </summary>
    internal class ToolImage : ToolObject
    {
        public ToolImage()
        {
            //Cursor = new Cursor(GetType(), "Rectangle.cur");
            Cursor = new Cursor(Application.StartupPath + "\\Resources\\Cursor\\Rectangle.cur");
        }
        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            Point p = drawArea.BackTrackMouse(new Point(e.X, e.Y));
            AddNewObject(drawArea, new DrawImage(p.X, p.Y));
        }

        public override void OnMouseMove(DrawArea drawArea, MouseEventArgs e)
        {
            drawArea.Cursor = Cursor;

            if (e.Button ==
                MouseButtons.Left)
            {
                Point point = drawArea.BackTrackMouse(new Point(e.X, e.Y));
                int al = drawArea.TheLayers.ActiveLayerIndex;
                drawArea.TheLayers[al].Graphics[0].MoveHandleTo(point, 5);
                drawArea.Refresh();
            }
        }

        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "选择一个插入的图片";
            ofd.Filter = "图片文件(*.jpg,*.gif,*.bmp)|*.jpg;*.gif;*.bmp";
            ofd.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
            int al = drawArea.TheLayers.ActiveLayerIndex;
            if (ofd.ShowDialog() ==
                DialogResult.OK)
            {
                Bitmap d = new Bitmap(ofd.FileName);
                ((DrawImage)drawArea.TheLayers[al].Graphics[0]).image = (Bitmap)(d.Clone());
                ((DrawImage)drawArea.TheLayers[al].Graphics[0]).ResizeImage(((DrawImage)drawArea.TheLayers[al].Graphics[0]).rectangle.Width, ((DrawImage)drawArea.TheLayers[al].Graphics[0]).rectangle.Height);
            }
            ofd.Dispose();
            base.OnMouseUp(drawArea, e);
        }
    }
}