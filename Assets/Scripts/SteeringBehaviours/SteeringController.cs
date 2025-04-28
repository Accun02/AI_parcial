using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SteeringController : MonoBehaviour
{
    [Header("Parameters")]
    public float maxVelocity = 10f;
    public float timePrediction = 1f;
    public SteeringMode mode;

    [Header("References")]
    public Transform target;
    public ObstacleAvoidance obstacleAvoidance; // (Se arrastra el script desde el inspector)

    private ISteering currentSteering;
    private Rigidbody rb;
    private Vector3 finalForce;

    public enum SteeringMode
    {
        seek,
        flee,
        persuit,
        evade
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        enabled = false;

        // Crea las instancias de steering con sus configuraciones
        switch (mode)
        {
            case SteeringMode.seek:
                currentSteering = new Seek(rb, target, maxVelocity);
                break;
            case SteeringMode.flee:
                currentSteering = new Flee(rb, target, maxVelocity);
                break;
            case SteeringMode.persuit:
                currentSteering = new Persuit(rb, target.GetComponent<Rigidbody>(), maxVelocity, timePrediction);
                break;
            case SteeringMode.evade:
                currentSteering = new Evade(rb, target.GetComponent<Rigidbody>(), maxVelocity, timePrediction);
                break;
        }
    }

    void FixedUpdate()
    {
        // Dirección base del comportamiento
        Vector3 steeringDir = currentSteering.MoveDirection();

        // Dirección de evasión de obstáculos
        Vector3 avoidDir = obstacleAvoidance ? obstacleAvoidance.Avoid() : Vector3.zero;

        // Suma de ambas fuerzas
        finalForce = steeringDir + avoidDir;

        // Aplicación de la fuerza al Rigidbody
        rb.AddForce(finalForce, ForceMode.Acceleration);

        // Rotación hacia la dirección de movimiento
        if (rb.velocity != Vector3.zero)
            transform.forward = rb.velocity.normalized;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + rb.velocity);

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + finalForce);
    }
}

