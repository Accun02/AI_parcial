using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PFEntity : MonoBehaviour
{
    public PFNodes endNode;
    public float reachDistance;
    [SerializeField] private List<PFNodes> path;
    float radius = 100;
    [SerializeField] LayerMask NodeLayerMask;
    [SerializeField] LayerMask Obsmask;
    public float speed;

    int node = 0;
    public List<PFNodes> SetPath { set { path = value; } }

    // Update is called once per frame
  public  void Move()
    {
        if (path.Count > 0)
        {
            Vector3 dir = path[node].transform.position - transform.position;
            transform.position += dir.normalized * speed * Time.deltaTime;
            if (dir.sqrMagnitude < reachDistance)

            {
                node++;
            }
        }
    }

    public bool checkdistancetowaypoint() //verifica si el enemigo llegó lo suficientemente cerca del waypoint actual
    {
        if (Vector3.Distance(transform.position, path[node].transform.position) <= reachDistance)
        {
        //cambia al prox waypoint segun la direccion
            return true; //esta en el rango
        }
        else { return false; } //no llego aun
    }
    public PFNodes SearchClose()
    {
        Collider[] Hits = Physics.OverlapSphere(this.transform.position, radius, NodeLayerMask);
        for (int i = 0; i < Hits.Length; i++)
        {
            Vector3 dir = Hits[i].transform.position - transform.position;
            if (Physics.Raycast(transform.position, dir.normalized, dir.magnitude, Obsmask))
            {
                Hits[i] = null;
            }
        }

        Collider Closestto = null;

        float closest = 200;

        foreach (Collider c in Hits)
        {
            if (c == null) continue;

            float distance = Vector3.Distance(transform.position, c.transform.position);

            if (Closestto == null || distance < closest)
            {
                closest = distance;
                Closestto = c;
            }

        
        }
        PFNodes closenode = Closestto.gameObject.GetComponent<PFNodes>();
        Debug.Log(closenode);
        return closenode;
    }
}
