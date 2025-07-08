using UnityEngine;

public class Evade : ISteering
{
    private Rigidbody rb;
    private CharacterController targetcc;

    private float maxVelocity;
    private float timePrediction;
    public Evade(Rigidbody rb, CharacterController target, float maxVelocity, float timePrediction)
    {
        this.rb = rb;
        this.targetcc = target;
        this.maxVelocity = maxVelocity;
        this.timePrediction = timePrediction;
    }
    public Vector3 MoveDirection()
    {
        //predice la futura posición del objetivo y se aleja de ella.
        Vector3 predicionPosition = targetcc.transform.position + targetcc.velocity * timePrediction * Vector3.Distance(rb.position, targetcc.transform.position);
        Vector3 desiredVelocity = (rb.position - predicionPosition).normalized * maxVelocity;
        Vector3 directionForce = desiredVelocity - rb.velocity;

        directionForce.y = 0;
        directionForce = Vector3.ClampMagnitude(directionForce, maxVelocity);

        rb.AddForce(directionForce, ForceMode.Acceleration);
        return directionForce;
    }
}
