using UnityEngine;

public class EnemyStateAttack : State<States>
{
    private PlayerController player;
    private int damage = 2;

    public EnemyStateAttack(PlayerController player)
    {
        this.player = player;
    }

    public override void Execute()
    {
        if (player != null)
        {
            player.Health -= damage;
        }
        else
        {
            Debug.LogWarning("Player no asignado en EnemyStateAttack");
        }
    }

    public override void OnExit() { }
}
