using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class EnemyStatePatrol : State<States>
{
    List<Vector3> _waypoints;
    Transform Enemy;
    int positions = 0;
    IIAmove move;

    public EnemyStatePatrol(IIAmove movement, List<Vector3> Waypoints, Transform enemytrans)
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
        if (_waypoints.Count == 0) return;

        Vector3 target = _waypoints[positions];
        Vector3 dir = target - Enemy.position;

        move.Move(dir);

        if (Vector3.Distance(Enemy.position, target) < 1f)
        {
            positions++;
            if (positions >= _waypoints.Count)
                positions = 0;
        }
    }
}

