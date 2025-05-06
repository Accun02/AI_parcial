using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyStateExplode : State<States>
{
    Explodingenemy enemy;
    SteeringController controller;
    float timer = 3;

   public EnemyStateExplode (Explodingenemy expenemy, SteeringController controller)
    {
        this.controller = controller;
        this.enemy = expenemy;
    }

    public override void OnEnter()
    {
        controller.ChangeStearingMode(SteeringController.SteeringMode.None);
    }

    public override void Execute()
    {
        timer -= Time.deltaTime;
         enemy.enemySFX.clip = enemy.enemyExplodes;
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
