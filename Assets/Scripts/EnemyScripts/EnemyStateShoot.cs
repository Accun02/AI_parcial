using UnityEngine;

public class EnemyStateShoot : State<States>
{
    private SteeringController controller;
    private BaseClassEnemy enemy;
    private Transform target;
    public EnemyStateShoot(SteeringController controller, BaseClassEnemy enemy, Transform player)
    {
        this.controller = controller;
        this.enemy = enemy;
    target = player;
    }

    public override void OnEnter()
    {
        enemy.RangeAttack(target);
        controller.ChangeStearingMode(SteeringController.SteeringMode.None);
       
    }

    public override void Execute()
    {

    
        controller.ExecuteSteering();
        
    }
    public override void OnExit()
    {
      
    }
}