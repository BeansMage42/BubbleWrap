using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Change Health", menuName = "Commands/Text Commands/Change Health", order = 3)]
public class ChangeHealthCommand : TextCommand
{
    private PlayerHealth health;
    private int healthChange;
    public override void SetVar(string[] args)
    {
        if(health == null) health = FindObjectOfType<PlayerHealth>();
        healthChange = int.Parse(args[0]);
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
