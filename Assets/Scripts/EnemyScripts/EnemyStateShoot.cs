public class EnemyStateShoot : State<States>
{
    private SteeringController controller;
    private Enemy enemy;

    public EnemyStateShoot (SteeringController controller, Enemy enemy)
    {
        this.controller = controller;
        this.enemy = enemy;
    }

    public override void OnEnter()
    {
        if (enemy.CurrentBullets > 0)
        {
            enemy.RangeAttack();
        }
    }

    public override void Execute()
    {


      
        
    }
    public override void OnExit()
    {
      
    }
}