using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Istate<T>  
{

    void OnEnter();
    void Execute();
    void OnExit();

    Istate<T> Transition(T input);
    
}
