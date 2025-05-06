using UnityEngine;

public class EnemyStateAttack : State<States>
{

    Enemy enemy;
    SteeringController controller;
    EnemyStateChase enemchase;
    EnemyStatePatrol enempatrol;
    EnemyStateIdle enemidle;

    public EnemyStateAttack(Enemy enemy,SteeringController controller, EnemyStateChase chase, EnemyStatePatrol patrol, EnemyStateIdle idle)
    {
       this.enemy = enemy;
       this.controller = controller;
        enemchase = chase;
        enemidle = idle;
        enempatrol = patrol;
    }
    public override void OnEnter()
    {
        controller.ChangeStearingMode(SteeringController.SteeringMode.None);
        RemoveTransitions(enempatrol, States.Patrol);
        RemoveTransitions(enemchase, States.Chase);
        RemoveTransitions(enemidle, States.Idle);
    }
    public override void Execute()
    {
    enemy.Attack();
    }
    public override void FixedExecute()
    {
        controller.ExecuteSteering();
    }

    public override void OnExit() 
    {
        AddTransition(States.Patrol, enempatrol);
        AddTransition(States.Chase, enemchase);
        AddTransition(States.Idle, enemidle);
    }
}
