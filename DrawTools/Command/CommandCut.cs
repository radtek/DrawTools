using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using DrawToolsDrawing.Draw;
using DrawToolsDrawing;

namespace DrawTools.Command
{

    
    public class CommandCut : Command
    {
        private List<DrawObject> cloneList;
        public CommandCut(List<DrawObject> list)
		{
			cloneList = new List<DrawObject>();

			// Make clone of the list selection.

			foreach (DrawObject o in list)
			{
				cloneList.Add(o.Clone());
			}
		}

        public override void Undo(Layers list)
        {
            list[list.ActiveLayerIndex].Graphics.UnselectAll();

            // Add all objects from cloneList to list.
            foreach (DrawObject o in cloneList)
            {
                list[list.ActiveLayerIndex].Graphics.Add(o);
            }
        }

        public override void Redo(Layers list)
        {
            // Delete from list all objects kept in cloneList

            int n = list[list.ActiveLayerIndex].Graphics.Count;

            for (int i = n - 1; i >= 0; i--)
            {
                bool toDelete = false;
                DrawObject objectToDelete = list[list.ActiveLayerIndex].Graphics[i];

                foreach (DrawObject o in cloneList)
                {
                    if (objectToDelete.ID ==
                        o.ID)
                    {
                        toDelete = true;
                        break;
                    }
                }

                if (toDelete)
                {
                    list[list.ActiveLayerIndex].Graphics.RemoveAt(i);
                }
            }
        }


    }
}
