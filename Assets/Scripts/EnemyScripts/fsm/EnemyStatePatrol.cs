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

    //Instancia.
    public EnemyStatePatrol(SteeringController controller, WaypointController waypointController)
    {
        this.controller = controller;
        this.waypointController = waypointController;
    }

    public override void OnEnter()
    {
        //Cambia el Steering.
        controller.ChangeStearingMode(SteeringController.SteeringMode.seek);

        //El Waypoint al que el enemigo se tiene que dirigir.
        controller.gotoposition(waypointController.waypoints[waypointController.Targetpoints]);
    }

    public override void FixedExecute()
    {
        controller.ExecuteSteering();
    }

    public override void OnExit()
    {

    }
}

