using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    FSM<States> fsm;
    float timer = 0;
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
        var Chase = new EnemyStateChase();

        patrol.Transition(States.Idle,idle);
        patrol.Transition(States.Attack,Attack);
        patrol.Transition(States.RunAway, RunAway);
        patrol.Transition(States.Chase, Chase);

        idle.Transition(States.Patrol, patrol);
        idle.Transition(States.RunAway,RunAway);
        idle.Transition(States.Chase,Chase);

        Attack.Transition(States.RunAway,RunAway);
        Attack.Transition(States.Patrol,patrol);


        fsm = new FSM<States>(idle);
    }
    bool StandTime()
    {
        timer += Time.deltaTime;
        if (timer > 10f)
        {
            timer = 0;
            return true;

        }
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        
        fsm.Execute();
    }
}
