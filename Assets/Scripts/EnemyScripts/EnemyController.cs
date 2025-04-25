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
        var Attack = new EnemyStateAttack();
        var RunAway = new EnemyStateRun();

        patrol.Transition(States.Idle,idle);
        patrol.Transition(States.Attack,Attack);
        patrol.Transition(States.RunAway, RunAway);

        idle.Transition(States.Patrol, patrol);
        idle.Transition(States.RunAway,RunAway);

    }

    // Update is called once per frame
    void Update()
    {
        
        fsm.Execute();
    }
}
