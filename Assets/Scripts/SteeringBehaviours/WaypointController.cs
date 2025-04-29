using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
public class WaypointController : MonoBehaviour 
{
    public Transform Enemy;
    public Transform[] waypoints;
    public int Targetpoints = 0;
    public float radiustotarget =0.5f;
    private bool goingback;



    public bool checkdistancetowaypoint()
    {
        if(Vector3.Distance(Enemy.transform.position, waypoints[Targetpoints].position) <= radiustotarget)
        {
            increaseposition();
            return true;
        }
        else { return false; }
    }
    private void increaseposition()
    {
        if (goingback)
        {
            if (Targetpoints < waypoints.Length - 1)
            {
                Targetpoints++;
            }
            else
            {
                goingback = false;
                Targetpoints--;
            }


        }
        else
        {
           if (Targetpoints > 0)
            {
                Targetpoints--;

            }
           else
            {
                goingback = true; Targetpoints++;
            }
        }
     



    }

  
}