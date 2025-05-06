using UnityEngine;

public class Evade : ISteering
{
    private Rigidbody rb;
    private Rigidbody targetRb;

    private float maxVelocity;
    private float timePrediction;
    public Evade(Rigidbody rb, Rigidbody target, float maxVelocity, float timePrediction)
    {
        this.rb = rb;
        this.targetRb = target;
        this.maxVelocity = maxVelocity;
        this.timePrediction = timePrediction;
    }
    public Vector3 MoveDirection()
    {
        //predice la futura posición del objetivo y se aleja de ella.
        Vector3 predicionPosition = targetRb.position + targetRb.velocity * timePrediction * Vector3.Distance(rb.position, targetRb.position);
        Vector3 desiredVelocity = (rb.position - predicionPosition).normalized * maxVelocity;
        Vector3 directionForce = desiredVelocity - rb.velocity;

        directionForce.y = 0;
        directionForce = Vector3.ClampMagnitude(directionForce, maxVelocity);

        rb.AddForce(directionForce, ForceMode.Acceleration);
        return directionForce;
    }
}
