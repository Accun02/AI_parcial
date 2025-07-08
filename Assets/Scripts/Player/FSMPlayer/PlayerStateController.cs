using UnityEngine;

public class PlayerStateController : MonoBehaviour //gestiona los estados del jugador
{
    //maquina de estados finito y los estados idle y caminar
    public FSM<States> fsm; 
    private IdleState idle; 
    private WalkState walk;
    [SerializeField] PlayerController playerController;


    [SerializeField] private AudioSource SFX;
    [SerializeField] private AudioClip playerWalking;

    private void Start()
    {


        //crea estados con referencia al controller con el sonido
        idle = new IdleState(playerController);
        walk = new WalkState(playerController, playerWalking, SFX);

        //define las transiciones
        idle.AddTransition(States.Walk, walk);
        walk.AddTransition(States.Idle, idle);

        //crea FSM e inicia en Idle
        fsm = new FSM<States>(idle);
    }

    private void Update()
    {
        //ejecuta el estado actual
        fsm.OnExecute();
        fsm.OnFixedExecute();
        //cambios de estados según el input del jugador
        if (IsMoving())
        {
            fsm.OnTransition(States.Walk);
        
        }
        else
        {
            fsm.OnTransition(States.Idle);
        }
        playerController.Rotate();
    }

    private bool IsMoving() //inputs para detectar cuando el jugador se mueve
    {
        return (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0);



    }
}


