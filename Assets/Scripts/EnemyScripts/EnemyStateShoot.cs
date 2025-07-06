internal class EnemyStateShoot
{
    private SteeringController controller;
    private Enemy enemy;

    public EnemyStateShoot(SteeringController controller, Enemy enemy)
    {
        this.controller = controller;
        this.enemy = enemy;
    }
}