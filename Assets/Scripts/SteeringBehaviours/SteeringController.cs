using NUnit.Framework.Constraints;
using UnityEngine;
using static SteeringController;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody))]
public class SteeringController : MonoBehaviour
{
    [Header("Parameters")]
    public float maxVelocity = 10f;
    public float timePrediction = 1f;
    public SteeringMode mode;

    [Header("References")]
    public Transform target;
    public Rigidbody targetrb;
    public Transform[] Waypoints;
    public ObstacleAvoidance obstacleAvoidance; //(Se arrastra el script desde el inspector)
    public WaypointController controller;
    private ISteering currentSteering;
    public Rigidbody rb;
    private Vector3 finalForce;

    //instancias de los comportamientos
    Flee flee;
    Persuit persuit;
    Evade evade;
    Seek seek;
    None none;
    public enum SteeringMode
    {
        seek,
        flee,
        persuit,
        evade,
        None,
    }

    void Start()
    {
        //se crean los objetos de cada comportameinto con sus dependencias
        none = new(rb);
        flee = new(rb, target, maxVelocity);
        persuit = new(rb, targetrb, maxVelocity, timePrediction);
        evade = new(rb, targetrb, maxVelocity, timePrediction);
        seek = new(rb, target,maxVelocity);
      
        //el comp. inicial es ninguna
        currentSteering = none;
    }
    public void gotoposition(Transform wptarget) //cambia el objetivo del seek
    {
     seek.target = wptarget;
     
    }
    public void ExecuteSteering() //ejecuta la logica del comportamiento
    {
    
        Vector3 steeringDir = currentSteering.MoveDirection();                                                                                                                                       

        //dirección de evasión de obstáculos
        Vector3 avoidDir = obstacleAvoidance ? obstacleAvoidance.Avoid() : Vector3.zero;

        //suma de ambas fuerzas
        finalForce = steeringDir + avoidDir;

        if (steeringDir != Vector3.zero)
        {
            //aplicación de la fuerza al Rigidbody
            rb.AddForce(finalForce, ForceMode.Acceleration);

            //rotación hacia la dirección de movimiento
            if (rb.velocity != Vector3.zero)
                transform.forward = rb.velocity.normalized;
        }

      
    }

    public void ChangeStearingMode(SteeringMode mode) //cambia el comportamiento segun el modo
    {
        this.mode = mode;

        switch (mode)
        {
            case SteeringMode.seek:
                currentSteering = seek;
                break;
            case SteeringMode.flee:
                currentSteering = flee;
                break;
            case SteeringMode.persuit:
                currentSteering = persuit;
                break;
            case SteeringMode.evade:
                currentSteering = evade;
                break;
                case SteeringMode.None: currentSteering = none; 
                break;
            
        }
    }
}

