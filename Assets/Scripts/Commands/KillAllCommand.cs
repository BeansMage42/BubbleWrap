using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Kill all", menuName = "Commands/Text Commands/Kill all", order = 5)]

public class KillAllCommand : TextCommand
{
    public override void Execute()
    {
        GameManager.instance.KillAllCreatures();
    }

    public override void SetVar(string[] args)
    {
       
    }

    public override void Undo()
    {
        throw new System.NotImplementedException();
    }

    
}
