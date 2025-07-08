using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class EnemyShooter : EnemyAI
{
    public int minBullets = 3;
    public int maxBullets = 6;
    private int currentBullets;

    protected override void Start()
    {
        base.Start(); // <- ¡Esto es CLAVE!

        currentBullets = Random.Range(minBullets, maxBullets + 1);
    }


    protected override void Update()
    {
        if (player == null)
        {
            Debug.Log("Jugador no asignado.");
            return;
        }

        Debug.Log(gameObject.name + " está ejecutando Update()");

        if (CanSeePlayer())
        {
            Debug.Log(gameObject.name + " puede ver al jugador");
            transform.LookAt(player);

            if (currentBullets > 0)
            {
                Shoot();
            }
            else
            {
                type = EnemyType.Chaser;
                gameObject.AddComponent<EnemyChaser>();
                Destroy(this);

            }
        }
    }

    void Shoot()
    {
        Debug.DrawRay(transform.position + Vector3.up, transform.forward * visionRange, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out hit, visionRange))
        {
            if (hit.transform.CompareTag("Player"))
            {
                playerHealth.TakeDamage(20);
                currentBullets--;
            }
        }
    }
}
