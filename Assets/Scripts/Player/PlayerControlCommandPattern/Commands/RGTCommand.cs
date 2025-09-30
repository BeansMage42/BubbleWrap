using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGTCommand : BaseCommand
{
    PlayerController playerController;
    public RGTCommand(PlayerController player)
    {
        playerController = player;
    }
    public override void Execute()
    {
        Debug.Log("right command");
        playerController.MoveDir(Vector3.right);
        //throw new System.NotImplementedException();
    }

    public override void Undo()
    {
        playerController.MoveDir(-Vector3.right);
    }
}

