#region Using directives
using System.Drawing;
#endregion

namespace DrawToolsDrawing.GraphicsProperties
{
    /// <summary>
    /// Helper class used to show properties
    /// for one or more graphic objects
    /// </summary>
    public abstract class GraphicsPropertiesBase
    {
        #region MyRegion
        /// <summary>
        /// ��ɫ
        /// </summary>
        public Color LineColor{get;set;}
        /// <summary>
        /// �߿�
        /// </summary>
        public int LineWidth { get; set; }
        /// <summary>
        /// ��ʼ����
        /// </summary>
        public Point StartPoint { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public DrawingPens.PenType LineType { get; set; }
        /// <summary>
        /// �Ƿ������ɫ
        /// </summary>
        public bool Filled { get; set; }
        /// <summary>
        /// �����ɫ
        /// </summary>
        public Color FillColor { get; set; }
        #endregion

        #region Constructor
        public GraphicsPropertiesBase()
        {
            LineColor = System.Drawing.Color.White;
            LineWidth = 1;
            StartPoint = new Point(1,1);
            LineType = DrawingPens.PenType.Generic;
            Filled = false;
            FillColor = System.Drawing.Color.Black;
        } 
        #endregion
    }
}