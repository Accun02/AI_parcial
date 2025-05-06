using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClassEnemy : MonoBehaviour
{
    Rigidbody body;

   protected virtual void Awake()
   {
        body = GetComponent<Rigidbody>();
   }

    public virtual void Attack() { }

    public void Look(Vector3 lookdir) // Hacia donde mira el enemigo (dirección de dónde tiene que ir).
    {
       transform.forward = lookdir;
    }

    public void LookAt(Transform target) //Mira al objetivo (hasta donde tiene que ir: sea el waypoint o el jugador).
    {
       Vector3 dirtotarget = target.position - transform.position;
     Look(dirtotarget);
    }
}
