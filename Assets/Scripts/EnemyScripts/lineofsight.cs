using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class lineofsight : MonoBehaviour
{
    public float detectionRange;
    public float detectionAngle;
    private LayerMask obstaclesMask;

    public bool CheckDistance(Transform target) //Checks if the target is within range of vision.
    {
        float distance = Vector3.Distance(target.position, transform.position);
        return distance <= detectionRange;
    }

    public bool CheckAngle(Transform target) //Checks if the target is within the angle of vision.
    {
        Vector3 dir = target.position - transform.position;
        float angle = Vector3.Angle(transform.forward, dir);
        return angle <= detectionAngle / 2;
    }

    public bool CheckView(Transform target) //Checks if no obstacles obstructs the target out of view.
    {
        Vector3 dir = target.position - transform.position;

        return !Physics.Raycast(transform.position, dir.normalized, dir.magnitude, obstaclesMask);
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



