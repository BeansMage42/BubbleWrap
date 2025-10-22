using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "right Command", menuName = "Commands/Movement commands/right", order = 3)]
public class RGTCommand : MovementCommand
{
    public override void Execute()
    {
        base.Execute();
        //Debug.Log("right command");
        CollideAndSlideController.MoveDir(Vector3.right);
        //throw new System.NotImplementedException();
    }

    public override void Undo()
    {
        CollideAndSlideController.MoveDir(-Vector3.right);
    }
}

