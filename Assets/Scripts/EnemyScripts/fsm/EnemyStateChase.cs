using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyStateChase : State<States>
{
SteeringController controller;
EnemyStatePatrol enemPatrol;

    public EnemyStateChase(SteeringController controller, EnemyStatePatrol enemPatrol)
    {
        this.controller = controller;
        this.enemPatrol = enemPatrol;
    }

    public override void OnEnter()
    {
        controller.ChangeStearingMode(SteeringController.SteeringMode.persuit);

        RemoveTransitions(enemPatrol, States.Patrol);
   
    }
    public override void FixedExecute()
    {
        controller.ExecuteSteering();

    }
    
    

    public override void OnExit()
    {
        AddTransition(States.Patrol,enemPatrol);
    }
}
