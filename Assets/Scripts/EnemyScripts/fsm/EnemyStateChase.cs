using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateChase : State<States>
{
    Transform enemy;
    Transform player;
    bool seeingplayer;
    IIAmove iAmove;
    public EnemyStateChase(IIAmove movement,Transform enemy, Transform Target)
    {
     iAmove = movement;
        this.enemy = enemy;
        this.player = Target;
    }

    public override void OnEnter()
    {

    }

    public override void Execute()
    {
     Vector3 dirtotarget = player.position - enemy.position;
        iAmove.Move(dirtotarget.normalized);
        dirtotarget.y = 0;
        iAmove.LookAt(player);
     

     
    }

    public override void OnExit()
    {

    }
    
}