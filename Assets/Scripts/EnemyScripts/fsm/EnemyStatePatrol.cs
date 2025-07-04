using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class EnemyStatePatrol : State<States>
{
    SteeringController controller;
    PFEntity pFEntity;

    //Instancia.
    public EnemyStatePatrol(SteeringController controller, PFEntity entity)
    {
        this.controller = controller;
  this.pFEntity = entity;
    }

    public override void OnEnter()
    {

    }

    public override void FixedExecute()
    {
        pFEntity.Move();
    }

    public override void OnExit()
    {

    }
}

