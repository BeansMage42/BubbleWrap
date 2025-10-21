using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class StartCombatCommand : BaseCommand
{
    
    
    public override void Execute()
    {
        GameManager.instance.ActivateSleeperAgent();
    }

    public override void Undo()
    {
        throw new System.NotImplementedException();
    }

    
}
