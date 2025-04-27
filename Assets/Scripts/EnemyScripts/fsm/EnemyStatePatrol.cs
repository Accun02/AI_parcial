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
    IIAmove move;
    // Start is called before the first frame update

    public EnemyStatePatrol(IIAmove movement, List<Vector3> Waypoints, Transform enemytrans)
    {
        Enemy = enemytrans;
        _waypoints = Waypoints;
        move = movement;

    }

    public override void OnEnter()
    {
        patrolcicle = true;
        positions = 0;
  
    }
    public override void Execute()
    {
       
    
    }
    public override void OnExit()
    {

    }

}
