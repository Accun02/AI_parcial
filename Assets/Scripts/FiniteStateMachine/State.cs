using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State<T> : Istate<T>
{
    Dictionary<T, Istate <T>> _transitions = new Dictionary<T, Istate<T>>();
    public virtual void OnEnter()
    {
 
    }

    public virtual void Execute()
    {
     
    }

    public virtual void OnExit()
    {
 
    }


    public  virtual void Transition(T input, Istate<T> state)
    {
        _transitions[input] = state;
    }

  
}
