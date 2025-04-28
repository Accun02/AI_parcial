using UnityEngine;

public class EnemyStateAttack : State<States>
{
    private Enemy enemyattack;

    public EnemyStateAttack(Enemy enemy)
    {
      enemyattack = enemy;
    }

    public override void Execute()
    {
        enemyattack.Attack();
    }

    public override void OnExit() { }
}
