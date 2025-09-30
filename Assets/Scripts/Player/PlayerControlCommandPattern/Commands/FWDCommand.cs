using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FWDCommand :BaseCommand
{
    public override void Execute()
    {
        Debug.Log("forward command");

        //throw new System.NotImplementedException();
    }   

    public override void Undo()
    {
        throw new System.NotImplementedException();
    }
}
