using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTree : ItreeNode
{
    Action actions; //accion que se ejecutara cuando este nodo sea alcanzado
    public ActionTree(Action actions)
    {
        this.actions = actions; //se guarda la accion a ejecutar
    }

    
    public void Execute()
    {
        actions(); //se ejecuta la acción asignada
    }

}
