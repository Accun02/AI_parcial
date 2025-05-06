using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateRunAway : State<States>
{

    SteeringController controller;
    EnemyStatePatrol enempatrol;
    public EnemyStateRunAway(SteeringController controller,EnemyStatePatrol patrol)
    {
        this.controller = controller;   
        enempatrol = patrol;
    }

    public override void OnEnter()
    {
        controller.ChangeStearingMode(SteeringController.SteeringMode.flee);
        RemoveTransitions(enempatrol, States.Patrol);
    }

    public override void Execute()
    {
  
    }
    public override void FixedExecute()
    {
        controller.ExecuteSteering();
    }

    public override void OnExit()
    {
        AddTransition(States.Patrol, enempatrol);
    }
}

