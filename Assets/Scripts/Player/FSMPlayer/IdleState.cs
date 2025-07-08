using UnityEngine;

public class IdleState : State<States> //estado idle del personaje
{
    private PlayerController controller; //componenete fps controller

    public IdleState(PlayerController fps)
    {
        controller = fps;
    }

    public override void OnEnter()
    {
       //se ejecuta una vez al entrar en idle
    }

    public override void Execute()
    {
        controller.Shoot(); //mientras este en idle, no se mueve
    }

    public override void OnExit() //se ejecuta cuando se sale del estado idle
    {
  
        controller.canMove = true; //permite movimiento al salir
    }
}

