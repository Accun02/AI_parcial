using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class EnemyStatePatrol: State<States>
{
    List<Vector3> _waypoints = new List <Vector3> ();
    Transform Enemy;
    bool returntopos;
    bool patrolcicle;
    int positions = 0;
    // Start is called before the first frame update

    public EnemyStatePatrol( List<Vector3> Waypoints, Transform enemytrans)
    {
        Enemy = enemytrans;
        _waypoints = Waypoints;

        Debug.Log(_waypoints.Count);
    }

    public override void OnEnter()
    {
        patrolcicle = true;
        positions = 0;
    }
    public override void Execute()
    {
        do
        {
       

            if (positions <= _waypoints.Count && returntopos == false)
            {
                Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, _waypoints[positions], 100);
                positions++;

                if (positions == _waypoints.Count) returntopos = true; positions--;
            }

            if (positions < _waypoints.Count && returntopos == true )
            {
                Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, _waypoints[positions], 100);
                positions--;

                if (positions == 0)
                {
                    returntopos = false;
                    patrolcicle = false;
                    OnExit();
                }
            }
        } while (patrolcicle);
    }
    public override void OnExit()
    {

    }

}
