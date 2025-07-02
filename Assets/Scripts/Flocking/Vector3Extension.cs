using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extension
{
    public static Vector3 ignoreYByMakingItZero(this Vector3 newVector3)
    {
        return new Vector3(newVector3.x, 0, newVector3.z);
    }
}
