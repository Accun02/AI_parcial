using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    FSM<StateEnum> fsm;
    [SerializeField] List<Vector3> Waypoints = new List<Vector3>();

    void Start()
    {
        var patrol = new EnemyStatePatrol(Waypoints,this.transform);
        // var idle
        // var Attack
        // var RunAway
    }

    // Update is called once per frame
    void Update()
    {
        
        fsm.Execute();
    }
}
