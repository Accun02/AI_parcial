using System.ComponentModel.Design.Serialization;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Seek : ISteering
{
    private Rigidbody rb;
    public Transform target;
    public float maxVelocity;
    public Seek(Rigidbody rb, Transform target,float maxvel)
    {
        this.rb = rb;
        this.target = target;
        this.maxVelocity = maxvel;
        
    }

    public Vector3 MoveDirection()
    {
        if( (target == null) ) return Vector3.zero; //si no hay objetivo, no hace nada
        Vector3 desiredVelocity = (target.position - rb.position).normalized * maxVelocity; //calcula la velocidad hacia el objetivo
        Vector3 directionForce = desiredVelocity - rb.velocity; //calcula la diferencia con la velocidad actual para aplicar como fuerza
        directionForce.y = 0;
        directionForce = Vector3.ClampMagnitude(directionForce, maxVelocity);
        return directionForce;
    }
}