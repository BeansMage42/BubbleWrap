using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LFTCommand : BaseCommand
{
    public override void Execute()
    {
        Debug.Log("left command");

        //throw new System.NotImplementedException();
    }

    public override void Undo()
    {
        throw new System.NotImplementedException();
    }
}
