using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemyStateIdle : State<States>
{
    SteeringController controller;

    BasicEnemyController enemy;
    ExplodingEnemyController exploding;
    EnemyStateChase enemchase;
    EnemyStatePatrol enempatrol;
    
    public EnemyStateIdle(SteeringController controller, BasicEnemyController enemy,EnemyStateChase chase, EnemyStatePatrol patrol)
    {
        this.controller = controller;
        this.enemy = enemy;
        this.enemchase = chase;
        this.enempatrol = patrol;
      
    }
    public EnemyStateIdle (SteeringController controller, ExplodingEnemyController enemy, EnemyStatePatrol patrol)
    {
        this.controller = controller;
        this.exploding = enemy;
        this.enempatrol = patrol;
    }
    public override void OnEnter()
    {
        
        RemoveTransitions(enemchase, States.Chase);
        RemoveTransitions(enempatrol, States.Patrol);
        controller.ChangeStearingMode(SteeringController.SteeringMode.None);
      if (enemy != null)  enemy.timer = 10;
        if (exploding != null) exploding.timer = 10;
    }

    public override void Execute()
    {
        if (enemy != null)
        {
            enemy.timer -= Time.deltaTime;
            enemy.StandTime();
        }
        if (exploding != null)
        {
            exploding.timer -= Time.deltaTime;

            exploding.StandTime();
        }

      
    }
    public override void FixedExecute()
    {
    controller.ExecuteSteering();
    }
    public override void OnExit()
    {
        base.AddTransition(States.Patrol, enempatrol);
        base.AddTransition(States.Chase, enemchase);
    }



}
