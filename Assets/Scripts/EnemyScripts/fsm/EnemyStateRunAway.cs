using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateRunAway : State<States>
{

    SteeringController controller;
    public EnemyStateRunAway(SteeringController controller)
    {
        this.controller = controller;   
    }

    public override void OnEnter()
    {
        controller.ChangeStearingMode(SteeringController.SteeringMode.flee);
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

    }
}

