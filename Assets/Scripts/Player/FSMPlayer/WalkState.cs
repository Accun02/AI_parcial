using UnityEngine;

public class WalkState : State<States>
{
    private FPScontroller controller;

    [SerializeField] private AudioSource SFX;
    [SerializeField] private AudioClip playerWalking;

    public WalkState(FPScontroller fps, AudioClip soundWalking, AudioSource playerWalking)
    {
        controller = fps;
        this.SFX = playerWalking;
        this.playerWalking = soundWalking;
    }

    public override void OnEnter()
    {
        Debug.Log("WALK State Activated");
        controller.canMove = true; // Habilita el movimiento
        SFX.clip = playerWalking;
        SFX.Play();
    }

    public override void Execute()
    {

    }

    public override void OnExit()
    {
        Debug.Log("No More WALK State");
        controller.canMove = false; // Lo frenamos cuando sale
        SFX.Stop();
    }
}


