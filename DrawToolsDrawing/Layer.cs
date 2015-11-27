using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DrawToolsDrawing.Draw;

namespace DrawToolsDrawing
{
	//[Serializable]
	/// <summary>
	/// Each <see cref="Layer"/> contains a list of Graphic Objects <see cref="GraphicsList"/>.
	/// Each <see cref="Layer"/> is contained in the <see cref="Layers"/> collection.
	/// Properties:
	///	LayerName		User-defined name of the Layer
	///	Graphics			Collection of <see cref="DrawObject"/>s in the Layer
	///	IsVisible			True if the <see cref="Layer"/> is visible
	///	IsActive			True if the <see cref="Layer"/> is the active <see cref="Layer"/> (only one <see cref="Layer"/> may be active at a time)
	///	Dirty				True if any object in the <see cref="Layer"/> is dirty
	/// </summary>
	public class Layer
	{
		private string _name;
		private bool _isDirty;
		private bool _visible;
		private bool _active;
		private GraphicsList _graphicsList;
        public GraphicsList[,] AnalogGraphicsList;

        public GraphicsList[,] DigitGraphicsList;
        public Dictionary<string, DrawObject> DBMDIC;
        public GraphicsList LKlist;
        public List<string> LKStringList;

        public Dictionary<int, List<int>> AnalogList;
        public List<int> DigitList;

        public Layer()
        {
            LKStringList = new List<string>();
            LKlist = new GraphicsList();
            AnalogGraphicsList = new GraphicsList[20, 500]; //80 4000
            DigitGraphicsList = new GraphicsList[1, 10000]; //65535

            AnalogList = new Dictionary<int, List<int>>();
            DigitList = new List<int>();

            DBMDIC = new Dictionary<string, DrawObject>();
            DBMDIC.Clear();
            for (int i = 0; i < 20; i++)
            {
                for (int k = 0; k < 500;k++ )
                    AnalogGraphicsList[i,k] = new GraphicsList();

            }

            for (int i = 0; i < 1; i++)
            {
                for (int k = 0; k < 10000; k++)
                    DigitGraphicsList[i, k] = new GraphicsList();

            }
            MyService = new EditLayerService(this.Graphics, this);

        }
        EditLayerService MyService;
		/// <summary>
		/// <see cref="Layer"/> Name (User-defined)
		/// </summary>
		public string LayerName
		{
			get { return _name; }
			set { _name = value; }
		}
        public int TheMostLeft = 65535;
        public int TheMostRight = 0;
        public int TheMostButtom = 0;
        public int TheMostTop = 65535;
		/// <summary>
		/// List of Graphic objects (derived from <see cref="DrawObject"/>) contained by this <see cref="Layer"/>
		/// </summary>
		public GraphicsList Graphics
		{
			get { return _graphicsList; }
			set { _graphicsList = value; }
		}

		/// <summary>
		/// Returns True if this <see cref="Layer"/> is visible, else False
		/// </summary>
		public bool IsVisible
		{
			get { return _visible; }
			set { _visible = value; }
		}

		/// <summary>
		/// Returns True if this is the active <see cref="Layer"/>, else False
		/// </summary>
		public bool IsActive
		{
			get { return _active; }
			set { _active = value; }
		}

		/// <summary>
		/// Dirty is True if any elements in the contained <see cref="GraphicsList"/> are dirty, else False
		/// </summary>
		public bool Dirty
		{
			get
			{
				if (_isDirty == false)
					_isDirty = _graphicsList.Dirty;
				return _isDirty;
			}
			set
			{
				_graphicsList.Dirty = false;
				_isDirty = false;
			}
		}
       
        
		private const string entryLayerName = "LayerName";
		private const string entryLayerVisible = "LayerVisible";
		private const string entryLayerActive = "LayerActive";
		private const string entryObjectType = "ObjectType";
		private const string entryGraphicsCount = "GraphicsCount";

		public void SaveToStream(SerializationInfo info, int orderNumber)
		{
			info.AddValue(
				String.Format(CultureInfo.InvariantCulture,
				              "{0}{1}",
				              entryLayerName, orderNumber),
				_name);

			info.AddValue(
				String.Format(CultureInfo.InvariantCulture,
				              "{0}{1}",
				              entryLayerVisible, orderNumber),
				_visible);

			info.AddValue(
				String.Format(CultureInfo.InvariantCulture,
				              "{0}{1}",
				              entryLayerActive, orderNumber),
				_active);

			info.AddValue(
				String.Format(CultureInfo.InvariantCulture,
				              "{0}{1}",
				              entryGraphicsCount, orderNumber),
				_graphicsList.Count);

			for (int i = 0; i < _graphicsList.Count; i++)
			{
				object o = _graphicsList[i];
				info.AddValue(
					String.Format(CultureInfo.InvariantCulture,
					              "{0}{1}-{2}",
					              entryObjectType, orderNumber, i),
					o.GetType().FullName);

				((DrawObject)o).SaveToStream(info, orderNumber, i);
			}
		}
        public string filedirectory;
        public void LoadFromStream(SerializationInfo info, int orderNumber)
        {
            //try
            //{
            _graphicsList = new GraphicsList();

            _name = info.GetString(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}",
                              entryLayerName, orderNumber));

            _visible = info.GetBoolean(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}",
                              entryLayerVisible, orderNumber));

            _active = info.GetBoolean(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}",
                              entryLayerActive, orderNumber));

            int n = info.GetInt32(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}",
                              entryGraphicsCount, orderNumber));

            for (int i = 0; i < n; i++)
            {
                DateTime dt = DateTime.Now;

                string typeName;
                typeName = info.GetString(
                    String.Format(CultureInfo.InvariantCulture,
                                  "{0}{1}-{2}",
                                  entryObjectType, orderNumber, i));
                object drawObject;
                drawObject = Assembly.GetExecutingAssembly().CreateInstance(typeName);

                ((DrawObject)drawObject).LoadFromStream(info, orderNumber, i);

                if (((DrawObject)drawObject).GetMostButtom() != (-1))
                {
                    if (((DrawObject)drawObject).GetMostButtom() > this.TheMostButtom)
                    {
                        this.TheMostButtom = ((DrawObject)drawObject).GetMostButtom();
                    }

                }
                if (((DrawObject)drawObject).GetMostLeft() != (-1))
                {
                    if (((DrawObject)drawObject).GetMostLeft() < this.TheMostLeft)
                    {
                        this.TheMostLeft = ((DrawObject)drawObject).GetMostLeft();
                    }

                }
                if (((DrawObject)drawObject).GetMostTop() != (-1))
                {
                    if (((DrawObject)drawObject).GetMostTop() < this.TheMostTop)
                    {
                        this.TheMostTop = ((DrawObject)drawObject).GetMostTop();
                    }

                }
                if (((DrawObject)drawObject).GetMostRight() != (-1))
                {
                    if (((DrawObject)drawObject).GetMostRight() > this.TheMostRight)
                    {
                        this.TheMostRight = ((DrawObject)drawObject).GetMostRight();
                    }

                }
                _graphicsList.Append((DrawObject)drawObject);
            }
        }
        //void Layer_OpenSubFT_Status(object sender, EventArgs e)
        //{
        //    if (OpenSubFT_Status != null)
        //        OpenSubFT_Status(sender, new EventArgs());
        //}

        //public event EventHandler OpenSubFT_Status;
		internal void Draw(Graphics g)
		{
			_graphicsList.Draw(g);
		}


	}
}