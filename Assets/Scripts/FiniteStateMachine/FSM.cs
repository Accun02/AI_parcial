using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM<T> 
{
    Istate<T> current;

    public Action<T,Istate<T>> action;

    public FSM() { }

    //Coloca el estado inicial.
    public FSM(Istate<T> initial)
    {
        setInitial(initial);
    }

    public void setInitial(Istate<T> initial) //Estado inicial del FSM.
    {
        current = initial;
        current.OnEnter();
    }

    //Update del State.
    public void OnExecute()
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
        }
    }
    
    //Transición entre estados.
    public void OnTransition(T input)
    {
        current.OnExit();
        Istate<T> newState = current.GetTransition(input);
        if (newState == null) return;
        current = newState;
        Debug.Log(current);
        current.OnEnter();
    }
}
