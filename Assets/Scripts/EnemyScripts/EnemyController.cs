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


  
    ItreeNode root;

    void Start()
    {
        InitialilzeFSM();
        OnInin();
    }

    private void InitialilzeFSM()
    {

        var patrol = new EnemyStatePatrol(controller);
        var idle = new EnemyStateIdle(controller,this);
        var attack = new EnemyStateAttack(enemy);
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


        runAway.AddTransition(States.Idle, idle);
    

        fsm = new FSM<States>(patrol);
     
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
        var qChooseAction = new QuestionTree(ChooseWise,qdistance,runAway);
        var qseepalyer = new QuestionTree(() => LOS.CheckAngle(player) && LOS.CheckDistance(player) && LOS.CheckView(player), qChooseAction,idle);
        var qplayerexist = new QuestionTree(() => player != null, qseepalyer, idle);
        var qisidle = new QuestionTree(() => StandTime(),patrol,qseepalyer);
        root = qplayerexist; //root inicial
    }

 public  bool StandTime()
    {

        return true;
    ;
    }

    bool CanAttack()
    {
        return Vector3.Distance(player.transform.position, transform.position) <= enemy.AttackLOS.detectionRange;
    }

    bool  ChooseWise()
    {

      if (generateRandom() < 0.5f)
                {
                    return true;
         
                }
                else return false; 


    }
    float generateRandom()
    {
        float randomValue = UnityEngine.Random.Range(0f, 1f);
        Debug.Log(randomValue);
        return (float)randomValue;
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

