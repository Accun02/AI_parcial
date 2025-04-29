using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM<T> 
{
    Istate<T> current;
    public Action<T,Istate<T>> action;
    public FSM() { }
    public FSM(Istate<T> initial)
    {
        setInitial(initial);
    }

    public void setInitial(Istate<T> initial) // estado inicial de fsm
    {
     current = initial;
     current.OnEnter();
    }
    public void OnExecute() //update del state
    {
        if (current != null)
        {
          
            current.Execute();
           
        }

    }

    public void OnFixedExecute()
    {
        if (current != null)
        {

            current.FixedExecute();
            Debug.Log(current.ToString());
        }
    }
    

    public void OnTransition(T input)
    {
        Istate<T> newState = current.GetTransition(input);
        if (newState == null) return;
        var previous = current;
        current.OnExit();
        current = newState;
        current.OnEnter();

    }
}
