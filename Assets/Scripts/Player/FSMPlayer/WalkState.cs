using UnityEngine;

public class WalkState : State<States> //estado de caminata
{
    private FPScontroller controller;

    //audio de pasos para acompañar al estado de caminata
    [SerializeField] private AudioSource SFX; 
    [SerializeField] private AudioClip playerWalking;

    public WalkState(FPScontroller fps, AudioClip soundWalking, AudioSource playerWalking)
    {
        controller = fps;
        this.SFX = playerWalking;
        this.playerWalking = soundWalking;
    }

    public override void OnEnter() //cuando entro al estado walk
    {
      
         //habilita el movimiento
      
    }


    public override void FixedExecute()
    {
        controller.Movement();
    }
    public override void OnExit() //cuando salgo del estado walk
    {

         //se detiene el mov y el efecto de sonido
        SFX.Stop();
    }
}


