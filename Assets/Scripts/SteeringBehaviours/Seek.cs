using UnityEngine;

public class Seek : ISteering
{
    private Rigidbody rb;

    private Transform target;
    public Transform[] waypoints;
    public int Targetpoints;
    private float maxVelocity;
    public Seek(Rigidbody rb, Transform[] target, float maxVelocity)
    {
        this.rb = rb;
        this.waypoints = target;
        this.maxVelocity = maxVelocity;
    }

    public Vector3 MoveDirection()
    {
        if (rb.position == waypoints[Targetpoints].position)
        {
            increaseposition();
        }
        return rb.position = Vector3.MoveTowards(rb.position, waypoints[Targetpoints].position, maxVelocity);
    }

    private void increaseposition()
    {
        Targetpoints++;

    }
}