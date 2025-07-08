using UnityEngine;

public class Persuit : ISteering
{
    private Rigidbody rb;
    private CharacterController targetcc;
    private float maxVelocity;
    private float timePrediction = 1;

    public Persuit(Rigidbody rb, CharacterController target, float maxVelocity, float timePrediction)
    {
        this.rb = rb;
        this.targetcc = target;
        this.maxVelocity = maxVelocity;
        this.timePrediction = timePrediction;
    }

    public Vector3 MoveDirection()
    {
        // Predice la futura posición del objetivo según su velocidad.
        Vector3 predicitonPosition = targetcc.transform.position + targetcc.velocity * timePrediction * Vector3.Distance(rb.position, targetcc.transform.position);
        // Apunta hacia esa posición futura.
        Vector3 desiredVelocity = (predicitonPosition - rb.position).normalized * maxVelocity;
        Vector3 directionForce = desiredVelocity - rb.velocity;
        directionForce.y = 0;
        directionForce = Vector3.ClampMagnitude(directionForce, maxVelocity);
        return desiredVelocity;
    }
}
