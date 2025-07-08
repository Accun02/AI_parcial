using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Vida del jugador.
    private int health = 10;
    public int Health { get { return health; } set { health = value; } }
    [SerializeField] LayerMask eneemymask;
    private int ammo = 10;
    private int damage = 2;

    public void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))

        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 100, eneemymask) && ammo > 0)
            {
                hit.collider.gameObject.GetComponent<PlayerController>().Health -= damage;
            }
            ammo = 0;
        }
    }
}