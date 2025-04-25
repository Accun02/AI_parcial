using UnityEngine;

//Asegura que cualquier clase de comportamiento de steering implemente el método MoveDirection().
public interface ISteering 
{
    //Devuleve un Vector3 con la dirección del movimiento deseado.
    public Vector3 MoveDirection();
}
