using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

[CreateAssetMenu(fileName = "Start Combat", menuName = "Commands/Text Commands/Start Combat", order = 4)]
public class StartCombatCommand : TextCommand
{
    
    public override void SetVar(string[] args)
    {
       // throw new System.NotImplementedException();
    }
    
    public override void Execute()
    {
        GameManager.instance.ActivateSleeperAgent();
    }


    public override void Undo()
    {
        throw new System.NotImplementedException();
    }

    
}
