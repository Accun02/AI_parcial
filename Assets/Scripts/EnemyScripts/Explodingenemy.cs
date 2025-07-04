using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.ShaderData;

public class Explodingenemy : BaseClassEnemy
{
    [SerializeField] public AudioSource enemySFX;
    [SerializeField] public AudioClip enemyExplodes;

    public LayerMask layerMask;

    public float radius =  10; //Determina quÅEtan lejos llega el daÒo.



    //Define quÅEpasa cuando el enemigo "ataca" (explota).
    public override void Attack()
    {
        enemySFX.clip = enemyExplodes;
        enemySFX.Play();
        Collider[] hits = Physics.OverlapSphere(transform.position,radius, layerMask);
        if (hits != null)
        {
            foreach (var item in hits)
            {
                var currTarget = item.transform;
                GameManager.Instance.Dead();
                break;
            }
            Destroy(this.gameObject);
        }
    }
}
