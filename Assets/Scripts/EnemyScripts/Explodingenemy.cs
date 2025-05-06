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

    public float radius =  10; //Determina qu� tan lejos llega el da�o.

    private void Awake()
    {
        base.Awake();
    }

    //Define qu� pasa cuando el enemigo "ataca" (explota).
    public override void Attack()
    {
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
