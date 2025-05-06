using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class lineofsight : MonoBehaviour
{
    [SerializeField] private LayerMask obstaclesMask;

    public float detectionRange;
    public float loseplayer;
    public float detectionAngle;

    //Comprueba si el objetivo está dentro del rango de visión.
    public bool CheckDistance(Transform target) 
    {
        float distance = Vector3.Distance(target.position, transform.position);

        return distance <= detectionRange;
    }

    //Comprueba si el objetivo está fuera del rango de seguimiento (como para dejar de perseguirlo).
    public bool LosePlayer(Transform target)
    {
        float distance = Vector3.Distance(target.position, this.transform.position);

        return distance > loseplayer;
    }

    // Comprueba si el objetivo está dentro del ángulo de visión.
    public bool CheckAngle(Transform target)
    {
        Vector3 dir = target.position - transform.position;
        float angle = Vector3.Angle(transform.forward, dir);
        return angle <= detectionAngle / 2;
    }

    // Comprueba que no haya obstáculos que bloqueen la vista del objetivo.
    public bool CheckView(Transform target)
    {
        Vector3 dir = target.position - transform.position;

        return !Physics.Raycast(transform.position, dir.normalized, dir.magnitude, obstaclesMask);
    }

    private void OnDrawGizmos()
    {
        // Dibuja una esfera que representa el rango de detección.
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Dibuja líneas que representan el ángulo de visión.
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, detectionAngle / 2, 0) * transform.forward * detectionRange);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -detectionAngle / 2, 0) * transform.forward * detectionRange);
    }
}



