using System;
using System.Collections.Generic;

public class State<T> : Istate<T>
{
    // Diccionario que guarda transiciones a otros estados.
    private Dictionary<T, Istate<T>> _transitions = new Dictionary<T, Istate<T>>();

    public virtual void OnEnter()
    {
        //CÛdigo al entrar al estado.
    }

    public virtual void Execute()
    {
        //CÛdigo que se ejecuta mientras estÅEn en este estado.
    }

    public virtual void FixedExecute() { }

    public virtual void OnExit()
    {

    }

    public void AddTransition(T input, Istate<T> state)
    {
        if (!_transitions.ContainsKey(input))
        {
            _transitions.Add(input, state);
        }
        else
        {
            //Reemplaza si ya existe.
            _transitions[input] = state; 
        }
    }

    public  virtual Istate<T> GetTransition(T input)
    {
        if (_transitions.TryGetValue(input, out Istate<T> state))
        {
            return state;
        }

        return null; // Si no hay transiciÛn, devuelve null
    }


    public virtual void RemoveTransitions(Istate<T> state, T input)
    {
        for (int i = 0; i < _transitions.Count; i++)
        {
            if (state != null) 
            {
                if (_transitions.ContainsValue(state))
                {
                    _transitions.Remove(input, out state);
                }
            }
        }
    }

    internal void RemoveTransitions(EnemyStateChase enemchase)
    {
        throw new NotImplementedException();
    }
}

