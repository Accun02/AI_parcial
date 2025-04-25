using System.Collections.Generic;

public class State<T> : Istate<T>
{
    // Diccionario que guarda transiciones a otros estados
    private Dictionary<T, Istate<T>> _transitions = new Dictionary<T, Istate<T>>();

    public virtual void OnEnter()
    {
        // C�digo al entrar al estado
    }

    public virtual void Execute()
    {
        // C�digo que se ejecuta mientras est� en este estado
    }

    public virtual void OnExit()
    {
        // C�digo al salir del estado
    }

    public void AddTransition(T input, Istate<T> state)
    {
        if (!_transitions.ContainsKey(input))
        {
            _transitions.Add(input, state);
        }
        else
        {
            _transitions[input] = state; // Reemplaza si ya existe
        }
    }

    public Istate<T> GetTransition(T input)
    {
        if (_transitions.TryGetValue(input, out Istate<T> state))
        {
            return state;
        }

        return null; // Si no hay transici�n, devuelve null
    }

    public virtual void Transition(T input, Istate<T> state)
    {
        AddTransition(input, state);
    }
}

