using UnityEngine;

public class Seek : ISteering
{
    private Rigidbody rb;

    private Transform target;

    private float maxVelocity;
    public Seek(Rigidbody rb, Transform target, float maxVelocity)
    {
        this.rb = rb;
        this.target = target;
        this.maxVelocity = maxVelocity;
    }

    public Vector3 MoveDirection()
    {
        // Calcula la dirección deseada para llegar al objetivo.
        Vector3 desiredVelocity = (target.position - rb.position).normalized * maxVelocity;
        // Lo que se necesita cambiar en la velocidad actual para llegar a la deseada.
        Vector3 directionForce = desiredVelocity - rb.velocity;

        // Elimina movimiento vertical (2D).
        directionForce.y = 0;
        // Limita la fuerza.
        directionForce = Vector3.ClampMagnitude(directionForce, maxVelocity);

        // Aplica la fuerza.
        rb.AddForce(directionForce, ForceMode.Acceleration);
        return directionForce;
    }
}