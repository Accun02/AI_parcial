using UnityEngine;

[RequireComponent(typeof(FPScontroller))]
public class PlayerStateController : MonoBehaviour
{
    public FSM<States> fsm;
    private IdleState idle;
    private WalkState walk;

    private FPScontroller controller;

    private void Start()
    {
        controller = GetComponent<FPScontroller>();

        // Crear estados con referencia al controller
        idle = new IdleState(controller);
        walk = new WalkState(controller);

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
      
    }

  
}


