using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGTCommand : BaseCommand
{
    CollideAndSlideController CollideAndSlideController;
    public RGTCommand(CollideAndSlideController player)
    {
        CollideAndSlideController = player;
    }
    public override void Execute()
    {
        Debug.Log("right command");
        CollideAndSlideController.MoveDir(Vector3.right);
        //throw new System.NotImplementedException();
    }

    public override void Undo()
    {
        CollideAndSlideController.MoveDir(-Vector3.right);
    }
}

