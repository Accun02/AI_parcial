using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PFNodes : MonoBehaviour
{
    [SerializeField]
    private List<PFNodes> neighbors = new List<PFNodes>();
    [SerializeField] private int widthPos;
    [SerializeField] private int heightPos;
    [SerializeField] private float cost = 1;
    [SerializeField] private bool blocked;
    
     public float radius = 5f;
    public float Range = 10;
    public bool Blocked => blocked;
    public List<PFNodes> Neighbors
    {
        get { return neighbors; }
    }

  
    public int x { get { return widthPos; } }
    public int y { get { return heightPos; } }
    public float Cost { get { return cost; } }


    public void SetIndexes(int w, int h)
    {
        widthPos = w;
        heightPos = h;
    }
    public void SetNeighbors( LayerMask nodes, LayerMask  obstacles)
    {
  
        Collider[] Hits = Physics.OverlapSphere(this.transform.position, radius, nodes);
        for (int i = 0; i < Hits.Length; i++)
        {
            Vector3 dir = Hits[i].transform.position - transform.position;
            if (!Physics.Raycast(transform.position, dir.normalized, dir.magnitude, obstacles))
            {
                PFNodes neigbourNode = Hits[i].gameObject.GetComponent<PFNodes>();
                if (neigbourNode != null) 
                neighbors.Add(neigbourNode);
            }
            
        }
    }
    }

