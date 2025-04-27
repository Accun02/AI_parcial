using UnityEngine;

public class WalkState : State<States>
{
    private FPScontroller controller;

    public WalkState(FPScontroller fps)
    {
        controller = fps;
    }

    public override void OnEnter()
    {
        Debug.Log("WALK State Activated");
        controller.canMove = true; // Habilita el movimiento
    }

    public override void Execute()
    {

    }

    public override void OnExit()
    {
        Debug.Log("No More WALK State");
        controller.canMove = false; // Lo frenamos cuando sale
    }
}


