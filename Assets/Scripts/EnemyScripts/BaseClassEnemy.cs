using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClassEnemy : MonoBehaviour
{
    Rigidbody body;
    private int health = 10;
    public int Health { get { return health; } set { health = value; } }
    public int damage = 20;
    public float lastAttackTime;
    public float attackCooldown = 4f;
    protected virtual void Awake()
   {
       body = GetComponent<Rigidbody>();
   }

    public virtual void Attack() { }

    public virtual void RangeAttack() { }
    public void Look(Vector3 lookdir) // Hacia donde mira el enemigo (dirección de dónde tiene que ir).
    {
       transform.forward = lookdir;
    }

    public void LookAt(Transform target) //Mira al objetivo (hasta donde tiene que ir: sea el waypoint o el jugador).
    {
        Vector3 dirtotarget = target.position - transform.position;
        Look(dirtotarget);
    }
    public  virtual void TakeDamage(int amount)
    {
      
    }
}
