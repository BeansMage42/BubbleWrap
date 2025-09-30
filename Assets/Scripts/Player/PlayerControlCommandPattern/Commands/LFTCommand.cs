using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LFTCommand : BaseCommand
{
    PlayerController playerController;
    public LFTCommand(PlayerController player)
    {
        playerController = player;
    }
    public override void Execute()
    {
        Debug.Log("left command");
        playerController.MoveDir(Vector3.left);
        //throw new System.NotImplementedException();
    }

    public override void Undo()
    {
        playerController.MoveDir(Vector3.right);
    }
}
