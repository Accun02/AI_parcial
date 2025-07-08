using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class ShootingEnemyController : MonoBehaviour
{
    [SerializeField] private lineofsight LOS;
    [SerializeField] private Transform player;
    [SerializeField] private ShootingEnemy enemy;
    [SerializeField] private SteeringController controller;

    [SerializeField] PFEntity entity;

    private FSM<States> fsm;

    public float timer;

    bool checkplayer;

    EnemyStateIdle idle;
    EnemyStatePatrol patrol;
    EnemyStateChase chase;
    EnemyStateAttack attack;
    EnemyStateShoot shoot;
    EnemyStateRunAway runAway;

    ItreeNode root;

    void Start()
    {
        InitialilzeFSM();
        OnInin();
    }

    private void InitialilzeFSM()
    {

        patrol = new EnemyStatePatrol(controller,entity);
        idle = new EnemyStateIdle(controller,this,patrol);
        shoot = new EnemyStateShoot(controller, enemy,player);
        runAway = new EnemyStateRunAway(controller,patrol);
        chase = new EnemyStateChase(controller,patrol);
        attack = new EnemyStateAttack(enemy, controller,chase,patrol,idle);


        // Transiciones
        patrol.AddTransition(States.Idle, idle);
        patrol.AddTransition(States.RunAway, runAway);
        patrol.AddTransition(States.Chase, chase);
        patrol.AddTransition(States.Shoot, shoot);


        chase.AddTransition(States.Attack, attack);
        chase.AddTransition(States.Idle, idle);


        idle.AddTransition(States.Patrol, patrol);
        idle.AddTransition(States.Chase, chase);
        idle.AddTransition(States.Shoot, shoot);

        shoot.AddTransition(States.Patrol, patrol);
        shoot.AddTransition(States.RunAway, runAway);

        attack.AddTransition(States.Patrol, patrol);

        runAway.AddTransition(States.Idle, idle);
        runAway.AddTransition(States.Patrol, patrol);

        fsm = new FSM<States>(patrol); //ANTES ESTABA EN IDLE
    }

    private void OnInin()
    {

        //Ejecuta los estados.
        var patrol = new ActionTree(() => fsm.OnTransition(States.Patrol));
        var idle = new ActionTree(() => fsm.OnTransition(States.Idle));
        var runAway = new ActionTree(() => fsm.OnTransition(States.RunAway));
        var shoot = new ActionTree(() => fsm.OnTransition(States.Shoot));
        var attack = new ActionTree(() => fsm.OnTransition(States.Attack));
        var chase = new ActionTree(() => fsm.OnTransition(States.Chase));
        //Cambia entre estados.
        var waitorcontinuepatrolling = new QuestionTree(() => waitorcontinue(), idle, patrol);

        var lostplayerr = new QuestionTree(() => LOS.LosePlayer(player), waitorcontinuepatrolling, runAway);
        var qdistance = new QuestionTree(CanAttack, attack, chase); //Si el enemigo esta muy cerca del jugador, lo ataca.
        var Gotorelaod = new QuestionTree(() => bullets(), runAway, shoot);
        var qChooseAction = new QuestionTree(() => ChooseWise(), Gotorelaod, qdistance);   

        var qgoingtodestination = new QuestionTree(() => entity.checkdistancetowaypoint(), waitorcontinuepatrolling, patrol);

        var qseepalyer = new QuestionTree(() => CheckPlayer(), qChooseAction, qgoingtodestination);

        var qisidle = new QuestionTree(() => StandTime(), patrol, qseepalyer);

        var qplayerexist = new QuestionTree(() => player != null, qisidle, null);

        root = qplayerexist; //Root inicial.
    }

    private bool bullets()
    {
        var random = generateRandom();
        if (random + enemy.CurrentBullets / 10 < 0.6f)
        {
            return true;
        }
        return false;
    }
    bool CanAttack()
    {
        return Vector3.Distance(player.transform.position, transform.position) <= enemy.AttackLOS.detectionRange;
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

    bool  ChooseWise()
    {
        var random = UnityEngine.Random.Range(0f, 0.3f);
        if (random + enemy.CurrentBullets/10 > 0.7f)
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
        //checkplayer = LOS.CheckAngle(player) && LOS.CheckDistance(player) && LOS.CheckView(player);
    }

    bool CheckPlayer()
    {
        if (fsm.current == idle || fsm.current == patrol)
        {
            checkplayer = LOS.CheckAngle(player) && LOS.CheckDistance(player) && LOS.CheckView(player);

        }
        else
        {
            checkplayer = LOS.CheckDistance(player) && LOS.CheckView(player);

        }
        return checkplayer;
    }

    void FixedUpdate()
    {
       fsm.OnFixedExecute();
        
    }
}

