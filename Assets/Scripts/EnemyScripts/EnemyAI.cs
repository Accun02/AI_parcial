using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyType { Shooter, Chaser, Coward }
    public EnemyType type;
    public float visionRange = 10f;
    public Transform player;
    protected NavMeshAgent agent;
    protected PlayerHealth playerHealth;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Debug.Log("Start() del EnemyAI ejecutado en: " + gameObject.name);

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
                playerHealth = playerObj.GetComponent<PlayerHealth>();
            }
            else
            {
                Debug.LogWarning("No se encontró el jugador con tag 'Player'");
            }
        }
    }

    protected bool CanSeePlayer()
    {
        float dist = Vector3.Distance(transform.position, player.position);
        Debug.Log(gameObject.name + " distancia al jugador: " + dist);
        return dist <= visionRange;
    }

    protected virtual void Update() { }
}
