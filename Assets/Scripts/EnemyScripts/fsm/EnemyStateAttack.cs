using UnityEngine;

public class EnemyStateAttack : State<States>
{
    //Referencias a otras clases y estados del enemigo.
    Enemy enemy;
    SteeringController controller;
    EnemyStateChase enemchase;
    EnemyStatePatrol enempatrol;
    EnemyStateIdle enemidle;

    //Constructor que recibe referencias necesarias para manejar transiciones entre estados.
    public EnemyStateAttack(Enemy enemy,SteeringController controller, EnemyStateChase chase, EnemyStatePatrol patrol, EnemyStateIdle idle)
    {
       this.enemy = enemy;
       this.controller = controller;
       enemchase = chase;
       enemidle = idle;
       enempatrol = patrol;
    }

    //Se ejecuta una vez cuando el enemigo entra en el estado de ataque.
    public override void OnEnter()
    {
        //Elimina las transiciones posibles hacia otros estados mientras está atacando.
        RemoveTransitions(enempatrol, States.Patrol);
        RemoveTransitions(enemchase, States.Chase);
        RemoveTransitions(enemidle, States.Idle);

        //Cambia el modo de movimiento a "ninguno" mientras ataca.
        controller.ChangeStearingMode(SteeringController.SteeringMode.None);
    }

    // Lógica principal del ataque, se ejecuta en cada frame.
    public override void Execute()
    {
    enemy.Attack();
    }

    //Se ejecuta en cada frame de física.
    public override void FixedExecute()
    {
        controller.ExecuteSteering();
    }

    //Se ejecuta al salir del estado de ataque.
    public override void OnExit() 
    {
        // Vuelve a permitir transiciones hacia los otros estados.
        AddTransition(States.Patrol, enempatrol);
        AddTransition(States.Chase, enemchase);
        AddTransition(States.Idle, enemidle);
    }
}
