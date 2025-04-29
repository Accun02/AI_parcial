using NUnit.Framework.Constraints;
using UnityEngine;
using static SteeringController;
using static UnityEngine.GraphicsBuffer;
public class None : ISteering
{
    public None() { }
    public Vector3 MoveDirection()
    {
        return Vector3.zero;
    }
}