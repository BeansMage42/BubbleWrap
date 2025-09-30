using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCKCommand : BaseCommand
{
    PlayerController playerController;
    public BCKCommand(PlayerController player) 
    {
        playerController = player;
    }
    public override void Execute()
    {
        Debug.Log("backCommand");
        playerController.MoveDir(-Vector3.forward);
        //throw new System.NotImplementedException();
    }

    public override void Undo()
    {
        playerController.MoveDir(Vector3.forward);
    }
}
