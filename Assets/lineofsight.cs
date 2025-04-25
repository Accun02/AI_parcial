using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class lineofsight : MonoBehaviour
{
    [SerializeField] int distance;
    [SerializeField] float angle;
   public LayerMask playermask;
 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit; 
            if( Physics.SphereCast(transform.position, angle, Vector3.forward, out hit, distance, playermask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(0, 0, distance) * hit.distance, Color.green);
        }

 
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}
