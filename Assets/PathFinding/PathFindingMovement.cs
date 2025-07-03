using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingMovement : MonoBehaviour
{
    public List<PFNodes> pFNodesStartAndEnd;
    [SerializeField] LayerMask mask;
     public float distanceNodeArrival;
    private int beginningNode = 0;
    private int endingNode =1;

    public List<PFNodes> pFNodesAllRoute;
    public PFNodeGrid NodeManager;
    public int currentNodeRoute {  get; private set; }
    
    public Transform currentNodeGoingNow { get; private set; }

    private void Start()
    {
      
        currentNodeRoute = 0;
       currentNodeGoingNow = pFNodesAllRoute[currentNodeRoute].transform;
    }


    public void CheckForCurrentNode()
    {


        if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                             new Vector3(pFNodesAllRoute[currentNodeRoute].transform.position.x, 0, pFNodesAllRoute[currentNodeRoute].transform.position.z)) <= distanceNodeArrival)
        {
            if (currentNodeRoute < pFNodesAllRoute.Count - 1)
            {
                currentNodeRoute++;
                currentNodeGoingNow = pFNodesAllRoute[currentNodeRoute].transform;
            }
        }
    }

}
