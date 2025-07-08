using UnityEngine;
public class EnemyCoward : EnemyAI
{
    public float fleeDistance = 15f;

    protected override void Update()
    {
        if (CanSeePlayer())
        {
            Vector3 dirToPlayer = transform.position - player.position;
            Vector3 fleePosition = transform.position + dirToPlayer.normalized * fleeDistance;
        }
    }
}
