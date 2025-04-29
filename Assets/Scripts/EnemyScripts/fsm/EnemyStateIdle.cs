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
    public EnemyStateIdle(SteeringController controller,EnemyController enemy,float timer)
    {
        this.controller = controller;
        this.enemy = enemy;
        this.timer = timer;
    }

    public override void OnEnter()
    {
        controller.ChangeStearingMode(SteeringController.SteeringMode.None);
        timer = 10;
    }

    public override void Execute()
    {
          enemy.timer -= Time.deltaTime;
      
    }
    public override void FixedExecute()
    {
        controller.ExecuteSteering();
    }



    public override void OnExit()
    {
        enemy.timer = 10;
    }
}
