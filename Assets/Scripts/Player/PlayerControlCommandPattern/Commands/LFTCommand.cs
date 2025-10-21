using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LFTCommand : BaseCommand
{
    CollideAndSlideController CollideAndSlideController;
    public LFTCommand(CollideAndSlideController player)
    {
        CollideAndSlideController = player;
    }
    public override void Execute()
    {
      //  Debug.Log("left command");
        CollideAndSlideController.MoveDir(Vector3.left);
        //throw new System.NotImplementedException();
    }

    public override void Undo()
    {
        CollideAndSlideController.MoveDir(Vector3.right);
    }
}
