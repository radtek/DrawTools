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
        /// 线色
        /// </summary>
        public Color LineColor{get;set;}
        /// <summary>
        /// 线宽
        /// </summary>
        public int LineWidth { get; set; }
        /// <summary>
        /// 起始坐标
        /// </summary>
        public Point StartPoint { get; set; }
        /// <summary>
        /// 线型
        /// </summary>
        public DrawingPens.PenType LineType { get; set; }
        /// <summary>
        /// 是否填充颜色
        /// </summary>
        public bool Filled { get; set; }
        /// <summary>
        /// 填充颜色
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