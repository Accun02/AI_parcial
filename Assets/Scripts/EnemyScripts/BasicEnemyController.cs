using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    [SerializeField] private lineofsight LOS;
    [SerializeField] private Transform player;
    [SerializeField] private Enemy enemy;
    [SerializeField] private SteeringController controller;
    private FSM<States> fsm;
    [SerializeField] WaypointController waypointController;
    public float timer;
    bool checkplayer;
    EnemyStateIdle idle;
    EnemyStateAttack attack;
    EnemyStatePatrol patrol;
    EnemyStateChase chase;
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
        idle = new EnemyStateIdle(controller,this,chase,patrol);
        attack = new EnemyStateAttack(enemy,controller,chase,patrol,idle);
        chase = new EnemyStateChase(controller,patrol);
        runAway = new EnemyStateRunAway(controller,patrol);

        // Transiciones
        patrol.AddTransition(States.Idle, idle);
        patrol.AddTransition(States.Chase, chase);

        idle.AddTransition(States.Patrol, patrol);
        idle.AddTransition(States.Chase,chase);

        attack.AddTransition(States.Idle, idle);
        attack.AddTransition(States.Patrol,patrol);

        chase.AddTransition(States.Idle, idle);
        chase.AddTransition(States.Attack, attack);
        chase.AddTransition(States.Patrol, patrol);

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
        var qdistance = new QuestionTree(CanAttack,attack,chase); //si esta muy cerca ataca al jugador

        var waitorcontinuepatrolling = new QuestionTree(() => waitorcontinue(), idle, patrol); // 

        
        var insight = new QuestionTree(() => checkplayer , qdistance, idle);

        var lostplayerr = new QuestionTree(() => LOS.LosePlayer(player), waitorcontinuepatrolling, runAway);
        var qChooseAction = new QuestionTree(() => ChooseWise(),insight,lostplayerr);   

        var qgoingtodestination = new QuestionTree(() => waypointController.checkdistancetowaypoint(), waitorcontinuepatrolling, patrol);

        var qseepalyer = new QuestionTree(() =>checkplayer, qChooseAction,qgoingtodestination);

        var qisidle = new QuestionTree(() => StandTime(),patrol,qseepalyer);

        var qplayerexist = new QuestionTree(() => player != null, qisidle, null);

        root = qplayerexist; //root inicial
    }

    private bool waitorcontinue()
    {
        var random = generateRandom();
        if (random < 0.7f)
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

    bool CanAttack()
    {

        return Vector3.Distance(player.transform.position, transform.position) <= enemy.AttackLOS.detectionRange;
    }

    bool  ChooseWise()
    {
        var random =  generateRandom();
        if (random < 0.3f)
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

