using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "End Game", menuName = "Commands/Text Commands/End Game", order = 2)]
public class GameEndCommand: TextCommand
{
    private bool playerSurvived;
    public override void Execute()
    {
        if (playerSurvived)
        {
            GameManager.instance.PlayerSurvived();
        }
        else
        {
            GameManager.instance.PlayerDied();
        }
    }

    public override void SetVar(string[] args)
    {
        if (args[0].Equals("true", StringComparison.OrdinalIgnoreCase))
        {
            playerSurvived = true;
        }
        else playerSurvived = false;
    }

    public override void Undo()
    {
    }

    
}
