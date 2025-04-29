using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private lineofsight LOS;
    [SerializeField] private Transform player;
    [SerializeField] private Enemy enemy;
    [SerializeField] private SteeringController controller;
    private FSM<States> fsm;

    private float timer = 0f;

    ItreeNode root;

    void Start()
    {
        InitialilzeFSM();
        OnInin();
    }

    private void InitialilzeFSM()
    {
        IIAmove iAmove = GetComponent<IIAmove>();

        var patrol = new EnemyStatePatrol(controller);
        var idle = new EnemyStateIdle(controller);
        var attack = new EnemyStateAttack(player.GetComponent<PlayerController>());

        var chase = new EnemyStateChase(controller);
        var runAway = new EnemyStateRunAway(controller);

        // Transiciones
        patrol.Transition(States.Idle, idle);
        patrol.Transition(States.Attack, attack);
        patrol.Transition(States.RunAway, runAway);
        patrol.Transition(States.Chase, chase);

        idle.Transition(States.Patrol, patrol);
        idle.Transition(States.RunAway, runAway);
        idle.Transition(States.Chase, chase);

        attack.Transition(States.RunAway, runAway);
        attack.Transition(States.Patrol, patrol);

        chase.Transition(States.Idle, idle);
        chase.Transition(States.Attack, attack);
        chase.Transition(States.RunAway, runAway);

        fsm = new FSM<States>(idle);
    }

    private void OnInin()
    {

        //ejecuta los estados
        var patrol = new ActionTree(() => fsm.OnTransition(States.Patrol));
        var idle = new ActionTree(() => fsm.OnTransition(States.Idle));
        var attack = new ActionTree(() => fsm.OnTransition(States.Attack));
        var chase = new ActionTree(() => fsm.OnTransition(States.Chase));
        var runAway = new ActionTree(() => fsm.OnTransition(States.RunAway));

        //cambia entre estados
        var qdistance = new QuestionTree(CanAttack,attack,chase);
        var qseepalyer = new QuestionTree(LOS.OnRange,qdistance,idle);
        var qplayerexist = new QuestionTree(() => player != null, qseepalyer, idle);
        var qisidle = new QuestionTree(StandTime,patrol,qseepalyer);
        root = qplayerexist; //rot inicial
    }

    bool StandTime()
    {
        return false;
    ;
    }

    bool CanAttack()
    {
        return Vector3.Distance(player.transform.position, transform.position) <= enemy.AttackLOS.distance;
    }

    void Update()
    {
    
        root.Execute();  
    }

     void FixedUpdate()
    {
        fsm.OnFixedExecute();
    }
}

