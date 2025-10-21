using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCKCommand : BaseCommand
{
    CollideAndSlideController CollideAndSlideController;
    public BCKCommand(CollideAndSlideController player) 
    {
        CollideAndSlideController = player;
    }
    public override void Execute()
    {
        //Debug.Log("backCommand");
        CollideAndSlideController.MoveDir(-Vector3.forward);
        //throw new System.NotImplementedException();
    }

    public override void Undo()
    {
        CollideAndSlideController.MoveDir(Vector3.forward);
    }
}
