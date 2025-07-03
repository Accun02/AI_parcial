using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PFNodes : MonoBehaviour
{
    [SerializeField]
    private List<PFNodes> neighbors = new List<PFNodes>();
    [SerializeField] private int widthPos;
    [SerializeField] private int heightPos;
    [SerializeField] private float cost;
    [SerializeField] private bool blocked;
    private Renderer rend;
    [SerializeField] LayerMask Nodemask;
    [SerializeField] LayerMask Wallmask;
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

    private void Awake()
    {

        Collider[] hits = Physics.OverlapSphere(transform.position, radius,Nodemask);
        List<PFNodes> nodeclose = new List<PFNodes>();
        for (int i = 0; i < hits.Length; i++) 
        {
            if (hits[i].transform == this.transform) 
            {
                continue;
            }
            if (hits[i].TryGetComponent<PFNodes>(out PFNodes component))
            {
                nodeclose.Add(component);
            }
            
        }
        for ( int j = 0; j < nodeclose.Count; j++) 
        {

            if(!Physics.Raycast(transform.position, nodeclose[j].transform.position,Range,Wallmask))
            {
                neighbors.Add(nodeclose[j]);
            }
        }

      
    }
    public void SetIndexes(int w, int h)
    {
        widthPos = w;
        heightPos = h;
    }
    public void SetNeighbors(List<PFNodes> neighbors)
    {
        this.neighbors = neighbors;
    }

  }

