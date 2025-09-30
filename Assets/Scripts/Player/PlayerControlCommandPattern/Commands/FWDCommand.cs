using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FWDCommand :BaseCommand
{
    PlayerController playerController;
    
    public FWDCommand(PlayerController player)
    {
        playerController = player;
    }
    public override void Execute()
    {
        Debug.Log("forward command");
        playerController.MoveDir(Vector3.forward);
    }   

    public override void Undo()
    {

        Debug.Log("forward released");
        playerController.MoveDir(-Vector3.forward);
    }
}
