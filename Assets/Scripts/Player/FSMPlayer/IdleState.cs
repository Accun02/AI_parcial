using UnityEngine;

public class IdleState : State<States> //estado idle del personaje
{
    private FPScontroller controller; //componenete fps controller

    public IdleState(FPScontroller fps)
    {
        controller = fps;
    }

    public override void OnEnter()
    {
        Debug.Log("IDLE State Activated"); //se ejecuta una vez al entrar en idle
    }

    public override void Execute()
    {
        controller.canMove = false; //mientras este en idle, no se mueve
    }

    public override void OnExit() //se ejecuta cuando se sale del estado idle
    {
        Debug.Log("No More IDLE State");
        controller.canMove = true; //permite movimiento al salir
    }
}

