using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

//Clase que representa el estado Idle de un enemigo.
public class EnemyStateIdle : State<States>
{
    SteeringController controller;

    BasicEnemyController enemy;
    ExplodingEnemyController exploding;

    EnemyStateChase enemchase;
    EnemyStatePatrol enempatrol;

    //Constructor para enemigos básicos con posibilidad de patrullar y perseguir.
    public EnemyStateIdle(SteeringController controller, BasicEnemyController enemy,EnemyStateChase chase, EnemyStatePatrol patrol)
    {
        this.controller = controller;
        this.enemy = enemy;
        this.enemchase = chase;
        this.enempatrol = patrol;
    }

    //Constructor alternativo para enemigos explosivos que solo patrullan.
    public EnemyStateIdle (SteeringController controller, ExplodingEnemyController enemy, EnemyStatePatrol patrol)
    {
        this.controller = controller;
        this.exploding = enemy;
        this.enempatrol = patrol;
    }

    public override void OnEnter()
    {
        //Se eliminan transiciones hacia los estados de patrullaje y persecución.
        base.RemoveTransitions(enemchase, States.Chase);
        base.RemoveTransitions(enempatrol, States.Patrol);

        //Se desactiva el movimiento del enemigo.
        controller.ChangeStearingMode(SteeringController.SteeringMode.None);

        //Se reinicia el temporizador de espera del enemigo (según el tipo).
        if (enemy != null)  enemy.timer = 10;
        if (exploding != null) exploding.timer = 10;
    }

    public override void Execute()
    {
        //Si el enemigo es del tipo básico, se actualiza su temporizador y comportamiento de espera.
        if (enemy != null)
        {
            enemy.timer -= Time.deltaTime;
            enemy.StandTime();
        } 

        //Si el enemigo es del tipo explosivo, se hace lo mismo.
        if (exploding != null)
        {
            exploding.timer -= Time.deltaTime;
            exploding.StandTime();
        }
    }

    public override void FixedExecute()
    {
        //Aunque el enemigo no se mueve, se llama por si hay lógica pasiva de Steering activa.
        controller.ExecuteSteering();
    }

    public override void OnExit()
    {
        //Se vuelven a permitir las transiciones hacia patrullaje y persecución.
        base.AddTransition(States.Patrol, enempatrol);
        base.AddTransition(States.Chase, enemchase);
    }
}
