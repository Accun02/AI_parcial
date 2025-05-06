using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//Clase que representa el estado Chase de un enemigo en una máquina de estados.
public class EnemyStateChase : State<States>
{
    //Referencia al controlador de Steering (dirección y movimiento).
    SteeringController controller;

    // Referencia al estado de Patrol.
    EnemyStatePatrol enemPatrol;

    //Constructor que recibe el controlador de Steering y el estado de Patrol.
    public EnemyStateChase(SteeringController controller, EnemyStatePatrol enemPatrol)
    {
        this.controller = controller;
        this.enemPatrol = enemPatrol;
    }

    //Método llamado al entrar en el estado de Pursuit.
    public override void OnEnter()
    {
        //Se eliminan posibles transiciones hacia el estado de patrullaje.
        base.RemoveTransitions(enemPatrol, States.Patrol);

        //Se cambia el modo de movimiento del controlador al modo Pursuit.
        controller.ChangeStearingMode(SteeringController.SteeringMode.persuit);
    }

    public override void FixedExecute()
    {
        //Ejecuta el comportamiento de movimiento actual del enemigo.
        controller.ExecuteSteering();
    }
    
    public override void OnExit()
    {
        //Se vuelve a permitir la transición hacia el estado de Patrol.
        base.AddTransition(States.Patrol,enemPatrol);
    }
}
