using UnityEngine;

public class ObstacleAvoidance : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private float detectionRange = 5f; // Radio de detección de obstáculos.
    [SerializeField] private float avoidForce = 10f;     // Fuerza para evitar el obstáculo.
    [SerializeField] private LayerMask obstacleMask;     // Capas consideradas obstáculos.

    public Vector3 Avoid()
    {
        // Detecta todos los colliders dentro del rango
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange, obstacleMask);

        if (colliders.Length == 0) return Vector3.zero;

        float minDist = detectionRange + 1;

        Collider closestCol = null;

        // Busca el obstáculo más cercano
        foreach (var col in colliders)
        {
            float currentDist = Vector3.Distance(transform.position, col.ClosestPoint(transform.position));

            if (currentDist < minDist)
            {
                minDist = currentDist;
                closestCol = col;
            }
        }

        if (closestCol == null) return Vector3.zero;

        // Calcula dirección opuesta al punto más cercano del obstáculo
        Vector3 directionAway = transform.position - closestCol.ClosestPoint(transform.position);

        // Ignora la altura (y)
        directionAway.y = 0;

        // Normaliza la dirección y aplica la fuerza de evasión
        Vector3 avoidDir = directionAway.normalized * avoidForce;

        // Aplica atenuación según quEtan cerca estEdel obstáculo
        float distanceFactor = Mathf.Lerp(1, 0, minDist / detectionRange);

        avoidDir *= distanceFactor;

        return avoidDir;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
