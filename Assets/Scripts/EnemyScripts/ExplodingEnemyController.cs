using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class ExplodingEnemyController : MonoBehaviour
{
    [SerializeField] private lineofsight LOS;
    [SerializeField] private Transform player;
    [SerializeField] private Explodingenemy enemy;
    [SerializeField] private SteeringController controller;

    [SerializeField] WaypointController waypointController;

    private FSM<States> fsm;

    public float timer;

    bool checkplayer;

    EnemyStateIdle idle;
    EnemyStatePatrol patrol;
    EnemyStateExplode explode;
    EnemyStateRunAway runAway;

    ItreeNode root;

    void Start()
    {
        InitialilzeFSM();
        OnInin();
    }

    private void InitialilzeFSM()
    {

        patrol = new EnemyStatePatrol(controller,waypointController);
        idle = new EnemyStateIdle(controller,this,patrol);
        explode = new EnemyStateExplode(enemy,controller);
        runAway = new EnemyStateRunAway(controller);

        // Transiciones
        patrol.AddTransition(States.Idle, idle);
        patrol.AddTransition(States.Explode, explode);
        patrol.AddTransition(States.RunAway, runAway);

        idle.AddTransition(States.Patrol, patrol);
        idle.AddTransition(States.Explode,explode);
        idle.AddTransition(States.RunAway, runAway);

        explode.AddTransition(States.Idle, idle);

        runAway.AddTransition(States.Idle, idle);
        runAway.AddTransition(States.Patrol, patrol);

        fsm = new FSM<States>(idle);
     
    }

    private void OnInin()
    {

        //Ejecuta los estados
        var patrol = new ActionTree(() => fsm.OnTransition(States.Patrol));
        var idle = new ActionTree(() => fsm.OnTransition(States.Idle));
        var explode = new ActionTree(() => fsm.OnTransition(States.Explode));
        var runAway = new ActionTree(() => fsm.OnTransition(States.RunAway));

        //Cambia entre estados
        var waitorcontinuepatrolling = new QuestionTree(() => waitorcontinue(), idle, patrol); // 
        var lostplayerr = new QuestionTree(() => LOS.LosePlayer(player), waitorcontinuepatrolling, runAway);
        var qChooseAction = new QuestionTree(() => ChooseWise(), lostplayerr,explode);   

        var qgoingtodestination = new QuestionTree(() => waypointController.checkdistancetowaypoint(), waitorcontinuepatrolling, patrol);

        var qseepalyer = new QuestionTree(() =>checkplayer, qChooseAction,qgoingtodestination);

        var qisidle = new QuestionTree(() => StandTime(),patrol,qseepalyer);

        var qplayerexist = new QuestionTree(() => player != null, qisidle, null);

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
        if (timer <= 0)
        {
            return true;
        }
      else  return false;
    ;
    }


    bool  ChooseWise()
    {
        var random =  generateRandom();
        if (random < 0.1f)
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
        checkplayer = LOS.CheckAngle(player) && LOS.CheckDistance(player) && LOS.CheckView(player);
    }

     void FixedUpdate()
    {
        fsm.OnFixedExecute();
    }
}

