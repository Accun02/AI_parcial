using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateRunAway : State<States>
{
    SteeringController steering;

    public EnemyStateRunAway(SteeringController steeringController)
    {
        steering = steeringController;
    }

    public override void OnEnter()
    {
        steering.mode = SteeringController.SteeringMode.flee;
        steering.enabled = true;
    }

    public override void Execute()
    {
        // No se necesita código acá porque el SteeringController se encarga
    }

    public override void OnExit()
    {
        steering.enabled = false;
    }
}

