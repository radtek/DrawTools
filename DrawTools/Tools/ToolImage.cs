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
        private bool isCustom;
        private string imageName;
		public ToolImage()
		{
            Cursor = new Cursor("..\\..\\Resources\\Cursor\\Rectangle.cur");
		}

        public ToolImage(bool isCustom,string imageName)
        {
            Cursor = new Cursor("..\\..\\Resources\\Cursor\\Rectangle.cur");
            this.isCustom = isCustom;
            this.imageName = imageName;
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
            if (isCustom)
            {
                ((DrawImage)drawArea.TheLayers[drawArea.TheLayers.ActiveLayerIndex].Graphics[0]).TheImage = (Bitmap)Bitmap.FromFile("ZTT\\" + imageName + ".gif");
                base.OnMouseUp(drawArea, e);
            }
            else
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Select an Image to insert into map";
                //ofd.Filter = "Bitmap (*.bmp)|*.bmp|JPEG (*.jpg)|*.jpg|Fireworks (*.png)|*.png|GIF (*.gif)|*.gif|Icon (*.ico)|*.ico|All files|*.*";
                ofd.Filter = "WMFÊ¸Á¿Í¼ (*.wmf)|*.wmf";
                ofd.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
                int al = drawArea.TheLayers.ActiveLayerIndex;
                if (ofd.ShowDialog() ==
                    DialogResult.OK)
                // ((DrawImage)drawArea.TheLayers[al].Graphics[0]).TheImage = (Bitmap)Bitmap.FromFile(ofd.FileName);
                // ((DrawImage)drawArea.TheLayers[al].Graphics[0]).TheImage = (Metafile)Metafile.FromFile(ofd.FileName);
                {
                    // Metafile fiele = new Metafile(ofd.FileName);

                    // string a = "D:\\G\\fra.wmf";
                    Bitmap d = new Bitmap(ofd.FileName);
                    // g.DrawImage(d, rectangle);
                    //((DrawImage)drawArea.TheLayers[al].Graphics[0]).TheImage = d;
                    ((DrawImage)drawArea.TheLayers[al].Graphics[0])._image = (Bitmap)(d.Clone());
                    ((DrawImage)drawArea.TheLayers[al].Graphics[0]).filename = ofd.FileName;
                    ((DrawImage)drawArea.TheLayers[al].Graphics[0]).ResizeImage(((DrawImage)drawArea.TheLayers[al].Graphics[0]).rectangle.Width, ((DrawImage)drawArea.TheLayers[al].Graphics[0]).rectangle.Height);
                }
                ofd.Dispose();
                base.OnMouseUp(drawArea, e);
            }
		}
	}
}