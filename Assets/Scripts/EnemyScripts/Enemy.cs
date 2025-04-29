using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseClassEnemy
{
    [SerializeField] Transform center;
    int damage = 2;
    public lineofsight AttackLOS;
    [SerializeField] LayerMask layerMask;



    protected override void Awake()
    {
        base.Awake();
    }
    public override void Attack()
    {

        Collider[] hits = Physics.OverlapSphere(center.position, AttackLOS.distance, layerMask);

        if (hits != null)
        {
            GameManager.Instance.Damage(damage);
        }
    }
}
