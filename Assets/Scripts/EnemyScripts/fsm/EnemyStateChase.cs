using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyStateChase : State<States>
{

    Enemy enemy;
    Transform target;
    public EnemyStateChase(Enemy enemy, Transform Target)
    {
        this.enemy = enemy;
        target = Target;
    }



    public override void Execute()
    {
        // Calcula la dirección deseada para llegar al objetivo.
        Vector3 desiredVelocity = (target.position - enemy.transform.position).normalized * enemy.speed;
        // Lo que se necesita cambiar en la velocidad actual para llegar a la deseada.
        Vector3 directionForce = desiredVelocity - (enemy.body.velocity);

        // Elimina movimiento vertical (2D).
        directionForce.y = 0;
        // Limita la fuerza.
        directionForce = Vector3.ClampMagnitude(directionForce, enemy.speed);

        // Aplica la fuerza.
        enemy.body.AddForce(directionForce, ForceMode.Acceleration);

    }

        public override void OnExit()
    {
    
    }
}


    

