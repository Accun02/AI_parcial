using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Istate<T>  
{
    void OnEnter();

    void Execute();

    void OnExit();

    void FixedExecute();

    void AddTransition(T input, Istate<T> state);

    Istate<T> GetTransition(T input);
}
