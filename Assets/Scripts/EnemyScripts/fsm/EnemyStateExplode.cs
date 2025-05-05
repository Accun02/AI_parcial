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
        con
    }

    public override void Execute()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            enemy.Attack();
        }
    }
}
