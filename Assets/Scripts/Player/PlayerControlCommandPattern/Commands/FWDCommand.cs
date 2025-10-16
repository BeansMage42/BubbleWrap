using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FWDCommand :BaseCommand
{
    CollideAndSlideController CollideAndSlideController;
    
    public FWDCommand(CollideAndSlideController player)
    {
        CollideAndSlideController = player;
    }
    public override void Execute()
    {
        Debug.Log("forward command");
        CollideAndSlideController.MoveDir(Vector3.forward);
    }   

    public override void Undo()
    {

        Debug.Log("forward released");
        CollideAndSlideController.MoveDir(-Vector3.forward);
    }
}
