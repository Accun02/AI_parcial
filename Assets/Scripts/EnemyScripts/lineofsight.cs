using UnityEngine;

public class lineofsight : MonoBehaviour
{
    [SerializeField] private LayerMask obstaclesMask;

    public float detectionRange = 10f;
    public float loseplayer = 15f;
    public float detectionAngle = 120f;

    public bool CheckDistance(Transform target)
    {
        float distance = Vector3.Distance(target.position, transform.position);
        return distance <= detectionRange;
    }

    public bool LosePlayer(Transform target)
    {
        float distance = Vector3.Distance(target.position, transform.position);
        return distance > loseplayer;
    }

    public bool CheckAngle(Transform target)
    {
        Vector3 dir = target.position - transform.position;
        float angle = Vector3.Angle(transform.forward, dir);
        return angle <= detectionAngle / 2f;
    }

    public bool CheckView(Transform target)
    {
        Vector3 origin = transform.position + Vector3.up * 1.5f; // ajuste altura
        Vector3 dir = target.position - origin;

        return !Physics.Raycast(origin, dir.normalized, dir.magnitude, obstaclesMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, detectionAngle / 2, 0) * transform.forward * detectionRange);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -detectionAngle / 2, 0) * transform.forward * detectionRange);
    }
}



