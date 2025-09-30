using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCKCommand : BaseCommand
{
    public override void Execute()
    {
        Debug.Log("backCommand");
        //throw new System.NotImplementedException();
    }

    public override void Undo()
    {
        throw new System.NotImplementedException();
    }
}
