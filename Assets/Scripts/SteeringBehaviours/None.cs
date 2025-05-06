using NUnit.Framework.Constraints;
using UnityEngine;
using static SteeringController;
using static UnityEngine.GraphicsBuffer;
public class None : ISteering 
{
    Rigidbody body;
    public None(Rigidbody rigidbody) 
    {
        body = rigidbody;
    }

    public Vector3 MoveDirection()
    {
        //detiene el mov.
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        body.rotation = Quaternion.identity;
        return Vector3.zero;
    }
}