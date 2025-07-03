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
    [SerializeField] PFEntity[] entities;
    [SerializeField] PFNodeGrid grid;
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
            var startNode = entities[i].SearchClose();
            Debug.Log(startNode);
            var endNode = grid.nodeGrid[Random.Range(20, 80)];
            var path = new List<PFNodes>();
            entities[i].endNode = endNode;
            path = PathFinding.Astar(startNode, endNode , walls);
       
            entities[i].SetPath = path;   
        }
     
    }

}
