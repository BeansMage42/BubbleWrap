using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHealthCommand : BaseCommand
{
    private PlayerHealth health;
    private int healthChange;
    public ChangeHealthCommand(PlayerHealth healthControl, int changeAmount)
    {
        health = healthControl;
        healthChange = changeAmount;
    }
    public override void Execute()
    {
        health.TakeDamage(-healthChange);
    }

    public override void Undo()
    {
        throw new System.NotImplementedException();
    }

    
}
