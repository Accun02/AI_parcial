using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateChase : State<States>
{
    SteeringController steering;

    public EnemyStateChase(SteeringController steeringController)
    {
        this.steering = steeringController;
    }

    public override void Execute()
    {
        steering.enabled = true;
    }

    public override void OnExit()
    {
        steering.enabled = false; // se apaga en otros estados
    }
}
