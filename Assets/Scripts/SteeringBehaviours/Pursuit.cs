using UnityEngine;

public class Persuit : ISteering
{
    private Rigidbody rb;
    private Rigidbody targetRb;

    private float maxVelocity;
    private float timePrediction = 1;

    public Persuit(Rigidbody rb, Rigidbody target, float maxVelocity, float timePrediction)
    {
        this.rb = rb;
        this.targetRb = target;
        this.maxVelocity = maxVelocity;
        this.timePrediction = timePrediction;
    }

    public Vector3 MoveDirection()
    {
        // Predice la futura posición del objetivo según su velocidad.
        Vector3 predicionPosition = targetRb.position + targetRb.velocity * timePrediction * Vector3.Distance(rb.position, targetRb.position);
        // Apunta hacia esa posición futura.
        Vector3 desiredVelocity = (predicionPosition - rb.position).normalized * maxVelocity;
        Vector3 directionForce = desiredVelocity - rb.velocity;

        directionForce.y = 0;
        directionForce = Vector3.ClampMagnitude(directionForce, maxVelocity);

        rb.AddForce(directionForce, ForceMode.Acceleration);
        return desiredVelocity;
    }
}
