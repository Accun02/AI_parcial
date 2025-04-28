using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyStateRunAway : State<States>
{
    EnemyController controller;

    Rigidbody target;
    private float maxVelocity;
    private float timePrediction = 1;

    public EnemyStateRunAway(EnemyController enemy,Rigidbody player)
    {
       controller = enemy;
        target = player;
    }

    public override void OnEnter()
    {
     
    }

    public override void Execute()
    {
        // Predice la futura posición del objetivo y se aleja de ella.
        Vector3 predicionPosition = controller.body.position + target.velocity * timePrediction * Vector3.Distance(controller.body.position, target.position);
        Vector3 desiredVelocity = (controller.body.position - predicionPosition).normalized * maxVelocity;
        Vector3 directionForce = desiredVelocity - controller.body.velocity;

        directionForce.y = 0;
        directionForce = Vector3.ClampMagnitude(directionForce, maxVelocity);

        controller.body.AddForce(directionForce, ForceMode.Acceleration);
    }

    public override void OnExit()
    {
       
    }
}

