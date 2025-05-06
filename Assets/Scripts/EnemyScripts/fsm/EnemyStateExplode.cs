using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

//Clase que representa el estado Explode de un enemigo que se autodestruye.
public class EnemyStateExplode : State<States>
{
    Explodingenemy enemy;
    EnemyStatePatrol enempatrol;
    EnemyStateIdle enemidle;

    SteeringController controller;

    float timer = 3; //Temporizador para esperar antes de detonar.

    //Constructor que recibe el enemigo, su controlador y los otros estados posibles.
    public EnemyStateExplode (Explodingenemy expenemy, SteeringController controller, EnemyStatePatrol patrol, EnemyStateIdle idle)
    {
        this.controller = controller;
        this.enemy = expenemy;
        enemidle = idle;
        enempatrol = patrol;
    }

    public override void OnEnter()
    {
        //Se detiene cualquier movimiento del enemigo.
        controller.ChangeStearingMode(SteeringController.SteeringMode.None);
    }

    public override void Execute()
    {
        //Se reduce el temporizador con el tiempo que ha pasado desde el ˙ltimo frame.
        timer -= Time.deltaTime;

        //Se asigna el clip de sonido de explosiÛn.
        enemy.enemySFX.clip = enemy.enemyExplodes;

        // Ejecuta cualquier comportamiento de steering activo.
        controller.ExecuteSteering();

        //Si el sonido a˙n no estÅEreproduciÈndose, lo reproduce.
        if (!enemy.enemySFX.isPlaying )
        {
            enemy.enemySFX.Play();
        }

        // Cuando el temporizador llega a 0, el enemigo realiza su ataque.
        if (timer < 0)
        {
            enemy.Attack();
        }
    }
}
