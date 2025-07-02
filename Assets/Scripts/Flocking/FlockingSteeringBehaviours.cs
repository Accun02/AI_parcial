using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingSteeringBehaviour : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] protected float _maxSpeed;
    [SerializeField] protected float _maxForce;
    protected Vector3 _actualVelocity;

    private Rigidbody _rigidbody;

    public Vector3 Velocity => _actualVelocity;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    protected Vector3 FlockingSeek(Vector3 target)
    {
        Vector3 desired = (target - transform.position).normalized * _maxSpeed;

        return CalculateFlockingSteering(desired);
    }

    protected Vector3 CalculateFlockingSteering(Vector3 desired)
    {
        Vector3 steering = desired - _actualVelocity;

        return Vector3.ClampMagnitude(steering, _maxForce * Time.deltaTime);
    }

    protected void AddForce(Vector3 force) => _actualVelocity = Vector3.ClampMagnitude(_actualVelocity + force, _maxSpeed); //Es un método de una sola línea, por eso está el =>.

    protected void Move()
    {
        if(_actualVelocity == Vector3.zero) return;

        transform.forward = _actualVelocity; //Mira siempre hacia adelante, por eso el forward.
       _rigidbody.velocity = _actualVelocity;
    }
}
