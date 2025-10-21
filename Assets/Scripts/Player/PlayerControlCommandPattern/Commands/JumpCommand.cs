using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommand : BaseCommand
{
    CollideAndSlideController CollideAndSlideController;
    public JumpCommand(CollideAndSlideController player)
    {
        CollideAndSlideController = player;
    }
    public override void Execute()
    {
      //  CollideAndSlideController.Jump();
       // throw new System.NotImplementedException();
    }

    public override void Undo()
    {
        //throw new System.NotImplementedException();
    }
}
