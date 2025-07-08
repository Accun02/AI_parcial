using UnityEngine;

public class EnemyChaser : EnemyAI
{
    public float attackDistance = 2f;
    public float attackCooldown = 2f;
    private float lastAttackTime;

    protected override void Update()
    {
        if (player == null)
        {
            Debug.Log("Jugador no asignado.");
            return;
        }

        Debug.Log(gameObject.name + " está ejecutando Update()");

        if (CanSeePlayer())
        {
            Debug.Log(gameObject.name + " puede ver al jugador");
            agent.SetDestination(player.position);

            if (Vector3.Distance(transform.position, player.position) <= attackDistance)
            {
                if (Time.time - lastAttackTime > attackCooldown)
                {
                    playerHealth.TakeDamage(10);
                    lastAttackTime = Time.time;
                }
            }
        }
    }
}
