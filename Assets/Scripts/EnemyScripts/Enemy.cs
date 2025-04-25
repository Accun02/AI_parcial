using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Vector3 distance = Vector3.zero;
    float angle;
    LayerMask playermask;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray raycast = GetComponent<Ray>(); 
    }
}
