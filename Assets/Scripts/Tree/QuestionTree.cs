using NUnit.Framework.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionTree : ItreeNode
{
    Func<bool> question; //pregunta o condicion que se evaluara
    //ramas izq (si es verdadero) y der (si es falso)
    ItreeNode ltnode;
    ItreeNode rtnode;

    public QuestionTree (Func<bool> question, ItreeNode ltnode, ItreeNode rtnode)
    {
        this.question = question;
        this.ltnode = ltnode;
        this.rtnode = rtnode;
    }

    public void Execute()
    {
        if (question() == true)
        {
            ltnode.Execute(); //si la condición es verdadera, ejecuta rama izquierda
        }
        else
        {
            rtnode.Execute();
        }
    }
}
