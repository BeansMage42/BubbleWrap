using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Back Command", menuName = "Commands/Movement commands/back", order = 1)]
public class BCKCommand : MovementCommand
{
    public override void Execute()
    {
        //Debug.Log("backCommand");
        base.Execute();
        CollideAndSlideController.MoveDir(-Vector3.forward);
        //throw new System.NotImplementedException();
    }

    public override void Undo()
    {
        CollideAndSlideController.MoveDir(Vector3.forward);
    }
}
