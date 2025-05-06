using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyStateExplode : State<States>
{
    Explodingenemy enemy;
    SteeringController controller;
    float timer = 3;
    EnemyStatePatrol enempatrol;
    EnemyStateIdle enemidle;
    public EnemyStateExplode (Explodingenemy expenemy, SteeringController controller, EnemyStatePatrol patrol, EnemyStateIdle idle)
    {
        this.controller = controller;
        this.enemy = expenemy;
        enemidle = idle;
        enempatrol = patrol;
    }

    public override void OnEnter()
    {
        controller.ChangeStearingMode(SteeringController.SteeringMode.None);

    }

    public override void Execute()
    {
        timer -= Time.deltaTime;
         enemy.enemySFX.clip = enemy.enemyExplodes;
        controller.ExecuteSteering();
         if (!enemy.enemySFX.isPlaying )
        {
            enemy.enemySFX.Play();
        }
        if (timer < 0)
        
        {
            enemy.Attack();
        }
    }
}
