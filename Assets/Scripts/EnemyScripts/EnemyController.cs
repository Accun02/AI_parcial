using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private FSM<States> fsm;

    private int life = 4;
  
     [SerializeField] private Transform player;
    private int Life
    {
        get { return life; }
        set { life = value; }
    }

    private float timer = 0f;

    [SerializeField] private List<Vector3> Waypoints = new List<Vector3>();

    void Start()
    {
        InitialilzeFSM();
    }

    void InitialilzeFSM()
    {
        var patrol = new EnemyStatePatrol(Waypoints, this.transform);
        var idle = new EnemyStateIdle();
     //  var attack = new EnemyStateAttack(CanAttack());
        var runAway = new EnemyStateRun();
        var chase = new EnemyStateChase();

         //Transiciones entre estados
        patrol.Transition(States.Idle, idle);
     //patrol.Transition(States.Attack, attack);
        patrol.Transition(States.RunAway, runAway);
        patrol.Transition(States.Chase, chase);

        idle.Transition(States.Patrol, patrol);
        idle.Transition(States.RunAway, runAway);
        idle.Transition(States.Chase, chase);

        //attack.Transition(States.RunAway, runAway);
        //attack.Transition(States.Patrol, patrol);

        // Estado inicial
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

    //bool CanAttack()
    //{
    //    return Vector3.Distance(player.transform.position, transform.position) <= 10; 
    //}

    void Update()
    {
        fsm.Execute();

     
    }
}

