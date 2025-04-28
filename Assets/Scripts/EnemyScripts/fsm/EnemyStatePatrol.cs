using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class EnemyStatePatrol : State<States>
{
    List<Transform> _waypoints;
    Transform Enemy;
    int positions = 0;
    IIAmove move;

    public EnemyStatePatrol(IIAmove movement, List<Transform> Waypoints, Transform enemytrans)
    {
        Enemy = enemytrans;
        _waypoints = Waypoints;
        move = movement;
    }

    public override void OnEnter()
    {
        positions = 0;
    }

    public override void Execute()
    {
        

        Vector3 target = _waypoints[positions].position;
        Vector3 dir = target - Enemy.position;

        Debug.Log(target);
        move.Move(dir);

        if (Vector3.Distance(Enemy.position, target) < 1f)
        {
            positions++;
            if (positions >= _waypoints.Count)
                positions = 0;
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}

