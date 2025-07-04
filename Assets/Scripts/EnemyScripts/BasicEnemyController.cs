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
    [SerializeField] private PFEntity entity;
    private FSM<States> fsm;

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

        patrol = new EnemyStatePatrol(controller, entity);
        chase = new EnemyStateChase(controller, patrol);
        idle = new EnemyStateIdle(controller, this, chase, patrol);
        attack = new EnemyStateAttack(enemy, controller, chase, patrol, idle);
        runAway = new EnemyStateRunAway(controller, patrol);

        // Transiciones
        patrol.AddTransition(States.Idle, idle);
        patrol.AddTransition(States.Chase, chase);

        idle.AddTransition(States.Patrol, patrol);
        idle.AddTransition(States.Chase, chase);

        attack.AddTransition(States.Idle, idle);
        attack.AddTransition(States.Patrol, patrol);

        chase.AddTransition(States.Idle, idle);
        chase.AddTransition(States.Attack, attack);
        chase.AddTransition(States.Patrol, patrol);

        runAway.AddTransition(States.Idle, idle);
        runAway.AddTransition(States.Patrol, patrol);

        fsm = new FSM<States>(idle);

    }

    private void OnInin()
    {
        //Ejecuta los estados.
        var patrol = new ActionTree(() => fsm.OnTransition(States.Patrol));
        var idle = new ActionTree(() => fsm.OnTransition(States.Idle));
        var attack = new ActionTree(() => fsm.OnTransition(States.Attack));
        var chase = new ActionTree(() => fsm.OnTransition(States.Chase));
        var runAway = new ActionTree(() => fsm.OnTransition(States.RunAway));

        //Cambia entre estados.
        var qdistance = new QuestionTree(CanAttack, attack, chase); //Si el enemigo esta muy cerca del jugador, lo ataca.

        var waitorcontinuepatrolling = new QuestionTree(() => waitorcontinue(), idle, patrol); // El enemigo ve si se queda quieto o patrulla.

        var insight = new QuestionTree(() => checkplayer, qdistance, idle); // El enemigo checkea si el jugadore est・cerca.

        var lostplayerr = new QuestionTree(() => LOS.LosePlayer(player), waitorcontinuepatrolling, runAway); // Si el enemigo est・lejos de el jugador.
        var qChooseAction = new QuestionTree(() => ChooseWise(), insight, lostplayerr);  // El enemigo eligue entre dos opciones: RunAway o Chase al jugador. 

        var qgoingtodestination = new QuestionTree(() => entity.checkdistancetowaypoint(), waitorcontinuepatrolling, patrol); // Si est・yendo en direcci al Waypoint, o si no, empieza a Patrol de nuevo.

        var qseepalyer = new QuestionTree(() => checkplayer, qChooseAction, qgoingtodestination); //Si el enemigo puede ver al jugador.

        var qisidle = new QuestionTree(() => StandTime(), patrol, qseepalyer); //Si el enemigo est・quieto.

        var qplayerexist = new QuestionTree(() => player != null, qisidle, null); //Si existe el jugador.

        root = qplayerexist; //La root inicial.
    }

    //Si contin俉 Patrol o se pone en Idle.
    private bool waitorcontinue()
    {
        var random = generateRandom();
        if (random < 0.7f)
        {
            return true;
        }
        else return false;
    }

    //El tiempo de Idle del enemigo.
    public bool StandTime()
    {
        if (timer <= 0)
        {
            return true;
        }
        else return false;
        ;
    }

    //Si puede atacar el enemigo.
    bool CanAttack()
    {
        return Vector3.Distance(player.transform.position, transform.position) <= enemy.AttackLOS.detectionRange;
    }

    //Elige entre si RunAway o Chase.
    bool ChooseWise()
    {
        var random = generateRandom();
        if (random < 0.5f)
        {
            return true;

        }
        else return false;
    }

    //Genera n伹ero aleatorio.
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

