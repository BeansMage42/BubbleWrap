using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Jump Command", menuName = "Commands/Movement commands/jump", order = 5)]
public class JumpCommand : MovementCommand
{
    public override void Execute()
    {
        base.Execute();
        CollideAndSlideController.Jump();
       // throw new System.NotImplementedException();
    }

    public override void Undo()
    {
        //throw new System.NotImplementedException();
    }
}
