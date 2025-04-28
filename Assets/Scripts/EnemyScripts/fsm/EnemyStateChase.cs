using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyStateChase : State<States>
{

    EnemyController controller;
    Transform target;
    public EnemyStateChase(EnemyController enemy, Transform Target)
    {
        controller = enemy;
        target = Target;
    }



    public override void Execute()
    {
        // Calcula la dirección deseada para llegar al objetivo.
        Vector3 desiredVelocity = (target.position - controller.body.position).normalized * controller.maxvel;
        // Lo que se necesita cambiar en la velocidad actual para llegar a la deseada.
        Vector3 directionForce = desiredVelocity - controller.body.velocity;

        // Elimina movimiento vertical (2D).
        directionForce.y = 0;
        // Limita la fuerza.
        directionForce = Vector3.ClampMagnitude(directionForce, controller.maxvel);

        // Aplica la fuerza.
        controller.body.AddForce(directionForce, ForceMode.Acceleration);
    }

        public override void OnExit()
    {
    
    }
}


    

