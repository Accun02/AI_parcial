using UnityEngine;

public class IdleState : State<States>
{
    private FPScontroller controller;

    public IdleState(FPScontroller fps)
    {
        controller = fps;
    }

    public override void OnEnter()
    {
        Debug.Log("IDLE State Activated");
    }

    public override void Execute()
    {
        controller.canMove = false; // No se mueve en idle
    }

    public override void OnExit()
    {
        Debug.Log("No More IDLE State");
        controller.canMove = true; // Permitir movimiento al salir
    }
}

