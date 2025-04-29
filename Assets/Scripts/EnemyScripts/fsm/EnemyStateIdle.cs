using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStateIdle : State<States>
{
    SteeringController controller;

    EnemyController enemy;
    float timer = 10;
    public EnemyStateIdle(SteeringController controller,EnemyController enemy)
    {
        this.controller = controller;
        this.enemy = enemy;
    }

    public override void OnEnter()
    {
        controller.ChangeStearingMode(SteeringController.SteeringMode.None);
    }

    public override void Execute()
    {
       timer -= Time.deltaTime;

        if (timer < 0) 
        {
            enemy.StandTime();
        }
    }

   

    public override void OnExit()
    {
        timer = 10;
    }
}
