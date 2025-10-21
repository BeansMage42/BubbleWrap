using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndCommand:BaseCommand
{
    
    public override void Execute()
    {
        GameManager.instance.PlayerSurvived();
    }

    public override void Undo()
    {
       GameManager.instance.PlayerDied();
    }

    
}
