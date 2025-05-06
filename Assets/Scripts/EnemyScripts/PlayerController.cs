using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Vida del jugador.
    private int health = 10;
    public int Health { get { return health; } set { health = value; } }
}