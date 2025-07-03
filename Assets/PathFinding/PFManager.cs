using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.VFX;
using UnityEditor.Experimental.GraphView;
using Random = UnityEngine.Random;
public class PFManager : MonoBehaviour
{
    public static PFManager Instance { get; private set; }
    [SerializeField] PathFindingMovement[] entities;
    [SerializeField] PFNodeGrid grid;
    [SerializeField] float distanceToTarget;
    [SerializeField] LayerMask walls;

    public PFNodeGrid Grid => grid;


    private void Awake()
    {
        Instance = this;

      
    }
    private void Start()
    {
        SetPath();
    }


    public void SetPath()
    {
      
        for (int i = 0; i < entities.Length; i++) 
        {
            var startNode = grid.nodeGrid.

            Where(x => (x.transform.position - entities[i].transform.position).sqrMagnitude <= entities[i].distanceNodeArrival * entities[i].distanceNodeArrival)
               .OrderBy(x => (x.transform.position - entities[i].transform.position).sqrMagnitude).FirstOrDefault();
            var endNode = grid.nodeGrid[Random.Range(20, 80)];
        var path = new List<PFNodes>();  
            
        path = PathFinding.Astar(startNode, endNode , walls);
       
            entities[i].pFNodesStartAndEnd[0] = startNode;
            entities[i].pFNodesStartAndEnd[1] = endNode;
            entities[i].pFNodesAllRoute = path;
        }
     
    }

}
