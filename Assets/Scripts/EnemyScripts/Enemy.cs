using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : BaseClassEnemy
{
    [SerializeField] private Transform center;
    [SerializeField] private LayerMask layerMask;
    int damage = 2;
    protected override void Awake()
    {
        base.Awake();
    }


    public void attack()
    {
        Collider[] hits = Physics.OverlapBox(center.position,Vector3.forward,Quaternion.identity,layerMask);

        if (hits != null ) 
        {
            GameManager.Instance.Damage(damage);
        }
    }
}
