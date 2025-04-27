using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTree : ItreeNode
{
    Action actions;
    public ActionTree(Action actions)
    {
        this.actions = actions;
    }

    
    public void Execute()
    {
        actions();
    }

}
