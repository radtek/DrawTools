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
                if (typeName == "DrawTools.DrawPIE")
                    typeName = "DrawTools.DrawPie";

                object drawObject;
                drawObject = Assembly.GetExecutingAssembly().CreateInstance(typeName);
                //((DrawObject)drawObject).OpenSubFT_Status += Layer_OpenSubFT_Status;
                //if (i == 84)
                //{

                //    int d = 1;
                //    d++;
                //}

                //((DrawObject)drawObject).fileDirectory = this.filedirectory;
                ((DrawObject)drawObject).LoadFromStream(info, orderNumber, i);

                //this.drawlist[((DrawObject)drawObject).TIEDAID].Append(((DrawObject)drawObject));

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

                // string a = ((DrawObject)drawObject).TIEDASTRING;

                string a = ((DrawObject)drawObject).TIEDASTRING;

                if (a != "" && a != null)
                {
                    //if (!Regex.IsMatch(a, @"^\d{1,3}.*$"))
                    //{
                        a = a.Remove(0, 1);
                        a = a.Remove(a.Length - 1, 1);
                    //}

                    if (a.StartsWith("0;;"))
                    {

                    }
                    else if (a.StartsWith("LK;"))
                    {
                        a = a.Remove(0, 3);
                        LKStringList.Add(a);
                        this.LKlist.Append(((DrawObject)drawObject));
                    }
                    else if (a.StartsWith("DBM;"))
                    {

                        a = a.Remove(0, 4);
                        ((DrawObject)drawObject).IsDianBaoMa = true;
                        ((DrawObject)drawObject).DianBaoMa = a;
                        DBMDIC.Add(a, (DrawObject)drawObject);
                    }
                    else
                    {
                        string[] hh = a.Split(new Char[] { ';' }, 20, StringSplitOptions.RemoveEmptyEntries);
                        int count = 1;
                        foreach (string childstring in hh)
                        {
                            string[] childchild = childstring.Split(new Char[] { ',' }, 20, StringSplitOptions.RemoveEmptyEntries);

                            int index1 = -1;
                            //if (!string.IsNullOrEmpty(childchild[0]))
                                int.TryParse(childchild[0], out index1);

                                int index2 = -1;
                            //if (!string.IsNullOrEmpty(childchild[1]))
                                int.TryParse(childchild[1], out index2);

                           // ((DrawObject)drawObject).Color = Color.Lime;
                            ((DrawObject)drawObject).FillColor = Color.Lime;
                            if (index1 == 0)
                            {
                                if (!DigitList.Contains(index2))
                                {
                                    DigitList.Add(index2);
                                }
                               
                                if (count == 1)
                                {
                                    ((DrawObject)drawObject).statusnum = 1;
                                    // this.drawlist[index1, index2].Append(((DrawObject)drawObject));
                                    this.DigitGraphicsList[index1, index2].Listcolor = System.Drawing.Color.Lime;
                                    this.DigitGraphicsList[index1, index2].Append(((DrawObject)drawObject));
                                }
                                if (count == 2)
                                {
                                    ((DrawObject)drawObject).statusnum = 2;
                                    this.DigitGraphicsList[index1, index2].Listcolor = System.Drawing.Color.Yellow;
                                    this.DigitGraphicsList[index1, index2].Append(((DrawObject)drawObject));
                                }
                                if (count == 3)
                                {
                                    ((DrawObject)drawObject).statusnum = 3;
                                    this.DigitGraphicsList[index1, index2].Listcolor = System.Drawing.Color.Red;
                                    this.DigitGraphicsList[index1, index2].Append(((DrawObject)drawObject));
                                }
                                count++;
                            }
                            else// if(index1!=-1 && index2!=-1)
                            {
                                if (AnalogGraphicsList.GetUpperBound(0) <= index1 || AnalogGraphicsList.GetUpperBound(1) <= index2)
                                {
                                    MessageBox.Show("Ä£ÄâÁ¿ÅäÖÃÔ½½ç:±àºÅ" + index1 + "" + index2);
                                }
                                else
                                {
                                    if (!AnalogList.ContainsKey(index1))
                                        AnalogList.Add(index1, new List<int>());
                                    if (!AnalogList[index1].Contains(index2))
                                        AnalogList[index1].Add(index2);

                                    this.AnalogGraphicsList[index1, index2].Append(((DrawObject)drawObject));
                                }
                                // drawArea.TheLayers[al].drawlist[index1, index2].Append(o);
                            }
                            //else
                            //    MessageBox.Show("´íÎó±àºÅ" + index1 + "" + index2);

                           

                        }
                    }
                }
                _graphicsList.Append((DrawObject)drawObject);
            }
        }
        void Layer_OpenSubFT_Status(object sender, EventArgs e)
        {
            if (OpenSubFT_Status != null)
                OpenSubFT_Status(sender, new EventArgs());
        }

        public event EventHandler OpenSubFT_Status;
		internal void Draw(Graphics g)
		{
			_graphicsList.Draw(g);
		}


	}
}