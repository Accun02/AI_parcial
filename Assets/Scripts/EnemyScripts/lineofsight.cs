using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class lineofsight : MonoBehaviour
{
    [SerializeField] float distance;
    [SerializeField] float angle;
    public bool isOnRange;
    public Transform objective;
    public LayerMask playermask;
    public LayerMask obstacleMask;


    public bool OnRange()
    {
        return  isOnRange ? true : false;
    }
    void Update()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, angle, Vector3.forward, out hit, distance, playermask))
            if (objective != null)
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(0, 0, distance) * hit.distance, Color.green);
            }
        Vector3 directionToTarget = (objective.position - transform.position).normalized;
        float distanceToTarget = Vector3.Distance(transform.position, objective.position);


        //verifica si esta dentro de la distancia
        if (distanceToTarget <= distance)
        {
            //se fija si esta dentro del angulo de vision
            float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);
            if (angleToTarget <= angle / 2)
            {
                //se fija si hay algo en el medio obstaculizando
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    isOnRange = true;
                    Debug.Log(" El enemigo te esta viendo!");
                    Debug.DrawLine(transform.position, objective.position, Color.green);
                }
                else
                {
                    isOnRange = false;
                    Debug.DrawLine(transform.position, objective.position, Color.red);
                }
            }
            else
            {
                isOnRange = false;
            }
        }
        else
        {
            isOnRange = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);

        // Dibujar el  ngulo de visi n en el editor
        Vector3 rightLimit = Quaternion.Euler(0, angle / 2, 0) * transform.forward;
        Vector3 leftLimit = Quaternion.Euler(0, -angle / 2, 0) * transform.forward;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, rightLimit * distance);
        Gizmos.DrawRay(transform.position, leftLimit * distance);
    }
}



