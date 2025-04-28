using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyStateRunAway : State<States>
{
    Enemy enemy;

    Rigidbody target;
    private float maxVelocity;
    private float timePrediction = 1;

    public EnemyStateRunAway(Enemy enemy,Rigidbody player)
    {
       this.enemy = enemy;
        target = player;
    }

    public override void OnEnter()
    {
     
    }

    public override void Execute()
    {
        // Predice la futura posición del objetivo y se aleja de ella.
        Vector3 predicionPosition = enemy.transform.position + target.velocity * timePrediction * Vector3.Distance(enemy.transform.position, target.position);
        Vector3 desiredVelocity = (enemy.transform.position - predicionPosition).normalized * maxVelocity;
        Vector3 directionForce = desiredVelocity - (enemy.transform.position * enemy.speed);

        directionForce.y = 0;
        directionForce = Vector3.ClampMagnitude(directionForce, maxVelocity);

        enemy.body.AddForce(directionForce, ForceMode.Acceleration);
    }

    public override void OnExit()
    {
       
    }
}

