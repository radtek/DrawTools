using System.Collections.Generic;
using DrawToolsDrawing;
using DrawToolsDrawing.Draw;

namespace DrawTools.Command
{
    /// <summary>
    /// Changing state of existing objects:
    /// move, resize, change properties.
    /// </summary>
    internal class CommandChangeProperty : Command
    {
        private DrawObject prepareHitProject;
        public CommandChangeProperty(DrawObject prepareHitProject)
        {
            this.prepareHitProject = prepareHitProject;
        }


        public override void Undo(Layers list)
        {
            if (this.prepareHitProject != null)
            {
                prepareHitProject.ApplyProperties(prepareHitProject.OldProperties);
            }

        }

        public override void Redo(Layers list)
        {
            if (this.prepareHitProject != null)
            {
                prepareHitProject.ApplyProperties(prepareHitProject.NewProperties);
            }
        }
    }
}