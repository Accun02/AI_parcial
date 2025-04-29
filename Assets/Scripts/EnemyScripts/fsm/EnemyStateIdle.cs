using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateIdle : State<States>
{
    SteeringController controller;
    public EnemyStateIdle(SteeringController controller)
    {
        this.controller = controller;
    }

    public override void OnEnter()
    {
        controller.ChangeStearingMode(SteeringController.SteeringMode.None);
    }
}
