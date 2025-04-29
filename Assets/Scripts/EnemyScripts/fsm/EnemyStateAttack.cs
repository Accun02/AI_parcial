using UnityEngine;

public class EnemyStateAttack : State<States>
{

    Enemy enemy;
    SteeringController controller;

    public EnemyStateAttack(Enemy enemy,SteeringController controller)
    {
       this.enemy = enemy;
       this.controller = controller;
    }
    public override void OnEnter()
    {
        controller.ChangeStearingMode(SteeringController.SteeringMode.None);
    }
    public override void Execute()
    {
    enemy.Attack();
    }
    public override void FixedExecute()
    {
        controller.ExecuteSteering();
    }

    public override void OnExit() { }
}
