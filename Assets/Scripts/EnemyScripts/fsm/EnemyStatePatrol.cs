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
    WaypointController waypointController;

    public EnemyStatePatrol(SteeringController controller, WaypointController waypointController)
    {
        this.controller = controller;
        this.waypointController = waypointController;
    }

    public override void OnEnter()
    {
        controller.ChangeStearingMode(SteeringController.SteeringMode.seek);
        controller.gotoposition(waypointController.waypoints[waypointController.Targetpoints]);
    }

    public override void FixedExecute()
    {
        controller.ExecuteSteering();
       
    }
}

