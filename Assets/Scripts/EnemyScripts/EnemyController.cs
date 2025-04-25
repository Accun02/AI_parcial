using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    FSM<States> fsm;
    [SerializeField] List<Vector3> Waypoints = new List<Vector3>();

    void Start()
    {

        InitialilzeFSM();
    }
    void InitialilzeFSM()
    {
        var patrol = new EnemyStatePatrol(Waypoints, this.transform);
        var idle = new EnemyStateIdle();
        // var Attack
        // var RunAway
        patrol.Transition(States.Idle,idle);

        idle.Transition(States.Patrol, patrol);

    }

    // Update is called once per frame
    void Update()
    {
        
        fsm.Execute();
    }
}
