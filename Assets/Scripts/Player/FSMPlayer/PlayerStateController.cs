using UnityEngine;

[RequireComponent(typeof(FPScontroller))] //necesita que el GO tenga FPS controller
public class PlayerStateController : MonoBehaviour //gestiona los estados del jugador
{
    //maquina de estados finito y los estados idle y caminar
    public FSM<States> fsm; 
    private IdleState idle; 
    private WalkState walk;

    private FPScontroller controller; //compnenete fps controller

    [SerializeField] private AudioSource SFX;
    [SerializeField] private AudioClip playerWalking;

    private void Start()
    {
        controller = GetComponent<FPScontroller>();

        //crea estados con referencia al controller con el sonido
        idle = new IdleState(controller);
        walk = new WalkState(controller, playerWalking, SFX);

        //define las transiciones
        idle.AddTransition(States.Walk, walk);
        walk.AddTransition(States.Idle, idle);

        //crea FSM e inicia en Idle
        fsm = new FSM<States>(idle);
    }

    private void Update()
    {
        fsm.OnExecute(); //ejecuta el estado actual

        //cambios de estados según el input del jugador
        if (IsMoving())
        {
            fsm.OnTransition(States.Walk);
        }
        else
        {
            fsm.OnTransition(States.Idle);
        }
    }

    private bool IsMoving() //inputs para detectar cuando el jugador se mueve
    {
        return Input.GetKey(KeyCode.UpArrow) ||
               Input.GetKey(KeyCode.DownArrow) ||
               Input.GetKey(KeyCode.LeftArrow) ||
               Input.GetKey(KeyCode.RightArrow);
    }
}


