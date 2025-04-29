using UnityEngine;

public class EnemyStateAttack : State<States>
{

    Enemy enemy;

    public EnemyStateAttack(Enemy enemy)
    {
       this.enemy = enemy;
    }

    public override void Execute()
    {
    enemy.Attack();

    }

    public override void OnExit() { }
}
