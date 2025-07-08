using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Enemy : BaseClassEnemy
{
    [SerializeField] Transform center;
    private int health = 10;
    [SerializeField] lineofsight los;
    public int Health {  get { return health; } set { health = value; } }
    [SerializeField] LayerMask layerMask;

    public int maxBullets = 6;
    public int CurrentBullets = 7;
    int damage = 20;

    public float attackCooldown = 4f;
    private float lastAttackTime;

    public lineofsight AttackLOS;



    private void Update()
    {
        CanAttack();

    }

    private bool CanAttack()
    {
        lastAttackTime += Time.deltaTime;
    
    return lastAttackTime >= attackCooldown;
    }

    //Ataque del enemigo.
    public void RangeAttack()
    {
        Debug.DrawRay(transform.position + Vector3.up, transform.forward * los.detectionRange, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out hit, los.detectionRange))
        {
            if (hit.transform.CompareTag("Player"))
            {
                hit.collider.gameObject.GetComponent<PlayerController>().TakeDamage(20);
                
            }
        }
        CurrentBullets--;
    }
    public override void Attack()
    {
        if (CanAttack())
        {
            Collider[] hits = Physics.OverlapSphere(center.position, AttackLOS.detectionRange, layerMask);
            if (hits != null)
            {

                foreach (var item in hits)
                {

                    var currTarget = item.transform;

                    item.gameObject.GetComponent<PlayerController>().TakeDamage(damage);

                    break;


                }
            }
            lastAttackTime = 0;
        }
      
    }
    public void TakeDamage(int amount)
    {
        health = Mathf.Max(0, health - amount);

        Debug.Log("enem recibio daño");
        if (health <= 0)
        {
           Destroy(gameObject);
        }
    }
}
