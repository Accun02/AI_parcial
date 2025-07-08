public class EnemyStateShoot : State<States>
{
    private SteeringController controller;
    private BaseClassEnemy enemy;

    public EnemyStateShoot (SteeringController controller, BaseClassEnemy enemy)
    {
        this.controller = controller;
        this.enemy = enemy;
    }

    public override void OnEnter()
    {
        controller.ChangeStearingMode(SteeringController.SteeringMode.None);
        enemy.RangeAttack();
    }

    public override void Execute()
    {


        controller.ExecuteSteering();
        
    }
    public override void OnExit()
    {
      
    }
}