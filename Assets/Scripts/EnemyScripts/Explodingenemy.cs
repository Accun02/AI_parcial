using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explodingenemy : BaseClassEnemy
{
    [SerializeField] private AudioSource enemySFX;
    [SerializeField] private AudioClip enemyExplodes;

    public LayerMask layerMask;

    public float radius =  10;

    private void Awake()
    {
        base.Awake();
    }

    public override void Attack()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position,radius, layerMask);
        if (hits != null)
        {
            enemySFX.clip = enemyExplodes;
            enemySFX.Play();

            Destroy(this.gameObject);
            foreach (var item in hits)
            {
                var currTarget = item.transform;
                GameManager.Instance.Dead();
                break;
            }
        }
    }
}
