using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Windows.Forms;
using DrawToolsDrawing.Draw;

namespace DrawToolsDrawing
{
    /// <summary>
    /// List of graphic objects
    /// </summary>
    [Serializable]
    public class GraphicsList : ISerializable
    {


        public Color Listcolor = System.Drawing.Color.White;

        public ArrayList graphicsList;
        private bool _isDirty;

        public bool Dirty
        {
            get
            {
                if (_isDirty == false)
                {
                    foreach (DrawObject o in graphicsList)
                    {
                        if (o.Dirty)
                        {
                            _isDirty = true;
                            break;
                        }
                    }
                }
                return _isDirty;
            }
            set
            {
                foreach (DrawObject o in graphicsList)
                    o.Dirty = false;
                _isDirty = false;
            }
        }

        /// <summary>
        /// Returns IEnumerable object which may be used for enumeration
        /// of selected objects.
        /// 
        /// Note: returning IEnumerable<DrawObject> breaks CLS-compliance
        /// (assembly CLSCompliant = true is removed from AssemblyInfo.cs).
        /// To make this program CLS-compliant, replace 
        /// IEnumerable<DrawObject> with IEnumerable. This requires
        /// casting to object at runtime.
        /// </summary>
        /// <value></value>
        public List<DrawObject> Selection
        {
            get
            {
                List<DrawObject> list = new List<DrawObject>();
                foreach (DrawObject o in graphicsList)
                {
                    if (o.Selected)
                    {
                        list.Add(o);
                    }
                }
                return list;
            }
        }

        private const string entryCount = "ObjectCount";
        private const string entryType = "ObjectType";

        #region 构造
        public GraphicsList()
        {
            graphicsList = new ArrayList();
        }

        public GraphicsList(SerializationInfo info, StreamingContext context)
        {
            graphicsList = new ArrayList();
            int count = info.GetInt32(entryCount);
            for (int i = 0; i < count; i++)
            {
                string typeName = info.GetString(
                   String.Format(CultureInfo.InvariantCulture,
                                 "{0}{1}-{2}",
                                 entryType, 0, i));
                object _drawObject;
                _drawObject = Assembly.GetExecutingAssembly().CreateInstance(typeName);
                ((DrawObject)_drawObject).LoadFromStream(info, 0, i);
                graphicsList.Add(_drawObject);
            }
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(entryCount, Count);
            int addCount = 0;
            foreach (DrawObject drawObject in graphicsList)
            {
                info.AddValue(
                    String.Format(CultureInfo.InvariantCulture,
                                  "{0}{1}-{2}",
                                  entryType, 0, addCount),
                    drawObject.GetType().FullName);
                drawObject.SaveToStream(info, 0, addCount);
                addCount++;
            }
        }
        #endregion

        #region 保存/新增
        public void LoadFromStream(SerializationInfo info, int orderNumber)
        {
            graphicsList = new ArrayList();

            // Get number of DrawObjects in this GraphicsList
            int numberObjects = info.GetInt32(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}",
                              entryCount, orderNumber));

