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
        Debug.Log("WALK State Activated");
        controller.canMove = true; //habilita el movimiento
        SFX.clip = playerWalking;
        SFX.Play();
    }

    public override void Execute()
    {

    }

    public override void OnExit() //cuando salgo del estado walk
    {
        Debug.Log("No More WALK State");
        controller.canMove = false; //se detiene el mov y el efecto de sonido
        SFX.Stop();
    }
}


