using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DrawToolsDrawing.GraphicsProperties;

namespace DrawToolsDrawing.PropertiesControl
{
    public partial class ucProperiesLine : ucProperiesBase
    {
        #region Constructor
        public ucProperiesLine()
        {
            InitializeComponent();
        }
        #endregion

        #region Virtual Functions

        /// <summary>
        /// 填充窗体
        /// </summary>
        public override void FillProperties()
        {
            spinOldLineLength.Value = Convert.ToDecimal(GetLenght(graphicsPropertiesBase.StartPoint, ((GraphicsPropertiesLine)graphicsPropertiesBase).EndPoint));
            spinNewLineLength.Value = Convert.ToDecimal(GetLenght(graphicsPropertiesBase.StartPoint, ((GraphicsPropertiesLine)graphicsPropertiesBase).EndPoint));

            cpOldLineColor.Color = ((GraphicsPropertiesLine)graphicsPropertiesBase).LineColor;
            cpNewLineColor.Color = ((GraphicsPropertiesLine)graphicsPropertiesBase).LineColor;

            cmbOldLineWdith.EditValue = ((GraphicsPropertiesLine)graphicsPropertiesBase).LineWidth;
            cmbNewLineWdith.EditValue = ((GraphicsPropertiesLine)graphicsPropertiesBase).LineWidth;

            spinStartPointX.Value = ((GraphicsPropertiesLine)graphicsPropertiesBase).StartPoint.X;
            spinStartPointY.Value = ((GraphicsPropertiesLine)graphicsPropertiesBase).StartPoint.Y;

            if (graphicsPropertiesBase.StartPoint.X==((GraphicsPropertiesLine)graphicsPropertiesBase).EndPoint.X)
            {
                chkIsVertical.Checked=true;
            }
            else if (graphicsPropertiesBase.StartPoint.Y==((GraphicsPropertiesLine)graphicsPropertiesBase).EndPoint.Y)
            {
                chkIsLevel.Checked = true;
            }
            else
            {
                chkIsVertical.Checked = chkIsLevel.Checked = false;
                ;
            }
        }

        /// <summary>
        /// 获取属性
        /// </summary>
        public override GraphicsPropertiesBase GetProperties()
        {
            ((GraphicsPropertiesLine)graphicsPropertiesBase).LineColor = cpNewLineColor.Color;
            ((GraphicsPropertiesLine)graphicsPropertiesBase).LineWidth = Convert.ToInt32(cmbNewLineWdith.EditValue);
            if (chkIsLevel.Checked||chkIsVertical.Checked)
            {
                ((GraphicsPropertiesLine)graphicsPropertiesBase).EndPoint = GetNewEndPoint(chkIsLevel.Checked,graphicsPropertiesBase.StartPoint, Convert.ToDouble(spinNewLineLength.Value));
            }
            return graphicsPropertiesBase;
        }
        /// <summary>
        /// 通过延长线距离获取根据延长线
        /// </summary>
        /// <param name="IsLevel"></param>
        /// <param name="startPoint"></param>
        /// <param name="lineLenght"></param>
        /// <returns></returns>
        public Point GetNewEndPoint(bool IsLevel,Point startPoint, double lineLenght)
        {
            Point point = new Point();
            //原长
            //double l1 = Math.Sqrt(Math.Abs(startPoint.X - endPoint.X) * Math.Abs(startPoint.X - endPoint.X) + Math.Abs(startPoint.Y - endPoint.Y) * Math.Abs(startPoint.Y - endPoint.Y));

            if (IsLevel)
            {
                point.X = Convert.ToInt32(startPoint.X + lineLenght);
                //if (startPoint.X == endPoint.X)
                //    point.X = Convert.ToInt32(startPoint.X);
                //else if (startPoint.X - endPoint.X > 0)
                //    point.X = Convert.ToInt32(startPoint.X - lineLenght);
                //else
                //    point.X = Convert.ToInt32(startPoint.X + lineLenght);
                point.Y = Convert.ToInt32(startPoint.Y);
            }
            else
            {
                point.X = Convert.ToInt32(startPoint.X);
                point.Y = Convert.ToInt32(startPoint.Y + lineLenght);

                //if (startPoint.Y == endPoint.Y)
                //    point.Y = Convert.ToInt32(startPoint.Y);
                //else if (startPoint.Y - endPoint.Y > 0)
                //    point.Y = Convert.ToInt32(startPoint.Y - lineLenght);
                //else
                //    point.Y = Convert.ToInt32(startPoint.Y + lineLenght);
            }
            
            


            ////起始点X坐标距新新结束点X坐标距离
            //double xWidth = Math.Abs(endPoint.X - Math.Abs(startPoint.X - endPoint.X) / (l1 / lineLenght));
            ////起始点Y坐标距新新结束点Y坐标距离
            //double yHeight = Math.Abs(endPoint.Y - Math.Abs(startPoint.Y - endPoint.Y) / (l1 / lineLenght));
            //if (startPoint.X - endPoint.X < 0)
            //    point.X = Convert.ToInt32(startPoint.X + xWidth);
            //else if (startPoint.X == endPoint.X)
            //    point.X = Convert.ToInt32(startPoint.X);
            //else
            //    point.X = Convert.ToInt32(startPoint.X - xWidth);
            //if (startPoint.Y - endPoint.Y < 0)
            //    point.Y = Convert.ToInt32(startPoint.Y + yHeight);
            //else if (startPoint.Y == endPoint.Y)
            //    point.Y = Convert.ToInt32(startPoint.Y);
            //else
            //    point.Y = Convert.ToInt32(startPoint.Y - yHeight);
            return point;
        }
        /// <summary>
        /// 获取两点之间的距离
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public double GetLenght(Point startPoint, Point endPoint)
        {
            return Math.Sqrt(Math.Abs(startPoint.X - endPoint.X) * Math.Abs(startPoint.X - endPoint.X) + Math.Abs(startPoint.Y - endPoint.Y) * Math.Abs(startPoint.Y - endPoint.Y));
        }
        #endregion

        private void chkIsVertical_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsVertical.Checked)
            {
                chkIsLevel.Checked = false;
            }
        }

        private void chkIsLevel_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsLevel.Checked)
            {
                chkIsVertical.Checked = false;
            }
        }
    }
}
