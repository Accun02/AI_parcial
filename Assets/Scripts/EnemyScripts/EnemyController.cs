using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private FSM<States> fsm;
    ItreeNode root;
    [SerializeField] private lineofsight LOS;
    [SerializeField] private Transform player;
    [SerializeField] private Transform enemy;
    private float timer = 0f;
    [SerializeField] private List<Vector3> Waypoints = new List<Vector3>();

    void Start()
    {
        InitialilzeFSM();
        OnInin();
    }

   private void InitialilzeFSM()
    {
        IIAmove iAmove = GetComponent<IIAmove>();
        var patrol = new EnemyStatePatrol(iAmove,Waypoints, enemy);
        var idle = new EnemyStateIdle();
        var attack = new EnemyStateAttack(CanAttack());
        var runAway = new EnemyStateRun();
        var chase = new EnemyStateChase(iAmove,enemy,player);

        //guarda Transiciones posibles entre estados
        patrol.Transition(States.Idle, idle);
        patrol.Transition(States.Attack, attack);
        patrol.Transition(States.RunAway, runAway);
        patrol.Transition(States.Chase, chase);

        idle.Transition(States.Patrol, patrol);
        idle.Transition(States.RunAway, runAway);
        idle.Transition(States.Chase, chase);

        attack.Transition(States.RunAway, runAway);
        attack.Transition(States.Patrol, patrol);

        chase.Transition(States.Idle,idle);
        chase.Transition(States.Attack, attack);
        chase.Transition(States.RunAway,runAway);

        // Estado inicial
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
        return Vector3.Distance(player.transform.position, transform.position) <= 3; 
    }

    void Update()
    {
        timer += Time.deltaTime;
        fsm.OnExecute();
        root.Execute();  
    }
}

