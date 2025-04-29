using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class EnemyStatePatrol : State<States>
{
   SteeringController controller;

    public EnemyStatePatrol(SteeringController controller)
    {
      this.controller = controller;
    }

    public override void OnEnter()
    {
        controller.ChangeStearingMode(SteeringController.SteeringMode.seek);
    }

    public override void FixedExecute()
    {
        controller.ExecuteSteering();
       
    }
}

