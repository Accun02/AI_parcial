using System.Collections;
using System.Collections.Generic;
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
    int damage = 2;

    public lineofsight AttackLOS;

 

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
        Collider[] hits = Physics.OverlapSphere(center.position, AttackLOS.detectionRange, layerMask);
        if (hits != null)
        {
            foreach (var item in hits)
            {
                var currTarget = item.transform;
                if (!AttackLOS.CheckAngle(currTarget)) continue;
                if (!AttackLOS.CheckView(currTarget)) continue;
                item.gameObject.GetComponent<PlayerController>().TakeDamage(20);
                break;
            }
        }

      
    }
    public void TakeDamage(int amount)
    {
        health = Mathf.Max(0, health - amount);
       
        if (health <= 0)
        {
           Destroy(gameObject);
        }
    }
}
