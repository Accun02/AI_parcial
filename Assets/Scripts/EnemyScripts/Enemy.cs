using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseClassEnemy
{
    [SerializeField] Transform center;

    [SerializeField] LayerMask layerMask;

    int damage = 2;

    public lineofsight AttackLOS;

    protected override void Awake()
    {
        base.Awake();
    }

    //Ataque del enemigo.
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
                GameManager.Instance.Dead();
                break;
            }
        }
    }
}
