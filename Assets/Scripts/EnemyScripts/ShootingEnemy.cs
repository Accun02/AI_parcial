using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.ShaderData;

public class ShootingEnemy : BaseClassEnemy
{
 
    public float radius =  10; //Determina quEtan lejos llega el daño.

    public int maxBullets = 6;
    public float CurrentBullets = 7;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform center;
    public lineofsight AttackLOS;
    [SerializeField] lineofsight  LOS;

    //Define quEpasa cuando el enemigo "ataca" (explota).
    private bool CanAttack()
    {
        lastAttackTime += Time.deltaTime;

        return lastAttackTime >= attackCooldown;
    }

    //Ataque del enemigo.

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
    public override void RangeAttack()
    {

        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out hit, LOS.detectionRange,layerMask))
        {

            Debug.Log(hit.collider.name);
              hit.collider.gameObject.GetComponent<PlayerController>().TakeDamage(damage); 
               
            

        }
        CurrentBullets--;
    }
    public override void TakeDamage(int amount)
    {
        Health = Mathf.Max(0, Health - amount);

        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
