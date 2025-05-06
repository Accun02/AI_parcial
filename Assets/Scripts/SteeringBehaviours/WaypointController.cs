using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
public class WaypointController : MonoBehaviour //ruta que sigue el enemigo
{
    public Transform Enemy;
    public Transform[] waypoints;
    public int Targetpoints = 0;
    public float radiustotarget =0.5f;
    private bool goingback;



    public bool checkdistancetowaypoint() //verifica si el enemigo llegó lo suficientemente cerca del waypoint actual
    {
        if(Vector3.Distance(Enemy.transform.position, waypoints[Targetpoints].position) <= radiustotarget)
        {
            increaseposition(); //cambia al prox waypoint segun la direccion
            return true; //esta en el rango
        }
        else { return false; } //no llego aun
    }
    private void increaseposition() //cambia el índice del waypoint según si va hacia adelante o hacia atrás
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