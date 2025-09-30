using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadCommand : BaseCommand
{

    PlayerController playerController;
    public ReloadCommand(PlayerController player)
    {
        playerController = player;
    }
    public override void Execute()
    {

        //Debug.Log("reload command");
        //throw new System.NotImplementedException();
    }

    public override void Undo()
    {
        throw new System.NotImplementedException();
    }
}
