using UnityEngine;

[RequireComponent(typeof(FPScontroller))]
public class PlayerStateController : MonoBehaviour
{
    public FSM<States> fsm;
    private IdleState idle;
    private WalkState walk;

    private FPScontroller controller;

    [SerializeField] private AudioSource SFX;
    [SerializeField] private AudioClip playerWalking;

    private void Start()
    {
        controller = GetComponent<FPScontroller>();

        // Crear estados con referencia al controller
        idle = new IdleState(controller);
        walk = new WalkState(controller, playerWalking, SFX);

        // Definir transiciones
        idle.AddTransition(States.Walk, walk);
        walk.AddTransition(States.Idle, idle);

        // Crear FSM e iniciar en Idle
        fsm = new FSM<States>(idle);
    }

    private void Update()
    {
        fsm.OnExecute();

        // Transiciones automáticas según input
        if (IsMoving())
        {
            fsm.OnTransition(States.Walk);
        }
        else
        {
            fsm.OnTransition(States.Idle);
        }
    }

    private bool IsMoving()
    {
        return Input.GetKey(KeyCode.UpArrow) ||
               Input.GetKey(KeyCode.DownArrow) ||
               Input.GetKey(KeyCode.LeftArrow) ||
               Input.GetKey(KeyCode.RightArrow);
    }
}


