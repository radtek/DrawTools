using System;
using System.Windows.Forms;

namespace DrawTools.Tools
{
	/// <summary>
	/// Base class for all drawing tools
	/// </summary>
	internal abstract class Tool
	{
		/// <summary>
		/// Left nous button is pressed
		/// </summary>
		/// <param name="drawArea"></param>
		/// <param name="e"></param>
		public virtual void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
		{
		}


		/// <summary>
		/// Mouse is moved, left mouse button is pressed or none button is pressed
		/// </summary>
		/// <param name="drawArea"></param>
		/// <param name="e"></param>
		public virtual void OnMouseMove(DrawArea drawArea, MouseEventArgs e)
		{
		}


		/// <summary>
		/// Left mouse button is released
		/// </summary>
		/// <param name="drawArea"></param>
		/// <param name="e"></param>
		public virtual void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
		{
		}


        public virtual void OnMouseDoubleClick(DrawArea drawArea, MouseEventArgs e)
        {


        }

        public virtual void MouseLeave(DrawArea drawArea, EventArgs e)
        {

        }

	}
}