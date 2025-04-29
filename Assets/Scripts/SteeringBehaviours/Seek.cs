using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class Seek : ISteering
{
    private Rigidbody rb;
 public Transform[] waypoints;
    public int Targetpoints;
    private float maxVelocity;
    private bool goingback;
    public Seek(Rigidbody rb, Transform[] target, float maxVelocity)
    {
        this.rb = rb;
        this.waypoints = target;
        this.maxVelocity = maxVelocity;
    }

    public Vector3 MoveDirection()
    {
        if (rb.position == waypoints[Targetpoints].position && !goingback)
        {
            increaseposition();
        }
        else if (rb.position == waypoints[Targetpoints].position && goingback)
        {
            decreaseposition();
        }
        return rb.position = Vector3.MoveTowards(rb.position, waypoints[Targetpoints].position, maxVelocity);

        
    }

    private void increaseposition()
    {
        if (Targetpoints == waypoints.Length)
        {
            goingback = true;
        }
        else 
        {
            Targetpoints++;
        }

    }

    private void decreaseposition()
    {
        if (Targetpoints == 0)
        {
            goingback = false;
        }
        else
        {
            Targetpoints--;
        }
    }
}