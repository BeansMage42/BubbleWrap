using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGTCommand : BaseCommand
{
    public override void Execute()
    {
        Debug.Log("right command");
        //throw new System.NotImplementedException();
    }

    public override void Undo()
    {
        throw new System.NotImplementedException();
    }
}

