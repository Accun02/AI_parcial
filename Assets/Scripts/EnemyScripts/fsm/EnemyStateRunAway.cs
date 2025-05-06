using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateRunAway : State<States>
{
    SteeringController controller;
    EnemyStatePatrol enempatrol;

    //Instancia.
    public EnemyStateRunAway(SteeringController controller,EnemyStatePatrol patrol)
    {
        this.controller = controller;   
        enempatrol = patrol;
    }

    public override void OnEnter()
    {
        //Cambia el Steering.
        controller.ChangeStearingMode(SteeringController.SteeringMode.flee);

        RemoveTransitions(enempatrol, States.Patrol);
    }

    public override void Execute()
    {
  
    }

    public override void FixedExecute()
    {
        //Ejecuta el Steering.
        controller.ExecuteSteering();
    }

    public override void OnExit()
    {
        //Agrega transición.
        AddTransition(States.Patrol, enempatrol); 
    }
}

