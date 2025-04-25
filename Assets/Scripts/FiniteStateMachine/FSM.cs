using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM<T> 
{
    Istate<T> current;
    public Istate<T> GetCurrent => current;
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
    public void Execute() //update del state
    {
        if (current != null)
        {
            current.Execute();
        }

    }
    public void OnTransition(T input)
    {
        Istate<T> newState = current.Transition(input);
        if (newState == null) return;
        var previous = current;
        current.OnExit();
        current = newState;
        current.OnEnter();

    }
}