            for (int i = 0; i < numberObjects; i++)
            {
                string typeName;
                typeName = info.GetString(
                    String.Format(CultureInfo.InvariantCulture,
                                  "{0}{1}",
                                  entryType, i));

                object drawObject;
                drawObject = Assembly.GetExecutingAssembly().CreateInstance(
                    typeName);

                // Let the Draw Object load itself
                ((DrawObject)drawObject).LoadFromStream(info, orderNumber, i);

                graphicsList.Add(drawObject);
            }
        }

        public void SaveToStream(SerializationInfo info, int orderNumber)
        {
            // First store the number of DrawObjects in the list
            info.AddValue(
                String.Format(CultureInfo.InvariantCulture,
                              "{0}{1}",
                              entryCount, orderNumber),
                graphicsList.Count);
            // Next save each individual object
            int i = 0;
            foreach (DrawObject o in graphicsList)
            {
                info.AddValue(
                    String.Format(CultureInfo.InvariantCulture,
                                  "{0}{1}",
                                  entryType, i),
                    o.GetType().FullName);
                // Let each object save itself
                o.SaveToStream(info, orderNumber, i);
                i++;
            }
        }
        #endregion

        #region 画图
        public void Draw(Graphics g)
        {
            int numberObjects = graphicsList.Count;

            // Enumerate list in reverse order
            // to get first object on the top
            //graphicsList.Sort();
            for (int i = numberObjects - 1; i >= 0; i--)
            {
                DrawObject o;
                o = (DrawObject)graphicsList[i];
                // Only draw objects that are visible
                if (o.IntersectsWith(Rectangle.Round(g.ClipBounds)))
                    o.Draw(g);

                if (o.Selected)
                    o.DrawTracker(g);
            }
        }
        #endregion

        /// <summary>
        /// Clear all objects in the list
        /// </summary>
        /// <returns>
        /// true if at least one object is deleted
        /// </returns>
        public bool Clear()
        {
            bool result = (graphicsList.Count > 0);
            graphicsList.Clear();
            // Set dirty flag based on result. Result is true only if at least one item was cleared and since the list is empty, there can be nothing dirty.
            if (result)
                _isDirty = false;
            return result;
        }

        /// <summary>
        /// Count and this [nIndex] allow to read all graphics objects
        /// from GraphicsList in the loop.
        /// </summary>
        public int Count
        {
            get { return graphicsList.Count; }
        }
        /// <summary>
        /// Allow accessing Draw Objects by index
        /// </summary>
        /// <param name="index">0-based index to retrieve</param>
        /// <returns>Selected DrawObject</returns>
        public DrawObject this[int index]
        {
            get
            {
                if (index < 0 ||
                    index >= graphicsList.Count)
                    return null;

                return (DrawObject)graphicsList[index];
            }
        }

        /// <summary>
        /// SelectedCount and GetSelectedObject allow to read
        /// selected objects in the loop
        /// </summary>
        public int SelectionCount
        {
            get
            {
                int n = 0;

                foreach (DrawObject o in graphicsList)
                {
                    if (o.Selected)
                        n++;
                }

                return n;
            }
        }

        public DrawObject GetSelectedObject(int index)
        {
            int n = -1;

            foreach (DrawObject o in graphicsList)
            {
                if (o.Selected)
                {
                    n++;

                    if (n == index)
                        return o;
                }
            }

            return null;
        }

        public void Add(DrawObject obj)
        {
            graphicsList.Sort();
            foreach (DrawObject o in graphicsList)
                o.ZOrder++;

            graphicsList.Insert(0, obj);
        }

        /// <summary>
        /// Thanks to Member 3272353 for this fix to object ordering problem.
        /// </summary>
        /// <param name="obj"></param>
        public void Append(DrawObject obj)
        {
            graphicsList.Add(obj);
        }

        public void SelectInRectangle(Rectangle rectangle)
        {
            UnselectAll();

            foreach (DrawObject o in graphicsList)
            {
                if (o.IntersectsWith(rectangle))
                    o.Selected = true;
            }
        }

        public void UnselectAll()
        {
            foreach (DrawObject o in graphicsList)
            {
                o.Selected = false;
            }
        }

        public void SelectAll()
        {
            foreach (DrawObject o in graphicsList)
            {
                o.Selected = true;
            }
        }

        /// <summary>
        /// Delete selected items
        /// </summary>
        /// <returns>
        /// true if at least one object is deleted
        /// </returns>
        public bool DeleteSelection()
        {
            bool result = false;

            int n = graphicsList.Count;

            for (int i = n - 1; i >= 0; i--)
            {
                if (((DrawObject)graphicsList[i]).Selected)
                {
                    graphicsList.RemoveAt(i);
                    result = true;
                }
            }
            if (result)
                _isDirty = true;
            return result;
        }

        /// <summary>
        /// Delete last added object from the list
        /// (used for Undo operation).
        /// </summary>
        public void DeleteLastAddedObject()
        {
            if (graphicsList.Count > 0)
            {
                graphicsList.RemoveAt(0);
            }
        }

        /// <summary>
        /// Replace object in specified place.
        /// Used for Undo.
        /// </summary>
        public void Replace(int index, DrawObject obj)
        {
            if (index >= 0 &&
                index < graphicsList.Count)
            {
                graphicsList.RemoveAt(index);
                graphicsList.Insert(index, obj);
            }
        }

        /// <summary>
        /// Remove object by index.
        /// Used for Undo.
        /// </summary>
        public void RemoveAt(int index)
        {
            graphicsList.RemoveAt(index);
        }

        /// <summary>
        /// Move selected items to front (beginning of the list)
        /// </summary>
        /// <returns>
        /// true if at least one object is moved
        /// </returns>
        public bool MoveSelectionToFront()
        {
            int n;
            int i;
            ArrayList tempList;

            tempList = new ArrayList();
            n = graphicsList.Count;

            // Read source list in reverse order, add every selected item
            // to temporary list and remove it from source list
            for (i = n - 1; i >= 0; i--)
            {
                if (((DrawObject)graphicsList[i]).Selected)
                {
                    tempList.Add(graphicsList[i]);
                    graphicsList.RemoveAt(i);
                }
            }

            // Read temporary list in direct order and insert every item
            // to the beginning of the source list
            n = tempList.Count;

            for (i = 0; i < n; i++)
            {
                graphicsList.Insert(0, tempList[i]);
            }
            if (n > 0)
                _isDirty = true;
            return (n > 0);
        }

        /// <summary>
        /// Move selected items to back (end of the list)
        /// </summary>
        /// <returns>
        /// true if at least one object is moved
        /// </returns>
        public bool MoveSelectionToBack()
        {
            int n;
            int i;
            ArrayList tempList;

            tempList = new ArrayList();
            n = graphicsList.Count;

            // Read source list in reverse order, add every selected item
            // to temporary list and remove it from source list
            for (i = n - 1; i >= 0; i--)
            {
                if (((DrawObject)graphicsList[i]).Selected)
                {
                    tempList.Add(graphicsList[i]);
                    graphicsList.RemoveAt(i);
                }
            }

            // Read temporary list in reverse order and add every item
            // to the end of the source list
            n = tempList.Count;

            for (i = n - 1; i >= 0; i--)
            {
                graphicsList.Add(tempList[i]);
            }
            if (n > 0)
                _isDirty = true;
            return (n > 0);
        }

        #region 移动选择项
        public void MoveSelection(int moveX, int moveY)
        {
            foreach (DrawObject drawObject in graphicsList)
            {
                if (drawObject.Selected)
                {
                    drawObject.Move(moveX, moveY);

                }
            }
        }
        #endregion
    }
}