using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IIAmove 
{
    void Move(Vector3 dir);
    void Look(Vector3 lookdir);

    void LookAt(Transform dir);
}
