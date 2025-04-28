using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClassEnemy : MonoBehaviour, IIAmove
{
    Rigidbody body;
    public float speed;

    protected virtual void Awake()

    {
        body = GetComponent<Rigidbody>();
    }
    public void Look(Vector3 lookdir) // a donde mira
    {
        transform.forward = lookdir;
    }

    public void LookAt(Transform target) //mirar al objetivo
    {
        Vector3 dirtotarget = target.position - transform.position;
        Look(dirtotarget);
    }

    public virtual void Move(Vector3 dir) //moverse
    {

        dir = dir.normalized;
        dir *= speed;
        dir.y = body.velocity.y;
        body.velocity = dir;

    }

    public virtual void Attack(){}
}
