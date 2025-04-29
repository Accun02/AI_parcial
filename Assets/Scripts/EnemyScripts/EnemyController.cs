using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private lineofsight LOS;
    [SerializeField] private Transform player;
    [SerializeField] private Enemy enemy;
    [SerializeField] private SteeringController controller;
    private FSM<States> fsm;
    [SerializeField] WaypointController waypointController;
    public float timer;
  
    ItreeNode root;

    void Start()
    {
        InitialilzeFSM();
        OnInin();
    }

    private void InitialilzeFSM()
    {

        var patrol = new EnemyStatePatrol(controller,waypointController);
        var idle = new EnemyStateIdle(controller,this,timer);
        var attack = new EnemyStateAttack(enemy,controller);
        var chase = new EnemyStateChase(controller);
        var runAway = new EnemyStateRunAway(controller);

        // Transiciones
        patrol.Transition(States.Idle, idle);
        patrol.Transition(States.RunAway, runAway);
        patrol.Transition(States.Chase, chase);

        idle.Transition(States.Patrol, patrol);
        idle.Transition(States.RunAway, runAway);
        idle.Transition(States.Chase, chase);

        attack.Transition(States.Idle, idle);

        chase.Transition(States.Idle, idle);
        chase.Transition(States.Attack, attack);



        runAway.AddTransition(States.Idle, idle);
        runAway.AddTransition(States.Patrol, patrol);

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
        var qChooseAction = new QuestionTree(() => ChooseWise(),qdistance,runAway);
        var waitorcontinuepatrolling = new QuestionTree(() => waitorcontinue(), patrol, idle);
        var qgoingtodestination = new QuestionTree(() => waypointController.checkdistancetowaypoint(), waitorcontinuepatrolling, patrol);
        var qseepalyer = new QuestionTree(() => LOS.CheckAngle(player) && LOS.CheckDistance(player) && LOS.CheckView(player), qChooseAction,qgoingtodestination);
        var qplayerexist = new QuestionTree(() => player != null, qseepalyer, idle);
        var qisidle = new QuestionTree(() => StandTime(),patrol,qseepalyer);
        root = qplayerexist; //root inicial
    }

    private bool waitorcontinue()
    {
        var random = generateRandom();
        if (random < 0.5f)
        {
            return true;

        }
        else return false;
    }

    public  bool StandTime()
    {

        if (timer < 0)
        {
            return true;
        }
        else return false;
    ;
    }

    bool CanAttack()
    {

        return Vector3.Distance(player.transform.position, transform.position) <= enemy.AttackLOS.detectionRange;
    }

    bool  ChooseWise()
    {
        var random =  generateRandom();
        if (random < 0.8f)
                {
                    return true;
         
                }
                else return false; 


    }

    float generateRandom()
    {
        float randomValue = UnityEngine.Random.Range(0f, 1f);

        return randomValue;
    }

    void Update()
    {
        fsm.OnExecute();
        root.Execute();  
    }

     void FixedUpdate()
    {
        fsm.OnFixedExecute();
    }

    
}

