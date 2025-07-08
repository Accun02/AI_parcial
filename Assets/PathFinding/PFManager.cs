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
    [SerializeField] PFEntity[] entities;

    [SerializeField] LayerMask walls;

    public static PFManager Instance { get; private set; }

    public PFNodeGrid grid;
    public PFNodeGrid Grid => grid;

    public List<RechargeAmmo> rechargeAmmoList; 


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
            var endNode = grid.nodeGrid[Random.Range(20, 80)];
            var path = new List<PFNodes>();
            entities[i].endNode = endNode;
            path = PathFinding.Astar(startNode, endNode , walls);
       
            entities[i].SetPath = path;   
        }
    }
}
