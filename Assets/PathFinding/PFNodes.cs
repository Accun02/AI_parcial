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
    public void SetNeighbors(List<PFNodes> neighbors)
    {
        this.neighbors = neighbors;
    }
    }

