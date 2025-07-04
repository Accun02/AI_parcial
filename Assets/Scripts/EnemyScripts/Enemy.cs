using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseClassEnemy
{
    [SerializeField] Transform center;
    private int health = 10;
    public int Health {  get { return health; } set { health = value; } }
    [SerializeField] LayerMask layerMask;

    int balas = 8;
    int damage = 2;

    public lineofsight AttackLOS;

    protected override void Awake()
    {
       
    }

    //Ataque del enemigo.
    public void RangeAttack()
    {
        if (Physics.Raycast(transform.position,transform.forward, out RaycastHit hit,100,layerMask)  && balas > 0)
        {
            hit.collider.gameObject.GetComponent<PlayerController>().Health -= damage;
        }
        balas--;
    }
    public override void Attack()
    {
        Collider[] hits = Physics.OverlapSphere(center.position, AttackLOS.detectionRange, layerMask);
        if (hits != null)
        {
            foreach (var item in hits)
            {
                var currTarget = item.transform;
                if (!AttackLOS.CheckAngle(currTarget)) continue;
                if (!AttackLOS.CheckView(currTarget)) continue;
               currTarget.GetComponent<PlayerController>().Health -= damage;
                break;
            }
        }
    }
}
