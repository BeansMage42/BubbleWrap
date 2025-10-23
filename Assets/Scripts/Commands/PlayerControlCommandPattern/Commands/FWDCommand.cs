using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Forward Command", menuName = "Commands/Movement commands/forward", order = 2)]
public class FWDCommand : MovementCommand
{
    public override void Execute()
    {
        //  Debug.Log("forward command");
        base.Execute();
        CollideAndSlideController.MoveDir(Vector3.forward);
    }   

    public override void Undo()
    {

       // Debug.Log("forward released");
        CollideAndSlideController.MoveDir(-Vector3.forward);
    }
}
