using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using DrawToolsDrawing.Draw;
namespace DrawToolsDrawing
{
    public partial class EditLayerService
    {
       public EditLayerService(GraphicsList CurrentList,Layer layer)
        {
            this.CurrentList = CurrentList;

        }

        public DrawObject CurrentObject;
        public GraphicsList CurrentList;
        public int JudgeVertical(Point A,Point B)
        {
            if ((Math.Abs(A.X - B.X ))< 4)
            {
                return 1;
            }
            if ((Math.Abs(A.Y - B.Y)) < 4)
            {
                return 2;
            }
            return 0;
        }
        public int MoveService(DrawObject client,GraphicsList GList,Point Goal)
        {

            
            client.PretendToMoveStart(Goal.X, Goal.Y);
            foreach (DrawObject o in GList.graphicsList)
            {
                if (o == client)
                {
                    continue;

                }
                else
                {   //judge the point?if line judge point, if rectangle?
                    //we have two point list
                    ArrayList clientpoints = client.GetCriticalPointList();
                    ArrayList Servicepoints = o.GetCriticalPointList();

                    for (int i = 0; i < clientpoints.Count; i++)
                    {
                        for(int j=0;j<Servicepoints.Count;j++)
                        {
                            int result = JudgeVertical(((Point)clientpoints[i]), ((Point)Servicepoints[j]));
                            if (result>0)
                            {
                                
                                client.PretendToMoveOver(Goal.X, Goal.Y);

                                if (result == 1)
                                {
                                    client.Move(((Point)Servicepoints[j]).X, Goal.Y);
                                    return 1;

                                }
                                else if (result == 2)
                                {
                                    client.Move(Goal.X, ((Point)Servicepoints[j]).Y);
                                    return 1;

                                }
                                else
                                {

                                }
                                

                            }

                        }

                    }

                }


            }
            client.PretendToMoveOver(Goal.X, Goal.Y);
            //every object has his own relation algrithum critical point 
            //otherobject
            //if havent relation
            client.Move(Goal.X, Goal.Y);
            //client origin move(Goal)
            //if haverelation 
            //
            return 1;
        }

       
    }
}
