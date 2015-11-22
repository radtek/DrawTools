﻿using System.Collections.Generic;
using DrawToolsDrawing;
using DrawToolsDrawing.Draw;

namespace DrawTools.Command
{
    /// <summary>
    /// Delete command
    /// </summary>
    internal class CommandPaste : Command
    {
        private List<DrawObject> cloneList; // contains selected items which are deleted

        // Create this command BEFORE applying Delete All function.
        public CommandPaste(Layers list)
        {
            cloneList = new List<DrawObject>();

            // Make clone of the list selection.

            foreach (DrawObject o in list[list.ActiveLayerIndex].Graphics.Selection)
            {
                cloneList.Add(o.Clone());
            }
        }

        public override void Undo(Layers list)
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

        public override void Redo(Layers list)
        {

            list[list.ActiveLayerIndex].Graphics.UnselectAll();

            // Add all objects from cloneList to list.

            for (int i = cloneList.Count - 1; i >= 0; i--)
            {
                list[list.ActiveLayerIndex].Graphics.Add(cloneList[i]);
            }
        }
    }
}