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
    public partial class ucProperiesBase : UserControl
    {
        #region Properties
        protected GraphicsPropertiesBase graphicsPropertiesBase = null; 
        #endregion

        #region Constructor
        public ucProperiesBase()
        {
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="graphicsProperties"></param>
        public void SetGraphicsProperties(GraphicsPropertiesBase graphicsProperties)
        {
            this.graphicsPropertiesBase = graphicsProperties;

        }

        #region Virtual Functions

        /// <summary>
        /// 填充窗体
        /// </summary>
        public virtual void FillProperties()
        {

        }

        /// <summary>
        /// 获取属性
        /// </summary>
        public virtual GraphicsPropertiesBase GetProperties()
        {
            return null;
        }
        #endregion
    }
}
