using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IIAmove 
{
    void Move(Vector3 dir); //Mueve al enemigo.
    void Look(Vector3 lookdir); //Lo orienta hacia una dirección.
    void LookAt(Transform dir); //Lo orienta hacia otro "objeto" en la escena.
}
