using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Left Command", menuName = "Commands/Movement commands/left", order = 4)]
public class LFTCommand : MovementCommand
{
    public override void Execute()
    {
        //  Debug.Log("left command");
        base.Execute();
        CollideAndSlideController.MoveDir(Vector3.left);
        //throw new System.NotImplementedException();
    }

    public override void Undo()
    {
        CollideAndSlideController.MoveDir(Vector3.right);
    }
}
