using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemyStateIdle : State<States>
{
    SteeringController controller;

    EnemyController enemy;

    EnemyStateChase enemchase;
    EnemyStatePatrol enempatrol;
    
    public EnemyStateIdle(SteeringController controller, EnemyController enemy,EnemyStateChase chase, EnemyStatePatrol patrol)
    {
        this.controller = controller;
        this.enemy = enemy;
        this.enemchase = chase;
        this.enempatrol = patrol;
      
    }

    public override void OnEnter()
    {
        
        RemoveTransitions(enemchase, States.Chase);
        RemoveTransitions(enempatrol, States.Patrol);
        controller.ChangeStearingMode(SteeringController.SteeringMode.None);

    }

    public override void Execute()
    {
          
        enemy.timer -= Time.deltaTime;
        enemy.StandTime();
      
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
