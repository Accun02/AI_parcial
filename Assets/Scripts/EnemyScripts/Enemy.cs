using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : BaseClassEnemy
{
    [SerializeField] private Transform center;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] lineofsight AttackLOS;
    public Rigidbody body;
    int damage = 2;

    public float AttackRange => AttackLOS.distance;
    protected override void Awake()
    {
        base.Awake();
    }


    public override void Attack()
    {
        Collider[] hits = Physics.OverlapSphere(center.position,AttackLOS.distance,layerMask);

        if (hits != null ) 
        {
            GameManager.Instance.Damage(damage);
        }
       
    }

    private void OnDrawGizmos()
    {
        
   
    }

}
