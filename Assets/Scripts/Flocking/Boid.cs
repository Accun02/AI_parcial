using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Boid : FlockingSteeringBehaviour
{
    [Header("Radiuses")]
    [SerializeField] float separationRadius;
    [SerializeField] float detectionRadius;

    [Header("Weights")]
    [SerializeField, Range(0, 3f)] float separationWheight;
    [SerializeField, Range(0, 1f)] float cohesionWheight;
    [SerializeField, Range(0, 1f)] float alignmentWheight;
    [SerializeField] LayerMask layerMaskBoid;

    Transform playerTranform;

    protected override void Awake()
    {
        base.Awake();

        playerTranform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        Vector3 direction = playerTranform.position.ignoreYByMakingItZero() - this.transform.position.ignoreYByMakingItZero();
        
        AddForce(direction.normalized * _maxSpeed);
    }

    void Update()
    {
        Flocking();
        Move();
    }

    private void Flocking()
    {
        Vector3 direction = playerTranform.position.ignoreYByMakingItZero() - this.transform.position.ignoreYByMakingItZero();

        Vector3 flockingForce = Separation() * separationWheight + 
            Cohesion() * cohesionWheight + 
            Alignment() * alignmentWheight; //Combina los behaviours.

        AddForce(flockingForce + direction.normalized);
    }

    private Vector3 Separation()
    {
        int countEntities = 0;

        Vector3 direction = Vector3.zero;

        var boidsInRange = Physics.OverlapSphere(transform.position, separationRadius, layerMaskBoid);

        for (int i = 0; i < boidsInRange.Length; i++)
        {
            Boid boid = boidsInRange[i].GetComponent<Boid>();

            if (boid != null || boid == this) continue;

            direction += (transform.position - boid.transform.position).normalized / separationRadius;

            countEntities ++;
        }

        if (countEntities == 0) return direction;
        direction /= countEntities;
        return CalculateFlockingSteering(direction);
    }

    private Vector3 Cohesion()
    {
        int countPos = 0;

        Vector3 centrePos = Vector3.zero;

        var boidsInRange = Physics.OverlapSphere(transform.position, detectionRadius, layerMaskBoid);

        for (int i = 0; i < boidsInRange.Length; i++)
        {
            Boid boid = boidsInRange[i].GetComponent<Boid>();

            if (boid != null || boid == this) continue;

            centrePos += boid.transform.position; //Promedio de posiciones.

            countPos ++;
        }

        if (countPos == 0) return centrePos;
        centrePos /= countPos;
        return FlockingSeek(centrePos);
    }

    private Vector3 Alignment() 
    {
        int countPos = 0;

        Vector3 desired = Vector3.zero;

        var boidsInRange = Physics.OverlapSphere(transform.position, detectionRadius, layerMaskBoid);

        for (int i = 0; i < boidsInRange.Length; i++)
        {
            Boid boid = boidsInRange[i].GetComponent<Boid>();

            if (boid != null || boid == this) continue;

            desired += boid.Velocity;

            countPos++;
        }

        if (countPos == 0) return desired;
        desired /= countPos;
        return CalculateFlockingSteering(desired.normalized * _maxSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, separationRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
