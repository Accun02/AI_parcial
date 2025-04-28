using UnityEngine;

public class Seek 
{
    private Rigidbody rb;

    private Transform target;

    private float maxVelocity;
    public Seek(Rigidbody rb, Transform target, float maxVelocity)
    {
        this.rb = rb;
        this.target = target;
        this.maxVelocity = maxVelocity;
    }

  
}